using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot
{
    public class Battle
    {

        //Calculates thé fight
        //Returnere winner player og loser player objekter
        public static (Player winner, Player loser) CalculateFight(Player player1, Player player2)
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
                return (player1, player2);
            }
            else if (player1WinChance < player2WinChance)
            {
                return (player2, player1);
            }
            else 
            {
                //DRAW
                return (null, null);
            }
            
        }



        //Tager vinder og taber fra calculate battle og generer en tilfældig battle historie
        public static string battleStory((Player winner, Player loser) fightResult)
        {

            var (winner, loser) = fightResult;

            if(winner == null || loser == null)
            {
                return "Spillerne kæmpede og kæmpede, men på slagmarken matchede de hinandens kamp evner. TIE";
            }

            List<string> battleSummaries = new List<string> {
        $"{winner.Navn} charges forward, clashing swords with {loser.Navn} as sparks fly from their intense duel.",
        $"{loser.Navn} sidesteps just in time, dodging {winner.Navn}’s attack and countering with a swift kick to the ribs.",
        $"{winner.Navn} rolls beneath {loser.Navn}’s strike, quickly regaining their footing and delivering a solid punch to the gut.",
        $"{loser.Navn} spins around, deflecting {winner.Navn}’s blade and pushing them back with a powerful shove.",
        $"{winner.Navn} swings high, but {loser.Navn} ducks and sweeps their leg, sending {winner.Navn} tumbling to the ground.",
        $"{loser.Navn} jumps back to avoid the swing, then lunges forward with a series of quick jabs that force {winner.Navn} to retreat.",
        $"{winner.Navn} blocks {loser.Navn}’s blow with their shield, then counters with a sharp elbow to the side.",
        $"{loser.Navn}’s sword narrowly misses as {winner.Navn} sidesteps and delivers a hard kick to the shin.",
        $"{winner.Navn} ducks under {loser.Navn}’s heavy strike, then slams their fist into {loser.Navn}’s side, knocking the wind out of them.",
        $"{loser.Navn} grabs {winner.Navn}’s arm mid-swing and twists, throwing them off balance but not before {winner.Navn} kicks free."
    };

            List<string> deathSummaries = new List<string>{
        $"{loser.Navn} stumbles after a fierce exchange, and {winner.Navn} seizes the moment to deliver a fatal blow to the chest.",
        $"{winner.Navn} swings their sword with precision, striking {loser.Navn}’s neck and watching them crumple to the ground.",
        $"{loser.Navn} gasps for breath as {winner.Navn}’s final strike pierces through their armor, ending the fight.",
        $"{winner.Navn} thrusts their blade deep into {loser.Navn}’s side, and {loser.Navn} falls, bleeding out on the arena floor.",
        $"{loser.Navn} falters after a missed strike, giving {winner.Navn} the opening to drive a dagger into their heart.",
        $"{winner.Navn} delivers a crushing blow to {loser.Navn}’s head, and {loser.Navn} collapses, unmoving.",
        $"{loser.Navn} falls to their knees, their vision fading as {winner.Navn} delivers the finishing strike to their chest.",
        $"{winner.Navn}’s sword pierces {loser.Navn}’s gut, and {loser.Navn} slumps to the ground, gasping for air.",
        $"{loser.Navn} collapses after a brutal slash to the throat, their lifeblood spilling out as {winner.Navn} stands victorious.",
        $"{winner.Navn} drives their blade through {loser.Navn}’s back, and {loser.Navn} drops to the ground, their final breath escaping."

            };

            List<string> postFightSentences = new List<string>
{
    $"Standing alone, amidst blood and gore... THE WINNER IS: {winner.Navn}",
    $"With the crowd roaring in approval, {winner.Navn} stands tall, victorious and unchallenged.",
    $"{winner.Navn} wipes the sweat from their brow, the battlefield quiet... THE WINNER IS: {winner.Navn}",
    $"Amidst the fallen and the silence, one fighter remains standing... THE WINNER IS: {winner.Navn}",
    $"The dust settles and only one fighter remains... THE WINNER IS: {winner.Navn}",
    $"{winner.Navn} surveys the battlefield, triumphant and victorious over all who opposed them.",
    $"With a final strike, the battle is over, and {winner.Navn} reigns supreme as the victor!",
    $"The arena falls silent, and all eyes turn to the one left standing... THE WINNER IS: {winner.Navn}",
    $"Covered in dirt and sweat, but victorious nonetheless... THE WINNER IS: {winner.Navn}",
    $"As the last echoes of battle fade, {winner.Navn} stands tall, the undeniable champion of the arena."
};


            Random random = new Random();

            //Vælger 3 tilfældige battle scenarier
            string battle1 = battleSummaries[random.Next(battleSummaries.Count)];
            string battle2 = battleSummaries[random.Next(battleSummaries.Count)];
            string battle3 = battleSummaries[random.Next(battleSummaries.Count)];

            //Vælger 1 tilfælduig death scenarie
            string deathSummary = deathSummaries[random.Next(deathSummaries.Count)];

            //Vælger 1 tilfældig afterfight scenarie
            string postFight = postFightSentences[random.Next(postFightSentences.Count)];


            //kombiner og returnerer dem alle

            string fightStory = $"Round 1: {battle1}\n" +
                        "" +
                        $"Round 2: {battle2}\n" +
                        "" +
                        $"Round 3: {battle3}\n" +
                        "" +
                        $"{deathSummary}\n" +
                        "" +
                        $"{postFight}";

            return fightStory;
        }
    
    }
        
}
