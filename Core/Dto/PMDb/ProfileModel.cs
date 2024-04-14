using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class ProfileModel
    {
        public ViewAllEmployee UserData { get; set; }
        public List<NewsFeedModels> ListRecentActivityModel { get; set; }
        public List<ViewRolesProjectEmployee> RolesProjectEmployee { get; set; }
    }
}
