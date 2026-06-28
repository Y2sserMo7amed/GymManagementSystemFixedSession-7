using Microsoft.AspNetCore.Http;
namespace GymManagementSystem.BLL.Interfaces
{
    
    public interface IAttachmentService
    {
      
        string? UploadFile(IFormFile file, string folderName);

        
        bool DeleteFile(string fileName, string folderName);
    }
}
