using Aztamlider.Services.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.User
{
    public interface IContactUsCreateServices
    {
        public Task ContactUsCreate(ContactUsCreateDto contactUsCreateDto);
        public Task PhoneNumberCheck(string number);
        public Task EmailCheck(string email);
        public Task ValuesCheck(ContactUsCreateDto contactUsCreateDto);
    }
}
