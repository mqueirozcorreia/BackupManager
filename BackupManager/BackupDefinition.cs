using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupManager
{
    public class BackupDefinition
    {
        public BackupDefinition()
        {
            IntervalList = new List<BackupIntervalDefinition>();
        }

        public List<BackupIntervalDefinition> IntervalList { get; set; }

        public bool IsOverriding()
        {
            for(int i=0; i<IntervalList.Count;i++)
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
}
