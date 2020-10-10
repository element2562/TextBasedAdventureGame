using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TextBasedAdventure.Models
{
    public class Zone
    {
        public int ZoneId { get; set; }

        public string ZoneName { get; set; }

        public List<Monster> Monsters { get; set; }
    }
}
