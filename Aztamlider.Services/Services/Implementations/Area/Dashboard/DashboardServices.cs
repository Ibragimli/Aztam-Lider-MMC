using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.Dashboard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Dashboard
{
    public class DashboardServices : IDashboardServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> GetCareerCount()
        {
            return await _unitOfWork.CareerRepository.GetTotalCountAsync(x => x.CreatedDate.Year == DateTime.UtcNow.Year);

        }

        public async Task<int> GetContactCount()
        {
            return await _unitOfWork.ContactUsRepository.GetTotalCountAsync(x => x.CreatedDate.Year == DateTime.UtcNow.Year);
        }
    }
}
