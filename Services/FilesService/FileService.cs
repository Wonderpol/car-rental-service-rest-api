using System;
using System.IO;
using System.Threading.Tasks;
using CarRentalRestApi.Models.Responses;
using Microsoft.AspNetCore.Http;

namespace CarRentalRestApi.Services.FilesService
{
    public class FileService: IFileService
    {
        private readonly string _rootFolderPath = Directory.GetCurrentDirectory();
        
        public async Task<string> UploadVehiclePhoto(int vehicleId, IFormFile image)
        {
            if (image.ContentType != "image/jpeg")
            {
                throw new Exception("Wrong file format");
            }

            var imagesFolderPath = $"{_rootFolderPath}/VehicleImages";

            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }
            
            var vehicleFilePath =
                $"{imagesFolderPath}/{vehicleId.GetHashCode()}";

            var fileStream = new FileStream(vehicleFilePath, FileMode.CreateNew);

            await image.CopyToAsync(fileStream);
            
            fileStream.Close();

            return vehicleFilePath;
            
        }

        public async Task<(byte[], string)> GetVehicleImage(int vehicleId)
        {
            var imagesFolderPath = $"{_rootFolderPath}/VehicleImages";

            var vehicleFilePath =
                $"{imagesFolderPath}/{vehicleId.GetHashCode()}";

            var image = await File.ReadAllBytesAsync(vehicleFilePath);
            return (image, "image/jpeg");
        }

    }
}