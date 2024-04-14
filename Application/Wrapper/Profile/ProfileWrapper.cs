using Application.Repositories.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dto.PMDb;

namespace Application.Wrapper.Profile
{
    public class ProfileWrapper : IProfileWrapper
    {
        private readonly IProfileDA _profileDA;

        public ProfileWrapper(IProfileDA profileDA)
        {
            _profileDA = profileDA;
        }

        public ProfileModel GetProfileByEmployeeId(int id)
        {
            return _profileDA.GetProfileByEmployeeId(id);
        }
    }
}
