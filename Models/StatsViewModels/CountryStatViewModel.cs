using System.Collections.Generic;
using WorldMap.Data;

namespace WorldMap.Models.StatsViewModels
{
    public class CountryStatsViewModel
    {
        public ApplicationUser User { get; set; }

        public Country Country { get; set; }

        public int Successes { get; set; }

        public IEnumerable<int> TriesList { get; set; }

        public int Percentage { get; set; }

        public int AvgNumTries { get; set; }

        public int MaxNumTries { get; set; }

        public int MeanNumTries { get; set; }

        public CountryStatsViewModel (Country country, ApplicationUser user, ApplicationDbContext context)
        {
            
        }

    }
}