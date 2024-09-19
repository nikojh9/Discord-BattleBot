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
        private static Dictionary<ulong, Dictionary<string, DateTime>> cooldowns = new Dictionary<ulong, Dictionary<string, DateTime>>();

        public static bool isOnCooldown(ulong userId, string action)
        {
            if (cooldowns.TryGetValue(userId, out var userCooldowns))
            {
                if (DateTime.Now < cooldownEndTime)
                {
                    return true;
                }
            }
            return false;
        }



        public static void SetCooldown(ulong userId, string action, TimeSpan cooldownDuration)
        {
            if (!cooldowns.ContainsKey(userId))
            {
                cooldowns[userId] = new Dictionary<string, DateTime>();
            }
            cooldowns[userId][action] = DateTime.Now.Add(cooldownDuration);
        }

        public static TimeSpan GetRemainingCooldown(ulong userId, string action)
        {
            if(cooldowns.TryGetValue(userId, out var userCooldowns))
            {
                if(userCooldowns.TryGetValue(action, out var cooldownEndTime))
                {
                    return cooldownEndTime - DateTime.Now;
                }
            }
            return TimeSpan.Zero; 
        }
    }
}

