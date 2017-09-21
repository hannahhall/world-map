using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace WorldMap.Models
{
    public class Stats
    {
        [Key]
        public int StatsId { get; set; }

        [Required]
        public int CountryId { get; set; }

        public Country Country { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public int Tries { get; set; }

        public int Success { get; set; }

        public DateTime DateCreated { get; set; }
    }
}