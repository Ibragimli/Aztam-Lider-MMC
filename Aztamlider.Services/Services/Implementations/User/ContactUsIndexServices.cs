﻿using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.User
{
    public class HomeIndexServices : IHomeIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async  Task<IEnumerable<LanguageBase>> GetLanguageBase()
        {
            return await _unitOfWork.LanguageBaseRepository.GetAllAsync(x => !x.IsDelete);

        }

        public async Task<IEnumerable<Setting>> GetSettings()
        {
            return await _unitOfWork.SettingRepository.GetAllAsync(x => !x.IsDelete);
        }

        public async Task<IEnumerable<MainSlider>> MainSliders()
        {
            return await _unitOfWork.MainSliderRepository.GetAllAsync(x => !x.IsDelete);

        }

        public async Task<IEnumerable<Partner>> Partners()
        {
            return await _unitOfWork.PartnerRepository.GetAllAsync(x => !x.IsDelete);

        }
    }
}
