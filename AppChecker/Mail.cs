using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;

namespace AppChecker
{
    public class Mail
    {
        private readonly string[] to = ConfigurationManager.AppSettings["to"].Split(',');

        internal void Send(List<ApplicationModel> application)
        {
            string body = "<h1>Application Checker Results ([Host])</h1>";
            body += "<table style=\"color: #333333; backgroud-color: #000000; width: 80%;\" border=\"1\" cellspacing=\"1\" cellpadding=\"4\">";
            body += "<thead style=\"color: white; background-color: #5d7b9d; font-weight: bold;\"><tr><td>Application Name</td><td><p>Executable</p></td><td>State</td></tr></thead>";
            body += "<tbody>";
            foreach (ApplicationModel item in application)
            {
                body += String.Format("<tr {3}><td>{0}</td><td>{1}</td><td>{2}</td></tr>", item.name, item.process, item.State,item.State==State.Stopped? "style=\"background-color: #b50707; color: #ffffff;\"":"");
            }
            body += "</tbody></table>";
            body += "<p><strong>For remove application Check File:</strong></p>";
            body += "<pre>Config.json</pre>";

            MailMessage message = new MailMessage();
             message.Body = body;
            message.Subject = "Application Status";
            foreach (string item in to)
            {
                message.To.Add(item);
            }
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Send(message);
        }

    }
}
