using Aztamlider.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.Loggers
{
    public interface IAdminLoggerIndexServices
    {
        public IQueryable<Logger> GetLogger(string name);
    }
}
