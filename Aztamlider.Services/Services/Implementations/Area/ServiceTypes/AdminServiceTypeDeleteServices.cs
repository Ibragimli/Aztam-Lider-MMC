using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.ServiceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.ServiceTypes
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
        public async Task DeleteServiceType(int id)
        {
            bool check = false;
            var project = await _unitOfWork.ServiceTypeRepository.GetAsync(x => !x.IsDelete && x.Id == id);
            if (project == null)
                throw new ItemNotFoundException("404");
            if (await _unitOfWork.ReferenceRepository.IsExistAsync(x => x.ServiceTypeId == project.Id))
                throw new ItemUseException("Bu xidmət növü referanslarda istifadə olunduğu üçün silmək mümkün olmadı!");

            check = true;
            if (check)
            {
                _unitOfWork.ServiceTypeRepository.Remove(project);
                await _unitOfWork.CommitAsync();
            }

        }
    }
}
