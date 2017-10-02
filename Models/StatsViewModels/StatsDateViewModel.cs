using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldMap.Models.StatsViewModels
{
    public class StatsDateViewModel
    {
        public IEnumerable<Dictionary<DateTime, double>> OverallStats { get; set; }
        public IEnumerable<Dictionary<DateTime, double>> EuropeStats { get; set; }
        public IEnumerable<Dictionary<DateTime, double>> AsiaStats { get; set; }
        public IEnumerable<Dictionary<DateTime, double>> NorthAmericaStats { get; set; }
        public IEnumerable<Dictionary<DateTime, double>> SouthAmericaStats { get; set; }
        public IEnumerable<Dictionary<DateTime, double>> OceaniaStats { get; set; }
        public IEnumerable<Dictionary<DateTime, double>> AfricaStats { get; set; }

        public IEnumerable<Dictionary<DateTime, double>> GetTheGroup(List<Stats> stats, string area = null)
        {
            if (area == null)
            {
                return stats.GroupBy(s => s.DateCreated, s => s.Success,
                    (key, g) => new Dictionary<DateTime, double>(){
                        {key, g.Average() * 100}
                    });
            }
            else
            {
                return stats.Where(s => s.Country.Continent.Name == area)
                    .GroupBy(s => s.DateCreated, s => s.Success,
                        (key, g) => new Dictionary<DateTime, double>(){
                            {key, g.Average() * 100}
                    });
            }
        }

        public StatsDateViewModel(List<Stats> stats)
        {
            OverallStats = GetTheGroup(stats);
            EuropeStats = GetTheGroup(stats, "Europe");
            AsiaStats = GetTheGroup(stats, "Asia");
            NorthAmericaStats = GetTheGroup(stats, "North America");
            SouthAmericaStats = GetTheGroup(stats, "South America");
            OceaniaStats = GetTheGroup(stats, "Oceania");
            AfricaStats = GetTheGroup(stats, "Africa");
        }
    
    }
}