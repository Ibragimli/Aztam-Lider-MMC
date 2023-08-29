using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Services.Interfaces.Area.Settings;
using AutoMapper;
using Aztamlider.Core.Entites;

namespace Aztamlider.Services.Services.Implementations.Area.Settings
{
    public class SettingCreateServices : ISettingCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;
        private readonly IMapper _mapper;

        public SettingCreateServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
            _mapper = mapper;
        }


        public async Task SettingCreate(SettingCreateDto SettingCreate)
        {
            if (SettingCreate.Key == null)
                throw new ItemFormatException("Key boş ola bilməz");

            if (SettingCreate.Value == null && SettingCreate.KeyImageFile == null)
                throw new ItemFormatException("Dəyər əlavə edin!");

            var setting = _mapper.Map<Setting>(SettingCreate);
            if (SettingCreate.KeyImageFile != null)
            {
                _manageImageHelper.PosterCheck(SettingCreate.KeyImageFile);
                setting.Value = _manageImageHelper.FileSave(SettingCreate.KeyImageFile, "setting");
            }
            await _unitOfWork.SettingRepository.InsertAsync(setting);
            await _unitOfWork.CommitAsync();
        }

    }
}
