using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public SubRegion SubRegion { get; set; }

        public string Coordinates { get; set; }

        [Required]
        public string Capital { get; set; }

        [Required]
        public int ContinentId { get; set; }

        [JsonIgnore]
        public Continent Continent { get; set; }

        public ICollection<Stats> CountryStats { get; set; }

        

    }
}