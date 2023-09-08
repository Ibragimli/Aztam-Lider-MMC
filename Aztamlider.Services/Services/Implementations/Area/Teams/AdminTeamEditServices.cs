using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Teams;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Teams
{
    public class AdminTeamEditServices : IAdminTeamEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminTeamEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }

        public async Task EditTeam(Team Team)
        {
            bool checkBool = false;

            var oldTeam = await GetTeam(Team.Id);
            if (oldTeam == null)
                throw new ItemNullException("Kamanda tapılmadı!");

            await Check(Team);

            if (ImageChange(Team, oldTeam) == 1)
                checkBool = true;

            if (oldTeam.Row != Team.Row)
            {
                var teamExist = await _unitOfWork.TeamRepository.GetAsync(x => x.Row == Team.Row);
                if (teamExist != null)
                    teamExist.Row = oldTeam.Row;
                oldTeam.Row = Team.Row;
                checkBool = true;
            }
            if (oldTeam.PositionEn != Team.PositionEn)
            {
                oldTeam.PositionEn = Team.PositionEn;
                checkBool = true;
            }
            if (oldTeam.PositionAz != Team.PositionAz)
            {
                oldTeam.PositionAz = Team.PositionAz;
                checkBool = true;
            }
            if (oldTeam.DescriptionAz != Team.DescriptionAz)
            {
                oldTeam.DescriptionAz = Team.DescriptionAz;
                checkBool = true;
            }
            if (oldTeam.DescriptionEn != Team.DescriptionEn)
            {
                oldTeam.DescriptionEn = Team.DescriptionEn;
                checkBool = true;
            }

            if (oldTeam.Name != Team.Name)
            {
                oldTeam.Name = Team.Name;
                checkBool = true;
            }
            oldTeam.ModifiedDate = DateTime.UtcNow.AddHours(4);

            if (checkBool)
                await _unitOfWork.CommitAsync();
        }

        public async Task<Team> GetTeam(int id)
        {
            var Team = await _unitOfWork.TeamRepository.GetAsync(x => x.Id == id);
            return Team;
        }

        private int ImageChange(Team Team, Team projectExist)
        {
            if (Team.ImageFile != null)
            {
                var posterImageFile = Team.ImageFile;

                if (projectExist.Image == null) throw new ImageNullException("Şəkil tapılmadı!");

                string filename = _manageImageHelper.FileSave(posterImageFile, "Teams");
                _manageImageHelper.DeleteFile(projectExist.Image, "Teams");
                projectExist.Image = filename;
                return 1;
            }
            return 0;

        }


        private async Task Check(Team Team)
        {

            if (Team.PositionAz == null && Team.PositionEn == null)
            {
                throw new ItemNullException("Vəzifəni qeyd edin!");
            }

            if (Team.DescriptionEn == null && Team.DescriptionAz == null)
            {
                throw new ItemNullException("Təsviri qeyd edin!");
            }
            if (Team.Name == null)
            {
                throw new ItemNullException("Əməkdaşın adını qeyd edin!");
            }
            if (Team.PositionEn?.Length < 3 && Team.PositionAz?.Length < 3)
            {
                throw new ValueFormatExpception("Vəzifənin uzunluğu minimum 3 ola bilər");
            }
            if (Team.PositionAz?.Length > 150 && Team.PositionEn?.Length > 150)
            {
                throw new ValueFormatExpception("Vəzifənin uzunluğu maksimum 150 ola bilər");
            }
            if (Team.DescriptionEn?.Length > 450 && Team.DescriptionAz?.Length > 450)
            {
                throw new ValueFormatExpception("Məlumat uzunluğu maksimum 450 ola bilər");
            }



            if (Team.ImageFile != null)
                _manageImageHelper.PosterCheck(Team.ImageFile);

        }
    }
}
