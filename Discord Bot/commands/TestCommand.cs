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
        [Command("test")]
        public async Task tester(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("TEST DIN LUDER");
        }
    }
}
