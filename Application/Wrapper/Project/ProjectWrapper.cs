using Application.Repositories.Project;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Project
{
    public class ProjectWrapper : IProjectWrapper
    {
        private readonly IProjectDA _projectDA;

        public ProjectWrapper(IProjectDA projectDA)
        {
            _projectDA = projectDA;
        }


        public void CreateData(Core.Dto.PMDb.Project model)
        {
            _projectDA.CreateData(model);
        }
        public void Delete(Core.Dto.PMDb.Project model)
        {
            _projectDA.Delete(model);
        }
        public IEnumerable<ViewAllProject> GetAllData()
        {
            return _projectDA.GetAllData();
        }
        public ViewModelsProjectEmployee GetAllProjectByEmployeeId(int id)
        {
            return _projectDA.GetAllProjectByEmployeeId(id);
        }
        public IEnumerable<ViewProjectGroupEmployeeModels> GetAllProjectsByEmployeeId(int id)
        {
            return _projectDA.GetAllProjectsByEmployeeId(id);
        }
        public Core.Dto.PMDb.Project GetDataById(int id)
        {
            return _projectDA.GetDataById(id);
        }
        public void Update(Core.Dto.PMDb.Project model)
        {
            _projectDA.Update(model);
        }
    }
}
