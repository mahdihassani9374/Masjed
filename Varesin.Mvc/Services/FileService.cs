using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using Varesin.Utility;

namespace Varesin.Mvc.Services
{
    public class FileService
    {
        private readonly IHostingEnvironment _env;
        public FileService(IHostingEnvironment env)
        {
            _env = env;
        }
        public ServiceResult<string> Upload(IFormFile file, string folderName, long? maxLength)
        {
            var serviceResult = new ServiceResult<string>(true);

            if (file == null)
                serviceResult.AddError("فایلی انتخاب نشده است");
            else
            {
                if (file.Length > maxLength)
                    serviceResult.AddError("حجم فایل انتخابی بزرگ می باشد");
                else
                {
                    var extension = System.IO.Path.GetExtension(file.FileName);
                    var fileName = $"{Guid.NewGuid()}{extension}";

                    var path = System.IO.Path.Combine(_env.WebRootPath, "Files", folderName, fileName);

                    var fileStream = new System.IO.FileStream(path,
                        System.IO.FileMode.Create);

                    file.CopyTo(fileStream);

                    fileStream.Close();

                    serviceResult.Data = fileName;
                }
            }

            return serviceResult;
        }

        public ServiceResult Delete(string fileName, string folderName)
        {
            var serviceResult = new ServiceResult(true);

            var path = System.IO.Path.Combine(_env.WebRootPath, "Files", folderName, fileName);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            else serviceResult.AddError("فایل وجود ندارد");

            return serviceResult;

        }
    }
}
