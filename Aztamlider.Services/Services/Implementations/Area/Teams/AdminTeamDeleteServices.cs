using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Teams
{
    public class AdminTeamDeleteServices : IAdminTeamDeleteServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminTeamDeleteServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }
        public async Task DeleteTeam(int id)
        {
            bool check = false;
            var project = await _unitOfWork.TeamRepository.GetAsync(x => !x.IsDelete && x.Id == id);
            if (project == null)
                throw new ItemNotFoundException("404");
            _manageImageHelper.DeleteFile(project.Image, "Teams");
            check = true;
            if (check)
            {
                _unitOfWork.TeamRepository.Remove(project);
                await _unitOfWork.CommitAsync();
            }

        }
    }
}
