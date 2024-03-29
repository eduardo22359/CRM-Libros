using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Infrastructure;

public class SmtpClientEmailService
{
    private string Displayname;
    private string Address;
    private string Host;
    private int Port;
    private string Username;
    private string Password;
    public SmtpClientEmailService(IConfigurationSection config)
    {
        Displayname = config["Displayname"];
        Address = config["Address"];
        Host = config["Host"];
        Port = Convert.ToInt16(config["Port"]);
        Username = config["Username"];
        Password = config["Password"];

    }
    public void SendEmail(string email, string subject, string message, bool isBodyHtml = true)
    {
        var client = new SmtpClient();
        client.Host = Host;
        client.Port = Port;
        client.UseDefaultCredentials = false;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new NetworkCredential(Username, Password);
        client.TargetName = "STARTTLS/smtp.office365.com";
        client.EnableSsl = true;

        var mail = new MailMessage();
        mail.From = new MailAddress(Address, Displayname);
        mail.Subject = subject;
        mail.IsBodyHtml = isBodyHtml;
        mail.Body = message;
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.SubjectEncoding = System.Text.Encoding.UTF8;

        mail.To.Add(email);

        try
        {
            client.Send(mail);
        }
        catch (Exception)
        {
            //Envía una excepción al log
            throw;
        }
    }
}