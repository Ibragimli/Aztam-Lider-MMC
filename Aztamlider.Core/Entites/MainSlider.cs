using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Core.Entites
{
    public class MainSlider : BaseEntity
    {
        public string TitleAz { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionAz { get; set; }
        public string DescriptionEn { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
