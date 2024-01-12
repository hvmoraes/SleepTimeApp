using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTimeApp
{
    internal class StudyDay
    {
        public int ID { get; set; }
        public String Date { get; set; }
        public String Time { get; set; }
        public List<Studies> Studies { get; internal set; }
    }
}
