using Aztamlider.Core.Entites;
using Aztamlider.Services.Helper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Dtos.Area
{
    public class ProjectTypeCreateDto
    {
        public string NameAz { get; set; }
        public string NameEn { get; set; }

    }
    public class ProjectTypeCreateDtoValidator : AbstractValidator<ProjectTypeCreateDto>
    {
        public ProjectTypeCreateDtoValidator()
        {
            RuleFor(x => x.NameAz).NotEmpty().WithMessage("Ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Ad hissəsinin uzunluğu 3-dən az ola bilməz!").MaximumLength(150).WithMessage("Ad hissəsinin uzunluğu 150-dən böyük ola bilməz!");
            RuleFor(x => x.NameEn).NotEmpty().WithMessage("Ad  hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Ad hissəsinin uzunluğu 3-dən az ola bilməz!").MaximumLength(150).WithMessage("Ad hissəsinin uzunluğu 150-dən böyük ola bilməz!");

        }
    }
}

