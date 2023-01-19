using Demo.DAL.Entites;
using System.Net.Mail;
using System.Net;

namespace Demo.PL.Helpers
{
    public static class EmailSettings
    {
        public static void Send(Email email)
        {
            var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("taghreed.ashraf593@gmail.com", "wdvqfqhoaotvgeic");
            smtp.Send("taghreed.ashraf593@gmail.com", email.To, email.Subject, email.Body);

        }
    }
}
