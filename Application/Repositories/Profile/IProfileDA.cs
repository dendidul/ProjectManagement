using Core.Dto.PMDb;

namespace Application.Repositories.Profile
{
    public interface IProfileDA
    {
        ProfileModel GetProfileByEmployeeId(int id);
    }
}