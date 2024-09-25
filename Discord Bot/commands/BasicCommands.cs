using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Discord_Bot.commands
{
    public class BasicCommands : BaseCommandModule
    {
        //BRUG DENNE TIL MANIPULERING
        private readonly Controller.PlayerController playerController = new Controller.PlayerController();

        //DISCORD BATTLEBOT INFORMATION TEST
        [Command("info")]
        public async Task Info(CommandContext ctx)
        {
            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle("BATTLEBOT INFO")
                .WithDescription("WELCOME TO BATTLEBOT\r\n\r\nEACH MONDAY THE BATTLEBOT WILL ANNOUNCE THE FIGHT LINEUP FOR SUNDAY.\r\n\r\nEVERY PLAYER WILL BE GIVEN A RANDOM OPPONENT ON THE SERVER.\r\n\r\nTHE MATCH IS CALCULATED BASED ON CHANCE\r\n\r\nTHE FIGHTER WITH THE HIGHEST STATS HAS THE HIGHEST CHANCE TO WIN.\r\n\r\nDOING THE WEEK UP TO THE FIGHT\r\n\r\nTHE PLAYERS MUST TRAIN THEIR CHARACTER TO INCREASE THEIR STATS AND CHANCES\r\n\r\nTHE WINNERS GET +ELO AND THE LOSERS WILL GET -ELO\r\n\r\nMAY THE BEST FIGHTER WIN GLORY AND FAME ON THE LEADERBOARDS\r\n\r\nMADE BY NIKOJH9 AND JEPPSITO")
                .WithColor(DiscordColor.Red)
                .WithThumbnail("https://cdn3.iconfinder.com/data/icons/chat-bot-glyph-silhouettes-1/300/14112417Untitled-3-512.png"));


            await ctx.Channel.SendMessageAsync(message);
        }


        //OPRET TEST
        [Command("create")] //add flare til efter konto oprettet
        public async Task opretProfil(CommandContext ctx)
        {
            string currentUser = ctx.User.Username;
            string currentUserImage = ctx.User.AvatarUrl;

            await playerController.AddNewPlayer(currentUser, currentUserImage);

            await ctx.Channel.SendMessageAsync("Done");
        }

        //VIS PROFIL HVIS DER ER EN. ELLERS HENVIS TIL AT LAVE EN
        [Command("p")]
        public async Task Profil(CommandContext ctx)
        {

            string currentUser = ctx.User.Username;

            var players = await playerController.LoadPlayersData();

            //CHECK AT SPILLER EKSISTERE

            var currentPlayer = players.FirstOrDefault(p => p.Navn == currentUser);

            //HVIS SPILLER IKKE EKSISTERE SKAL DER KOMME PROMPT OP MED INFO TIL AT OPRETTE SPILLER
            if (currentPlayer == null)
            {
                var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle($"Ingen bruger oprettet med navn: {currentUser}")
                .WithDescription($"Brug kommandoen **!create** for at oprette en bruger med din Discord profil.")
                .WithColor(DiscordColor.Red)
                );

                await ctx.Channel.SendMessageAsync(message);

            }
            else //{currentPlayer.Wins}
            {
                var profilemessage = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithTitle($"{currentPlayer.Navn}'s Profil\n{currentPlayer.ProfilBillede}")
                    .WithDescription($"\n Name: {currentPlayer.Navn}\nW/L/T RATIO: {currentPlayer.Wins}/{currentPlayer.Loses}/{currentPlayer.Ties}\nELO RATING: {currentPlayer.Elo}\n\nSTATS:\nVigor: {currentPlayer.Vigor}\nSTRENGTH: {currentPlayer.Strength}\nDefence: {currentPlayer.Defence}")
                    .WithColor(DiscordColor.Blurple)
                    );

                await ctx.Channel.SendMessageAsync(profilemessage);
            }
        }




       
    }




}
