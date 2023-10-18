using RedMango_Api.Services.Contracts;
using Microsoft.AspNetCore.Components.Forms;

namespace RedMango_Api.Services
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public FileUpload(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\redmango\\{fileName}";
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            try
            {
                //FileInfo fileInfo = new FileInfo(file.Name);
                var extension = Path.GetExtension(file.FileName);
                var fileName = Guid.NewGuid().ToString() + extension;
                var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\redmango";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "redmango", fileName);

                var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);

                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.WriteTo(fs);
                }
                var url = $"{_configuration.GetValue<string>("ServerUrl")}";
                var fullPath = $"{url}redmango/{fileName}";
                return fullPath;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
