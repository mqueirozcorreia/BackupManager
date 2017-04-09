using BackupManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BackupManager.Tests
{
    public class FileBackupManagerTest
    {
        private FileBackupManager FileBackupManagerInstance { get; set; }

        public FileBackupManagerTest()
        {
            FileBackupManagerInstance = new FileBackupManager();
        }

        [Fact]
        public void Test_UpdateFileHistory()
        {
            FileBackupManagerInstance.IntervalManager.SetIntervalDefinition(
                new DateTime(2017,4,9),
                dailyBackups : 7,
                weeklyBackups : 4,
                monthlyBackups : 12,
                yearlyBackups : 5);
        }
    }
}
