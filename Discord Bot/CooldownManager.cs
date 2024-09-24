using System;
using System.Collections.Generic;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity.Extensions;
using System.Threading.Tasks;

namespace Discord_Bot
{
    public class CooldownManager
    {
        // Dictionary til at holde styr på cooldowns for hver bruger og træningstype
        private static Dictionary<ulong, Dictionary<string, DateTime>> cooldowns = new Dictionary<ulong, Dictionary<string, DateTime>>();

        // Tjekker om en bruger er på cooldown for en bestemt træningstype
        public static bool IsOnCooldown(ulong userId, string action)
        {
            if (cooldowns.TryGetValue(userId, out var userCooldowns))  // Finder brugerens cooldowns
            {
                if (userCooldowns.TryGetValue(action, out var cooldownEndTime))  // Finder cooldown for den valgte handling
                {
                    if (DateTime.Now < cooldownEndTime)  // Hvis nuværende tid er før cooldown-slut, er brugeren stadig på cooldown
                    {
                        return true;
                    }
                }
            }
            return false;  // Brugeren er ikke på cooldown, eller cooldown er udløbet
        }

        // Sætter cooldown for en bestemt træningstype
        public static void SetCooldown(ulong userId, string action, TimeSpan cooldownDuration)
        {
            if (!cooldowns.ContainsKey(userId))  // Hvis der ikke allerede er cooldowns for denne bruger, opretter vi en ny indgang
            {
                cooldowns[userId] = new Dictionary<string, DateTime>();
            }

            // Sætter cooldown-tiden for den valgte handling (træningstype)
            cooldowns[userId][action] = DateTime.Now.Add(cooldownDuration);
        }

        // Henter hvor meget tid der er tilbage på en brugers cooldown for en specifik handling
        public static TimeSpan GetRemainingCooldown(ulong userId, string action)
        {
            if (cooldowns.TryGetValue(userId, out var userCooldowns))  // Finder brugerens cooldowns
            {
                if (userCooldowns.TryGetValue(action, out var cooldownEndTime))  // Finder cooldown for den valgte handling
                {
                    return cooldownEndTime - DateTime.Now;  // Returnerer den resterende tid
                }
            }

            return TimeSpan.Zero;  // Ingen cooldown fundet
        }
    }
}

