using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LawyerService.BL.Helpers
{
    public class MailService
    {
        public static async Task<bool> SendAsync(IConfiguration config, string body, string subject, string to)
        {
            try
            {
                MailMessage mailMessage = new MailMessage()
                {
                    Body = body,
                    Subject = subject,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);
                await SendMailAsync(mailMessage, config);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static async Task SendMailAsync(MailMessage mailMessage, IConfiguration config)
        {
            // Email админа:
            var credentialUserName = config.GetSection("Email")["MailAccount"];
            var pwd = config.GetSection("Email")["MailPassword"];

            mailMessage.From = new MailAddress(credentialUserName);

            var client = new SmtpClient(config.GetSection("Email")["SMTPClient"])
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(credentialUserName, pwd),
                EnableSsl = false,
                Timeout = 60000 // 60 секунд
            };

            await client.SendMailAsync(mailMessage);
        }
    }
}