using System.Collections.Generic;

namespace WorldMap.Models.ContinentViewModels
{
    public class ContinentDetailViewModel
    {
        public ApplicationUser User { get; set; }

        public Continent Continent { get; set; }

        public List<Country> Countries { get; set; }

        public int Answer { get; set; }
        
    }
}