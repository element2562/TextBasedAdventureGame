using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TextBasedAdventure.Models
{
    public class Monster
    {

        public int MonsterId { get; set; }

        public string MonsterName { get; set; }

        public int Level { get; set; }

        public int Health { get; set; }

        public int MaxHealth { get; set; }

        public Zone Zone { get; set; }

    }
}
