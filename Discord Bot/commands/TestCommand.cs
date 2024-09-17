using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace Discord_Bot.commands
{
    public class TestCommand : BaseCommandModule



    {

        //SHOP TEST
        //INITIALIZE SHOP
        static TestCommand()
        {
            Shop.initiateShopItems();
        }

        //SHOP COMMAND
        [Command("shop")]
        public async Task shop(CommandContext ctx)
        {
            string response = "Available items:\n";
            foreach (var item in Shop.ShopAllItems)
            {
                response += item.ToString() + "\n\n";
            }

            await ctx.Channel.SendMessageAsync(response);

        }


        //
        [Command("test")]
        public async Task tester(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("TEST DIN LUDER");
        }


        //SNATCH USER INFORMATION 

        [Command("start")]
        public async Task Snatch(CommandContext ctx)
        {
            string name = ctx.Member.DisplayName;
            await ctx.Channel.SendMessageAsync("Knep dig selv " + name);
        }

        //SNATCH PROFILEPIC

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
