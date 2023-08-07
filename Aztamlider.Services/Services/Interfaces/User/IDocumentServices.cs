using Aztamlider.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.User
{
    public interface IDocumentServices
    {
        public Task<IEnumerable<Setting>> GetSettings();
        public Task<IEnumerable<Document>> GetDocuments();
        public Task<IEnumerable<LanguageBase>> GetLanguageBase();

    }
}
