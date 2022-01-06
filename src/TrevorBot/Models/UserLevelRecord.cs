using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrevorBot.Models
{
    internal struct UserLevelRecord
    {
        public string? Username { get; set; }
        public int Level { get; set; } = 0;
        public int Experiance { get; set; } = 0;

    }
}
