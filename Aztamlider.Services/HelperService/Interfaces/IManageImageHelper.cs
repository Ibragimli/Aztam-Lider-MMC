using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.HelperService.Interfaces
{
    public interface IManageImageHelper
    {
        public void PosterCheck(IFormFile posterImageFile);
        public void ImagesCheck(List<IFormFile> Images);
        public string FileSave(IFormFile Image, string folderName);
        public void DeleteFile(string image, string folderName);
    }
}
