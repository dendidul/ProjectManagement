using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class ProgressBarModel
    {
        public int Completed { get; set; }
        public int NotCompleted { get; set; }

        public string ColorCompleted { get; set; }
        public string ColorNotCompleted { get; set; }
    }
}
