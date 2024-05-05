using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public  class ViewAllEmployee
    {
        public int id { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public Nullable<int> Is_Active { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
    }
}
