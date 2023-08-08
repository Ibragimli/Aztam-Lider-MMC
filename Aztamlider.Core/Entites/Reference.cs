using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Core.Entites
{
    public class Reference : BaseEntity
    {
        public string Name { get; set; }
        public string Orderer { get; set; }
        public string Location { get; set; }
        public string BuildingType { get; set; }
        public int SquareMetr { get; set; }
        public string Date { get; set; }
        public bool Status { get; set; }
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public int ServiceNameId { get; set; }
        public ServiceName ServiceName { get; set; }
        public ICollection<ReferenceImage> ReferenceImages { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }
        [NotMapped]
        public IFormFile ReferenceImageFile { get; set; }
        [NotMapped]
        public List<int> ReferenceImagesIds { get; set; }

    }
}
