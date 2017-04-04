using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupManager
{
    public class Interval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public bool IsOverriding(Interval interval2)
        {
            if (this.Start <= interval2.Start &&
                this.End > interval2.Start)
            {
                return true;
            }

            if (interval2.Start <= this.Start &&
                interval2.End > this.Start)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{Start} {End}";
        }
    }
}
