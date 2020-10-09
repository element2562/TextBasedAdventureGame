using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextBasedAdventure.Models
{
    public class Npc
    {

        public int NpcId { get; set; }

        public string NpcName { get; set; }

        public bool IsMerchant { get; set; }

        public bool GivesQuests { get; set; }
        
        public List<Quest> Quests { get; set; }
    }
}
