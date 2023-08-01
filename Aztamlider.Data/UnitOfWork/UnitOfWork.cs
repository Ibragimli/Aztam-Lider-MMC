using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Data.Datacontext;
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
        //private IImageSettingRepository _imageSettingRepository;
        //private ISettingRepository _settingRepository;
        //private IEmailSettingRepository _emailSettingRepository;
        //private IAppUserRepository _userRepository;
        //private IContactUsRepository _contactUsRepository;


        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        //public IImageSettingRepository ImageSettingRepository => _imageSettingRepository = _imageSettingRepository ?? new ImageSettingRepository(_context);
        //public ISettingRepository SettingRepository => _settingRepository = _settingRepository ?? new SettingRepository(_context);
        //public IAppUserRepository AppUserRepository => _userRepository = _userRepository ?? new AppUserRepository(_context);
        //public ICameraRepository CameraRepository => _cameraRepository = _cameraRepository ?? new CameraRepository(_context);

        //public IContactUsRepository ContactUsRepository => _contactUsRepository = _contactUsRepository ?? new ContactUsRepository(_context);

        //public IEmailSettingRepository EmailSettingRepository => _emailSettingRepository = _emailSettingRepository ?? new EmailSettingRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
