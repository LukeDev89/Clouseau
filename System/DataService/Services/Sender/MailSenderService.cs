using DataModel.Request;
using System.Net.Mail;
using System.Net;
using DataContext.Interfaces.Management;

namespace DataService.Services.Sender
{
    public class MailSenderService
    {
        private readonly ISystemConfigRepository _systemConfigRepository;

        public MailSenderService(ISystemConfigRepository systemConfigRepository)
        {
            _systemConfigRepository = systemConfigRepository;
        }

        public async Task SendMail(MailSender request)
        {
            var from = (await _systemConfigRepository.GetByKeyAsync("Email")).DataValue;
            var password = (await _systemConfigRepository.GetByKeyAsync("EmailPassword")).Name;
            var smtpAddress = (await _systemConfigRepository.GetByKeyAsync("Smtp")).Name;
            var port = int.Parse((await _systemConfigRepository.GetByKeyAsync("SmtpPort")).Name);
            var fromAddress = new MailAddress(from, from.Split('@')[0]);

            var subject = request.Subject;
            var body = request.Body;

            foreach (var to in request.To)
            {
                var toAddress = new MailAddress(to, to.Split('@')[0]);
                var smtp = new SmtpClient
                {
                    Host = smtpAddress,
                    Port = port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, password)
                };

                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}
