using System.Collections.Generic;
using System.Linq;
using WorldMap.Models;

namespace WorldMap.Models.StatsViewModels
{
    public class StatsIndexViewModel
    {
        public IEnumerable<Dictionary<int, int>> OverallStats {get; set;}
        public IEnumerable<Dictionary<int, int>> EuropeStats {get; set;}
        public IEnumerable<Dictionary<int, int>> AsiaStats { get; set; }
        public IEnumerable<Dictionary<int, int>> NorthAmericaStats { get; set; }
        public IEnumerable<Dictionary<int, int>> SouthAmericaStats { get; set; }
        public IEnumerable<Dictionary<int, int>> OceaniaStats { get; set; }
        public IEnumerable<Dictionary<int, int>> AfricaStats { get; set; }

        public IEnumerable<Dictionary<int, int>> GetTheGroup(List<Stats> stats, string area = null)
        {
            if (area == null)
            {
                return stats.GroupBy(s => s.Success, s => s.StatsId, 
                    (key, g) => new Dictionary<int, int>(){
                        {key, g.Count()}
                    });
            }
            else 
            {
                return stats.Where(s => s.Country.Continent.Name == area)
                    .GroupBy(s => s.Success, s => s.StatsId, 
                        (key, g) => new Dictionary<int, int>() { 
                            {key, g.Count()} 
                        });
            }
        }

        public StatsIndexViewModel (List<Stats> stats)
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