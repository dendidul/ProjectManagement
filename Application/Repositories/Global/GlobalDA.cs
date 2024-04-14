using Core.Dto.PMDb;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Global
{
    public class GlobalDA : IGlobalDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public decimal GetProjectProgress(int id)
        {
            //           string query = @"select 
            //                    ((select count(*) as count from Task where StatusId = 5 and del_flag = 0 and ProjectId = "+id+@")
            //	                    /
            //	                    (select count(*) as count from Task where del_flag = 0 and ProjectId = " + id + @")* 100
            //                    )
            //                            ";
            //           var data = SQLQueryUtil.ExecuteCustomQuery<int>(query).FirstOrDefault();
            var GetCompleteData = db.Tasks.Where(x => x.DelFlag == false && x.Projectid == id && x.Statusid == 5).Count();
            var GetAllData = db.Tasks.Where(x => x.DelFlag == false && x.Projectid == id).Count();

            if (GetAllData == 0)
            {
                return 0;
                //var data = ((GetCompleteData * 1.0) / (GetAllData * 1.0)) * 100;
                ////var data = Convert.ToDecimal(((Convert.ToInt32(GetCompleteData)*1) / (Convert.ToInt32(GetAllData)*1)) * 100);

                //return Convert.ToDecimal(data);
            }
            else
            {
                var data = ((GetCompleteData * 1.0) / (GetAllData * 1.0)) * 100;
                //var data = Convert.ToDecimal(((Convert.ToInt32(GetCompleteData)*1) / (Convert.ToInt32(GetAllData)*1)) * 100);

                return Convert.ToDecimal(data);
            }


        }

        public ProgressBarModel ProgressTaskByProject(int id)
        {
            var GetCompleteData = db.Tasks.Where(x => x.DelFlag == false && x.Projectid == id && x.Statusid == 5 && x.Type == 1).Count();
            var GetNotCompleteData = db.Tasks.Where(x => x.DelFlag == false && x.Projectid == id && x.Type == 1 && x.Statusid != 5).Count();

            var data = new ProgressBarModel()
            {
                Completed = GetCompleteData,
                NotCompleted = GetNotCompleteData
            };

            return data;
        }


        public ProgressBarModel ProgressBugsByProject(int id)
        {
            var GetCompleteData = db.Tasks.Where(x => x.DelFlag == false && x.Projectid == id && x.Statusid == 5 && x.Type == 2).Count();
            var GetNotCompleteData = db.Tasks.Where(x => x.DelFlag == false && x.Projectid == id && x.Type == 2 && x.Statusid != 5).Count();

            var data = new ProgressBarModel()
            {
                Completed = GetCompleteData,
                NotCompleted = GetNotCompleteData
            };

            return data;
        }
    }
}
