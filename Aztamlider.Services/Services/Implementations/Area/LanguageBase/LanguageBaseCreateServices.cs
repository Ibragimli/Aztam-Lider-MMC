using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.LanguageBases;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.LanguageBases
{
    public class LanguageBaseCreateServices : ILanguageBaseCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LanguageBaseCreateServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<LanguageBase> CreateLanguageBase(LanguageBaseCreateDto LanguageBaseCreateDto)
        {
            await DtoCheck(LanguageBaseCreateDto);
            var LanguageBase = _mapper.Map<LanguageBase>(LanguageBaseCreateDto);

            await _unitOfWork.LanguageBaseRepository.InsertAsync(LanguageBase);
            await _unitOfWork.CommitAsync();

            return LanguageBase;
        }

        private async Task DtoCheck(LanguageBaseCreateDto LanguageBaseCreateDto)
        {
            if (LanguageBaseCreateDto.Value == null)
            {
                throw new ItemNullException("Value qeyd edin!");
            }
            if (LanguageBaseCreateDto.Value?.Length < 2)
            {
                throw new ValueFormatExpception("Value uzunluğu minimum 2 ola bilər");
            }
            if (LanguageBaseCreateDto.Value?.Length > 5000)
            {
                throw new ValueFormatExpception("Value uzunluğu maksimum 5000 ola bilər");
            }
        }
    }
}
