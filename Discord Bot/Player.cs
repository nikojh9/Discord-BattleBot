using System;

namespace Discord_Bot
{
    public class Player
    {

        //-----Profil oplysninger (DISCORD)-----

        //Navn = Tages fra Discord.
        //Navn bliver til ID når man opretter, og det vil blive gemt i database/
        public string Navn {  get; set; }
        //Profilbillede = Avatar URL tages fra Discord
        public string ProfilBillede { get; set; }


        //-----battle klasse (TBD)-----

        //ELO - Starter på 1200 - skal udregnes efter en ELO formel efter hver kamp
        public int Elo { get; set; } = 1200;
        public int Wins { get; set; } = 0;
        public int Ties { get; set; } = 0;
        public int Loses { get; set; } = 0;

        // Skal have nextOpponent variabel (tbd) hvordan det skal laves?? 
        //public string NextOpponent { get; set; }


        //-----STATS-----

        //Vigor = Health
        public int Vigor { get; set; }
        //Strength = Skade
        public int Strength { get; set; }
        //Defence = Skade penalty til modestander
        public int Defence { get; set; }


        //Player contructor
        public Player(string navn, string profilbillede, int vigor, int strength, int defence) {
            Navn = navn;
            ProfilBillede = profilbillede;
            this.Elo = Elo;
            this.Wins = Wins;
            this.Ties = Ties;
            this.Loses = Loses;
            Vigor = vigor;
            Strength = strength;
            Defence = defence;
            
               
        }

    }
}
