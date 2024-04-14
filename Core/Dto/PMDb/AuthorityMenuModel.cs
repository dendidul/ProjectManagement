using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public  class AuthorityMenuModel
    {
        public List<Menu> MenuList { get; set; }
        public List<Menu> SelectedMenuList { get; set; }
        public Menu Menu { get; set; }
        public Role Role { get; set; }
        public List<Rolesmenu> RolesMenuList { get; set; }
    }
}
