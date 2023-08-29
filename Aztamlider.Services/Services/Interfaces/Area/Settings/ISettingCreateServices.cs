using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Services.Dtos.Area;

namespace Aztamlider.Services.Services.Interfaces.Area.Settings
{
    public interface ISettingCreateServices
    {
        public Task SettingCreate(SettingCreateDto settingCreateDto);
    }
}
