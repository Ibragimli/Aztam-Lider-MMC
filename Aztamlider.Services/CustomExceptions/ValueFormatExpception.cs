using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.CustomExceptions
{
    public class ValueFormatExpception : Exception
    {
        public ValueFormatExpception(string msg) : base(msg)
        {

        }
    }
}
