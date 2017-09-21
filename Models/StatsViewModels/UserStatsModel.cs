using System;
using System.Collections.Generic;
using System.Linq;
using WorldMap.Data;

namespace WorldMap.Models.StatsViewModels
{
    public class UserStatsModel
    {

        public Country Country { get; set; }

        public DateTime Date { get; set; }

        public int SuccessesCumulative { get; set; }

        public int TriesCumulative {get; set; }

        public List<int> SuccessesList { get; set; }

        public List<int> TriesList { get; set; }

        public decimal Percentage { get; set; }

        public double AvgNumTries { get; set; }

        public int MaxNumTries { get; set; }

        public int MedianNumTries { get; set; }
    }
}