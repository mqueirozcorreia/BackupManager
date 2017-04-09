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
            ItemList = new List<BackupIntervalDefinition>();
        }

        public List<BackupIntervalDefinition> ItemList { get; set; }

        public bool IsOverriding()
        {
            for(int i=0; i<ItemList.Count;i++)
            {
                for (int j = i + 1; j < ItemList.Count; j++)
                {
                    if (ItemList[i].IsOverriding(ItemList[j]))
                        return true;
                }
            }

            return false;
        }
    }
}
