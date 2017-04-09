using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupManager
{
    public class BackupIntervalManager
    {
        public BackupIntervalManager()
        {
            Definition = new BackupDefinition();
        }

        public BackupDefinition Definition { get; set; }

        public void SetIntervalDefinition(
            DateTime baseDateTime,
            int dailyBackups = 0,
            int weeklyBackups = 0,
            int monthlyBackups = 0,
            int yearlyBackups = 0)
        {
            if (dailyBackups != 0)
            {
                SetInterval(baseDateTime,
                    BackupIntervalEnum.Daily,
                    dailyBackups);
            }

            if (weeklyBackups != 0)
            {
                if (this.Definition.ItemList.Count > 0)
                {
                    SetNextInterval(BackupIntervalEnum.Weekly,
                        weeklyBackups);
                }
                else
                {
                    SetInterval(baseDateTime,
                        BackupIntervalEnum.Weekly,
                        weeklyBackups);
                }
            }

            if (monthlyBackups != 0)
            {
                if (this.Definition.ItemList.Count > 0)
                {
                    SetNextInterval(BackupIntervalEnum.Monthly,
                        monthlyBackups);
                }
                else
                {
                    SetInterval(baseDateTime,
                        BackupIntervalEnum.Monthly,
                        monthlyBackups);
                }
            }

            if (yearlyBackups != 0)
            {
                if (this.Definition.ItemList.Count > 0)
                {
                    SetNextInterval(BackupIntervalEnum.Yearly,
                        yearlyBackups);
                }
                else
                {
                    SetInterval(baseDateTime,
                        BackupIntervalEnum.Yearly,
                        yearlyBackups);
                }
            }
        }

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
                int dayToAdd = 1;

                if (interval == BackupIntervalEnum.Weekly)
                {
                    dayToAdd = 7;
                }
                else if (interval == BackupIntervalEnum.Monthly)
                {
                    dayToAdd = 30;
                }
                else if (interval == BackupIntervalEnum.Yearly)
                {
                    dayToAdd = 365;
                }

                result.IntervalList.Add(new Interval()
                {
                    Start = dateReference.AddDays(-1 * ((i * dayToAdd) + dayToAdd)),
                    End = dateReference.AddDays(-1 * (i * dayToAdd)),
                });
            }

            Definition.ItemList.Add(result);

            return result;
        }

        public bool IsOverriding(BackupIntervalDefinition dailyInterval, BackupIntervalDefinition weeklyInterval)
        {
            foreach (var dailyIntervalItem in dailyInterval.IntervalList)
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
            return SetInterval(this.Definition.ItemList.Last().Start.Value, interval, size);
        }

    }
}
