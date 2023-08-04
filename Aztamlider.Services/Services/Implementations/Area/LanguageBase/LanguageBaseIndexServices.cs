using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.LanguageBases;
using Aztamlider.Services.Services.Interfaces.Area.Settings;

namespace Aztamlider.Services.Services.Implementations.Area.LanguageBases
{
    public class LanguageBaseIndexServices : ILanguageBaseIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public LanguageBaseIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<LanguageBase> SearchCheck(string search)
        {
            var LanguageBaseLast = _unitOfWork.LanguageBaseRepository.asQueryable();
            if (search != null)
            {
                search = search.ToLower();
                if (search != null)
                    LanguageBaseLast = LanguageBaseLast.Where(i => EF.Functions.Like(i.Key, $"%{search}%"));
            }
            return LanguageBaseLast;
        }

    }
}
