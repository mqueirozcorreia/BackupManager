using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupManager
{
    public class BackupIntervalDefinition
    {
        public BackupIntervalEnum IntervalType { get; set; }
        public List<Interval> IntervalList { get; set; }

        internal bool IsOverriding(BackupIntervalDefinition backupIntervalDefinition)
        {
            for(int i=0;i<IntervalList.Count;i++)
            {
                for (int j = i + 1; j < IntervalList.Count; j++)
                {
                    if (IntervalList[i].IsOverriding(IntervalList[j]))
                        return true;
                }
            }

            return false;
        }
    }

    public enum BackupIntervalEnum
    {
        Daily = 10,
        Weekly = 20
    }
}
