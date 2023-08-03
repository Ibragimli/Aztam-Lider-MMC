using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Services.Dtos.Area;

namespace Aztamlider.Services.Services.Interfaces.Area.ImageSettings
{
    public interface IImageSettingEditServices
    {
        Task ImageSettingEdit(ImageSettingEditDto ImageSettingEdit);
        Task<ImageSettingEditDto> IsExists(int id);
        Task<ImageSettingEditDto> GetSearch(int Id);
    }

}
