using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BackupManager.Tests
{
    public class BackupIntervalManagerTest
    {
        public BackupIntervalManager FileBackupManagerInstance { get; set; }

        public BackupIntervalManagerTest()
        {
            FileBackupManagerInstance = new BackupIntervalManager();
        }

        [Fact]
        public void Test_BackupInterval()
        {
            var interval = FileBackupManagerInstance.SetInterval(DateTime.Now, BackupIntervalEnum.Daily, 7);
            Assert.Equal(7, interval.IntervalList.Count);
        }

        [Fact]
        public void Test_BackupIntervalLast7Days()
        {
            DateTime dateReference = new DateTime(2017, 4, 1);
            var interval = FileBackupManagerInstance.SetInterval(dateReference, BackupIntervalEnum.Daily, 7);

            Assert.Equal(dateReference, interval.IntervalList[0].End);
            Assert.Equal(dateReference.AddDays(-1), interval.IntervalList[0].Start);
            Assert.Equal(dateReference.AddDays(-1), interval.IntervalList[1].End);
            Assert.Equal(dateReference.AddDays(-2), interval.IntervalList[1].Start);
        }

        [Fact]
        public void Test_DailyAndWeeklyBackupDefinition()
        {
            DateTime dateReference = new DateTime(2017, 4, 1);
            FileBackupManagerInstance.SetInterval(dateReference, BackupIntervalEnum.Daily, 7);
            var nextInterval = FileBackupManagerInstance.SetNextInterval(BackupIntervalEnum.Weekly, 4);

            Assert.Equal(dateReference.AddDays(-7),
                nextInterval.IntervalList.First().End);

            Assert.False(FileBackupManagerInstance.Definition.IsOverriding());
        }

        [Fact]
        public void Test_DefinitionNotOverriding()
        {
            DateTime dateReference = new DateTime(2017, 4, 1);
            var dailyInterval = FileBackupManagerInstance.SetInterval(dateReference, BackupIntervalEnum.Daily, 7);
            var weeklyInterval = FileBackupManagerInstance.SetInterval(dateReference, BackupIntervalEnum.Weekly, 7);
            Assert.True(FileBackupManagerInstance.IsOverriding(dailyInterval, weeklyInterval));
        }


        [Fact]
        public void Test_SetIntervalDefinition()
        {
            DateTime baseDateTime = new DateTime(2017, 4, 9);

            FileBackupManagerInstance.SetIntervalDefinition(
                baseDateTime,
                dailyBackups: 7,
                weeklyBackups: 4,
                monthlyBackups: 12,
                yearlyBackups: 5);

            Assert.Equal(BackupIntervalEnum.Daily, FileBackupManagerInstance.Definition.ItemList[0].IntervalType);
            Assert.Equal(baseDateTime,
                FileBackupManagerInstance.Definition.ItemList[0].End);
            DateTime olderDateDaily = baseDateTime.AddDays(-7);
            Assert.Equal(baseDateTime.AddDays(-7),
                FileBackupManagerInstance.Definition.ItemList[0].Start);

            Assert.Equal(BackupIntervalEnum.Weekly, FileBackupManagerInstance.Definition.ItemList[1].IntervalType);
            Assert.Equal(olderDateDaily,
                FileBackupManagerInstance.Definition.ItemList[1].End);
            DateTime olderDateWeekly = olderDateDaily.AddDays(-7 * 4);
            Assert.Equal(olderDateWeekly,
                FileBackupManagerInstance.Definition.ItemList[1].Start);
        }

    }
}
