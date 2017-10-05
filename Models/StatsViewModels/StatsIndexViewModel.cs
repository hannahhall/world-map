using System.Collections.Generic;
using System.Linq;
using WorldMap.Models;

namespace WorldMap.Models.StatsViewModels
{
    public class StatsIndexViewModel
    {
        public IEnumerable<Dictionary<int, int>> OverallStats {get; set;}
        public IEnumerable<Dictionary<string, double>> EuropeStats {get; set;}
        public IEnumerable<Dictionary<string, double>> AsiaStats { get; set; }
        public IEnumerable<Dictionary<string, double>> NorthAmericaStats { get; set; }
        public IEnumerable<Dictionary<string, double>> SouthAmericaStats { get; set; }
        public IEnumerable<Dictionary<string, double>> OceaniaStats { get; set; }
        public IEnumerable<Dictionary<string, double>> AfricaStats { get; set; }

        public IEnumerable<Dictionary<int, int>> GetOverall(List<Stats> stats, string area = null)
        {
            
            return stats.GroupBy(s => s.Success, s => s.StatsId, 
                        (key, g) => new Dictionary<int, int>(){ {key, g.Count()} });
        }
        public IEnumerable<Dictionary<string, double>> GetAreas(List<Stats> stats, string area)
        {
            return stats.OrderBy(s => s.Country.Name).Where(s => s.Country.Continent.Name == area)
                        .GroupBy(s => s.Country.Name, s => s.Success,
                            (key, g) => new Dictionary<string, double>() { {key, g.Average()} });
        }



        public StatsIndexViewModel (List<Stats> stats)
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