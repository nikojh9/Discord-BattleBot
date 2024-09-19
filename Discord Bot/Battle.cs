using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot
{
    public class Battle
    {
        //Returnerer String af testårsager
        public static String CalculateFight(Player player1, Player player2)
        {   //Vægt af stats(kan og burde ændres)
            int vigorhWeight = 1;
            int strengthWeight = 1;
            int defenceWeight = 1;
            
            //Udregning af samlede stats for spillere
            int player1Total = (player1.Vigor * vigorhWeight) + (player1.Defence * defenceWeight) + (player1.Strength * strengthWeight);
            int player2Total = (player2.Vigor * vigorhWeight) + (player2.Defence * defenceWeight) + (player2.Strength * strengthWeight);

            //Udregner vinder chancer
            int totalStats = player1Total + player2Total;
            int player1WinChance = (player1Total / totalStats) * 100; // I procent (ikke magic number c0_0c)
            int player2WinChance = (player2Total / totalStats) * 100;
            
            //Skaber tilfældighed
            Random random = new Random();
            int player1RandomFactor = random.Next() * 10 - 5;//Tilføjer en tilfældighed på -5 eller 5%
            int player2RandomFactor = random.Next() * 10 - 5;//Tilføjer en tilfældighed på -5 eller 5%

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
