using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TextBasedAdventure.Models
{
    public class Player
    {

        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public int Level { get; set; }

        public int Health { get; set; }

        public int MaxHealth { get; set; }

        public int Strength { get; set; }

        public int Defense { get; set; }

        public Zone CurrentZone { get; set; }

        public List<Item> Items { get; set; }

    }
}
