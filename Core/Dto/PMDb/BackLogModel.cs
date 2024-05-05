using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class BackLogModel
    {
        public Taskgroup TaskGroupModel { get; set; }
        public List<TaskModels> ListTask { get; set; }
    }
}
