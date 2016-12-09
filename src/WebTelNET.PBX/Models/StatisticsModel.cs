using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTelNET.PBX.Models
{
    public class StatisticsModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class PBXStatisticsModel : StatisticsModel
    {
        public byte Version { get; set; }
    }

    public class OverallStatisticsModel : StatisticsModel
    {
        public string Sip { get; set; }
        public bool CostOnly { get; set; }
        public string Type { get; set; }
    }
}
