using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Services.Interfaces.Area.LanguageBases;

namespace Aztamlider.Services.Services.Implementations.Area.LanguageBases
{
    public class LanguageBaseEditServices : ILanguageBaseEditServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public LanguageBaseEditServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<LanguageBaseEditDto> GetSearch(int Id)
        {
            var LanguageBase = await _unitOfWork.LanguageBaseRepository.GetAsync(x => x.Id == Id);
            if (LanguageBase == null)
                throw new ItemNotFoundException("Parametr tapılmadı");


            LanguageBaseEditDto LanguageBaseEditDto = new LanguageBaseEditDto
            {
                Id = LanguageBase.Id,
                Key = LanguageBase.Key,
                Value = LanguageBase.Value,
            };
            return LanguageBaseEditDto;
        }

        public async Task LanguageBaseEdit(LanguageBaseEditDto LanguageBaseEdit)
        {
            if (LanguageBaseEdit.Value == null)
                throw new ItemNotFoundException("Dəyər adı boş ola bilməz!");

            var lastLanguageBase = await _unitOfWork.LanguageBaseRepository.GetAsync(x => x.Id == LanguageBaseEdit.Id);

            if (lastLanguageBase == null)
                throw new ItemNotFoundException("Parametr tapilmadı!");

            if (LanguageBaseEdit.Value != null)
                lastLanguageBase.Value = LanguageBaseEdit.Value;


            lastLanguageBase.ModifiedDate = DateTime.UtcNow.AddHours(4);
            await _unitOfWork.CommitAsync();
        }

        public async Task<LanguageBaseEditDto> IsExists(int id)
        {
            var LanguageBaseExist = await _unitOfWork.LanguageBaseRepository.GetAsync(x => x.Id == id);
            if (LanguageBaseExist == null)
                throw new ItemNotFoundException("ERROR");
            LanguageBaseEditDto editDto = new LanguageBaseEditDto
            {
                Value = LanguageBaseExist.Value,
                Id = LanguageBaseExist.Id,
            };
            return editDto;
        }
    }
}
