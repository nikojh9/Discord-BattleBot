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
        [Command("testOpret")]
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




        [Command("train")]
        public async Task luder(CommandContext ctx)
        {
            //Player player1 = new Player("user", "billede");
            var currentUserName = ctx.User.Username;

            var players = await playerController.LoadPlayersData();

            Player player1 = players.FirstOrDefault(p => p.Navn == currentUserName);

            if (player1 == null)
            {
                await ctx.Channel.SendMessageAsync("Ingen bruger fundet. **!create** for at oprette en bruger.");
            }

            var interactitvity = Program.Client.GetInteractivity();

            Train training = new Train();

            //Venter på brugeren skriver 1, 2, eller 3;
            await ctx.Channel.SendMessageAsync("Tryk 1 for at træne Vigor ❤️ \n Tryk 2 for at træne Strength 💪 \n Tryk 3 for at træne Defence 🛡️");
            var messageResponse = await interactitvity.WaitForMessageAsync(
           x => x.Author.Id == ctx.User.Id && (x.Content == "1" || x.Content == "2" || x.Content == "3"),
           TimeSpan.FromSeconds(30)//30 sekunder time-out tid til at svare
           );

            //Hvis brugeren ikke svarede inden for timeout
            if (messageResponse.TimedOut)
            {
                await ctx.Channel.SendMessageAsync("Du lagde dig tilbage i sengen, ingen træning fuldført");
                return;
            }


            //valg af træningstype
            string trainingType;
            switch (messageResponse.Result.Content)
            {
                case "1":
                    trainingType = "Vigor";
                    break;
                case "2":
                    trainingType = "Strength";
                    break;
                case "3":
                    trainingType = "Defence";
                    break;
                default:
                    trainingType = string.Empty;
                    break;
            }


            //Her sættes varigheden af cooldown
            TimeSpan cooldownDuration = TimeSpan.FromSeconds(30);//!!!SKAL ÆNDRES!!!
            var userId = ctx.User.Id;

            if (CooldownManager.IsOnCooldown(userId, trainingType))
            {
                //Hvis brugeren har cooldown, beregnes resten af tiden
                TimeSpan remainingCooldown = CooldownManager.GetRemainingCooldown(userId, trainingType);
                await ctx.Channel.SendMessageAsync($"Du er for træt efter en hård {trainingType} træning! Du er klar igen om {remainingCooldown.Seconds} sekunder!"); //Skal tilføjes {remainingCoolDown.Minutes hvis tid ændres til over 60 sec}
                return;
            }

            //udførelse af træningstyper
            if (trainingType == "Vigor")
            {
                await ctx.Channel.SendMessageAsync("Løber fra drager... "); //Sender besked
                await Task.Delay(2000);//Venter 2 sekunder

                await ctx.Channel.SendMessageAsync("Hopper over Kløfter...");
                await Task.Delay(2000);

                await ctx.Channel.SendMessageAsync("Sprinter op ad bjerge...");
                await Task.Delay(2000);

                await training.TrainVigor(player1);
            }
            else if (trainingType == "Strength")
            {
                await ctx.Channel.SendMessageAsync("Lægger arm med orker...");
                await Task.Delay(2000);

                await ctx.Channel.SendMessageAsync("Slår på sten....");
                await Task.Delay(2000);

                await ctx.Channel.SendMessageAsync("Løfter træer...");
                await Task.Delay(2000);

                await training.TrainStrength(player1);
            }
            else if (trainingType == "Defence")
            {
                await ctx.Channel.SendMessageAsync("Undviger ildkugler...");
                await Task.Delay(2000);

                await ctx.Channel.SendMessageAsync("Parerer Kødsværd....");
                await Task.Delay(2000);

                await ctx.Channel.SendMessageAsync("Undslipper Kevins headlocks...");
                await Task.Delay(2000);


                await training.TrainDefence(player1);
            }

            //efter succesfuld træning, sættes cooldown for brugeren
            CooldownManager.SetCooldown(userId, trainingType, cooldownDuration);

            //Sender besked om, at træningen er afsluttet, og hvornår de kan træne igen
            await ctx.Channel.SendMessageAsync($"Tillykke! Du har gennemført en voldsom {trainingType} træning! Du kan træne igen om {cooldownDuration}");

        }
    }




}
