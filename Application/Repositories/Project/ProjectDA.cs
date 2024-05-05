using Core.Dto.PMDb;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Project
{
    public class ProjectDA : IProjectDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();
        //   SQLQueryUtil SQLQueryUtil = new SQLQueryUtil();
        public IEnumerable<ViewAllProject> GetAllData()
        {
            //var data = db.ViewAllProjects.Where(x => x.del_flag == false).ToList();

           var data = db.ViewAllProjects.FromSqlInterpolated<ViewAllProject>($@" 
                             SELECT projectgroupname as ProjectGroupName, 
		                        projectgroupid as ProjectGroupId, 
		                        projectname as ProjectName,
		                        ispublic as IsPublic,
		                        projectid as ProjectId, 
		                        del_flag
                                FROM viewallprojects where del_flag is false;
                            
                        ").Select(x => x).ToList();

           


            return data;
        }

        public ViewModelsProjectEmployee GetAllProjectByEmployeeId(int id)
        {
            var dataquery = "";



            var dataprojectGroup = new List<ViewModelProjectGroup>();

            if (id == 100)
            {

                dataprojectGroup = db.ViewModelProjectGroup.FromSqlInterpolated<ViewModelProjectGroup>($@"
                            select 
								rpe.empId as EmployeeId,
								concat(emp.firstname, ' ',emp.lastname) as EmployeeName,
                                pg.Id as ProjectGroupId,
                                p.id as ProjectId,
                                p.ProjectsName as ProjectName,
                                pg.ProjectGroupName as ProjectGroupName

                                 from RolesProjectEmployee rpe
                                inner join Projects p on rpe.ProjectID = p.id
                                inner join Employee emp on rpe.EmpID = emp.id
                                inner join ProjectGroup pg on p.ProjectGroupId = pg.Id

                                group by p.id, pg.Id,p.ProjectsName,pg.ProjectGroupName,
                                rpe.empId,concat(emp.firstname, ' ',emp.lastname) 



                            ").Select(x => x).ToList();
            }
            else
            {


                dataprojectGroup = db.ViewModelProjectGroup.FromSqlInterpolated<ViewModelProjectGroup>($@"
                    select 
                            pg.Id as ProjectGroupId, pg.ProjectGroupName as ProjectGroupName,rpe.empId as EmployeeId,
								concat(emp.firstname, ' ',emp.lastname) as EmployeeName,
                                 p.id as ProjectId,
                                p.ProjectsName as ProjectName,

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
                                p.id as ProjectId,
                                p.ProjectsName as ProjectName
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


                data = db.ViewProjectGroupEmployeeModels.FromSqlInterpolated<ViewProjectGroupEmployeeModels>($@"
               select 
								rpe.empId as EmployeeId,
								concat(emp.firstname, ' ',emp.lastname) as EmployeeName,
                                pg.Id as ProjectGroupId,
                                p.id as ProjectId,
                                p.ProjectsName as ProjectName,
                                pg.ProjectGroupName as ProjectGroupName

                                 from RolesProjectEmployee rpe
                                inner join Projects p on rpe.ProjectID = p.id
                                inner join Employee emp on rpe.EmpID = emp.id
                                inner join ProjectGroup pg on p.ProjectGroupId = pg.Id

                                group by p.id, pg.Id,p.ProjectsName,pg.ProjectGroupName,
                                rpe.empId,concat(emp.firstname, ' ',emp.lastname)
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


        public IEnumerable<ViewProjectGroupEmployeeModels> GetAllProjectsByEmployeeId(int id)
        {
            var data = (from a in db.Rolesprojectemployees
                        join c in db.Projects on a.Projectid equals c.Id
                        join d in db.Employees on a.Empid equals d.Id
                        join b in db.Projectgroups on c.Projectgroupid equals b.Id
                        where d.Id == id
                        && a.DelFlag == false
                        && c.DelFlag == false
                        && d.DelFlag == false
                        && b.DelFlag == false
                        select new ViewProjectGroupEmployeeModels
                        {
                            EmployeeId = d.Id,
                            ProjectGroupId = b.Id,
                            ProjectId = c.Id,
                            EmployeeName = d.Firstname + " " + d.Lastname,
                            ProjectGroupName = b.Projectgroupname,
                            ProjectName = c.Projectsname
                        }).ToList();
            return data;
        }

        public Core.Dto.PMDb.Project GetDataById(int id)
        {
            var data = db.Projects.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Core.Dto.PMDb.Project model)
        {
            model.DelFlag = false;
            db.Projects.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Project model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Project model)
        {
            Core.Dto.PMDb.Project item = db.Projects.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
