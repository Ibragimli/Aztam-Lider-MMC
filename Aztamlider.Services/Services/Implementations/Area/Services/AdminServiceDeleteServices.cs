using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Services
{

    public class AdminServiceDeleteServices : IAdminServiceDeleteServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminServiceDeleteServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }
        public async Task DeleteService(int id)
        {
            bool check = false;
            var project = await _unitOfWork.ServiceRepository.GetAsync(x => !x.IsDelete && x.Id == id);
            if (project == null)
                throw new ItemNotFoundException("404");
            var images = await _unitOfWork.ServiceImageRepository.GetAllAsync(x => x.ServiceId == project.Id && !x.IsDelete);

            if (images != null)
            {
                foreach (var image in images)
                {
                    _unitOfWork.ServiceImageRepository.Remove(image);
                    _manageImageHelper.DeleteFile(image.Image, "Services");
                }
                check = true;
            }
            if (check)
            {
                _unitOfWork.ServiceRepository.Remove(project);
                await _unitOfWork.CommitAsync();
            }

        }
    }
}
