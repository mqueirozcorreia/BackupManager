using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackupManager
{
    public class BackupFileFinder
    {
        private const string REGEX_DIRECTORY_SEPARATOR = "[\\\\|/]";
        private const string REGEX_CANONICAL_DATETIME_WITH_DOT = @"\d{4}-\d{2}-\d{2}(?=\.)";

        public BackupFileInfo GetBackupFileInfo(string backupFullName, string backupFileName)
        {
            if (!IsExpectedBackupFile(backupFullName, backupFileName))
                return null;

            return GetBackupFileInfo(backupFullName);
        }

        public BackupFileInfo GetBackupFileInfo(string backupFullName)
        {
            BackupFileInfo result = new BackupFileInfo();

            Match matchLastSlash = Regex.Match(backupFullName, REGEX_DIRECTORY_SEPARATOR, RegexOptions.RightToLeft);
            result.DirectoryPath = backupFullName.Substring(0, matchLastSlash.Index + 1);

            Match matchDateTime = Regex.Match(backupFullName, REGEX_CANONICAL_DATETIME_WITH_DOT, RegexOptions.RightToLeft);
            result.DateTime = DateTime.ParseExact(matchDateTime.Value,
                "yyyy-MM-dd",
                System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat);

            result.Name = backupFullName.Substring(matchLastSlash.Index + 1);

            return result;
        }


        public bool IsExpectedBackupFile(string backupFullName, string backupBaseName)
        {
            return GetBackupFileRegex(backupBaseName).IsMatch(backupFullName);
        }

        private static Regex GetBackupFileRegex(string backupBaseName)
        {
            return new Regex($@"{REGEX_DIRECTORY_SEPARATOR}{backupBaseName} {REGEX_CANONICAL_DATETIME_WITH_DOT}");
        }

        public List<BackupFileInfo> GetBackupFileInfoList(string backupBaseName, params string[] backupFullNameArray)
        {
            if (backupFullNameArray == null)
                return null;

            Regex regex = GetBackupFileRegex(backupBaseName);

            List<string> matchedBackupFilesPathList = backupFullNameArray
                .Where(f => regex.IsMatch(f))
                .ToList();

            return matchedBackupFilesPathList
                .Select(m => GetBackupFileInfo(m))
                .ToList();
        }
    }
}
