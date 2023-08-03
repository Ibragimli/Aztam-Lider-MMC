using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.Area;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.Documents
{
    public interface IAdminDocumentCreateServices
    {
        Task<Document> CreateDocument(DocumentCreateDto DocumentCreateDto);

    }
}
