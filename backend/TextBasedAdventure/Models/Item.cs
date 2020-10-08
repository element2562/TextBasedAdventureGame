using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TextBasedAdventure.Models
{
    public class Item
    {

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public int LevelRequirement { get; set; }

        public string ItemType { get; set; }

        public bool Equipped { get; set; }

        public int StrengthBonus { get; set; }

        public int DefenseBonus { get; set; }

        public int HealthBonus { get; set; }

        public Player Player { get; set; }

    }
}
