using System.Collections.Generic;

namespace Obd2Net
{
    public sealed class Status
    {
        public Status()
        {
            Tests = new List<Test>();
        }

        public bool MIL { get; set; }
        public int DTCCount { get; set; }
        public string IgnitionType { get; set; }
        public List<Test> Tests { get; set; }
    }
}
