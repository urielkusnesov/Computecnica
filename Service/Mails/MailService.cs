using Model;
using System;
using System.Net;
using System.Net.Mail;

namespace Service.Mails
{
    public class MailService : IMailService
    {
        public Result Send(Mail mail)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("computecnica.arg@gmail.com", "serv2018"),
                    EnableSsl = true
                };
                client.Send("computecnica.arg@gmail.com", "service@computecnica.com.ar", mail.Subject, mail.Body);
                return new Result { Object = mail };
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Result { Error = "No se pudo envíar la solicitud" };
            }
        }
    }
}
