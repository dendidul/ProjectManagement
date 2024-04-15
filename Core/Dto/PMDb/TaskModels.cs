using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class TaskModels
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string? Severity { get; set; }

        public string ProjectName { get; set; }

        public string AssignedTo { get; set; }

        public string? StatusName { get; set; }
        public int Type { get; set; }
        public int? TaskGroupId { get; set; }
        public int ProjectId { get; set; }
        public string? TaskGroup { get; set; }
        public string? TaskName { get; set; }
        public string? TypeName { get; set; }
        public string ReviewBy { get; set; }
    }
}
