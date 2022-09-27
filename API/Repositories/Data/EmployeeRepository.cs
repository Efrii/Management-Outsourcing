using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class EmployeeRepository : GenericRepository<Employee, int>
    {
        MyContext myContext;

        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public bool CekEmailEmployee(string email)
        {
            var data = myContext.Employees.FirstOrDefault(x => x.Email_employee == email);

            return data != null;
        }

        public bool CekEmailCompany(string email)
        {
            var data = myContext.Companys.FirstOrDefault(x => x.Email_company == email);

            return data != null;
        }

        // Send Job Mapping
        public int JobMapping(JobMappingVM jobMapping)
        {
            string HtmlJobMapping = $"<html lang='en'><body><center><img src='https://i.ibb.co/rGBhT6f/logo.png' width='250' style='display:block' class='CToWUd' data-bit='iit'><hr></center><p>Dear Kak <b> {jobMapping.Name_employe} </b></p><p>Dengan ini kami informasikan bahwa anda telah selesai dalam porgram <b>Metrodata Coding Camp</b>, untuk itu Kami dari team Operasional ADD 2, ingin menginformasikan kepada anda untuk dapat segera mengirimkan Curriculum Vitae (CV) terbaru dengan format dan contoh CV yang terdapat di lampiran email ini.</p><p>Adapun Ketentuan dalam mengirimkan CV dan pengisian data dapat di lihat di note di email ini, dan proses pengisisan di lakukan di website Management Resource.</p><b>Note :</b><table><tr><td style='width:150px'>Work Assigment 1</td><td>:</td><td><b>Jr Application Developer</b></td></tr><tr><td>Perusahaan Client</td><td>:</td><td><b>{jobMapping.Company_name}</b></td></tr><tr><td>Posis</td><td>:</td><td><b>{jobMapping.Job_position}</b></td></tr></table><br><p><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans - serif;color:#1f497d'>Terima Kasih <b> I </b> Best Regards <br>{jobMapping.Name_operasional} </span><span lang='IN' style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>.</span><b><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>I Outsource Operations Management</span></b><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'><br><b>PT.Mitra Integrasi Informatika –</b>member of METRODATA<br>APL Tower</span><span lang='IN' style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>18</span><sup><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>th</span></sup><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>Fl., Jl.Letjend.S.Parman kav. 28, Jakarta 11470</span></p></body></html>";

            string to = jobMapping.Email;  
            string from = "outsourcingmcc@gmail.com";   
            MailMessage message = new MailMessage(from, to);
            message.Subject = "JOB MAPPING MCC " + jobMapping.Name_employe;
            message.Body = HtmlJobMapping;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            var attachmentPDF = new Attachment("Attachment/CV-MII-Example.pdf", MediaTypeNames.Application.Pdf);
            var attachmentDOCX = new Attachment("Attachment/CV-MII-Template.docx", MediaTypeNames.Application.Octet);
            message.Attachments.Add(attachmentPDF);
            message.Attachments.Add(attachmentDOCX);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);    
            NetworkCredential basicCredential1 = new NetworkCredential("outsourcingmcc@gmail.com", "zlhwpduzupwabwhn");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            client.Send(message);


            return 1;
        }

        // Send Schedule Interview To Employee MCC
        public int SendInterview(SendInterviewVM interviewVM)
        {
            string HtmlString = $"<html lang='en'><body><center><img src='https://i.ibb.co/rGBhT6f/logo.png' width='250' style='display:block' class='CToWUd' data-bit='iit'><hr></center><p>Dear Kak <b> {interviewVM.Name_employee}</b></p><p>Sehubungan dengan CV yang anda kirimkan ke bagian Operasional atau TIM ADD 2, kami ingin mengundang saudara untuk melakukan interview User dengan Perusahaan Client, adapun jadwal dan informasi lainnya dapat anda lihat di note email ini.</p><b>Note :</b><table><tr><td style='width:150px'>Perusahaan</td><td>:</td><td><b>{interviewVM.Name_company}</b></td></tr><tr><td>Posis</td><td>:</td><td><b>{interviewVM.Job_position}</b></td></tr><tr><td>Interviewer</td><td>:</td><td><b>{interviewVM.Interviewer_name}</b></td></tr><tr><td>Tanggal Interview</td><td>:</td><td><b>{interviewVM.Date_interview}</b></td></tr><tr><td>URL Interview</td><td>:</td><td><a href='{interviewVM.Url_Intertview}'><b>{interviewVM.Url_Intertview}</b></a></td></tr></table><br><p><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans - serif;color:#1f497d'>Terima Kasih<b>I</b>Best Regards<br>{interviewVM.Name_operasional}</span><span lang='IN' style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>.</span><b><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>I Outsource Operations Management</span></b><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'><br><b>PT.Mitra Integrasi Informatika –</b>member of METRODATA<br>APL Tower</span><span lang='IN' style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>18</span><sup><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>th</span></sup><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>Fl., Jl.Letjend.S.Parman kav. 28, Jakarta 11470</span></p></body></html>";

            string to = interviewVM.Email_employee;
            string from = "outsourcingmcc@gmail.com"; 
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Undangan Interview " + interviewVM.Name_employee ;
            message.Body = HtmlString;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); 
            NetworkCredential basicCredential1 = new NetworkCredential("outsourcingmcc@gmail.com", "zlhwpduzupwabwhn");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            client.Send(message);

            return 1;
        }

        // Send Result Interview With Company To Employee MCC
        public int SendInterviewResult(SendInterviewVM interviewVM)
        {
            string HtmlString = $"<html lang='en'><body><center><img src='https://i.ibb.co/rGBhT6f/logo.png' width='250' style='display:block' class='CToWUd' data-bit='iit'><hr></center><p>Dear Kak <b> {interviewVM.Name_employee} </b></p><p>Sehubungan dengan interview yang saudara laksanakan sebelumnya kami dari bagian Operasional atau TIM ADD 2 ingin menginformasikan hasil dari intervewi yang telah di kirimkan oleh perusahaan client, adapaun hasil dapat saudara lihat di note email ini.</p><b>Note :</b><table><tr><td style='width:150px'>Perusahaan</td><td>:</td><td><b>{interviewVM.Name_company}</b></td></tr><tr><td>Posis</td><td>:</td><td><b>{interviewVM.Job_position}</b></td></tr><tr><td>Interviewer</td><td>:</td><td><b>{interviewVM.Interviewer_name}</b></td></tr><tr><td>Tanggal Interview</td><td>:</td><td><b>{interviewVM.Date_interview}</b></td></tr><tr><td>Hasil Interview</td><td>:</td><td><b>{interviewVM.Hasil_interview}</b></td></tr></table><br><p><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans - serif;color:#1f497d'>Terima Kasih<b>I</b>Best Regards <br>{interviewVM.Name_operasional}</span><span lang='IN' style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>.</span><b><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>I Outsource Operations Management</span></b><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'><br><b>PT.Mitra Integrasi Informatika –</b>member of METRODATA<br>APL Tower</span><span lang='IN' style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>18</span><sup><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>th</span></sup><span style='font-size:10pt;line-height:107%;font-family:Arial Narrow,sans-serif;color:#1f497d'>Fl., Jl.Letjend.S.Parman kav. 28, Jakarta 11470</span></p></body></html>";

            string to = interviewVM.Email_employee;     
            string from = "outsourcingmcc@gmail.com";
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Hasil Interview " + interviewVM.Name_employee;
            message.Body = HtmlString;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);    
            NetworkCredential basicCredential1 = new NetworkCredential("outsourcingmcc@gmail.com", "zlhwpduzupwabwhn");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            client.Send(message);

            return 1;
        }

        public List<UserRole> GetEmployeByRole(string role)
        {
            var data = myContext.UserRoles
                .Include(x => x.User.Employees)
                .Where(x => x.Role.Name_role == role)
                .ToList();

            return data;
        }

        public List<Employee> GetDetailClass(int id)
        {
            var data = myContext.Employees
                .Include(x => x.Class)
                .Where(x => x.Class.Id_class == id)
                .ToList();

            return data;
        }

        public List<GetTrainnerVM> GetTrainner(int id)
        {
            var data = from a in myContext.Employees
                       join b in myContext.Employees
                       on a.Id_trainner equals b.Id_employee
                       where a.Id_employee == id
                       select new GetTrainnerVM { Name_trainner = b.Name_employee };

            return data.ToList();
        }

    }
}
