using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.ProjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.ProjectTypes
{
    public class AdminProjectTypeDeleteServices : IAdminProjectTypeDeleteServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminProjectTypeDeleteServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteProjectType(int id)
        {
            bool check = false;
            var project = await _unitOfWork.ProjectTypeRepository.GetAsync(x => !x.IsDelete && x.Id == id);
            if (project == null)
                throw new ItemNotFoundException("404");
            if (await _unitOfWork.ProjectRepository.IsExistAsync(x => x.ProjectTypeId == project.Id))
                throw new ItemUseException("Bu xidmət növü referanslarda istifadə olunduğu üçün silmək mümkün olmadı!");

            check = true;
            if (check)
            {
                _unitOfWork.ProjectTypeRepository.Remove(project);
                await _unitOfWork.CommitAsync();
            }

        }
    }
}
