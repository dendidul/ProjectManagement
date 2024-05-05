using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class UserProjectModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int EmployeeId { get; set; }
        public string Email { get; set; }
        public string EmployeeName { get; set; }
        public string RolesName { get; set; }
        public string PhotoUrl { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
    }
}
