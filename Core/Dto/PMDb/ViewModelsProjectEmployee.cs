using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class ViewModelsProjectEmployee
    {
        public List<ViewProjectGroupEmployeeModels> ViewProjectGroupEmployeeModelList { get; set; }
        public List<ViewModelProjectGroup> ViewModelProjectGroupList { get; set; }
    }


    public class ViewModelProjectGroup
    {
        public int ProjectGroupId { get; set; }
        public string ProjectGroupName { get; set; }
    }


    public class ViewProjectGroupEmployeeModels
    {
        public int EmployeeId { get; set; }
        public int ProjectGroupId { get; set; }
        public int ProjectId { get; set; }
        public string EmployeeName { get; set; }
        public string ProjectName { get; set; }
        public string ProjectGroupName { get; set; }



    }
}
