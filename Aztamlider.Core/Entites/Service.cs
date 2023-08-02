using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Core.Entites
{
    public class Service:BaseEntity
    {
        public string TitleAz { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionAz { get; set; }
        public string DescriptionEn { get; set; }
        public ICollection<ServiceImage> ServiceImages { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }
        [NotMapped]
        public IFormFile ServiceImageFile { get; set; }
        [NotMapped]
        public List<int> ServiceImagesIds { get; set; }
    }
}
