﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.Documents
{
    public interface IAdminDocumentDeleteServices
    {
        public Task DeleteDocument(int id);
    }
}
