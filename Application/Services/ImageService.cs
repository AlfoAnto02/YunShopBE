﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Model.Entities;
using Model.Repositories;

namespace Application.Services {
    public class ImageService : IImageService {
        private readonly ImageRepository _imageRepository;
        private string [] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        public ImageService(ImageRepository imageRepository) {
            _imageRepository = imageRepository;
        }

        public async Task AddAsync(Image entity) {
            _imageRepository.Add(entity);
            await _imageRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Image>> GetAllAsync() {
            return await _imageRepository.GetAllAsync();
        }

        public async Task<Image> GetAsync(int id)
        {
            var image = await _imageRepository.GetAsync(id);
            if (image == null)
            {
                throw new Exception("Image not found");
            }

            return image;
        }

        public async Task<IEnumerable<Image>> GetImagesByProductId(int productId)
        {
            return await _imageRepository.GetImagesByProductId(productId);
        }

        public async Task DeleteAsync(int id)
        {
            var image = await _imageRepository.GetAsync(id);
            if (image == null)
            {
                throw new Exception("Image not found");
            }

            _imageRepository.Delete(image);
            await _imageRepository.SaveChangesAsync();
        }

        public void CheckImages(IEnumerable<Image> images)
        {
            foreach (var image in images)
            {
                if (Uri.TryCreate(image.Url, UriKind.Absolute, out Uri uriResult) &&
                    (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    if (!allowedExtensions.Any(ext =>
                        uriResult.AbsolutePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
                    {
                        throw new Exception("Invalid image extension");
                    }
                }
                else
                {
                    throw new Exception("Invalid image URL");
                }
            }
        }
    }
}
