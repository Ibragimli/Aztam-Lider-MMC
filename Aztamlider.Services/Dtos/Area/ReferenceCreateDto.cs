﻿using Aztamlider.Core.Entites;
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
    public class ReferenceCreateDto
    {
        public string Name { get; set; }
        public string Orderer { get; set; }
        public string LocationAz { get; set; }
        public string LocationEn { get; set; }
        public string BuildingTypeAz { get; set; }
        public string BuildingTypeEn { get; set; }
        public int SquareMetr { get; set; }
        public string Date { get; set; }
        public bool Status { get; set; }
        public int ServiceTypeId { get; set; }
        public int ServiceNameId { get; set; }
        public List<IFormFile> ImageFiles { get; set; }

    }
    public class ReferenceCreateDtoValidator : AbstractValidator<ReferenceCreateDto>
    {
        public ReferenceCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Referansın ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Referansın adının uzunluğu 3-dən az ola bilməz!").MaximumLength(150).WithMessage("Referansın adının uzunluğu 150-dən böyük ola bilməz!");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Şifarişçi adı boş olmamalıdır.").MinimumLength(3).WithMessage("Şifarişçi adının uzunluğu 3-dən az ola bilməz!").MaximumLength(100).WithMessage("Şifarişçi adının uzunluğu 100-dən böyük ola bilməz!");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Vaxt hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Vaxt hissəsinin uzunluğu 3-dən az ola bilməz!").MaximumLength(40).WithMessage("Vaxt hissəsinin uzunluğu 40-dən böyük ola bilməz!");
            RuleFor(x => x.LocationAz).NotEmpty().WithMessage("Ünvan hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Ünvan hissəsinin uzunluğu 3-dən az ola bilməz!").MaximumLength(120).WithMessage("Ünvan hissəsinin uzunluğu 120-dən böyük ola bilməz!");
            RuleFor(x => x.LocationEn).NotEmpty().WithMessage("Ünvan hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Ünvan hissəsinin uzunluğu 3-dən az ola bilməz!").MaximumLength(120).WithMessage("Ünvan hissəsinin uzunluğu 120-dən böyük ola bilməz!");
            RuleFor(x => x.BuildingTypeAz).NotEmpty().WithMessage("Xidmət sahəsi  boş olmamalıdır.").MinimumLength(3).WithMessage("Xidmət sahəsinin uzunluğu 3-dən az ola bilməz!").MaximumLength(150).WithMessage("Xidmət sahəsinin uzunluğu 150-dən böyük ola bilməz!");
            RuleFor(x => x.BuildingTypeEn).NotEmpty().WithMessage("Xidmət sahəsi  boş olmamalıdır.").MinimumLength(3).WithMessage("Xidmət sahəsinin uzunluğu 3-dən az ola bilməz!").MaximumLength(150).WithMessage("Xidmət sahəsinin uzunluğu 150-dən böyük ola bilməz!");
        }
    }
}
