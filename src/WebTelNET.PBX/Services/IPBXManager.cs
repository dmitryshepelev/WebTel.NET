using System.Collections.Generic;
using System.Linq;

namespace WebTelNET.PBX.Services
{
    public interface IPBXManager
    {
        IList<PBXStatisticsInfoTyped> DefinePBXStatisticsRecords(GroupedPBXStatistics groupedPbxStatistics);
    }
}