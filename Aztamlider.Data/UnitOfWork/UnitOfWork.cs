using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Core.Repositories;
using Aztamlider.Data.Datacontext;
using Aztamlider.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IImageSettingRepository _imageSettingRepository;
        private ISettingRepository _settingRepository;
        private IEmailSettingRepository _emailSettingRepository;
        private IAppUserRepository _userRepository;
        private IContactUsRepository _contactUsRepository;
        private IDocumentRepository documentRepository;
        private ITeamRepository _teamRepository;
        private IServiceImageRepository serviceImageRepository;
        private IServiceRepository serviceRepository;
        private IServiceTypeRepository serviceTypeRepository;
        private IReferenceRepository referenceRepository;
        private IReferenceImageRepository referenceImageRepository;
        private IProjectRepository projectRepository;
        private IPartnerRepository partnerRepository;
        private IMainSliderRepository mainSliderRepository;
        private ILoggerRepository _loggerRepository;
        private ILanguageBaseRepository _languageBaseRepository;
        private IServiceNameRepository _serviceNameRepository;


        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IImageSettingRepository ImageSettingRepository => _imageSettingRepository = _imageSettingRepository ?? new ImageSettingRepository(_context);
        public ISettingRepository SettingRepository => _settingRepository = _settingRepository ?? new SettingRepository(_context);
        public IAppUserRepository AppUserRepository => _userRepository = _userRepository ?? new AppUserRepository(_context);

        public IContactUsRepository ContactUsRepository => _contactUsRepository = _contactUsRepository ?? new ContactUsRepository(_context);

        public IEmailSettingRepository EmailSettingRepository => _emailSettingRepository = _emailSettingRepository ?? new EmailSettingRepository(_context);

        public IDocumentRepository DocumentRepository => documentRepository = documentRepository ?? new DocumentRepository(_context);

        public IMainSliderRepository MainSliderRepository => mainSliderRepository = mainSliderRepository ?? new MainSliderRepository(_context);

        public IPartnerRepository PartnerRepository => partnerRepository = partnerRepository ?? new PartnerRepository(_context);
        public IProjectRepository ProjectRepository => projectRepository = projectRepository ?? new ProjectRepository(_context);

        public IReferenceRepository ReferenceRepository => referenceRepository = referenceRepository ?? new ReferenceRepository(_context);

        public IReferenceImageRepository ReferenceImageRepository => referenceImageRepository = referenceImageRepository ?? new ReferenceImageRepository(_context);

        public IServiceRepository ServiceRepository => serviceRepository = serviceRepository ?? new ServiceRepository(_context);

        public IServiceImageRepository ServiceImageRepository => serviceImageRepository = serviceImageRepository ?? new ServiceImageRepository(_context);

        public IServiceTypeRepository ServiceTypeRepository => serviceTypeRepository = serviceTypeRepository ?? new ServiceTypeRepository(_context);

        public ILanguageBaseRepository LanguageBaseRepository => _languageBaseRepository = _languageBaseRepository ?? new LanguageBaseRepository(_context);

        public ILoggerRepository LoggerRepository => _loggerRepository = _loggerRepository ?? new LoggerRepository(_context);

        public IServiceNameRepository ServiceNameRepository => _serviceNameRepository = _serviceNameRepository ?? new ServiceNameRepository(_context);

        public ITeamRepository TeamRepository => _teamRepository = _teamRepository ?? new TeamRepository(_context);


        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
