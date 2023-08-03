using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Partners;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Partners
{
    public class AdminPartnerEditServices : IAdminPartnerEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminPartnerEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }

        public async Task EditPartner(Partner Partner)
        {
            bool checkBool = false;

            var oldPartner = await GetPartner(Partner.Id);
            if (oldPartner == null)
                throw new ItemNullException("Layihə tapılmadı!");

            await Check(Partner);

            if (ImageChange(Partner, oldPartner) == 1)
                checkBool = true;


            oldPartner.ModifiedDate = DateTime.UtcNow.AddHours(4);

            if (checkBool)
                await _unitOfWork.CommitAsync();
        }

        public async Task<Partner> GetPartner(int id)
        {
            var Partner = await _unitOfWork.PartnerRepository.GetAsync(x => x.Id == id);
            return Partner;
        }

        private int ImageChange(Partner Partner, Partner projectExist)
        {
            if (Partner.ImageFile != null)
            {
                var posterImageFile = Partner.ImageFile;



                if (projectExist.Image == null) throw new ImageNullException("Şəkil tapılmadı!");

                string filename = _manageImageHelper.FileSave(posterImageFile, "Partners");
                _manageImageHelper.DeleteFile(projectExist.Image, "Partners");
                projectExist.Image = filename;
                return 1;
            }
            return 0;

        }


        private async Task Check(Partner Partner)
        {

            if (Partner.ImageFile != null)
                _manageImageHelper.PosterCheck(Partner.ImageFile);

        }
    }
}
