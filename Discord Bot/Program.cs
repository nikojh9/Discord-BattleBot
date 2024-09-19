using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot
{
    internal class Program
    {
        
        public static DiscordClient Client { get; set; }
        private static CommandsNextExtension Commands {  get; set; }
        
        static async Task Main(string[] args)
        {
            //Læser botten prefix & token
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            // BASIC BOT CONFIG - ÆNDRE KUN HVIS MEGET NØDVENDIGT
            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discordConfig);

            //INTERACTIVITY CONFIGURATION - ÆNDRE HVIS KNAPPER SKAL HAVE LÆNGERE LEVETID AT BLIVE TRYKKET PÅ
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });
            Client.Ready += Client_Ready;


            //COMMANDS CONFIG - ÆNDRE KUN HVIS MEGET NØDVENDIGT
            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { jsonReader.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<commands.TestCommand>();
            Commands.RegisterCommands<commands.BasicCommands>();


           
            



            //Connect BOT til DISC
            await Client.ConnectAsync();

            //Sørger for at botten kører indtil programmet lukker. Værdien = Hvor lang tid robotten lever
            await Task.Delay(-1);

        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
