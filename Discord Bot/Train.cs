using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord_Bot;


namespace Discord_Bot
{
    internal class Train
    {
        private readonly Controller.PlayerController playerController = new Controller.PlayerController();

        public async Task TrainVigor(Player player)
        {
            await playerController.UpdatePlayerStats(player, 0, 0, 0, 3, 0, 0);
        }

        public async Task TrainStrength(Player player)
        {
            await playerController.UpdatePlayerStats(player, 0, 0, 0, 0, 3, 0);
        }


        public async Task TrainDefence(Player player) 
        {
            await playerController.UpdatePlayerStats(player, 0, 0, 0, 0, 0, 3);
        }

     
    }
}
