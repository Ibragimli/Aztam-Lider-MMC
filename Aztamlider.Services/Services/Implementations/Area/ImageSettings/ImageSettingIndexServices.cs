using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.ImageSettings;

namespace Aztamlider.Services.Services.Implementations.Area.ImageSettings
{
    public class ImageSettingIndexServices : IImageSettingIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ImageSettingIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<ImageSetting> SearchCheck(string search)
        {
            var ImageSettingLast = _unitOfWork.ImageSettingRepository.asQueryable();
            if (search != null)
            {
                search = search.ToLower();
                if (search != null)
                    ImageSettingLast = ImageSettingLast.Where(i => EF.Functions.Like(i.Key, $"%{search}%"));
            }
            return ImageSettingLast;
        }

    }
}
