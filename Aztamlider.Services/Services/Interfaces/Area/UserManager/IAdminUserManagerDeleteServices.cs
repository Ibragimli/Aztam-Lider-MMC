﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.UserManagers
{
    public interface IAdminUserManagerDeleteServices
    {
        public Task DeleteUserManager(string id);
    }
}
