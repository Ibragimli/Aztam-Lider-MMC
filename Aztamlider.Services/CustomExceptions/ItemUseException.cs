using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.CustomExceptions
{
    public class ItemUseException : Exception
    {
        public ItemUseException(string msg) : base(msg)
        {

        }
    }
}
