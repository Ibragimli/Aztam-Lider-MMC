using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Core.IUnitOfWork
{
    public interface IUnitOfWork
    {
        //ISettingRepository SettingRepository { get; }
        //IEmailSettingRepository EmailSettingRepository { get; }
        //IImageSettingRepository ImageSettingRepository { get; }
        //IAppUserRepository AppUserRepository { get; }
        //IContactUsRepository ContactUsRepository { get; }
        Task<int> CommitAsync();

    }
}
