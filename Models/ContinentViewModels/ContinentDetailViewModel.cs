using System.Collections.Generic;

namespace WorldMap.Models.ContinentViewModels
{
    public class ContinentDetailViewModel
    {
        public string UserId { get; set; }

        public Continent Continent { get; set; }

        public List<Country> Countries { get; set; }

        public List<SubRegion> SubRegions { get; set; }        
    }
}