using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot
{
    internal class Battle
    {
        //Returnerer String af testårsager
        public static String CalculateFight(Player player1, Player player2)
        {   //Vægt af stats(kan og burde ændres)
            double vigorhWeight = 0.4;
            double strengthWeight = 0.3;
            double defenceWeight = 0.2;
            
            //Udregning af samlede stats for spillere
            double player1Total = (player1.Vigor * vigorhWeight) + (player1.Defence * defenceWeight) + (player1.Strength * strengthWeight);
            double player2Total = (player2.Vigor * vigorhWeight) + (player2.Defence * defenceWeight) + (player2.Strength * strengthWeight);

            //Udregner vinder chancer
            double totalStats = player1Total + player2Total;
            double player1WinChance = (player1Total / totalStats) * 100; // I procent (ikke magic number c0_0c)
            double player2WinChance = (player2Total / totalStats) * 100;
            
            //Skaber tilfældighed
            Random random = new Random();
            double player1RandomFactor = random.NextDouble() * 10 - 5;//Tilføjer en tilfældighed på -5 eller 5%
            double player2RandomFactor = random.NextDouble() * 10 - 5;//Tilføjer en tilfældighed på -5 eller 5%

            //Tilføjer tilfældigheden til deres vinderchance
            player1WinChance += player1RandomFactor;
            player2WinChance += player2RandomFactor;

            //Bestemmer vinderen(Skal nok ændres)
            if (player1WinChance > player2WinChance)
            {
                return player1.Navn + " vinder!";
            }
            else if (player1WinChance < player2WinChance)
            {
                return player2.Navn + " vinder!";
            }
            else 
            {
                return " Uafgjort!";
            }
            
        }

        
    }
}
