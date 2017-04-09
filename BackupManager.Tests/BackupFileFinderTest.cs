using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BackupManager.Tests
{
    public class BackupFileFinderTest
    {
        private BackupFileFinder BackupFileFinderInstance { get; set; }

        public BackupFileFinderTest()
        {
            BackupFileFinderInstance = new BackupFileFinder();
        }

        [Fact]
        public void Test_BackupFileInfoReadFromFullNameAndBaseName()
        {
            DateTime backupDate = new DateTime(2017, 04, 08);
            string directoryPath, backupFileName;
            string backupBaseName = "backup";
            string fileFullPath = MockBackupFullName(backupBaseName, backupDate, out directoryPath, out backupFileName);

            var backupFileInfo = BackupFileFinderInstance.GetBackupFileInfo(fileFullPath, backupBaseName);

            Assert.Equal(directoryPath, backupFileInfo.DirectoryPath);
            Assert.Equal(backupFileName, backupFileInfo.Name);
            Assert.Equal(backupDate, backupFileInfo.DateTime);
        }

        [Fact]
        public void Test_BackupFileInfoReadFromFullNameOnly()
        {
            DateTime backupDate = new DateTime(2017, 04, 08);
            string directoryPath, backupFileName;
            string backupBaseName = "backup";
            string fileFullPath = MockBackupFullName(backupBaseName, backupDate, out directoryPath, out backupFileName);

            var backupFileInfo = BackupFileFinderInstance.GetBackupFileInfo(fileFullPath);

            Assert.Equal(directoryPath, backupFileInfo.DirectoryPath);
            Assert.Equal(backupFileName, backupFileInfo.Name);
            Assert.Equal(backupDate, backupFileInfo.DateTime);
        }

        [Fact]
        public void Test_ExpectedBackup()
        {
            string backupBaseName = "backup";
            string backupFullName = MockBackupFullName(backupBaseName, new DateTime(2017, 8, 3));
            Assert.True(BackupFileFinderInstance.IsExpectedBackupFile(backupFullName, backupBaseName));
        }

        [Fact]
        public void Test_NotExpectedBackup()
        {
            string backupBaseName = "backup";
            string backupFullName = MockBackupFullName(backupBaseName + " ", new DateTime(2017, 8, 3));
            Assert.False(BackupFileFinderInstance.IsExpectedBackupFile(backupFullName, backupBaseName));
        }

        [Fact]
        public void Test_GetBackupFileInfoFromNullArray()
        {
            string[] backupFullnameArray = null;

            var backupFiles = BackupFileFinderInstance.GetBackupFileInfoList(null, backupFullnameArray);

            Assert.Equal(null, backupFiles);
        }

        [Fact]
        public void Test_GetBackupFileInfoFromArrayExpectedOnly()
        {
            string backupBaseName = "backup";
            List<string> backupFullnameList = new List<string>();
            //Adding files for all the week long
            for (int i = 0; i < 7; i++)
            {
                backupFullnameList.Add(MockBackupFullName(backupBaseName, new DateTime(2017, 4, 2).AddDays(i)));
            }

            var backupFileInfoList = BackupFileFinderInstance
                .GetBackupFileInfoList(backupBaseName, backupFullnameList.ToArray());

            Assert.Equal(7, backupFileInfoList.Count);
        }

        [Fact]
        public void Test_GetBackupFileInfoFromArrayExpectedAndNotExptected()
        {
            string backupBaseName = "backup";
            List<string> backupFullnameList = new List<string>();
            //Adding files for all the week long
            for (int i = 0; i < 7; i++)
            {
                backupFullnameList.Add(MockBackupFullName(backupBaseName, new DateTime(2017, 4, 2).AddDays(i)));
                backupFullnameList.Add(MockBackupFullName("backup ", new DateTime(2017, 4, 2).AddDays(i)));
                backupFullnameList.Add(MockBackupFullName(" backup", new DateTime(2017, 4, 2).AddDays(i)));
            }

            var backupFileInfoList = BackupFileFinderInstance
                .GetBackupFileInfoList(backupBaseName, backupFullnameList.ToArray());

            Assert.Equal(7, backupFileInfoList.Count);
        }

        private static string MockBackupFullName(string backupBaseName, DateTime backupDate, out string directoryPath, out string backupFileName)
        {
            directoryPath = @"c:\BDBackup\";
            string dateTimeFormatted = backupDate.ToString("yyyy-MM-dd");
            backupFileName = $"{backupBaseName} {dateTimeFormatted}.zip";
            return $"{directoryPath}{backupFileName}";
        }

        private static string MockBackupFullName(string backupBaseName, DateTime backupDate)
        {
            string directoryPath;
            string backupFileName;

            directoryPath = @"c:\BDBackup\";
            backupFileName = string.Format("{0} {1}.zip", backupBaseName, backupDate.ToString("yyyy-MM-dd"));
            return string.Format("{0}{1}", directoryPath, backupFileName);
        }
    }
}
