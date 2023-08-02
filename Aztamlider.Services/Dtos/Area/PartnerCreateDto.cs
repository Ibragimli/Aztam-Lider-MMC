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
    public class PartnerCreateDto
    {
        public IFormFile ImageFile { get; set; }

    }
    public class PartnerCreateDtoValidator : AbstractValidator<PartnerCreateDto>
    {
        public PartnerCreateDtoValidator()
        {
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Şəkil hissəsi boş olmamalıdır.");
        }
    }
}
