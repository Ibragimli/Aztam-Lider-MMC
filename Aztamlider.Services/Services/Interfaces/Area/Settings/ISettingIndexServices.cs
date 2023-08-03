using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Core.Entites;

namespace Aztamlider.Services.Services.Interfaces.Area.Settings
{
    public interface ISettingIndexServices
    {
        IQueryable<Setting> SearchCheck(string search);
    }
}
