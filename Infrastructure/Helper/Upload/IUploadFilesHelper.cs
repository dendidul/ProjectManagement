using Microsoft.AspNetCore.Http;

namespace Infrastructure.Helper.Upload
{
    public interface IUploadFilesHelper
    {
        Task<string> Upload(IFormFile file, string filePath);
    }
}