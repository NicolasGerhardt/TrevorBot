using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrevorBot.Models;

namespace TrevorBot.Repositories
{
    internal class InMemoryUserLevelRecordRepo : IUserLevelRecordRepo
    {
        private Dictionary<string, UserLevelRecord> _records;

        public InMemoryUserLevelRecordRepo()
        {
            _records = new Dictionary<string, UserLevelRecord>();
        }

        public async Task<UserLevelRecord> GetUserLevelRecordByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Must have valid username");

            if (!_records.ContainsKey(username))
            {
                _records.Add(username, new UserLevelRecord { Username = username, Level = 0, Experiance = 0});
            }

            return _records[username];
        }

        public async Task<UserLevelRecord> IncreeseUserExperiance(UserLevelRecord user, int amount = 1)
        {
            user = await GetUserLevelRecordByUsername(user.Username);

            user.Experiance += amount;

            _records[user.Username] = user;

            return _records[user.Username];
        }

        public async Task<UserLevelRecord> IncreeseUserLevel(UserLevelRecord user, int amount = 1)
        {
            user = await GetUserLevelRecordByUsername(user.Username);

            user.Level += amount;

            _records[user.Username] = user;

            return _records[user.Username];
        }
    }
}
