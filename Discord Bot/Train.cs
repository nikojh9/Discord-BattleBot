using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Discord_Bot
{
    public class Train
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













                List<string> VigorTrainingSentences = new List<string>
                {
            "***Løber fra drager...***", "***Sprinter op ad bjerge...***", "***Hopper over kløfter...***", "***Svømmer op ad floder...***",
            "***Klatrer op ad bjerge...***", "***Svinger mellem kæmpe trætoppe...***", "***Spiser salat...***", "***Sover 16 timer...***", "***Drikker mange liter vand...***",
            "***Svømmer fyn rundt...***", "***Spiser broccoli...***", "***Strækker ud i flere timer...***"
                };

                List<string> StrengthTrainingSentences = new List<string>
                {
            "***Lægger arm med orker...***", "***Slår på sten...***", "***Løfter træer...***", "***Kaster med sten...***", "***Tager 1000 armbøjninger...***", "***Bryder mod bjørne...***",
            "***Udfordrer alle på værtshuset til kamp...***", "***Skovler 4 millioner kvadrat-meter sne...***", "***Spiser 40kg kyllingebryst...***", "***Spiser 80L skovbærskyr...***",
            "***Sniffer 2 poser proteinpulver...***", "***Sniffer kreatin...***", "***Tager anabolske steroider...***", "***Coaching af Anders Trust...***"
                };

                List<string> DefenceTrainingSentences = new List<string>
                {
            "***Undviger ildkugler...***", "***Parerer kødsværd...***", "***Undslipper Kevins headlocks...***", "***Blokerer slag fra bjergtrolde...***",
            "***Forsvarer en fæstning mod invadere...***", "***Danser gennem en regn af pile...***", "***Undviger cumshots...***"
                };



        public async Task DisplayTrainingTexts(CommandContext ctx, string trainingType)
        {
            List<string> selectedSentences;
            switch (trainingType)
            {
                case "Vigor":
                    selectedSentences = VigorTrainingSentences;
                    break;
                case "Strength":
                    selectedSentences = StrengthTrainingSentences;
                    break;
                case "Defence":
                    selectedSentences = DefenceTrainingSentences;
                    break;
                default:
                    selectedSentences = new List<string>();
                    break;
            }
            //Vælger 3 random sætninger fra den valgte liste.
            Random random = new Random();
            List<string> randomSentences = selectedSentences.OrderBy(x => random.Next()).Take(3).ToList();

            //Printer de 3 sætninger med 2 sekunders mellemrum.
            foreach (var sentence in randomSentences)
            {
                await ctx.Channel.SendMessageAsync(sentence + "\n");
                await Task.Delay(2000);

            }

        }





    }


}
