using Discord_Bot.Controller;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Discord_Bot.commands
{

    public class TrainCommands : BaseCommandModule
    {

        //BRUG TIL MANIPULERING AF SPILLERE
        private readonly Controller.PlayerController playerController = new Controller.PlayerController();

        [Command("train")]
        public async Task Train(CommandContext ctx)
        {

            //FIND SPILLERNAVN
            var currentUserName = ctx.User.Username;
            //HENT ALLE SPILLERE
            var players = await playerController.LoadPlayersData();

            // Find spiller i JSON database
            Player player1 = players.FirstOrDefault(p => p.Navn == currentUserName);


            //CHECK OM BRUGER FINDES
            if (player1 == null)
            {
                await ctx.Channel.SendMessageAsync("Ingen bruger fundet. Brug **!create** for at oprette en bruger.");
                return;
            }

            //GEM GAMLE STATS TIL AT BLIVE BRUGT TIL SIDST
            var oldVigor = player1.Vigor;
            var oldStrength = player1.Strength;
            var oldDefence = player1.Defence;

            // Lav buttons til at trykke på
            var vigorButton = new DiscordButtonComponent(ButtonStyle.Primary, "vigor_button", "Vigor ❤️");
            var strengthButton = new DiscordButtonComponent(ButtonStyle.Primary, "strength_button", "Strength 💪");
            var defenceButton = new DiscordButtonComponent(ButtonStyle.Primary, "defence_button", "Defence 🛡️");


            //EMBED MESSAGE BUILDER
            var embed = new DiscordEmbedBuilder()
            .WithTitle($"TRÆNING FOR {player1.Navn}")
            .WithDescription($"\n**Nuværende stats:**\n**Vigor**: {player1.Vigor}\n**Strength**: {player1.Strength}\n**Defence**: {player1.Defence}\n\nVælg din træning!")
            .WithColor(DiscordColor.Blurple);

            // Send a message with the buttons
            var trainingMessage = new DiscordMessageBuilder()
                .AddComponents(vigorButton, strengthButton, defenceButton)
                .WithEmbed(embed);
                
                
            //send besked med trænings valgmuligheder
            var sentMessage = await ctx.Channel.SendMessageAsync(trainingMessage);

            // Vent på at brugeren trykker på en knap. Står til 30 sek.
            var buttonResponse = await sentMessage.WaitForButtonAsync(ctx.User, TimeSpan.FromSeconds(30));

            // Hvis brugeren ikke trykker på en knap
            if (buttonResponse.TimedOut)
            {
                await ctx.Channel.SendMessageAsync("Du tog en pause, ingen træning fuldført.");
                return;
            }

            // Se hvilken træning brugeren har valgt udfra hvilke buttons er trykket.
            string trainingType;
            switch (buttonResponse.Result.Id)
            {
                case "vigor_button":
                    trainingType = "Vigor";
                    break;
                case "strength_button":
                    trainingType = "Strength";
                    break;
                case "defence_button":
                    trainingType = "Defence";
                    break;
                default:
                    trainingType = string.Empty;
                    break;
            }

            // Handle at knap er blevet tryukket på
            await buttonResponse.Result.Interaction.CreateResponseAsync(DSharpPlus.InteractionResponseType.UpdateMessage);

            // Fejl håndtering - usandsynligt vi rammer den :DDD
            if (string.IsNullOrEmpty(trainingType))
            {
                await ctx.Channel.SendMessageAsync("Ukendt træningstype valgt.");
                return;
            }

            // Set cooldown
            TimeSpan cooldownDuration = TimeSpan.FromSeconds(30);
            var userId = ctx.User.Id;

            // Checker om bruger er på cooldown med træningsform
            if (CooldownManager.IsOnCooldown(userId, trainingType))
            {
                TimeSpan remainingCooldown = CooldownManager.GetRemainingCooldown(userId, trainingType);
                await ctx.Channel.SendMessageAsync($"Du er for træt efter {trainingType} træning! Du kan træne igen om {remainingCooldown.Seconds} sekunder.");
                return;
            }


            // Udfører den korrekte træning
            Train training = new Train();
            if (trainingType == "Vigor")
            {
                await training.TrainVigor(player1);
                await ctx.Channel.SendMessageAsync("Løber fra drager... ");
                await Task.Delay(2000);
                await ctx.Channel.SendMessageAsync("Hopper over Kløfter...");
                await Task.Delay(2000);
                await ctx.Channel.SendMessageAsync("Sprinter op ad bjerge...");
                await Task.Delay(2000);
            }
            else if (trainingType == "Strength")
            {
                await training.TrainStrength(player1);
                await ctx.Channel.SendMessageAsync("Lægger arm med orker...");
                await Task.Delay(2000);
                await ctx.Channel.SendMessageAsync("Slår på sten...");
                await Task.Delay(2000);
                await ctx.Channel.SendMessageAsync("Løfter træer...");
                await Task.Delay(2000); 
            }
            else if (trainingType == "Defence")
            {
                await training.TrainDefence(player1);
                await ctx.Channel.SendMessageAsync("Undviger ildkugler...");
                await Task.Delay(2000);
                await ctx.Channel.SendMessageAsync("Parerer Kødsværd...");
                await Task.Delay(2000);
                await ctx.Channel.SendMessageAsync("Undslipper Kevins headlocks...");
                await Task.Delay(2000);
            }

            // Set cooldown efter træning
            CooldownManager.SetCooldown(userId, trainingType, cooldownDuration);


            //UDSKRIV NYE STATS
            string switchMessage;
            switch (trainingType)
            {
                case "Vigor":
                    switchMessage = ($"Din **Vigor** stat er forhøjet fra **{oldVigor}** -> **{player1.Vigor}**");
                    break;
                case "Strength":
                    switchMessage = ($"Din **Strength** stat er forhøjet fra **{oldStrength}** -> **{player1.Strength}**");
                    break;
                case "Defence":
                    switchMessage = ($"Din **Defence** stat er forhøjet fra **{oldDefence}** -> **{player1.Defence}**");
                    break;
                default:
                    trainingType = string.Empty;
                    switchMessage = ("Fejl");
                    break;
            }

            // TRÆNING DONE
            var embedDone = new DiscordEmbedBuilder()
            .WithTitle($"\n{player1.Navn}! Du har gennemført en voldsom {trainingType} træning! Du kan træne igen om {cooldownDuration.Seconds} sekunder.\n\n{switchMessage}")
            .WithColor(DiscordColor.Green)
            ;
            await ctx.Channel.SendMessageAsync(embedDone);

            
        }
    }
}

