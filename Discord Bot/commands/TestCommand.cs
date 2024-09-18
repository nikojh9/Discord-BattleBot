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
        //KODEN GØR AT DEN VENTER PÅ EN SVARET HELLO. SÅ MAN KAN FÅ DEN TIL AT VENTE PÅ FORSKELLIGE INPUT FØR DEN REAGERER
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
    }

}
