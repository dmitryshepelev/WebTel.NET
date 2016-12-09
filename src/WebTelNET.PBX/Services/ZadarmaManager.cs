using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTelNET.PBX.Services
{
    public class PBXStatisticsInfoTyped : PBXStatisticsInfo
    {
        public CallType Type { get; set; }
    }

    public class PBXStatisticsGroup<T> where T : PBXStatisticsInfo
    {
        public IGrouping<string, T> Group { get; }

        public PBXStatisticsGroup(IGrouping<string, T> group)
        {
            Group = group;
        }

        public T GetCallByDestination(string destination)
        {
            return Group.FirstOrDefault(x => x.Destination == destination);
        }

        public IEnumerable<T> GetCallsByDisposition(string disposition)
        {
            return Group.Where(x => x.Disposition == disposition);
        }

        public T GetCallWithSameDisposition(T call)
        {
            var callsWithDisposition = GetCallsByDisposition(call.Disposition);
            return callsWithDisposition.FirstOrDefault(x => x.Destination != call.Destination);
        }
    }

    /// <summary>
    /// Class represents grouped pbx statistics state
    /// </summary>
    public class GroupedPBXStatistics
    {
        public IList<PBXStatisticsGroup<PBXStatisticsInfo>> Stats { get; }

        public GroupedPBXStatistics(IList<PBXStatisticsInfo> stats, ResponseVersion statsVersion = ResponseVersion.New)
        {
            Stats = new List<PBXStatisticsGroup<PBXStatisticsInfo>>();
            if (statsVersion == ResponseVersion.New)
            {
                var groups = stats.GroupBy(x => x.pbx_call_id);
                foreach (var group in groups)
                {
                    Stats.Add(new PBXStatisticsGroup<PBXStatisticsInfo>(group));
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }

    public class ZadarmaManager : IPBXManager
    {
        public IList<PBXStatisticsInfoTyped> DefinePBXStatisticsRecords(GroupedPBXStatistics groupedPbxStatistics)
        {
            var definedPBXStatisticsRecords = new List<PBXStatisticsInfoTyped>();
            foreach (var group in groupedPbxStatistics.Stats)
            {
                var incomingCallRecord = group.GetCallByDestination(CallType.Incoming);
                var targetCallRecord = (incomingCallRecord == null ? group.Group.First() : group.GetCallWithSameDisposition(incomingCallRecord)) as PBXStatisticsInfoTyped;

                definedPBXStatisticsRecords.Add(targetCallRecord);
            }
            return definedPBXStatisticsRecords;
        }
    }
}
