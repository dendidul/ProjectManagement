using Core.Dto.PMDb;

namespace Application.Wrapper.RolesMenu
{
    public interface IRolesMenuWrapper
    {
        IList<RolesMenuViewModel> BuildRoleMenu(int roleid);
        bool CheckMenuForRoles(int RolesID, string Controller);
        void CreateData(Rolesmenu model);
        void Delete(Rolesmenu model);
        IEnumerable<Rolesmenu> GetAllData();
        Rolesmenu GetDataById(int id);
        IEnumerable<Rolesmenu> GetRoleMenuByRolesId(int id);
        AuthorityMenuModel ReadRoleMenuByRoleID(int roleID);
        void Update(Rolesmenu model);
        ViewModelsProjectEmployee GetAllProjectByEmployeeId(int id);
    }
}