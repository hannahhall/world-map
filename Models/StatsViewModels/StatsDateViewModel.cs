using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldMap.Models.StatsViewModels
{
    public class StatsDateViewModel
    {
        public IEnumerable<Dictionary<DateTime, double>> OverallStats { get; set; }
        public IEnumerable<Dictionary<string, IEnumerable<Dictionary<DateTime, double>>>> EuropeStats { get; set; }
        public IEnumerable<Dictionary<string, IEnumerable<Dictionary<DateTime, double>>>> AsiaStats { get; set; }
        public IEnumerable<Dictionary<string, IEnumerable<Dictionary<DateTime, double>>>> NorthAmericaStats { get; set; }
        public IEnumerable<Dictionary<string, IEnumerable<Dictionary<DateTime, double>>>> SouthAmericaStats { get; set; }
        public IEnumerable<Dictionary<string, IEnumerable<Dictionary<DateTime, double>>>> OceaniaStats { get; set; }
        public IEnumerable<Dictionary<string, IEnumerable<Dictionary<DateTime, double>>>> AfricaStats { get; set; }

        public IEnumerable<Dictionary<DateTime, double>> GetOverall(List<Stats> stats)
        {
            
            return stats.OrderBy(s => s.DateCreated).GroupBy(s => s.DateCreated, s => s.Success,
                (key, g) => new Dictionary<DateTime, double>(){
                    {key, g.Average() * 100}
                });
            
        }

        public IEnumerable<Dictionary<string, IEnumerable<Dictionary<DateTime, double>>>> GetAreas (List<Stats> stats, string area)
        {
            return stats.Where(s => s.Country.Continent.Name == area)
                .OrderBy(s => s.Country.Name)
                .OrderBy(s => s.DateCreated)
                .GroupBy(s => s.Country.Name, s => s,
                    (k, g) => new Dictionary<string, IEnumerable<Dictionary<DateTime, double>>>()
                    {
                        {k, g.GroupBy(t => t.DateCreated, t => t.Success,
                            (key, x) => new Dictionary<DateTime, double>()
                            {
                                {key, x.Average() * 100}
                            })
                        }
                    }
                );
        }

        public StatsDateViewModel(List<Stats> stats)
        {
            OverallStats = GetOverall(stats);
            EuropeStats = GetAreas(stats, "Europe");
            AsiaStats = GetAreas(stats, "Asia");
            NorthAmericaStats = GetAreas(stats, "North America");
            SouthAmericaStats = GetAreas(stats, "South America");
            OceaniaStats = GetAreas(stats, "Oceania");
            AfricaStats = GetAreas(stats, "Africa");
        }
    
    }
}