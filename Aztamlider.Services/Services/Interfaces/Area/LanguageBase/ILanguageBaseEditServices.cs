using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Services.Dtos.Area;

namespace Aztamlider.Services.Services.Interfaces.Area.LanguageBases
{
    public interface ILanguageBaseEditServices
    {
        Task LanguageBaseEdit(LanguageBaseEditDto LanguageBaseEdit);
        Task<LanguageBaseEditDto> IsExists(int id);
        Task<LanguageBaseEditDto> GetSearch(int Id);
    }

}
