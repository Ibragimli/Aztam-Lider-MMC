using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Partners;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Partners
{
    public class AdminMainSliderCreateServices : IAdminPartnerCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminMainSliderCreateServices(IUnitOfWork unitOfWork, IMapper mapper, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _manageImageHelper = manageImageHelper;
        }


        public async Task<Partner> CreatePartner(PartnerCreateDto PartnerCreateDto)
        {
            await DtoCheck(PartnerCreateDto);
            var Partner = _mapper.Map<Partner>(PartnerCreateDto);
            Partner.Image = _manageImageHelper.FileSave(PartnerCreateDto.ImageFile, "Partners");
            await _unitOfWork.PartnerRepository.InsertAsync(Partner);
            await _unitOfWork.CommitAsync();

            return Partner;
        }

        private async Task DtoCheck(PartnerCreateDto PartnerCreateDto)
        {

            if (PartnerCreateDto.ImageFile != null)
                _manageImageHelper.PosterCheck(PartnerCreateDto.ImageFile);
        }
    }
}
