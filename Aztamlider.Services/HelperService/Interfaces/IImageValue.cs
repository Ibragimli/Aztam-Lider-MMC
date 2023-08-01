using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.HelperService.Interfaces
{
    public interface IImageValue
    {
        public string ValueStr(string key);
        public int ValueInt(string key);
    }
}
