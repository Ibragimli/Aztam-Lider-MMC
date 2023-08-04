using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Core.Entites;

namespace Aztamlider.Services.Services.Interfaces.Area.LanguageBases
{
    public interface ILanguageBaseIndexServices
    {
        IQueryable<LanguageBase> SearchCheck(string search);
    }
}
