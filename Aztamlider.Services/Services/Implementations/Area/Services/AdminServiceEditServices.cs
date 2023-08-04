using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Services
{
    public class AdminServiceEditServices : IAdminServiceEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminServiceEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }

        public async Task EditService(Service Service)
        {
            bool checkBool = false;

            var oldService = await GetService(Service.Id);
            if (oldService == null)
                throw new ItemNullException("Layihə tapılmadı!");


            Check(Service);
            if (Service.ServiceImageFile != null)
                _manageImageHelper.PosterCheck(Service.ServiceImageFile);

            if (Service.ImageFiles != null)
                _manageImageHelper.ImagesCheck(Service.ImageFiles);

            int deleteCount = DeleteImages(Service, oldService);
            if (deleteCount > 0)
                checkBool = true;

            if (PosterImageChange(Service, oldService) == 1)
                checkBool = true;

            if (await CreateImageFormFile(Service.ImageFiles, Service.Id, deleteCount) == 1)
                checkBool = true;

            if (oldService.TitleAz != Service.TitleAz)
            {
                oldService.TitleAz = Service.TitleAz;
                checkBool = true;

            }
            if (oldService.TitleEn != Service.TitleEn)
            {
                oldService.TitleEn = Service.TitleEn;
                checkBool = true;

            }
            if (oldService?.DescriptionAz != Service?.DescriptionAz)
            {
                oldService.DescriptionAz = Service.DescriptionAz;
                checkBool = true;
            }
            if (oldService?.DescriptionEn != Service?.DescriptionEn)
            {
                oldService.DescriptionEn = Service.DescriptionEn;
                checkBool = true;
            }
            oldService.ModifiedDate = DateTime.UtcNow.AddHours(4);
            if (checkBool)
                await _unitOfWork.CommitAsync();
        }

        public async Task<Service> GetService(int id)
        {
            var Service = await _unitOfWork.ServiceRepository.GetAsync(x => x.Id == id, "ServiceImages");
            return Service;
        }

        public async Task<IEnumerable<ServiceImage>> GetImages(int id)
        {
            var images = await _unitOfWork.ServiceImageRepository.GetAllAsync(x => x.ServiceId == id);
            return images;
        }
        private int PosterImageChange(Service Service, Service projectExist)
        {
            if (Service.ServiceImageFile != null)
            {
                var posterImageFile = Service.ServiceImageFile;

                ServiceImage posterImage = projectExist.ServiceImages.FirstOrDefault(x => x.IsPoster);

                if (posterImage == null) throw new ImageNullException("Şəkil tapılmadı!");

                string filename = _manageImageHelper.FileSave(posterImageFile, "Services");
                _manageImageHelper.DeleteFile(posterImage.Image, "Services");
                posterImage.Image = filename;
                posterImage.IsPoster = true;
                return 1;
            }
            return 0;

        }
        private async Task<int> CreateImageFormFile(List<IFormFile> imageFiles, int posterId, int deleteCount)
        {
            int countImage = await _unitOfWork.ServiceImageRepository.GetTotalCountAsync(x => x.ServiceId == posterId && !x.IsPoster);
            int i = 0;

            if (countImage < 9)
            {
                if (imageFiles != null)
                {
                    i = 8 - countImage - deleteCount;
                    if (i == 0)
                        throw new ImageCountException("Maksimum 8 şəkil əlavə edə bilərsiniz!");
                    foreach (var image in imageFiles)
                    {
                        if (i != 0)
                        {
                            ServiceImage Posterimage = new ServiceImage
                            {
                                IsPoster = false,
                                ServiceId = posterId,
                                Image = _manageImageHelper.FileSave(image, "Services"),
                            };
                            await _unitOfWork.ServiceImageRepository.InsertAsync(Posterimage);
                            i--;
                        }
                    }
                    return 1;
                }
            }
            return 0;
        }

        private int DeleteImages(Service poster, Service posterExist)
        {
            int i = 0;
            ICollection<ServiceImage> posterImages = posterExist.ServiceImages;
            if (poster.ServiceImagesIds != null)
            {
                foreach (var image in posterImages.ToList().Where(x => x.IsDelete == false && !x.IsPoster && !poster.ServiceImagesIds.Contains(x.Id)))
                {
                    _manageImageHelper.DeleteFile(image.Image, "Services");
                    posterExist.ServiceImages.Remove(image);
                    i++;
                }
                posterImages.ToList().RemoveAll(x => !poster.ServiceImagesIds.Contains(x.Id));
                return i;
            }
            else
            {
                if (poster.ImageFiles?.Count() > 0)
                {
                    foreach (var item in posterImages.ToList().Where(x => !x.IsDelete && !x.IsPoster))
                    {
                        _manageImageHelper.DeleteFile(item.Image, "Services");
                        posterExist.ServiceImages.Remove(item);
                        i++;
                    }
                    return i;
                }
                else if (posterImages.Any(x => !x.IsPoster))
                {
                    foreach (var item in posterImages.ToList().Where(x => !x.IsDelete && !x.IsPoster))
                    {
                        _manageImageHelper.DeleteFile(item.Image, "Services");
                        posterExist.ServiceImages.Remove(item);
                        i++;
                    }
                    return i;
                }
                else if (posterImages.Any(x => x.IsPoster))
                {
                    return i;
                }
                else throw new ImageCountException("Axırıncı şəkil silinə bilməz!");
            }
        }
        private void Check(Service Service)
        {
           
            if (Service.TitleAz?.Length < 3 || Service.TitleEn?.Length < 3)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu minimum 3 ola bilər");
            }
            if (Service.TitleAz?.Length > 150 || Service.TitleEn?.Length > 150)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu maksimum 150 ola bilər");
            }
            if (Service.DescriptionAz?.Length > 3500 || Service.DescriptionEn?.Length > 3500)
            {
                throw new ValueFormatExpception("Xidmət təsvirinin uzunluğu maksimum 3500 ola bilər");
            }
            if (Service.DescriptionAz?.Length < 3 || Service.DescriptionEn?.Length < 3)
            {
                throw new ValueFormatExpception("Xidmət təsvirinin uzunluğu minimum 3 ola bilər");
            }
        }

    }
}
