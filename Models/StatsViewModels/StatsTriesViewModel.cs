using System.Collections.Generic;
using System.Linq;

namespace WorldMap.Models.StatsViewModels
{
    public class StatsTriesViewModel
    {
        public IEnumerable<Dictionary<string, double>> OverallStats { get; set; }
        public IEnumerable<Dictionary<string, double>> EuropeStats { get; set; }
        public IEnumerable<Dictionary<string, double>> AsiaStats { get; set; }
        public IEnumerable<Dictionary<string, double>> NorthAmericaStats { get; set; }
        public IEnumerable<Dictionary<string, double>> SouthAmericaStats { get; set; }
        public IEnumerable<Dictionary<string, double>> OceaniaStats { get; set; }
        public IEnumerable<Dictionary<string, double>> AfricaStats { get; set; }

        public IEnumerable<Dictionary<string, double>> GetTheGroup(List<Stats> stats, string area = null)
        {
            if (area == null)
            {
                return stats
                    .OrderBy(s => s.Country.Continent.Name)
                    .GroupBy(s => s.Country.Continent.Name, s => s.Tries,
                    (key, g) => new Dictionary<string, double>(){
                        {key, g.Average()}
                    });
            }
            else
            {
                return stats.Where(s => s.Country.Continent.Name == area)
                    .OrderBy(s => s.Country.Name)
                    .GroupBy(s => s.Country.Name, s => s.Tries,
                    (key, g) => new Dictionary<string, double>(){
                        {key, g.Average()}
                    });
            }
        }

        public StatsTriesViewModel(List<Stats> stats)
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