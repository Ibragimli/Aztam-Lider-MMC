using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Projects
{
    public class AdminProjectDeleteServices : IAdminProjectDeleteServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminProjectDeleteServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }
        public async Task DeleteProject(int id)
        {
            bool check = false;
            var project = await _unitOfWork.ProjectRepository.GetAsync(x => !x.IsDelete && x.Id == id);
            if (project == null)
                throw new ItemNotFoundException("404");
            _manageImageHelper.DeleteFile(project.Image, "Projects");
            check = true;
            if (check)
            {
                _unitOfWork.ProjectRepository.Remove(project);
                await _unitOfWork.CommitAsync();
            }

        }
    }
}
