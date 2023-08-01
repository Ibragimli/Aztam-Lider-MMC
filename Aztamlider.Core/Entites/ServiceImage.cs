using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Core.Entites
{
    public class ServiceImage : BaseEntity
    {
        public string Image { get; set; }
        public int ReferenceId { get; set; }
        public Reference Reference { get; set; }
    }
}
