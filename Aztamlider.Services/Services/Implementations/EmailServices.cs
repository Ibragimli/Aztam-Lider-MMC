using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace Aztamlider.Services.Services.Implementations
{
    public class EmailServices : IEmailServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Send(string to, string subject, string html)
        {

            var portStr = (await _unitOfWork.EmailSettingRepository.GetAsync(x => x.Key == "SmtpPort")).Value;
            int port = int.Parse(portStr);
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse((await _unitOfWork.EmailSettingRepository.GetAsync(x => x.Key == "SmtpEmail"))?.Value));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect((await _unitOfWork.EmailSettingRepository.GetAsync(x => x.Key == "SmtpHost"))?.Value, port, SecureSocketOptions.StartTls);
            smtp.Authenticate((await _unitOfWork.EmailSettingRepository.GetAsync(x => x.Key == "SmtpEmail"))?.Value, (await _unitOfWork.EmailSettingRepository.GetAsync(x => x.Key == "SmtpPassword"))?.Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public async Task Send(string to, string subject, BodyBuilder html)
        {
            var portStr = (await _unitOfWork.EmailSettingRepository.GetAsync(x => x.Key == "SmtpPort")).Value;
            int port = int.Parse(portStr);
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse((await _unitOfWork.EmailSettingRepository.GetAsync(x => x.Key == "SmtpEmail"))?.Value));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = html.ToMessageBody();

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect((await _unitOfWork.EmailSettingRepository.GetAsync(x => x.Key == "SmtpHost"))?.Value, port, SecureSocketOptions.StartTls);
            smtp.Authenticate((await _unitOfWork.EmailSettingRepository.GetAsync(x => x.Key == "SmtpEmail"))?.Value, (await _unitOfWork.EmailSettingRepository.GetAsync(x => x.Key == "SmtpPassword"))?.Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
