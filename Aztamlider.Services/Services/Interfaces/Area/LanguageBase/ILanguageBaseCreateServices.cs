using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.Area;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.LanguageBases
{
    public interface ILanguageBaseCreateServices
    {
        Task<LanguageBase> CreateLanguageBase(LanguageBaseCreateDto LanguageBaseCreateDto);

    }
}
