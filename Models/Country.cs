using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WorldMap.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Sub Region")]
        public int SubRegionId { get; set;}

        public SubRegion SubRegion { get; set; }

        public string Coordinates { get; set; }

        [Required]
        public string Capital { get; set; }

        [Required]
        public int ContinentId { get; set; }

        public Continent Continent { get; set; }

        public ICollection<Stats> CountryStats { get; set; }

        

    }
}