
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Infrastructure.Common
{
    public class MailSenderService : IEmailService
    {
        #region Fields
        private readonly EmailConfiguration _emailSettings;
        #endregion

        private readonly IConfiguration _configuration;
        public MailSenderService(EmailConfiguration emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task<string> SendAsync(SendEmailDto request)
        {
            try
            {
                var builder = new BodyBuilder();
                var email = new MimeMessage();


                email.Sender = MailboxAddress.Parse(_emailSettings.Email);
                email.Subject = request.Subject;
                builder.HtmlBody = request.MessageBody;
                email.Body = builder.ToMessageBody();

                email.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Email));


                if (request.ToEmails.Any())
                    foreach (var mailAddress in request.ToEmails)
                        email.To.Add(MailboxAddress.Parse(mailAddress));
                else
                    email.To.Add(MailboxAddress.Parse(request.MailTo));



                if (request.EmailCC.Any())
                {
                    InternetAddressList internetAddresses = new InternetAddressList();

                    foreach (var cc in request.EmailCC)
                        internetAddresses.Add(InternetAddress.Parse(cc));

                    email.Cc.AddRange(internetAddresses);
                }


                if (request.EmailBCC.Any())
                {
                    InternetAddressList internetAddresses = new InternetAddressList();

                    foreach (var cc in request.EmailBCC)
                        internetAddresses.Add(InternetAddress.Parse(cc));

                    email.Bcc.AddRange(internetAddresses);
                }



                if (request.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in request.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using var ms = new MemoryStream();
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();

                            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }

                if (request.Priority == "High")
                    email.Priority = MessagePriority.Urgent;

                else if (request.Priority == "Low")
                    email.Priority = MessagePriority.NonUrgent;

                else
                    email.Priority = MessagePriority.Normal;





                using var smtp = new SmtpClient();


                await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);

                await smtp.AuthenticateAsync(_emailSettings.Email, _emailSettings.Password);

                var result = await smtp.SendAsync(email);

                await smtp.DisconnectAsync(true);

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Can't send email right now";
        }




        #region Custom
        #endregion Custom

    }
}
