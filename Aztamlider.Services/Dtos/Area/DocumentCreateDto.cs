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
    public class DocumentCreateDto
    {
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
        public IFormFile PDFFile { get; set; }

    }
    public class CreatePostDtoValidator : AbstractValidator<DocumentCreateDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Sənədin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Sənədin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(150).WithMessage("Sənədin adının uzunluğu 150-dən böyük ola bilməz!");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Şəkil hissəsi boş olmamalıdır.");
            RuleFor(x => x.PDFFile).NotEmpty().WithMessage("PDF hissəsi boş olmamalıdır.");
        }
    }
}
