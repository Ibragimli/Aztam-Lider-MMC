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
    public class AdminProjectEditServices : IAdminProjectEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminProjectEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }

        public async Task EditProject(Project Project)
        {
            bool checkBool = false;

            var oldProject = await GetProject(Project.Id);
            if (oldProject == null)
                throw new ItemNullException("Layihə tapılmadı!");

            await Check(Project);

            if (ImageChange(Project, oldProject) == 1)
                checkBool = true;


            if (oldProject.TitleAz != Project.TitleAz)
            {
                oldProject.TitleAz = Project.TitleAz;
                checkBool = true;
            }
            if (oldProject.TitleEn != Project.TitleEn)
            {
                oldProject.TitleEn = Project.TitleEn;
                checkBool = true;
            }

            if (oldProject.DescriptionAz != Project.DescriptionAz)
            {
                oldProject.DescriptionAz = Project.DescriptionAz;
                checkBool = true;
            }

            if (oldProject.DescriptionEn != Project.DescriptionEn)
            {
                oldProject.DescriptionEn = Project.DescriptionEn;
                checkBool = true;
            }


            oldProject.ModifiedDate = DateTime.UtcNow.AddHours(4);

            if (checkBool)
                await _unitOfWork.CommitAsync();
        }

        public async Task<Project> GetProject(int id)
        {
            var Project = await _unitOfWork.ProjectRepository.GetAsync(x => x.Id == id);
            return Project;
        }

        private int ImageChange(Project Project, Project projectExist)
        {
            if (Project.ImageFile != null)
            {
                var posterImageFile = Project.ImageFile;

                if (projectExist.Image == null) throw new ImageNullException("Şəkil tapılmadı!");

                string filename = _manageImageHelper.FileSave(posterImageFile, "Projects");
                _manageImageHelper.DeleteFile(projectExist.Image, "Projects");
                projectExist.Image = filename;
                return 1;
            }
            return 0;
        }



        private async Task Check(Project Project)
        {
            if (Project.TitleAz == null && Project.TitleEn == null)
            {
                throw new ItemNullException("Layihə adını qeyd edin!");
            }
            if (Project.DescriptionEn == null && Project.DescriptionAz == null)
            {
                throw new ItemNullException("Layihə təsvirini qeyd edin!");
            }
            if (Project.TitleAz?.Length < 3 || Project.TitleEn?.Length < 3)
            {
                throw new ValueFormatExpception("Layihə adının uzunluğu minimum 3 ola bilər");
            }
            if (Project.TitleAz?.Length > 120 || Project.TitleEn?.Length > 120)
            {
                throw new ValueFormatExpception("Layihə adının uzunluğu maksimum 120 ola bilər");
            }
            if (Project.DescriptionEn?.Length > 3000 || Project.DescriptionAz?.Length > 3000)
            {
                throw new ValueFormatExpception("Layihə təsvir uzunluğu maksimum 3000 ola bilər");
            }
           

            if (Project.ImageFile != null)
                _manageImageHelper.PosterCheck(Project.ImageFile);

        }
    }
}
