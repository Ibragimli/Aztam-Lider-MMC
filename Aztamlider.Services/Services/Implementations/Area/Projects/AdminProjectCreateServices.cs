using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Projects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Projects
{
    public class AdminProjectCreateServices : IAdminProjectCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminProjectCreateServices(IUnitOfWork unitOfWork, IMapper mapper, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _manageImageHelper = manageImageHelper;
        }


        public async Task<Project> CreateProject(ProjectCreateDto ProjectCreateDto)
        {
            await DtoCheck(ProjectCreateDto);
            var Project = _mapper.Map<Project>(ProjectCreateDto);
            Project.Image = _manageImageHelper.FileSave(ProjectCreateDto.ImageFile, "Projects");
            await _unitOfWork.ProjectRepository.InsertAsync(Project);
            await _unitOfWork.CommitAsync();

            return Project;
        }
        public async Task<IEnumerable<ProjectType>> GetAllProjectTypes()
        {
            var Projects = await _unitOfWork.ProjectTypeRepository.GetAllAsync(x => !x.IsDelete);
            return Projects;
        }


        private async Task DtoCheck(ProjectCreateDto ProjectCreateDto)
        {

            if (ProjectCreateDto.TitleAz?.Length < 3 || ProjectCreateDto.TitleEn?.Length < 3)
            {
                throw new ValueFormatExpception("Layihə adının uzunluğu minimum 3 ola bilər");
            }
            if (ProjectCreateDto.TitleAz?.Length > 120 || ProjectCreateDto.TitleEn?.Length > 120)
            {
                throw new ValueFormatExpception("Layihə adının uzunluğu maksimum 120 ola bilər");
            }
            if (ProjectCreateDto.DescriptionEn?.Length > 3000 || ProjectCreateDto.DescriptionAz?.Length > 3000)
            {
                throw new ValueFormatExpception("Layihə təsvir uzunluğu maksimum 3000 ola bilər");
            }
            if (ProjectCreateDto.ImageFile == null)
            {
                throw new ItemNullException("Şəkil əlavə edin!");
            }
            if (ProjectCreateDto.ProjectTypeId == 0)
            {
                throw new ItemNullException("Layihə növünü qeyd edin!");
            }
            if (ProjectCreateDto.ImageFile != null)
                _manageImageHelper.PosterCheck(ProjectCreateDto.ImageFile);
        }
    }
}
