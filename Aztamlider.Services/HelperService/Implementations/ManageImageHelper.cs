﻿using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.HelperService.Implementations
{
    public class ManageImageHelper : IManageImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly IImageValue _key;

        public ManageImageHelper(IWebHostEnvironment env, IImageValue key)
        {
            _env = env;
            _key = key;
        }
        public void PosterCheck(IFormFile PosterImageFile)
        {
            if (PosterImageFile.ContentType != _key.ValueStr("ImageType1") && PosterImageFile.ContentType != _key.ValueStr("ImageType2") && PosterImageFile.ContentType != _key.ValueStr("ImageType3") && PosterImageFile.ContentType != _key.ValueStr("ImageType5"))
                throw new ImageFormatException("Şəkil yalnız (png, jpg, mp4 və ya webp) type-ında ola bilər");

            if (PosterImageFile.Length > _key.ValueInt("ImageSize") * 1048576)
                throw new ImageFormatException("Şəklin max yaddaşı" + _key.ValueInt("ImageSize") + "MB ola bilər!");
        }
        public void ImagesCheck(List<IFormFile> Images)
        {
            if (Images != null)
            {
                if (Images.Count > 8)
                    throw new ImageCountException("Maksimum 8 şəkil əlavə edə bilərsiniz");

                foreach (var image in Images)
                {
                    if (image.ContentType != _key.ValueStr("ImageType1") && image.ContentType != _key.ValueStr("ImageType2") && image.ContentType != _key.ValueStr("ImageType3") && image.ContentType != _key.ValueStr("ImageType5"))
                        throw new ImageFormatException("Şəkil yalnız (png, jpg, mp4 və ya webp)  type-ında ola bilər");
                    if (image.Length > _key.ValueInt("ImageSize") * 1048576)
                        throw new ImageFormatException("Şəklin max yaddaşı " + _key.ValueInt("ImageSize") + "MB ola bilər!");
                }
            }

        }
        public string FileSave(IFormFile Image, string folderName)
        {
            string image = FileManager.Save(_env.WebRootPath, "uploads/" + folderName, Image);
            return image;
        }
        public void DeleteFile(string image, string folderName)
        {
            FileManager.Delete(_env.WebRootPath, "uploads/" + folderName, image);
        }
    }
}
