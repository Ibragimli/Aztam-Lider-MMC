using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Documents
{
    public class AdminMainSliderDeleteServices : IAdminServiceTypeDeleteServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminMainSliderDeleteServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }
        public async Task DeleteDocument(int id)
        {
            bool check = false;
            var project = await _unitOfWork.DocumentRepository.GetAsync(x => !x.IsDelete && x.Id == id);
            if (project == null)
                throw new ItemNotFoundException("404");
            _manageImageHelper.DeleteFile(project.Image, "documents");
            _manageImageHelper.DeleteFile(project.PDF, "documents");
            check = true;
            if (check)
            {
                _unitOfWork.DocumentRepository.Remove(project);
                await _unitOfWork.CommitAsync();
            }

        }
    }
}
