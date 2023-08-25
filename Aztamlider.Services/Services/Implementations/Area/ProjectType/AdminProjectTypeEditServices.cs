using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.ProjectTypes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.ProjectTypes
{
    public class AdminProjectTypeEditServices : IAdminProjectTypeEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminProjectTypeEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }

        public async Task EditProjectType(ProjectType ProjectType)
        {
            bool checkBool = false;

            var oldProjectType = await GetProjectType(ProjectType.Id);
            if (oldProjectType == null)
                throw new ItemNullException("Xidmət tapılmadı!");

            await Check(ProjectType);

            if (oldProjectType.NameAz != ProjectType.NameAz)
            {
                oldProjectType.NameAz = ProjectType.NameAz;
                checkBool = true;
            }
            if (oldProjectType.NameEn != ProjectType.NameEn)
            {
                oldProjectType.NameEn = ProjectType.NameEn;
                checkBool = true;
            }

            oldProjectType.ModifiedDate = DateTime.UtcNow.AddHours(4);

            if (checkBool)
                await _unitOfWork.CommitAsync();
        }

        public async Task<ProjectType> GetProjectType(int id)
        {
            var ProjectType = await _unitOfWork.ProjectTypeRepository.GetAsync(x => x.Id == id);
            return ProjectType;
        }


        private async Task Check(ProjectType ProjectType)
        {

            if (ProjectType.NameAz == null || ProjectType.NameEn == null)
            {
                throw new ItemNullException("Xidmət adını qeyd edin!");
            }
            if (ProjectType.NameAz?.Length < 3 || ProjectType.NameEn?.Length < 3)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu minimum 3 ola bilər");
            }
            if (ProjectType.NameAz?.Length > 150 || ProjectType.NameEn?.Length > 150)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu maksimum 150 ola bilər");
            }

        }
    }
}
