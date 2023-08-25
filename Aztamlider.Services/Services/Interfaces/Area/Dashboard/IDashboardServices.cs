using Aztamlider.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.Dashboard
{
    public interface IDashboardServices
    {
        public Task<int> GetContactCount();
        public Task<int> GetCareerCount();
        public Task<List<int>> GetMonthCareerCount();
        public Task<List<int>> GetMonthContactCount();
    }
}
