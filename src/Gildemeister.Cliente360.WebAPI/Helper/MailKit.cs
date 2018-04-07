using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using NLog;
using System;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.WebAPI
{
    public class MailKit : IMailKit
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        private readonly IOptions<EmailSettings> settings;
      

        public MailKit(IOptions<EmailSettings> settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toName"></param>
        /// <param name="toEmailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="bodyHtml"></param>
        /// <param name="bodyText"></param>
        /// <param name="retryCount"></param>
        public async Task SendEmailAsync(string toName, string toEmailAddress, string subject,
            string bodyHtml, string bodyText, int retryCount = 4)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(string.Empty, string.Empty));

            message.To.Add(new MailboxAddress(toName, toEmailAddress));

            message.Subject = subject;
            var builder = new BodyBuilder
            {
                TextBody = bodyText,
                HtmlBody = bodyHtml
            };
            message.Body = builder.ToMessageBody();

            for (var count = 1; count <= retryCount; count++)
            {
                try
                {
                    using (var client = new SmtpClient())
                    {
                        client.LocalDomain = settings.Value.PrimaryDomain;
                        client.Authenticate(settings.Value.Username, settings.Value.Password);

                        SecureSocketOptions secureSocketOptions;

                        if (!Enum.TryParse(null, out secureSocketOptions))
                        {
                            secureSocketOptions = SecureSocketOptions.Auto;
                        }


                        await client.ConnectAsync(settings.Value.PrimaryDomain, settings.Value.PrimaryPort, secureSocketOptions).ConfigureAwait(false);
                        await client.SendAsync(message).ConfigureAwait(false);
                        await client.DisconnectAsync(true).ConfigureAwait(false);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    if (retryCount >= 0)
                    {
                        throw ex;
                    }
                    await Task.Delay(count * 1000);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FromAddress"></param>
        /// <param name="FromAdressTitle"></param>
        /// <param name="ToAddress"></param>
        /// <param name="ToAdressTitle"></param>
        /// <param name="Subject"></param>
        /// <param name="BodyContent"></param>

        public async Task SendEmailAsync(string FromAddress, string FromAdressTitle, string ToAddress,
            string ToAdressTitle, string Subject, string bodyHtml, string BodyContent)
        {
            logger.Info(":::: Enviar correo electronico");

            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
                mimeMessage.Subject = Subject;


                var builder = new BodyBuilder
                {
                    TextBody = BodyContent,
                    HtmlBody = bodyHtml
                };

                mimeMessage.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    SecureSocketOptions secureSocketOptions;

                    if (!Enum.TryParse(null, out secureSocketOptions))
                    {
                        secureSocketOptions = SecureSocketOptions.Auto;
                    }
                    await client.ConnectAsync(settings.Value.PrimaryDomain, settings.Value.PrimaryPort, secureSocketOptions);
                    // Note: only needed if the SMTP server requires authentication 
                    // Error 5.5.1 Authentication  
                    await client.AuthenticateAsync(settings.Value.Username, settings.Value.Password);
                    await client.SendAsync(mimeMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                    client.Disconnect(true);
                    logger.Info(":::: Enviar correo electronico finzalizado");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Listar error applicacion :: : " + ex.Message);
                throw ex;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="FromAddress"></param>
        /// <param name="FromAdressTitle"></param>
        /// <param name="ToAddress"></param>
        /// <param name="ToAdressTitle"></param>
        /// <param name="Subject"></param>
        /// <param name="bodyHtml"></param>
        /// <param name="BodyContent"></param>
        /// <returns></returns>
        public async Task SendMultipleEmailAsync(string FromAddress, string FromAdressTitle, string ToAddress,
            string ToAdressTitle, string Subject, string bodyHtml, string BodyContent)
        {
            logger.Info("Enviar correo  :: : " + FromAddress + " " + FromAdressTitle);
            try
            {
                

                var mimeMessage = new MimeMessage();


                string[] emailAdress = ToAddress.Split(',');
                mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));

                foreach (string item in emailAdress)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, item));
                    }
                }

                mimeMessage.Subject = Subject;
                var builder = new BodyBuilder
                {
                    TextBody = BodyContent,
                    HtmlBody = bodyHtml
                };

                mimeMessage.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    SecureSocketOptions secureSocketOptions;

                    if (!Enum.TryParse(null, out secureSocketOptions))
                    {
                        secureSocketOptions = SecureSocketOptions.Auto;
                    }

                    await client.ConnectAsync(settings.Value.PrimaryDomain, settings.Value.PrimaryPort, secureSocketOptions);

                    await client.AuthenticateAsync(settings.Value.Username, settings.Value.Password);
                    await client.SendAsync(mimeMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                    client.Disconnect(true);

                }
                logger.Info("finalizado correo  :: : " + FromAddress + " " + FromAdressTitle);
            }
            catch (Exception ex)
            {
                logger.Error("Listar error applicacion :: : " + ex.Message);
                throw ex;
            }
        }
    }
}
