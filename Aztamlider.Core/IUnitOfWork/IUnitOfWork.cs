using Aztamlider.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Core.IUnitOfWork
{
    public interface IUnitOfWork
    {
        ISettingRepository SettingRepository { get; }
        IEmailSettingRepository EmailSettingRepository { get; }
        IImageSettingRepository ImageSettingRepository { get; }
        IAppUserRepository AppUserRepository { get; }
        IContactUsRepository ContactUsRepository { get; }
        IDocumentRepository DocumentRepository { get; }
        IMainSliderRepository MainSliderRepository { get; }
        IPartnerRepository PartnerRepository { get; }
        IProjectRepository ProjectRepository { get; }
        IReferenceRepository ReferenceRepository { get; }
        IReferenceImageRepository ReferenceImageRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IServiceImageRepository ServiceImageRepository { get; }
        IServiceTypeRepository ServiceTypeRepository { get; }
        ILanguageBaseRepository LanguageBaseRepository { get; }
        ILoggerRepository LoggerRepository { get; }
        IServiceNameRepository ServiceNameRepository { get; }
        ITeamRepository TeamRepository { get; }
        ICareerRepository CareerRepository { get; }
        IProjectTypeRepository ProjectTypeRepository { get; }
        Task<int> CommitAsync();

    }
}
