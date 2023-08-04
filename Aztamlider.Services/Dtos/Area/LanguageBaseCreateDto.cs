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
    public class LanguageBaseCreateDto
    {
        public string Key { get; set; }
        public string Value { get; set; }

    }
    public class LanguageBaseCreateDtoValidator : AbstractValidator<LanguageBaseCreateDto>
    {
        public LanguageBaseCreateDtoValidator()
        {
            RuleFor(x => x.Value).NotEmpty().WithMessage("Value hissəsi boş olmamalıdır.").MinimumLength(2).WithMessage("Value hissəsinin uzunluğu 2-dən az ola bilməz!").MaximumLength(50).WithMessage("Value hissəsinin uzunluğu 50-dən böyük ola bilməz!");
            RuleFor(x => x.Value).NotEmpty().WithMessage("Value hissəsi boş olmamalıdır.").MinimumLength(2).WithMessage("Value hissəsinin uzunluğu 2-dən az ola bilməz!").MaximumLength(5000).WithMessage("Value hissəsinin uzunluğu 5000-dən böyük ola bilməz!");

        }
    }
}
