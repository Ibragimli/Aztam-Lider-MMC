using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Dtos.Area
{
    public class LanguageBaseEditDto
    {
        public string Value { get; set; }
        public string Key { get; set; }
        public int Id { get; set; }

        public class LanguageBaseEditDtoValidator : AbstractValidator<LanguageBaseEditDto>
        {
            public LanguageBaseEditDtoValidator()
            {
                RuleFor(x => x.Value).NotEmpty().WithMessage("boş olmamalıdır.").MaximumLength(750);
            }
        }
    }
}
