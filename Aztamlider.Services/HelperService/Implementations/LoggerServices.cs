using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Dtos.User;
using Aztamlider.Services.HelperService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.HelperService.Implementations
{
    public class LoggerServices : ILoggerServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoggerServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task LoggerCreate(string controller, string action,  string name, string role, string? product = "salam")
        {
            LoggerPostDto newDto = new LoggerPostDto()
            {
                Action = action,
                Controller = controller,
                Name = name,
                Role = role,
                Product = product,
            };

            await LoggerCreater(newDto);
        }
        private async Task LoggerCreater(LoggerPostDto loggerPostDto)
        {
            var logger = _mapper.Map<Logger>(loggerPostDto);
            await _unitOfWork.LoggerRepository.InsertAsync(logger);
            await _unitOfWork.CommitAsync();
        }


    }
}
