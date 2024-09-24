using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;


namespace Discord_Bot.Controller
{
    public class PlayerController
    {
        // STIEN TIL JSON
        private const string JsonFilePath = "C:\\Users\\Nikolai\\source\\repos\\Discord BattleBot\\Discord Bot\\Account.json"; // ÆNDRE TIL DIN EGEN HVIS DU SKAL KØRE PROGRAMMET

        // Load all players from the JSON file
        public Task<List<Player>> LoadPlayersData()
        {
            // CHECKER OM STI TIL FILEN FINDES (DATABASE)
            if (!File.Exists(JsonFilePath))
            {
                throw new FileNotFoundException($"The file {JsonFilePath} was not found.");
            }

            // LÆS JSON FILEN (DATABASE)
            string jsonString = File.ReadAllText(JsonFilePath);
            var accountsData = JsonConvert.DeserializeObject<AccountsData>(jsonString) ?? new AccountsData();
            return Task.FromResult(accountsData.Accounts); // Return the list of players
        }

        // GEMMER OG OVERRIDER LISTEN MED ÆNDRINGER TIL SPILLERE
        public Task SavePlayersData(List<Player> players)
        {
            var accountsData = new AccountsData { Accounts = players }; // Smider players om til JSON format
            string jsonString = JsonConvert.SerializeObject(accountsData, Formatting.Indented); // Gør JSON læseligt og fedt opstillet
            File.WriteAllText(JsonFilePath, jsonString); // Skriver til JSON filen.
            return Task.CompletedTask; // Task done
        }

        // Tilføjer ny spiller
        public async Task AddNewPlayer(string username, string avatarUrl)
        {
            //LOADER ALLE SPILLERE
            var players = await LoadPlayersData();

            // Skaber ny spiller
            var newPlayer = new Player(username, avatarUrl);

            // Tilføjer den nye spiller.
            players.Add(newPlayer);

            // Gemmer den nye spiller og opdaterer listen af alle spillere
            await SavePlayersData(players);
        }

        // Get player ved brug af username
        public async Task<Player> GetPlayer(string username)
        {
            // Load alle spillere
            var players = await LoadPlayersData();
            return players.FirstOrDefault(p => p.Navn == username);
        }

        // Opdater en spillers stats
        public async Task UpdatePlayerStats(Player player, int wins, int losses, int ties, int vigor, int strength, int defence)
        {
            // Update wins/losses/ties
            player.Wins += wins;
            player.Loses += losses;
            player.Ties += ties;
            player.Elo = CalculateNewElo(player.Elo, wins, losses, ties);
            player.Vigor += vigor;
            player.Strength += strength;
            player.Defence += defence;

            var players = await LoadPlayersData();
            var existingPlayer = players.FirstOrDefault(p => p.Navn == player.Navn);

            if (existingPlayer != null)
            {
                existingPlayer.Wins = player.Wins;
                existingPlayer.Loses = player.Loses;
                existingPlayer.Ties = player.Ties;
                existingPlayer.Elo = player.Elo;
                existingPlayer.Vigor = player.Vigor;    
                existingPlayer.Strength = player.Strength;
                existingPlayer.Defence = player.Defence;
                await SavePlayersData(players);
            }
        }

        // Calculate ELO metode
        private int CalculateNewElo(int currentElo, int wins, int losses, int ties)
        {
            int newElo = currentElo;
            newElo += (wins * 50);
            newElo -= (losses * 30);
            newElo += (ties * 10);
            return newElo;
        }

        // Klasse til at repræsentere JSON array
        private class AccountsData
        {
            public List<Player> Accounts { get; set; } = new List<Player>();
        }
    }
}
