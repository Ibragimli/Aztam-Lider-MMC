using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Core.Entites
{
    public class Document : BaseEntity
    {
        public string NameAz { get; set; }
        public string NameEn { get; set; }
        public bool License { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string PDF { get; set; }
        [NotMapped]
        public IFormFile PDFFile { get; set; }
    }
}
