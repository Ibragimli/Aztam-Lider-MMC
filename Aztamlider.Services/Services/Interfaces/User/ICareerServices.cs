﻿using Aztamlider.Services.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.User
{
    public interface ICareerServices
    {
        void CheckValue(CareerPostDto careerPostDto);
        Task SendCV(CareerPostDto careerPostDto);
    }
}
