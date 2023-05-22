using BlazorShopDemo2.ServerApp.Services.IService;
using Microsoft.AspNetCore.Components.Forms;
using System.IO;

namespace BlazorShopDemo2.ServerApp.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploadService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public bool DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            FileInfo fileInfo = new(file.Name);

            var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
            var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\images\\product";

            if (!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }

            var filePath = Path.Combine(folderDirectory, fileName);

            await using FileStream fs = new(filePath, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(fs);

            return filePath;
        }
    }
}