﻿using Aztamlider.Services.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.HelperService.Interfaces
{
    public interface ILoggerServices
    {
        public Task LoggerCreate(string controller, string action, string name, string role, string? product = "-", string? username = "-");

    }
}
