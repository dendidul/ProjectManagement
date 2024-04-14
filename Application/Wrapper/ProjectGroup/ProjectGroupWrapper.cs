using Application.Repositories.ProjectGroup;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.ProjectGroup
{
    public class ProjectGroupWrapper : IProjectGroupWrapper
    {
        private readonly IProjectGroupDA _projectGroupDA;

        public ProjectGroupWrapper(IProjectGroupDA projectGroupDA)
        {
            _projectGroupDA = projectGroupDA;
        }

        public void CreateData(Projectgroup model)
        {
            _projectGroupDA.CreateData(model);
        }
        public void Delete(Projectgroup model)
        {
            _projectGroupDA.Delete(model);
        }
        public IEnumerable<Projectgroup> GetAllData()
        {
            return _projectGroupDA.GetAllData();
        }
        public Projectgroup GetDataById(int id)
        {
            return _projectGroupDA.GetDataById(id);
        }
        public void Update(Projectgroup model)
        {
            _projectGroupDA.Update(model);
        }
    }
}
