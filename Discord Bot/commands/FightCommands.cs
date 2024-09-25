using Discord_Bot.Controller;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.commands
{
    public class FightCommands : BaseCommandModule
    {
        //BRUG TIL MANIPULERING AF SPILLERE
        private readonly Controller.PlayerController playerController = new Controller.PlayerController();

        private Dictionary<string, ulong> playerUserMapping = new Dictionary<string, ulong>();

        [Command("fight")]
        public async Task Fight(CommandContext ctx, string targetName)
        {
            // Hent spillere
            var players = await playerController.LoadPlayersData();
            // Hent initiator
            var initiator = players.FirstOrDefault(p => p.Navn == ctx.User.Username);
            // Find Modstander
            var target = players.FirstOrDefault(p => p.Navn == targetName);

            // Check om target finder
            if (target == null)
            {
                await ctx.Channel.SendMessageAsync($"Ingen spiller fundet med dette input {targetName}.");
                return;
            }

            // TARGET MEMBER - FINDER MODSTANDER
            var targetMember = ctx.Guild.Members.Values.FirstOrDefault(m => m.Username.Equals(targetName, StringComparison.OrdinalIgnoreCase));

            // HVIS TARGET IKKE FINDES
            if (targetMember == null)
            {
                await ctx.Channel.SendMessageAsync($"Ingen Discord bruger fundet med navnet {targetName}.");
                return;
            }

            // Send ønske om at fight til spiller
            var requestMessage = new DiscordEmbedBuilder()
                .WithTitle($"@{target.Navn}, {ctx.User.Username} ønsker at kæmpe mod dig! Svar med **ja** for at acceptere, eller **nej** for at afslå.")
                .WithColor(DiscordColor.Red);
            await ctx.Channel.SendMessageAsync(requestMessage);

            // VIS STATS FOR SPILLERE
            var profileMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle($"Fight Profile: {initiator.Navn} vs {target.Navn}")
                .WithDescription(
                    $"**{initiator.Navn}'s STATS:**\n" +
                    $"**Vigor:** {initiator.Vigor}\n" +
                    $"**Strength:** {initiator.Strength}\n" +
                    $"**Defence:** {initiator.Defence}\n\n" +
                    $"**{target.Navn}'s STATS:**\n" +
                    $"**Vigor:** {target.Vigor}\n" +
                    $"**Strength:** {target.Strength}\n" +
                    $"**Defence:** {target.Defence}\n")
                .WithColor(DiscordColor.Blurple));

            await ctx.Channel.SendMessageAsync(profileMessage);

            // Vent for respons fra target player
            await ctx.Channel.SendMessageAsync($"{targetMember.Mention}, vil du kæmpe mod {ctx.User.Mention}? (ja/nej)");

            // Get the target user ID from the target member
            ulong targetUserId = targetMember.Id;

            var interactivity = ctx.Client.GetInteractivity();
            var response = await interactivity.WaitForMessageAsync(
                x => x.Author.Id == targetUserId && (x.Content.ToLower() == "ja" || x.Content.ToLower() == "nej"),
                TimeSpan.FromSeconds(30)
            );

            // HVIS RESPONS TIMER UD
            if (response.TimedOut)
            {
                await ctx.Channel.SendMessageAsync("Tiden er udløbet, anmodningen er annulleret.");
                return;
            }

            // HVIS SVARET ER JA
            if (response.Result.Content.ToLower() == "ja")
            {
                //await ctx.Channel.SendMessageAsync($"Fight mellem {ctx.User.Username} og {target.Navn} begynder nu!");
                var fightResult = Battle.CalculateFight(initiator, target);


                string result = Battle.battleStory(fightResult);


                string winnerText = fightResult.winner != null
                    ? $"**{fightResult.winner.Navn}**"
                    : "The match ended in a draw.";


                var embed = new DiscordEmbedBuilder()
                    .WithTitle($"{initiator.Navn} VS {target.Navn}")
                    .WithDescription(result)
                    .WithColor(fightResult.winner != null ? DiscordColor.Green : DiscordColor.Red)
                    .AddField("Winner", winnerText, inline: false);


                var message = new DiscordMessageBuilder()
                    .AddEmbed(embed);

                await ctx.Channel.SendMessageAsync(message);


                //MANIPULER STATS FOR VINDEREN
                //Giv win til vinderen
                await playerController.UpdatePlayerStats(fightResult.winner, 1,0,0,0,0,0);
                await playerController.UpdatePlayerStats(fightResult.loser, 0, 1, 0, 0, 0, 0);

            }
            // HVIS SVAR NEJ
            else
            {
                await ctx.Channel.SendMessageAsync($"{target.Navn} har afslået kampen.");
            }
        }

    }
}
