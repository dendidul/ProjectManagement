using Core.Dto.PMDb;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.RolesMenu
{
    public class RolesMenuDA : IRolesMenuDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();
        public IEnumerable<Rolesmenu> GetAllData()
        {
            var data = db.Rolesmenus.ToList();
            return data;
        }

        public ViewModelsProjectEmployee GetAllProjectByEmployeeId(int id)
        {
            var dataquery = "";



            var dataprojectGroup = new List<ViewModelProjectGroup>();

            if (id == 100)
            {

                dataprojectGroup = (from a in db.Projectgroups
                                    where a.DelFlag == false 
                                    select new ViewModelProjectGroup
                                        {
                                            ProjectGroupId = a.Id,
                                            ProjectGroupName = a.Projectgroupname
                                        }
                                    ).ToList();

        //        dataprojectGroup = db.ViewModelProjectGroup.FromSqlInterpolated<ViewModelProjectGroup>($@"
        //                    select 
        //0 as EmployeeId,
        //'' as EmployeeName,
        //                        pg.Id as ProjectGroupId,
        //                        p.id as ProjectId,
        //                        p.ProjectsName as ProjectName,
        //                        pg.ProjectGroupName as ProjectGroupName

                //                         from RolesProjectEmployee rpe
                //                        inner join Projects p on rpe.ProjectID = p.id

                //                        inner join ProjectGroup pg on p.ProjectGroupId = pg.Id

                //                        group by p.id, pg.Id,p.ProjectsName,pg.ProjectGroupName




                //                    ").Select(x => x).ToList();
            }
            else
            {
             

                dataprojectGroup = db.ViewModelProjectGroup.FromSqlInterpolated<ViewModelProjectGroup>($@"
                    select 
                            pg.Id as ProjectGroupId, pg.ProjectGroupName as ProjectGroupName,rpe.empId as EmployeeId,
								concat(emp.firstname, ' ',emp.lastname) as EmployeeName,
                                 p.id as ProjectId,
                                p.ProjectsName as ProjectName

                            from RolesProjectEmployee rpe
                            inner join Projects p on rpe.ProjectID = p.id
                            inner join Employee emp on rpe.EmpID = emp.id
                            inner join ProjectGroup pg on p.ProjectGroupId = pg.id 
                            where EmpID ={id}
                            and rpe.del_flag is false
                            and p.del_flag is false 
                            and pg.del_flag is false
                            group by  pg.Id, pg.ProjectGroupName,
                            rpe.empId,concat(emp.firstname, ' ',emp.lastname),
                                p.id ,
                                p.ProjectsName 
                ").Select(x => x).ToList();
            }

            //var dataprojectGroup = SQLQueryUtil.ExecuteCustomQuery<ViewModelProjectGroup>(dataquery).ToList();


            List<ViewModelProjectGroup> ListProjectGroup = new List<ViewModelProjectGroup>();
            foreach (var i in dataprojectGroup)
            {
                ViewModelProjectGroup model = new ViewModelProjectGroup();
                model.ProjectGroupId = i.ProjectGroupId;
                model.ProjectGroupName = i.ProjectGroupName;
                ListProjectGroup.Add(model);


            }

            ViewModelProjectGroup ViewModelProjectGroup = new ViewModelProjectGroup();


            var data = new List<ViewProjectGroupEmployeeModels>();

            if (id == 100)
            {

                //data = (from a in db.Projectgroups
                //                    where a.DelFlag == false
                //                    select new ViewProjectGroupEmployeeModels
                //                    {
                //                        ProjectGroupId = a.Id,
                //                        ProjectGroupName = a.Projectgroupname
                //                    }
                //                   ).ToList();

                data = db.ViewProjectGroupEmployeeModels.FromSqlInterpolated<ViewProjectGroupEmployeeModels>($@"
                       select 
                0 as EmployeeId,
                '' as EmployeeName,
                                        pg.Id as ProjectGroupId,
                                        p.id as ProjectId,
                                        p.ProjectsName as ProjectName,
                                        pg.ProjectGroupName as ProjectGroupName

                                         from RolesProjectEmployee rpe
                                        inner join Projects p on rpe.ProjectID = p.id

                                        inner join ProjectGroup pg on p.ProjectGroupId = pg.Id

                                        group by p.id, pg.Id,p.ProjectsName,pg.ProjectGroupName
                        ").Select(x => x).ToList();

            }
            else
            {
                data = (from a in db.Rolesprojectemployees
                        join c in db.Projects on a.Projectid equals c.Id
                        join d in db.Employees on a.Empid equals d.Id
                        join b in db.Projectgroups on c.Projectgroupid equals b.Id

                        where d.Id == id
                        && a.DelFlag == false
                        && c.DelFlag == false
                        && d.DelFlag == false
                        && b.DelFlag == false
                        // group c by c.id into g
                        select new ViewProjectGroupEmployeeModels
                        {
                            EmployeeId = d.Id,
                            ProjectGroupId = b.Id,
                            ProjectId = c.Id,
                            EmployeeName = d.Firstname + " " + d.Lastname,
                            ProjectGroupName = b.Projectgroupname,
                            ProjectName = c.Projectsname
                        }).ToList();
            }

            var getdata = new ViewModelsProjectEmployee
            {
                ViewProjectGroupEmployeeModelList = data,
                ViewModelProjectGroupList = ListProjectGroup

            };

            return getdata;
        }


        public AuthorityMenuModel ReadRoleMenuByRoleID(int roleID)
        {
            var roleMenu = (from x in db.Rolesmenus where x.Rolesid == roleID select x).ToList();
            var selectedMenu = (from x in db.Menus where (from m in db.Rolesmenus where m.Rolesid == roleID select m.Menuid).Contains(x.Id) select x).ToList();
            var menuList = db.Menus.ToList();
            var role = db.Roles.Where(c => c.Id == roleID).FirstOrDefault();
            var data = new AuthorityMenuModel()
            {
                //managerolelist = roleMenu,
                //selectedList = selectedMenu,
                //menuList = menuList,
                //rolename = role.name,
                //Roles = role

                RolesMenuList = roleMenu,
                SelectedMenuList = selectedMenu,
                MenuList = menuList,
                Role = role

            };
            return data;
        }

        public Rolesmenu GetDataById(int id)
        {
            var data = db.Rolesmenus.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Rolesmenu model)
        {
            db.Rolesmenus.Add(model);
            db.SaveChanges();
        }

        public void Update(Rolesmenu model)
        {

            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Rolesmenu model)
        {
            Rolesmenu item = db.Rolesmenus.Find(model.Id);
            db.Rolesmenus.Remove(item);
            //item.del_flag = true;
            //db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<RolesMenuViewModel> BuildRoleMenu(int roleid)
        {

            //var data = System.Web.HttpContext.Current.Session["Session_RoleID"];
            //var RolesID = Convert.ToInt32(data);
            var datamenu = (from mr in db.Rolesmenus
                            join r in db.Roles on mr.Rolesid equals r.Id
                            join m in db.Menus on mr.Menuid equals m.Id
                            where mr.Rolesid == roleid
                            orderby m.Sequence
                            select new RolesMenuViewModel
                            {
                                RolesMenuModel = mr,
                                MenuModel = m,
                                RolesModel = r
                            }).OrderBy(x => x.MenuModel.Sequence);

            IList<RolesMenuViewModel> mmlist = datamenu.ToList();

            return mmlist;
        }


        public bool CheckMenuForRoles(int RolesID, string Controller)
        {
            var dataCount = (from mr in db.Rolesmenus
                             join r in db.Roles on mr.Rolesid equals r.Id
                             join m in db.Menus on mr.Menuid equals m.Id
                             where mr.Rolesid == RolesID && m.Controllername.ToLower() == Controller.ToLower()
                             select new RolesMenuViewModel
                             {
                                 RolesMenuModel = mr,
                                 MenuModel = m,
                                 RolesModel = r
                             }).Any();
            return dataCount;

        }


        public IEnumerable<Rolesmenu> GetRoleMenuByRolesId(int id)
        {
            var data = db.Rolesmenus.Where(x => x.Rolesid == id).ToList();
            return data;
        }
    }
}
