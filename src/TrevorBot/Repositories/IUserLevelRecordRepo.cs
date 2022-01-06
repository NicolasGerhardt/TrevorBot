using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrevorBot.Models;

namespace TrevorBot.Repositories
{
    internal interface IUserLevelRecordRepo
    {
        public Task<UserLevelRecord> GetUserLevelRecordByUsername(string username);
        public Task<UserLevelRecord> IncreeseUserExperiance(UserLevelRecord user, int amount = 1);
        public Task<UserLevelRecord> IncreeseUserLevel(UserLevelRecord user, int amount = 1);
    }
}
