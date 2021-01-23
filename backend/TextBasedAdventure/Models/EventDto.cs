using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextBasedAdventure.Models
{
	public class EventDto
	{
        public int EventId { get; set; }

        public string EventSummary { get; set; }

        public bool EventAlreadyEncountered { get; set; }

        public bool EventPassed { get; set; }

        public int? MonsterId { get; set; }

        public int? ItemId { get; set; }

        public int ZoneId { get; set; }
    }
}
