using Aztamlider.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.Documents
{
    public interface IAdminServiceTypeIndexServices
    {
        public IQueryable<Document> GetDocument(string name);
    }
}
