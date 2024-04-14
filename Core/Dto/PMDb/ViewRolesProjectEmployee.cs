using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class ViewRolesProjectEmployee
    {
        public int id { get; set; }
        public string EmployeeName { get; set; }
        public string RolesName { get; set; }
        public string ProjectsName { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public int EmployeeId { get; set; }
        public int RolesId { get; set; }
    }
}
