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
    public class TeamCreateDto
    {
        public string PositionAz { get; set; }
        public string PositionEn { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAz { get; set; }
        public string Name { get; set; }
        public int Row { get; set; }
        public IFormFile ImageFile { get; set; }

    }

    public class TeamCreateDtoValidator : AbstractValidator<TeamCreateDto>
    {
        public TeamCreateDtoValidator()
        {
            RuleFor(x => x.DescriptionEn).NotEmpty().WithMessage("Komanda təsviri  boş olmamalıdır.").MinimumLength(3).WithMessage("Komanda təsvirinin uzunluğu 3-dən az ola bilməz!").MaximumLength(450).WithMessage("Komanda təsvirinin  uzunluğu 450-dən böyük ola bilməz!");

            RuleFor(x => x.DescriptionAz).NotEmpty().WithMessage("Komanda təsviri  boş olmamalıdır.").MinimumLength(3).WithMessage("Komanda təsvirinin uzunluğu 3-dən az ola bilməz!").MaximumLength(450).WithMessage("Komanda təsvirinin  uzunluğu 450-dən böyük ola bilməz!");

            RuleFor(x => x.PositionAz).NotEmpty().WithMessage("Komanda vəzifəsi  boş olmamalıdır.").MinimumLength(3).WithMessage("Komanda vəzifəsinin uzunluğu 3-dən az ola bilməz!").MaximumLength(150).WithMessage("Komanda vəzifəsinin uzunluğu 150-dən böyük ola bilməz!");
            RuleFor(x => x.PositionEn).NotEmpty().WithMessage("Komanda vəzifəsi  boş olmamalıdır.").MinimumLength(3).WithMessage("Komanda vəzifəsinin uzunluğu 3-dən az ola bilməz!").MaximumLength(150).WithMessage("Komanda vəzifəsinin uzunluğu 150-dən böyük ola bilməz!");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Şəkil hissəsi boş olmamalıdır.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad hissəsi boş olmamalıdır.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Sıra boş olmamalıdır.");
            RuleFor(x => x.PositionAz).NotEmpty().WithMessage("Ad hissəsi  boş olmamalıdır.").MinimumLength(3).WithMessage("Ad hissəsinin uzunluğu 3-dən az ola bilməz!").MaximumLength(100).WithMessage("Ad hissəsinin uzunluğu 100-dən böyük ola bilməz!");
        }
    }
}
