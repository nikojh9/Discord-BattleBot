using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.commands
{
    public class TestCommand : BaseCommandModule

       
        
    {
        static TestCommand()
        {
            Shop.initiateShopItems();
        }

        
    

        [Command("test")]
        public async Task tester(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("TEST DIN LUDER");
        }

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
    }
}
