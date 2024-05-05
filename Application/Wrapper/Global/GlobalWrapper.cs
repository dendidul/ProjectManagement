using Application.Repositories.Global;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Global
{
    public class GlobalWrapper : IGlobalWrapper
    {
        private readonly IGlobalDA _globalDA;

        public GlobalWrapper(IGlobalDA globalDA)
        {
            _globalDA = globalDA;
        }

        public decimal GetProjectProgress(int id)
        {
            return _globalDA.GetProjectProgress(id);
        }
        public ProgressBarModel ProgressBugsByProject(int id)
        {
            return _globalDA.ProgressBugsByProject(id);
        }
        public ProgressBarModel ProgressTaskByProject(int id)
        {
            return _globalDA.ProgressTaskByProject(id);
        }
    }
}
