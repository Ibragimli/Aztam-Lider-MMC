using Aztamlider.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.MainSliders
{
    public interface IAdminMainSliderEditServices
    {
        public Task<MainSlider> GetMainSlider(int id);
        public Task EditMainSlider(MainSlider MainSlider);

    }
}
