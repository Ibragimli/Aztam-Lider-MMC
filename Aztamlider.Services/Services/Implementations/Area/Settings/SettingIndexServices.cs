﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.Settings;

namespace Aztamlider.Services.Services.Implementations.Area.Settings
{
    public class SettingIndexServices : ISettingIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public SettingIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<Setting> SearchCheck(string search)
        {
            var SettingLast = _unitOfWork.SettingRepository.asQueryable();
            if (search != null)
            {
                search = search.ToLower();
                if (search != null)
                    SettingLast = SettingLast.Where(i => EF.Functions.Like(i.Key, $"%{search}%"));
            }
            return SettingLast;
        }

    }
}
