using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.ServiceNames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.ServiceNames
{
    public class AdminServiceNameDeleteServices : IAdminServiceNameDeleteServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminServiceNameDeleteServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteServiceName(int id)
        {
            bool check = false;
            var project = await _unitOfWork.ServiceNameRepository.GetAsync(x => !x.IsDelete && x.Id == id);
            if (project == null)
                throw new ItemNotFoundException("404");
            if (await _unitOfWork.ReferenceRepository.IsExistAsync(x => x.ServiceNameId == project.Id))
                throw new ItemUseException("Bu xidmət növü referanslarda istifadə olunduğu üçün silmək mümkün olmadı!");

            check = true;
            if (check)
            {
                _unitOfWork.ServiceNameRepository.Remove(project);
                await _unitOfWork.CommitAsync();
            }

        }
    }
}
