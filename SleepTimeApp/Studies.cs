using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTimeApp
{
    internal class Studies
    {
        public int ID { get; set; }
        public String TotalTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public String Notes { get; set; }
        public String Summary { get; set; }
    }
}
