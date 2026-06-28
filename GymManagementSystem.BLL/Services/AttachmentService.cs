using GymManagementSystem.BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GymManagementSystem.BLL.Services
{
    
    public class AttachmentService : IAttachmentService
    {
        private readonly IWebHostEnvironment _env;

        public AttachmentService(IWebHostEnvironment env)
        {
            _env = env;
        }

      
        public string? UploadFile(IFormFile file, string folderName)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                return null;  

            var maxSizeInBytes = 5 * 1024 * 1024; 
            if (file.Length > maxSizeInBytes)
                return null;  

            var folderPath = Path.Combine(_env.WebRootPath, "images", folderName);
            Directory.CreateDirectory(folderPath); 

            var uniqueFileName = $"{Guid.NewGuid()}{extension}";

            var fullPath = Path.Combine(folderPath, uniqueFileName);
            using var stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);  

            return $"{folderName}/{uniqueFileName}";
        }

       
        public bool DeleteFile(string fileName, string folderName)
        {
            var fullPath = Path.Combine(_env.WebRootPath, "images", fileName);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }

            return false;
        }
    }
}
