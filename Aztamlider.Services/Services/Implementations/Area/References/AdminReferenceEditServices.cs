using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.References;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.References
{
    public class AdminReferenceEditServices : IAdminReferenceEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminReferenceEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }

        public async Task EditReference(Reference Reference)
        {
            bool checkBool = false;

            var oldReference = await GetReference(Reference.Id);
            if (oldReference == null)
                throw new ItemNullException("Referans tapılmadı!");


            Check(Reference);
            if (Reference.ReferenceImageFile != null)
                _manageImageHelper.PosterCheck(Reference.ReferenceImageFile);

            if (Reference.ImageFiles != null)
                _manageImageHelper.ImagesCheck(Reference.ImageFiles);

            int deleteCount = DeleteImages(Reference, oldReference);
            if (deleteCount > 0)
                checkBool = true;

            if (PosterImageChange(Reference, oldReference) == 1)
                checkBool = true;

            if (await CreateImageFormFile(Reference.ImageFiles, Reference.Id, deleteCount) == 1)
                checkBool = true;

            if (oldReference.Name != Reference.Name)
            {
                oldReference.Name = Reference.Name;
                checkBool = true;
            }

            if (oldReference.ServiceTypeId != Reference.ServiceTypeId)
            {
                oldReference.ServiceTypeId = Reference.ServiceTypeId;
                checkBool = true;
            }

            if (oldReference.ServiceNameId != Reference.ServiceNameId)
            {
                oldReference.ServiceNameId = Reference.ServiceNameId;
                checkBool = true;
            }
            if (oldReference.LocationAz != Reference.LocationAz)
            {
                oldReference.LocationAz = Reference.LocationAz;
                checkBool = true;
            }
            if (oldReference.LocationEn != Reference.LocationEn)
            {
                oldReference.LocationEn = Reference.LocationEn;
                checkBool = true;
            }
            if (oldReference.BuildingTypeAz != Reference.BuildingTypeAz)
            {
                oldReference.BuildingTypeAz = Reference.BuildingTypeAz;
                checkBool = true;
            }
            if (oldReference.BuildingTypeEn != Reference.BuildingTypeEn)
            {
                oldReference.BuildingTypeEn = Reference.BuildingTypeEn;
                checkBool = true;
            }
            if (oldReference.SquareMetr != Reference.SquareMetr)
            {
                oldReference.SquareMetr = Reference.SquareMetr;
                checkBool = true;
            }
            if (oldReference.Date != Reference.Date)
            {
                oldReference.Date = Reference.Date;
                checkBool = true;
            }
            if (oldReference.Status != Reference.Status)
            {
                oldReference.Status = Reference.Status;
                checkBool = true;
            }
            if (oldReference.ServiceTypeId != Reference.ServiceTypeId)
            {
                oldReference.ServiceTypeId = Reference.ServiceTypeId;
                checkBool = true;
            }
            oldReference.ModifiedDate = DateTime.UtcNow.AddHours(4);
            if (checkBool)
                await _unitOfWork.CommitAsync();
        }


        public async Task<Reference> GetReference(int id)
        {
            var Reference = await _unitOfWork.ReferenceRepository.GetAsync(x => x.Id == id, "ReferenceImages");
            return Reference;
        }

        public async Task<IEnumerable<ReferenceImage>> GetImages(int id)
        {
            var images = await _unitOfWork.ReferenceImageRepository.GetAllAsync(x => x.ReferenceId == id);
            return images;
        }
        private int PosterImageChange(Reference Reference, Reference projectExist)
        {
            if (Reference.ReferenceImageFile != null)
            {
                var posterImageFile = Reference.ReferenceImageFile;

                ReferenceImage posterImage = projectExist.ReferenceImages.FirstOrDefault(x => x.IsPoster);

                if (posterImage == null) throw new ImageNullException("Şəkil tapılmadı!");

                string filename = _manageImageHelper.FileSave(posterImageFile, "References");
                _manageImageHelper.DeleteFile(posterImage.Image, "References");
                posterImage.Image = filename;
                posterImage.IsPoster = true;
                return 1;
            }
            return 0;
        }
        private async Task<int> CreateImageFormFile(List<IFormFile> imageFiles, int posterId, int deleteCount)
        {
            int countImage = await _unitOfWork.ReferenceImageRepository.GetTotalCountAsync(x => x.ReferenceId == posterId && !x.IsPoster);
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
                            ReferenceImage Posterimage = new ReferenceImage
                            {
                                IsPoster = false,
                                ReferenceId = posterId,
                                Image = _manageImageHelper.FileSave(image, "References"),
                            };
                            await _unitOfWork.ReferenceImageRepository.InsertAsync(Posterimage);
                            i--;
                        }
                    }
                    return 1;
                }
            }
            return 0;
        }
        private int DeleteImages(Reference poster, Reference posterExist)
        {
            int i = 0;
            ICollection<ReferenceImage> posterImages = posterExist.ReferenceImages;
            if (poster.ReferenceImagesIds != null)
            {
                foreach (var image in posterImages.ToList().Where(x => x.IsDelete == false && !x.IsPoster && !poster.ReferenceImagesIds.Contains(x.Id)))
                {
                    _manageImageHelper.DeleteFile(image.Image, "References");
                    posterExist.ReferenceImages.Remove(image);
                    i++;
                }
                posterImages.ToList().RemoveAll(x => !poster.ReferenceImagesIds.Contains(x.Id));
                return i;
            }
            else
            {

                if (poster.ImageFiles?.Count() > 0)
                {
                    foreach (var item in posterImages.ToList().Where(x => !x.IsDelete && !x.IsPoster))
                    {
                        _manageImageHelper.DeleteFile(item.Image, "References");
                        posterExist.ReferenceImages.Remove(item);
                        i++;
                    }
                    return i;
                }
                else if (posterImages.Any(x => !x.IsPoster))
                {
                    foreach (var item in posterImages.ToList().Where(x => !x.IsDelete && !x.IsPoster))
                    {
                        _manageImageHelper.DeleteFile(item.Image, "References");
                        posterExist.ReferenceImages.Remove(item);
                        i++;
                    }
                    return i;
                }
                else if (posterImages.Any(x => x.IsPoster))
                {
                    return i;
                }
                else throw new ImageCountException("Axırıncı şəkil silinə bilməz!");
                ;
            }
        }
        private void Check(Reference Reference)
        {

            if (Reference.ServiceTypeId == 0)
            {
                throw new ValueFormatExpception("Referans servis növü qeyd edin!");
            }

            if (Reference.ServiceNameId == 0)
            {
                throw new ValueFormatExpception("Referans servis adını qeyd edin!");
            }
            if (Reference.SquareMetr <= 0)
            {
                throw new ValueFormatExpception("Tikinti sahəni düzgün qeyd edin!");
            }

            if (Reference.Name.Length < 3)
            {
                throw new ValueFormatExpception("Referans adının uzunluğu minimum 3 ola bilər");
            }
            if (Reference.Name.Length > 150)
            {
                throw new ValueFormatExpception("Referans adının uzunluğu maksimum 150 ola bilər");
            }

            if (Reference.Date.Length > 50)
            {
                throw new ValueFormatExpception("Referans tarixinin uzunluğu maksimum 50 ola bilər");
            }

            if (Reference.LocationAz.Length > 120)
            {
                throw new ValueFormatExpception("Referans location uzunluğu maksimum 120 ola bilər");
            }
            if (Reference.LocationEn.Length > 120)
            {
                throw new ValueFormatExpception("Referans location uzunluğu maksimum 120 ola bilər");
            }
            if (Reference.BuildingTypeEn.Length > 150)
            {
                throw new ValueFormatExpception("Referans bina növünün uzunluğu maksimum 150 ola bilər");
            }
            if (Reference.BuildingTypeAz.Length > 150)
            {
                throw new ValueFormatExpception("Referans bina növünün uzunluğu maksimum 150 ola bilər");
            }
        }
        public async Task<IEnumerable<ServiceType>> GetAllServiceTypes()
        {
            var serviceType = await _unitOfWork.ServiceTypeRepository.GetAllAsync(x => !x.IsDelete);
            return serviceType;
        }

        public async Task<IEnumerable<ServiceName>> GetAllServiceNames()
        {
            var serviceName = await _unitOfWork.ServiceNameRepository.GetAllAsync(x => !x.IsDelete);
            return serviceName;
        }
    }
}
