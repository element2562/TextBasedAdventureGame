using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextBasedAdventure.Models
{
    public class Quest
    {

        public int QuestId { get; set; }

        public int XpReward { get; set; }

        public int GoldReward { get; set; }

        public bool IsComplete { get; set; }

        public Npc Npc { get; set; }

    }
}
