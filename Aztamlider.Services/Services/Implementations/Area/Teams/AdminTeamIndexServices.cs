﻿using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.Teams;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Teams
{
    public class AdminTeamIndexServices : IAdminTeamIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminTeamIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<Team> GetTeam(string name)
        {
            var poster = _unitOfWork.TeamRepository.asQueryable();
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.Name, $"%{name}%"));

            return poster;
        }
        public async Task<int> LastRow()
        {
            int lastRow = 0;
            var teams = await _unitOfWork.TeamRepository.GetAllAsync(x => !x.IsDelete);
            if (teams.Count() > 0)
                lastRow = teams.OrderByDescending(x => x.Row).Max(x => x.Row);
            return lastRow;
        }
    }
}
