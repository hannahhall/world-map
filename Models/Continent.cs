using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorldMap.Models
{
    public class Continent
    {
        [Key]
        public int ContinentId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Country> Countries { get; set; }

        public ICollection<SubRegion> Regions { get; set; }

    }
}