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

            List<string> battleStartSentences = new List<string>
{
    $"The battlefield roars with anticipation as {winner.Navn} and {loser.Navn} face off in a clash of titans. The ground shakes as they prepare to unleash their fury upon each other.",

    $"Under the fiery sunset, {winner.Navn} and {loser.Navn} stand poised for battle. Their eyes lock, and the air crackles with the energy of their impending confrontation.",

    $"The arena trembles with excitement as {winner.Navn} and {loser.Navn} square off. A deafening cheer rises from the crowd as the two warriors prepare to engage in a legendary struggle.",

    $"With a thunderous roar, {winner.Navn} and {loser.Navn} step into the arena. The tension is palpable as they ready themselves for a clash that will echo through the ages.",

    $"The arena falls silent, save for the pounding of hearts as {winner.Navn} and {loser.Navn} face each other. The air is charged with the promise of an epic battle to come.",

    $"As the first light of dawn breaks over the arena, {winner.Navn} and {loser.Navn} prepare for a battle that will be remembered in legend. The crowd holds its breath in anticipation.",

    $"The very ground quakes with the might of {winner.Navn} and {loser.Navn} as they stand ready to clash. Their fierce determination is palpable, setting the stage for a monumental duel.",

    $"In a dramatic display of strength, {winner.Navn} and {loser.Navn} prepare to engage in combat. The arena buzzes with excitement, knowing they are about to witness a fight for the ages.",

    $"The clash of metal and the roar of the crowd signal the start of an epic confrontation. {winner.Navn} and {loser.Navn} lock eyes, each ready to claim victory in this monumental battle.",

    $"With a ferocious cry, {winner.Navn} and {loser.Navn} prepare to face each other. The arena is alive with the electric energy of their impending clash, promising an unforgettable duel."
};





            var battleSequences = new List<Tuple<string, string, string>>
{
    Tuple.Create(
        $"{winner.Navn} swings their sword with precision, and {loser.Navn} struggles to evade, barely managing to counter with a quick jab to {winner.Navn}'s side.",
        $"{winner.Navn} staggers slightly but quickly recovers, blocking {loser.Navn}'s follow-up strike with a powerful parry.",
        $"{winner.Navn} takes advantage of the opening, delivering a series of rapid strikes that forces {loser.Navn} into a defensive stance."
    ),
    Tuple.Create(
        $"{winner.Navn} charges forward with a powerful swing, and {loser.Navn} evades but takes a sharp kick to {winner.Navn}'s midsection.",
        $"{winner.Navn} winces from the kick but swiftly raises their shield to block {loser.Navn}'s next attack.",
        $"{winner.Navn} takes advantage of the momentary opening, landing a decisive blow that forces {loser.Navn} to retreat."
    ),
    Tuple.Create(
        $"{winner.Navn} lunges with their spear, and {loser.Navn} ducks under but gets caught by a sharp slash from {winner.Navn}.",
        $"{loser.Navn} reels from the slash but quickly tries to counter, only to find {winner.Navn} deflecting with ease.",
        $"{winner.Navn} capitalizes on the opening, landing a decisive blow that leaves {loser.Navn} vulnerable."
    ),
    Tuple.Create(
        $"{winner.Navn} raises their axe high and swings down, and {loser.Navn} dodges but takes a precise stab from {winner.Navn}'s dagger.",
        $"{loser.Navn} grunts from the stab but quickly tries to block {winner.Navn}'s next strike.",
        $"{winner.Navn} presses the advantage, landing a powerful blow that forces {loser.Navn} into a defensive stance."
    ),
    Tuple.Create(
        $"{winner.Navn} charges with their sword drawn, and {loser.Navn} sidesteps but is met with a solid punch to {winner.Navn}'s ribs.",
        $"{winner.Navn} recoils but quickly raises their sword to block {loser.Navn}'s follow-up attack.",
        $"{winner.Navn} takes advantage of the momentary gap, landing a quick series of attacks that push {loser.Navn} back."
    ),
    Tuple.Create(
        $"{winner.Navn} swings their mace with full force, and {loser.Navn} evades but takes a low kick to {winner.Navn}'s legs.",
        $"{winner.Navn} stumbles but quickly recovers and deflects {loser.Navn}'s follow-up attack with a strong parry.",
        $"{winner.Navn} presses the attack, landing a decisive blow that forces {loser.Navn} to retreat."
    ),
    Tuple.Create(
        $"{winner.Navn} launches a series of rapid strikes, and {loser.Navn} blocks them but gets hit by a quick jab to {winner.Navn}'s side.",
        $"{winner.Navn} staggers but quickly recovers, blocking {loser.Navn}'s follow-up strike with a powerful parry.",
        $"{winner.Navn} takes advantage of the opening, delivering a decisive blow that forces {loser.Navn} into a defensive position."
    ),
    Tuple.Create(
        $"{winner.Navn} thrusts their spear forward, and {loser.Navn} sidesteps but takes a solid punch to {winner.Navn}'s chest.",
        $"{loser.Navn} staggers but quickly tries to counter, only to find {winner.Navn} blocking effectively.",
        $"{winner.Navn} takes advantage of the opening, landing a powerful blow that forces {loser.Navn} into a defensive stance."
    ),
    Tuple.Create(
        $"{winner.Navn} swings their sword in a wide arc, and {loser.Navn} dodges but is met with a sharp cut from {winner.Navn}.",
        $"{loser.Navn} grunts from the cut but tries to block {winner.Navn}'s next attack.",
        $"{winner.Navn} presses the advantage, landing a powerful blow that forces {loser.Navn} to retreat."
    ),
    Tuple.Create(
        $"{winner.Navn} charges with their sword, and {loser.Navn} sidesteps but takes a solid kick to {winner.Navn}'s midsection.",
        $"{winner.Navn} stumbles but quickly recovers, blocking {loser.Navn}'s follow-up attack with a strong parry.",
        $"{winner.Navn} takes advantage of the momentary gap, landing a decisive blow that forces {loser.Navn} into a defensive stance."
    )
};






            List<string> deathSummaries = new List<string>
{
    $"{loser.Navn} crumples to the ground after {winner.Navn}'s decisive stab finds its mark, their strength fading as they fall.",

    $"{winner.Navn} lands a critical blow to {loser.Navn}'s side, causing {loser.Navn} to stagger and collapse, defeated and bloodied.",

    $"{loser.Navn} staggers from {winner.Navn}'s powerful punch, and with a final, crushing blow, they fall to their knees, defeated.",

    $"{winner.Navn}'s precise jab to {loser.Navn}'s ribs leaves {loser.Navn} gasping for breath, their fight ending with a final collapse.",

    $"{loser.Navn} falls to the arena floor as {winner.Navn}'s final strike lands with lethal force, their last breath escaping into the silence.",

    $"{winner.Navn} delivers a lethal cut to {loser.Navn}'s arm, and {loser.Navn} falls, their energy draining as they succumb to their injuries.",

    $"{loser.Navn} collapses after {winner.Navn}'s swift strike lands true, their defeat marked by the finality of the blow.",

    $"{winner.Navn} thrusts their sword into {loser.Navn}'s side, and {loser.Navn} slumps to the ground, their fight coming to a tragic end.",

    $"{loser.Navn} gasps for air as {winner.Navn}'s final attack pierces through their defenses, their strength waning as they fall to the ground.",

    $"{winner.Navn} lands a final, devastating blow to {loser.Navn}'s chest, and {loser.Navn} collapses, their fight coming to a sorrowful end."
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

            //Vælger 1 tilfældig battle pre fight
            string prefight = battleStartSentences[random.Next(battleStartSentences.Count)];

            //Vælger 1 battleSequence
            var selectedSequence = battleSequences[random.Next(battleSequences.Count)];
            string battleRounds = $"Round 1: {selectedSequence.Item1}\n\nRound 2: {selectedSequence.Item2}\n\nRound 3: {selectedSequence.Item3}";

            //Vælger 1 tilfælduig death scenarie
            string deathSummary = deathSummaries[random.Next(deathSummaries.Count)];

            //Vælger 1 tilfældig afterfight scenarie
            string postFight = postFightSentences[random.Next(postFightSentences.Count)];


            //kombiner og returnerer dem alle

            string fightStory = $"{prefight}\n" +
                        "\n" +
                        $"{battleRounds}\n" +
                        "\n" +
                        $"{deathSummary}\n" +
                        "\n" +
                        $"{postFight}\n";

            return fightStory;
        }
    
    }
        
}
