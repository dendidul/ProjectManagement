using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class FormTaskModel
    {
        public Task TaskData { get; set; }
        public List<Attachmentfile> AttachmentList { get; set; }

        public List<Taskdayactivity> TaskDayActivity { get; set; }

        public List<TaskDayActivities> TaskDayActivties { get; set; }
    }
}
