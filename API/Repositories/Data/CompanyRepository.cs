using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class CompanyRepository : GenericRepository<Company, int>
    {
        public CompanyRepository(MyContext myContext) : base(myContext)
        {

        }

        public int SendInterview(string email)
        {
            string to = email; //To address    
            string from = "teguh.efriyanto@students.amikom.ac.id"; //From address    
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Reset Password ";
            message.Body = $"This email is used to send OTP for  resetting account password.\nHere is your OTP";
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            NetworkCredential basicCredential1 = new NetworkCredential("teguh.efriyanto@students.amikom.ac.id", "uziejsxxklcztwap");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            client.Send(message);

            return 1;
        }

        public int SendInterviewResult(string email)
        {
            string to = email; //To address    
            string from = "teguh.efriyanto@students.amikom.ac.id"; //From address    
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Reset Password ";
            message.Body = $"This email is used to send OTP for  resetting account password.\nHere is your OTP";
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            NetworkCredential basicCredential1 = new NetworkCredential("teguh.efriyanto@students.amikom.ac.id", "uziejsxxklcztwap");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            client.Send(message);

            return 1;
        }
    }
}
