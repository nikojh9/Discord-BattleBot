using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace Discord_Bot.commands
{
    public class TestCommand : BaseCommandModule
    {
        //
        [Command("test")]
        public async Task tester(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("TEST DIN LUDER");
        }

        //PROFIL KONCEPT

        [Command("profil")]
        public async Task Snatch2(CommandContext ctx)
        {
            string img = ctx.Member.AvatarUrl;
            string name = ctx.User.Username;
            await ctx.Channel.SendMessageAsync(name);
            await ctx.Channel.SendMessageAsync("W/T/L: " + "10/2/0");
            await ctx.Channel.SendMessageAsync("---STATS---");
            await ctx.Channel.SendMessageAsync("Vigour: " + "22");
            await ctx.Channel.SendMessageAsync("Endurance: " + "22");
            await ctx.Channel.SendMessageAsync("Strength: " + "22");
            await ctx.Channel.SendMessageAsync(img);
        }
    }

}
