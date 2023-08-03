using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.References
{
    public class AdminReferenceDeleteServices : IAdminReferenceDeleteServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminReferenceDeleteServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }

        public async Task DeleteReference(int id)
        {
            bool check = false;
            var project = await _unitOfWork.ReferenceRepository.GetAsync(x => !x.IsDelete && x.Id == id);
            if (project == null)
                throw new ItemNotFoundException("404");
            var images = await _unitOfWork.ReferenceImageRepository.GetAllAsync(x => x.ReferenceId == project.Id && !x.IsDelete);

            if (images != null)
            {
                foreach (var image in images)
                {
                    _unitOfWork.ReferenceImageRepository.Remove(image);
                    _manageImageHelper.DeleteFile(image.Image, "References");
                }
                check = true;
            }
            if (check)
            {
                _unitOfWork.ReferenceRepository.Remove(project);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
