using System.Threading.Tasks;
using CarRentalRestApi.Models.Responses;
using Microsoft.AspNetCore.Http;

namespace CarRentalRestApi.Services.FilesService
{
    public interface IFileService
    {
        Task<string> UploadVehiclePhoto(int vehicleId, IFormFile image);
        Task<(byte[], string)> GetVehicleImage(int vehicleId);

    }
}