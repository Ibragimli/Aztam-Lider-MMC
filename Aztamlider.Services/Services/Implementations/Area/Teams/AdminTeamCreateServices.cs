using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
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
    public class AdminTeamCreateServices : IAdminTeamCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminTeamCreateServices(IUnitOfWork unitOfWork, IMapper mapper, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _manageImageHelper = manageImageHelper;
        }


        public async Task<Team> CreateTeam(TeamCreateDto teamCreateDto)
        {
            await DtoCheck(teamCreateDto);
            var Team = _mapper.Map<Team>(teamCreateDto);
            int lastRow = 1;

            var teams = await _unitOfWork.TeamRepository.GetAllAsync(x => !x.IsDelete);
            if (teams.Count() > 0)
                lastRow = teams.OrderByDescending(x => x.Row).FirstOrDefault().Row;

            if (teamCreateDto.Row > lastRow + 1)
                throw new ValueFormatExpception("Sıra nömrəsi maksimum " + (lastRow + 1) + " ola bilər");
            var teamExist = await _unitOfWork.TeamRepository.GetAsync(x => !x.IsDelete && x.Row == teamCreateDto.Row);

            if (teamExist != null)
                teamExist.Row = lastRow + 1;
            else
                Team.Row = teamCreateDto.Row;


            Team.Image = _manageImageHelper.FileSave(teamCreateDto.ImageFile, "Teams");
            await _unitOfWork.TeamRepository.InsertAsync(Team);
            await _unitOfWork.CommitAsync();

            return Team;
        }

        private async Task DtoCheck(TeamCreateDto TeamCreateDto)
        {
            if (TeamCreateDto.Name == null)
            {
                throw new ValueFormatExpception("Əməkdaşın adını qeyd edin");
            }
            if (TeamCreateDto.PositionEn == null || TeamCreateDto.PositionAz == null)
            {
                throw new ValueFormatExpception("Əməkdaşın vəzifəsini qeyd edin");
            }
            if (TeamCreateDto.PositionEn?.Length < 3 && TeamCreateDto.PositionAz?.Length < 3)
            {
                throw new ValueFormatExpception("Vəzifənin uzunluğu minimum 3 ola bilər");
            }
            if (TeamCreateDto.PositionAz?.Length > 150 && TeamCreateDto.PositionEn?.Length > 150)
            {
                throw new ValueFormatExpception("Vəzifənin uzunluğu maksimum 150 ola bilər");
            }
            if (TeamCreateDto.Row < 1)
            {
                throw new ValueFormatExpception("Sıra nömrəsi minimum 1 ola bilər");
            }
            if (TeamCreateDto.Row > 200)
            {
                throw new ValueFormatExpception("Sıra nömrəsi maksimum 200 ola bilər");
            }
            if (TeamCreateDto.Name?.Length < 3)
            {
                throw new ValueFormatExpception("Əməkdaşın adının uzunluğu minimum 3 ola bilər");
            }
            if (TeamCreateDto.Name?.Length > 100)
            {
                throw new ValueFormatExpception("Əməkdaşın adının uzunluğu maksimum 100 ola bilər");
            }
            if (TeamCreateDto.ImageFile == null)
            {
                throw new ItemNullException("Şəkil əlavə edin!");
            }
            if (TeamCreateDto.ImageFile != null)
                _manageImageHelper.PosterCheck(TeamCreateDto.ImageFile);
        }
    }
}
