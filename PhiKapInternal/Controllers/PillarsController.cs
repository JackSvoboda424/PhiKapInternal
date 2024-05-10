using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhiKapInternal.Models.PKT_Internal;
using PhiKapInternal.Models.Members;
using System;
using System.Net;
using System.Net.Mail;

namespace PhiKapInternal.Controllers
{
    public class PillarsController : Controller
    {
        private readonly PktInternalContext _context;

        public PillarsController(PktInternalContext context)
        {
            _context = context;
        }

        // GET: Pillars/Intellectual
        public async Task<IActionResult> Intellectual()
        {
            return View();
        }

        public async Task<IActionResult> Social()
        {
            return View();
        }

        public async Task<IActionResult> Leadership()
        {
            return View();
        }

        public async Task<IActionResult> Fraternal()
        {
            return View();
        }

        public async Task<IActionResult> Spiritual()
        {
            return View();
        }


        private bool MemberExists(int id)
        {
          return (_context.Members?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public List<String> GetStatuses()
        {
            return new List<String>{ "Active", "Associate", "On Probation", "Suspended"};
        }
        public List<String> GetClasses()
        {
            return new List<String>{ "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa", "Lambda", "Mu", "Nu", "Xi", "Omicron", "Pi", "Rho", "Sigma", "Tau",
            "Upsilon", "Phi", "Chi", "Psi", "Omega"};
        }

        public List<String> GetPositions()
        {
            return new List<String>{ "President", "Risk Manager", "Leadership", "Fraternal", "Social", "Spiritual", "Intellectual", "Secretary", "Treasurer", "Recruitment", "AME", "AFR", "House Manager", "Kitchen Manager"
                };
        }

        public bool hasPrivlege() // there will probably need to be more nunace added to this. Not every position needs this privilege
        {
            string name = HttpContext.Session.GetString("User");
            if (name != null)
            {
                var user = _context.Members.Where(x => x.Name == name).FirstOrDefault();

                if (user.Position != null && user.Position != "")
                {
                    return true;
                }
            }
            return false;

        }

        public void emailSomething(string fromEmail, string toEmail, string body)
        {
            MailMessage message = new MailMessage(fromEmail, toEmail);
            message.Subject = "A Phi Kap Internal Account has been created for you";
            message.Body = body;

            // Create an instance of the SmtpClient class
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com"); // Change to your SMTP server address

            // Set SMTP server port and credentials
            smtpClient.Port = 587; // Use 587 for TLS
            smtpClient.Credentials = new NetworkCredential(fromEmail, "trains11!ns");

            // Enable SSL/TLS encryption
            smtpClient.EnableSsl = true;

            try
            {
                // Send the email
                smtpClient.Send(message);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email: " + ex.Message);
            }
            finally
            {
                // Dispose of the SmtpClient and MailMessage objects
                smtpClient.Dispose();
                message.Dispose();
            }

        }
    }
}
