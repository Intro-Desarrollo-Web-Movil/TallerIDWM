using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.Interfaces;
using TallerIDWM.src.Helpers;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace TallerIDWM.src.Services
{
    public class PhotoService : IPhotoService
    {

        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> configuration)
        {
            var account = new Account(
                configuration.Value.CloudName,
                configuration.Value.ApiKey,
                configuration.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }


        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {

                var allowedTypes = new List<string> { "image/jpeg", "image/png" };
                if (!allowedTypes.Contains(file.ContentType))
                {
                    throw new Exception("Tipo de archivo no permitido.");
                }

                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }


        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
            {
                throw new Exception("Id de imagen no encontrado.");
            }

            var deletionParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deletionParams);
            return result;
        }
        
    }
}