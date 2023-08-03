using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Partners
{
    public class AdminPartnerDeleteServices : IAdminPartnerDeleteServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminPartnerDeleteServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }
        public async Task DeletePartner(int id)
        {
            bool check = false;
            var project = await _unitOfWork.PartnerRepository.GetAsync(x => !x.IsDelete && x.Id == id);
            if (project == null)
                throw new ItemNotFoundException("404");
            _manageImageHelper.DeleteFile(project.Image, "Partners");
            check = true;
            if (check)
            {
                _unitOfWork.PartnerRepository.Remove(project);
                await _unitOfWork.CommitAsync();
            }

        }
    }
}
