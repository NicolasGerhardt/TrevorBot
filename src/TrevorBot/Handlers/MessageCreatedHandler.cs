using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrevorBot.Models;
using TrevorBot.Repositories;

namespace TrevorBot.Handlers
{
    internal static class MessageCreatedHandler
    {
        private const int ExperianceToLevel1 = 10;

        private static IUserLevelRecordRepo _userLevelRecordRepo = new InMemoryUserLevelRecordRepo();

        public static async Task Execute(DiscordClient sender, MessageCreateEventArgs e)
        {
            var user = await _userLevelRecordRepo.GetUserLevelRecordByUsername(e.Author.Username);

            user = await _userLevelRecordRepo.IncreeseUserExperiance(user);

            if (CanLevelUp(user))
            {
                user = await _userLevelRecordRepo.IncreeseUserLevel(user);
                await e.Message.RespondAsync($"{user.Username} is now level {user.Level}");
            }
        }

        private static bool CanLevelUp(UserLevelRecord user)
        {
            return user.Experiance > ExperianceNeededForLevel(user.Level + 1);
        }

        private static int ExperianceNeededForLevel(int level)
        {
            if (level < 0) return 0;
            if (level == 1) return ExperianceToLevel1;
            return (level * ExperianceToLevel1) + ExperianceNeededForLevel(level - 1);
        }

    }
}
