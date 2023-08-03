using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.Documents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Documents
{
    public class AdminMainSliderIndexServices : IAdminDocumentIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminMainSliderIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<Document> GetDocument(string name)
        {
            var poster = _unitOfWork.DocumentRepository.asQueryable();
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.Name, $"%{name}%"));

            return poster;
        }
    }
}
