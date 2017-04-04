using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupManager
{
    public class FileBackupManager
    {
        public FileBackupManager()
        {
            Definition = new BackupDefinition();
        }

        public BackupDefinition Definition { get; set; }

        public BackupIntervalDefinition SetInterval(DateTime dateReference,
            BackupIntervalEnum interval, 
            int size)
        {
            BackupIntervalDefinition result = new BackupIntervalDefinition()
            {
                IntervalType = interval,
                IntervalList = new List<Interval>()
            };

            for (int i = 0; i < size; i++)
            {
                result.IntervalList.Add(new Interval()
                {
                    Start = dateReference.AddDays(-1 * (i + 1)),
                    End = dateReference.AddDays(-1 * i),
                });
            }

            Definition.IntervalList.Add(result);

            return result;
        }

        public bool IsOverriding(BackupIntervalDefinition dailyInterval, BackupIntervalDefinition weeklyInterval)
        {
            foreach(var dailyIntervalItem in dailyInterval.IntervalList)
            {
                foreach (var weeklyIntervalItem in weeklyInterval.IntervalList)
                {
                    if (dailyIntervalItem.IsOverriding(weeklyIntervalItem))
                        return true;
                }
            }

            return false;
        }

        public BackupIntervalDefinition SetNextInterval(BackupIntervalEnum interval,
            int size)
        {
            return SetInterval(this.Definition.IntervalList.Last().IntervalList.Last().End, interval, size);
        }
    }

}
