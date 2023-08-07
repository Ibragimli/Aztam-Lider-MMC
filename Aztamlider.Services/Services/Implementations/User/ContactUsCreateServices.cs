using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.User;
using Aztamlider.Services.Services.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.User
{
    public class ContactUsCreateServices : IContactUsCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactUsCreateServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ContactUsCreate(ContactUsCreateDto contactUsCreateDto)
        {
            var contactus = _mapper.Map<ContactUs>(contactUsCreateDto);
            await _unitOfWork.ContactUsRepository.InsertAsync(contactus);
            await _unitOfWork.CommitAsync();
        }

        public Task EmailCheck(string email)
        {
            if (!EmailValidate(email))
            {
                throw new ItemFormatException("Zəhmət olmasa düzgün email daxil edin.");
            };
            return Task.CompletedTask;
        }



        public Task PhoneNumberCheck(string number)
        {
            PhoneNumberValidation(number);
            var filterNumber = PhoneNumberFilter(number);
            PhoneNumberPrefixValidation(filterNumber);
            return Task.CompletedTask;
        }
        private static bool EmailValidate(string emailAddress)
        {
            if (emailAddress != null)
            {
                string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                if (Regex.IsMatch(emailAddress, pattern)) return true;
            }
            return false;
        }
        private void PhoneNumberValidation(string phoneNumber)
        {
            if (phoneNumber != null)
            {
                if (Regex.IsMatch(phoneNumber, "[a-zA-Z]"))
                    throw new ItemFormatException("Nömrə yanlışdır");

                if (phoneNumber.Length > 15)
                    throw new ItemFormatException("Nömrə yanlışdır");

                if (!(phoneNumber.StartsWith("050") || phoneNumber.StartsWith("099") || phoneNumber.StartsWith("051") || phoneNumber.StartsWith("055") || phoneNumber.StartsWith("070") || phoneNumber.StartsWith("077") || phoneNumber.StartsWith("010")))
                    throw new ItemFormatException("Nömrənin prefiksi yanlışdır!");
            }
        }
        private void PhoneNumberPrefixValidation(string phoneNumber)
        {
            string phoneRegex = @"^(050|051|055|070|077|010|099)(\d{7})$";
            if (phoneNumber != null)
            {
                if (!Regex.IsMatch(phoneNumber, phoneRegex))
                    throw new ItemFormatException("Nömrə yanlışdır!");
            }
        }
        private string PhoneNumberFilter(string phoneNumber)
        {
            string number = "";
            if (phoneNumber != null)
            {
                string[] charsNumber = phoneNumber.Split('_', '-', ' ', '(', ')', ',', '.', '/', '?', '!', '+', '=', '|', '.');

                foreach (var item in charsNumber)
                {
                    number += item;
                }
                return number;

            }

            return phoneNumber;
        }

        public async Task ValuesCheck(ContactUsCreateDto contactUsCreateDto)
        {
            if (contactUsCreateDto.Email == null)
            {
                throw new ItemFormatException("Zəhmət olmasa email-i qeyd edin!");

            }
            if (contactUsCreateDto.Message == null)
            {
                throw new ItemFormatException("Zəhmət olmasa mesajınızı qeyd edin!");

            }
            if (contactUsCreateDto.Fullname == null)
            {
                throw new ItemFormatException("Zəhmət olmasa ad və soyadınızı qeyd edin!");

            }
            if (contactUsCreateDto.PhoneNumber == null)
            {
                throw new ItemFormatException("Zəhmət olmasa əlaqə nömrənizi qeyd edin!");

            }
        }
    }
}
