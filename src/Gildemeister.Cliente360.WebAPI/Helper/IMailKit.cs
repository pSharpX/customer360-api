using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.WebAPI
{
    public interface IMailKit
    {
        Task SendEmailAsync(string toName, string toEmailAddress, string subject, string bodyHtml, string bodyText, int retryCount = 4);

        Task SendEmailAsync(string FromAddress, string FromAdressTitle, string ToAddress, string ToAdressTitle, string Subject, string bodyHtml, string BodyContent);

        Task SendMultipleEmailAsync(string FromAddress, string FromAdressTitle, string ToAddress, string ToAdressTitle, string Subject, string bodyHtml, string BodyContent);
    }
}
