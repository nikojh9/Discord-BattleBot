using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot
{
    internal class Train
    {

        public static void TrainVigor(Player player)
        {
            player.Vigor += 3;
        }

        public static void TrainStrength(Player player)
        {
            player.Strength += 3;
        }


        public static void TrainDefenc(Player player) 
        {
            player.Defence += 3;
        }

     
    }
}
