using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WorldMap.Models
{
    public class SubRegion
    {
        [Key]
        public int SubRegionId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ContinentId {get; set; }

        [JsonIgnore]
        public Continent Continent { get; set; }

        public ICollection<Country> Countries { get; set; }



        
    }
}