using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextBasedAdventure.Models
{
    public class Event
    {

        public int EventId { get; set; }

        public string EventSummary { get; set; }

        public bool EventAlreadyEncountered { get; set; }

        public bool EventPassed { get; set; }

        public Monster? Monster { get; set; }

        public Item? Item { get; set; }

        public Zone Zone { get; set; }

    }
} 
