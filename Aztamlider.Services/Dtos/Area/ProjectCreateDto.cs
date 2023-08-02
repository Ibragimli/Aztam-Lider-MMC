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
    public class ProjectCreateDto
    {
        public string TitleAz { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionAz { get; set; }
        public string DescriptionEn { get; set; }
        public IFormFile ImageFile { get; set; }

    }
    public class ProjectCreateDtoValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateDtoValidator()
        {
            RuleFor(x => x.TitleAz).NotEmpty().WithMessage("Başlıq hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Başlıq hissəsinin uzunluğu 3-dən az ola bilməz!").MaximumLength(120).WithMessage("Başlıq hissəsinin uzunluğu 120-dən böyük ola bilməz!");
            RuleFor(x => x.TitleEn).NotEmpty().WithMessage("Başlıq  hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Başlıq hissəsinin uzunluğu 3-dən az ola bilməz!").MaximumLength(120).WithMessage("Başlıq hissəsinin uzunluğu 120-dən böyük ola bilməz!");
            RuleFor(x => x.DescriptionAz).NotEmpty().WithMessage("Təsvir hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Təsvir hissəsinin uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Təsvir hissəsinin uzunluğu 3000-dən böyük ola bilməz!");
            RuleFor(x => x.DescriptionEn).NotEmpty().WithMessage("Təsvir hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Təsvir hissəsinin uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Təsvir hissəsinin uzunluğu 3000-dən böyük ola bilməz!");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Şəkil hissəsi boş olmamalıdır.");
        }
    }
}
