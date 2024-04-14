using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class EventViewModel
    {

        public int id { get; set; }
        public string title { get; set; }
        public DateTime? date { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public string url { get; set; }
        public string EmployeeName { get; set; }
        public string ProjectName { get; set; }

        public bool allDay { get; set; }
        public string Description { get; set; }
        //public string workgroup { get; set; }

    }


    public class CalendarViewModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string url { get; set; }
        public string EmployeeName { get; set; }
        public string ProjectName { get; set; }

        public bool allDay { get; set; }
        public string Description { get; set; }
    }
}
