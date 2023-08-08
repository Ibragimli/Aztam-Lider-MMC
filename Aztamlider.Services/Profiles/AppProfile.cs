
using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<ContactUsCreateDto, ContactUs>();
            CreateMap<DocumentCreateDto, Document>();
            CreateMap<MainSliderCreateDto, MainSlider>();
            CreateMap<PartnerCreateDto, Partner>();
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<ReferenceCreateDto, Reference>();
            CreateMap<ServiceTypeCreateDto, ServiceType>();
            CreateMap<ServiceCreateDto, Service>();
            CreateMap<LanguageBaseCreateDto, LanguageBase>();
            CreateMap<ServiceNameCreateDto, ServiceName>();


        }
    }
}
