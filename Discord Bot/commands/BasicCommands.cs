using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Discord_Bot.commands
{
    public class BasicCommands : BaseCommandModule
    {


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


    }
}
