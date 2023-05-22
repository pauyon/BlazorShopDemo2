using Microsoft.AspNetCore.Components.Forms;
using System.Net;

namespace BlazorShopDemo2.ServerApp.Services.IService
{
    public interface IFileUploadService
    {
        Task<string> UploadFile(IBrowserFile file);

        bool DeleteFile(string filePath);
    }
}