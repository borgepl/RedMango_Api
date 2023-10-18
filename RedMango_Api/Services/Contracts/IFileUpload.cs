using Microsoft.AspNetCore.Components.Forms;

namespace RedMango_Api.Services.Contracts
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IFormFile file);

        bool DeleteFile(string fileName);
    }
}
