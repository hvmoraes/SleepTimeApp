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
        public String StartTime { get; set; }
        public String EndTime { get; set; }
        public String Summary { get; set; }
        public String Notes { get; set; }
        public int days_ID {  get; set; }
    }
}
