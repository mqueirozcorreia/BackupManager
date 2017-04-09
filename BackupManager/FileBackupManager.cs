using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupManager
{
    public class FileBackupManager
    {
        public BackupIntervalManager IntervalManager { get; set; }

        public FileBackupManager()
        {
            IntervalManager = new BackupIntervalManager();
        }
    }

}
