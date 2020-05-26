using System.Collections.Generic;

namespace DojoTracker.Models
{
    public class DojoStatistics
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Difficulty { get; set; }
        public IEnumerable<string> SolvedUserIds { get; set; }
        public int NumOfUsersSolved { get; set; }
        public Dictionary<string, double> MostFrequentlySolvedInLanguage { get; set; }
        public Dictionary<string, double> LeastFrequentlySolvedInLanguage { get; set; }
    }
}