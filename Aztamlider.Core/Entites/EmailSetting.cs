using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Core.Entites
{
    public class EmailSetting : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
