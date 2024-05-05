using Infrastructure.Helper.Config;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper.Upload
{
    public class UploadFilesHelper : IUploadFilesHelper
    {

        private readonly IHostingEnvironment _hostingEnvironment;
        private IConfigCreatorHelper _config;
        private readonly GoogleCredential _googleCredential;
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;
        private readonly string _pathUrl;
        // private readonly string _appName;
        // private readonly string _environment;
        private readonly string _sourceUploadFile;


        public UploadFilesHelper(IHostingEnvironment hostingEnvironment, IConfigCreatorHelper config)
        {
            _hostingEnvironment = hostingEnvironment;
            _config = config;
            _googleCredential = GoogleCredential.FromFile(_config.Get("GoogleStorage:GoogleCredentialFile"));
            _storageClient = StorageClient.Create(_googleCredential);
            _bucketName = _config.Get("GoogleStorage:GoogleCloudStorageBucket");
            _pathUrl = _config.Get("GoogleStorage:GoogleCloudStorageUrl");
            // _appName = _config.Get("GoogleStorage:AppName");
            // _environment = _config.Get("DefaultConnection:Environment.Info");
            _sourceUploadFile = _config.Get("SourceUploadFile");
        }

        public async Task<string> Upload(IFormFile file, string filePath)
        {
            if (_sourceUploadFile.ToUpper() == "GCS")
            {
                if (CheckIfImageFile(file))
                {
                    //var pathName = string.Format("{0}/{1}", _appName, _environment);
                    // var pathName = string.Format("{0}/{1}");
                   // var filename = Guid.NewGuid().ToString();
                    var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

                    var fileName = Guid.NewGuid().ToString() + extension;

                    if (filePath.Substring(0, 1) == "/") filePath = filePath.Remove(0, 1);

                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);

                       // var dataObject = await _storageClient.UploadObjectAsync(_bucketName, string.Format("{0}/{1}{2}", pathName, filePath, fileName), null, memoryStream);

                        var dataObject = await _storageClient.UploadObjectAsync(_bucketName, fileName, "image/"+extension.Replace(".",""), memoryStream);


                        return string.Format("{0}/{1}/{2}", _pathUrl, _bucketName, dataObject.Name);
                    }
                }
                else
                {
                    var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

                    var fileName = Guid.NewGuid().ToString() + extension;

                    if (filePath.Substring(0, 1) == "/") filePath = filePath.Remove(0, 1);

                   

                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);

                        // var dataObject = await _storageClient.UploadObjectAsync(_bucketName, string.Format("{0}/{1}{2}", pathName, filePath, fileName), null, memoryStream);

                        var checkmime = MimeMapping.GetMimeMapping(fileName);

                        var dataObject = await _storageClient.UploadObjectAsync(_bucketName, fileName, checkmime, memoryStream);


                        return string.Format("{0}/{1}/{2}", _pathUrl, _bucketName, dataObject.Name);
                    }
                }
            }
            else
            {
                var imagePath = string.Concat(_hostingEnvironment.WebRootPath, filePath);
                if (!Directory.Exists(imagePath))
                    Directory.CreateDirectory(imagePath);

                if (CheckIfImageFile(file))
                {
                    return await WriteFile(file, filePath);
                }
            }

            return "400";
        }



        private async Task<string> WriteFile(IFormFile file, string filePath)
        {
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = Guid.NewGuid().ToString() + extension; //Create a new Name 
                                                                  //for the file due to security reasons.

                var imagePath = string.Concat(_hostingEnvironment.WebRootPath, filePath);
                if (!Directory.Exists(imagePath))
                    Directory.CreateDirectory(imagePath);

                var path = Path.Combine(imagePath, fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return fileName;
        }

        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return GetImageFormat(fileBytes) != ImageFormat.unknown;
        }

        enum ImageFormat
        {
            bmp,
            jpeg,
            jpg,
            gif,
            tiff,
            png,
            unknown
        }

        private ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };              // PNG
            var tiff = new byte[] { 73, 73, 42 };                  // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };                 // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 };          // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };         // jpeg canon
            var jpg = new byte[] { 255, 216, 255 };          // jpg

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            if (jpg.SequenceEqual(bytes.Take(jpg.Length)))
                return ImageFormat.jpg;

            return ImageFormat.unknown;
        }
    }
}
