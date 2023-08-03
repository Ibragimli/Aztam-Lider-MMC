using Aztamlider.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.MainSliders
{
    public interface IAdminMainSliderIndexServices
    {
        public IQueryable<MainSlider> GetMainSlider(string name);
    }
}
