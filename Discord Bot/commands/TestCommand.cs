using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Discord_Bot.commands
{
    public class TestCommand : BaseCommandModule
    {
        // SKABELON TIL COMMANDS
        [Command("test")]
        public async Task tester(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("TEST DIN LUDER");
        }

        //INTERACTIVITY - VENT PÅ INPUT FRA BRUGEREN TEST
        //KODEN GØR AT DEN VENTER PÅ EN DER SVARE HELLO EFTER !ACTIVITYTEST KOMMANDO ER KALDT. SÅ MAN KAN FÅ DEN TIL AT VENTE PÅ FORSKELLIGE INPUT FØR DEN REAGERER
        [Command("activityTest")]
        public async Task testtttt(CommandContext ctx)
        {
            var activity = Program.Client.GetInteractivity();
            await ctx.Channel.SendMessageAsync("Sig hello for at jeg gentager det");
            var messageToRetrieve = await activity.WaitForMessageAsync(message => message.Content == "hello");

            if (messageToRetrieve.Result.Content == "hello")
            {
                await ctx.Channel.SendMessageAsync($"{ctx.User.Username} said hello");
            }
        }


        //test fight command

        [Command("testFight")]
        public async Task fightInfo(CommandContext ctx)
        {
            // Test players
            Player testPlayer10 = new Player("Juhlino", "https://gravatar.com/avatar/56d913cb58ca6142bb393e174db297f2?s=400&d=robohash&r=x");
            Player testPlayer20 = new Player("Zizto", "https://gravatar.com/avatar/56d913cb58ca6142bb393e174db297f2?s=400&d=robohash&r=x");

           
            var fightResult = Battle.CalculateFight(testPlayer10, testPlayer20);

           
            string result = Battle.battleStory(fightResult);

            
            string winnerText = fightResult.winner != null
                ? $"**{fightResult.winner.Navn}**"
                : "The match ended in a draw.";

            
            var embed = new DiscordEmbedBuilder()
                .WithTitle($"{testPlayer10.Navn} VS {testPlayer20.Navn}")
                .WithDescription(result)
                .WithColor(fightResult.winner != null ? DiscordColor.Green : DiscordColor.Red)
                .AddField("Winner", winnerText, inline: false)
                .WithThumbnail("https://cdn3.iconfinder.com/data/icons/chat-bot-glyph-silhouettes-1/300/14112417Untitled-3-512.png");

            
            var message = new DiscordMessageBuilder()
                .AddEmbed(embed);

            await ctx.Channel.SendMessageAsync(message);
        }

    }

}
