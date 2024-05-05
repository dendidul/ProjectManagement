using Core.Dto.PMDb;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Timesheet
{
    public class TimesheetDA : ITimesheetDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<TimesheetModel> GetTimesheet(int ProjectId, DateTime StartDate, DateTime DueDate, int EmpId)
        {
            //string query = @"select CONCAT(emp.FirstName, ' ', emp.LastName) as EmployeeName,
            //                tda.StartDate as StartDate,
            //                ta.TaskName as TaskName,
            //                p.ProjectsName as ProjectsName,
            //                s.SeverityName as SeverityName,
            //                ty.TypeName,
            //                tda.Estimate,
            //                tda.Description as Descripition

            //                from TaskDayActivity tda
            //                inner join Employee emp on tda.EmployeeId = emp.id
            //                inner join Task ta on ta.id = tda.TaskId
            //                inner join Projects p on ta.ProjectId = p.id
            //                left join Severity s on ta.SeverityId = s.id
            //                left join [Type] ty on ta.Type = ty.id 
            //                inner join Status st on ta.StatusId = st.id
            //                where
            //              --  (st.id = 4 or st.id = 5)
            //                st.id != 3
            //                 and emp.id = " + EmpId + @"
            //                                            and ta.ProjectId = " + ProjectId + @" 
            //                                            and ta.type != 3
            //                                            and
            //                                          tda.startdate between DATEADD(DD,-1,'" + StartDate + @"') and DATEADD(dd,1,'" + DueDate + @"')
            //                ";

            //var data = SQLQueryUtil.ExecuteCustomQuery<TimesheetModel>(query).ToList();

            var data = db.TimesheetModels.FromSqlInterpolated<TimesheetModel>($@"select CONCAT(emp.FirstName, ' ', emp.LastName) as EmployeeName,
                            tda.StartDate as StartDate,
                            ta.TaskName as TaskName,
                            p.ProjectsName as ProjectsName,
                            s.SeverityName as SeverityName,
                            ty.TypeName,
                            tda.Estimate,
                            tda.Description as Descripition

                            from TaskDayActivity tda
                            inner join Employee emp on tda.EmployeeId = emp.id
                            inner join Task ta on ta.id = tda.TaskId
                            inner join Projects p on ta.ProjectId = p.id
                            left join Severity s on ta.SeverityId = s.id
                            left join Type ty on ta.Type = ty.id 
                            inner join Status st on ta.StatusId = st.id
                            where
                          --  (st.id = 4 or st.id = 5)
                            st.id != 3
                             and emp.id = {EmpId}
                 and ta.ProjectId = {ProjectId } 
                                                        and ta.type != 3
                                                        and
                                                      tda.startdate between DATEADD(DD,-1,'{StartDate}') and DATEADD(dd,1,'{DueDate}')
                            ").Select(x=>x).ToList();
            return data;
        }
    }
}
