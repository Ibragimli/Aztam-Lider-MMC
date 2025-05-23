﻿using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.Loggers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Loggers
{
    public class AdminLoggerIndexServices : IAdminLoggerIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminLoggerIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<Logger> GetLogger(string name)
        {
            var poster = _unitOfWork.LoggerRepository.asQueryable();
            poster = poster.Where(x => !x.IsDelete);
            poster = poster.OrderByDescending(x => x.Id);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.Name, $"%{name}%"));

            return poster;
        }
    }
}
