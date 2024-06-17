using System.Collections.Generic;

namespace AzureEmbeddingAPI.Models
{
    public class Match
    {
        public string MatchName { get; set; }
        public double Weighting { get; set; }
        public int MatchID { get; set; }
    }

    public class EmbeddingResponse
    {
        public int InputID { get; set; }
        public string DataType { get; set; }
        public string InputName { get; set; }
        public string MatchType { get; set; }
        public List<Match> Matches { get; set; }
    }
}
