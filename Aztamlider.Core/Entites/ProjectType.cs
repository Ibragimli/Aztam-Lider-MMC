using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Core.Entites
{
    public class ProjectType : BaseEntity
    {
        public string NameAz { get; set; }
        public string NameEn { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
