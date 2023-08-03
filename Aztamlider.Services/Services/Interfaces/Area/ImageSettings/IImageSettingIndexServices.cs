using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Core.Entites;

namespace Aztamlider.Services.Services.Interfaces.Area.ImageSettings
{
    public interface IImageSettingIndexServices
    {
        IQueryable<ImageSetting> SearchCheck(string search);
    }
}
