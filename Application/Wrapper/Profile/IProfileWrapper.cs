using Core.Dto.PMDb;

namespace Application.Wrapper.Profile
{
    public interface IProfileWrapper
    {
        ProfileModel GetProfileByEmployeeId(int id);
    }
}