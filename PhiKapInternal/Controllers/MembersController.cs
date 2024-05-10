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
    public class MembersController : Controller
    {
        private readonly PktInternalContext _context;

        public MembersController(PktInternalContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            Console.WriteLine("hello world");
            if (hasPrivlege())
            {
                Console.WriteLine("found some members");
                var members = await _context.Members.ToListAsync();
                Console.WriteLine(members);
                return View(members);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Test
        public async Task<IActionResult> Test()
        {
            if (hasPrivlege())
            {
                Console.WriteLine("found some members");
                var members = await _context.Members.ToListAsync();
                var model = new Members_index();
                model.Members = members;
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string name = HttpContext.Session.GetString("User");
            var user = _context.Members.Where(x => x.Name == name).FirstOrDefault();

            if (hasPrivlege() || user.Id == id)
            {
                if (id == null || _context.Members == null)
                {
                    return NotFound();
                }

                var member = await _context.Members
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (member == null)
                {
                    return NotFound();
                }

                return View(member);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        // GET: Members/Create
        public IActionResult Create()
        {

            if (hasPrivlege())
            {
                var classes = GetClasses();
                ViewBag.Classes = new SelectList(classes);
                var positions = GetPositions();
                ViewBag.Positions = new SelectList(positions);
                var statuses = GetStatuses();
                ViewBag.Statuses = new SelectList(statuses);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Password,Status,Class,Position")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();

                emailSomething("jacksvoboda@live.com", "jacksvoboda24@gmail.com", "A new account has been created for you on the Phi Kap Internal site, with a username of " + member.Name + " and a passowrd of " +member.Password + ".");

                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string name = HttpContext.Session.GetString("User");
            var user = _context.Members.Where(x => x.Name == name).FirstOrDefault();

            if (hasPrivlege() || user.Id == id)
            {
                if (id == null || _context.Members == null)
                {
                    return NotFound();
                }

                var member = await _context.Members.FindAsync(id);
                if (member == null)
                {
                    return NotFound();
                }
                var classes = GetClasses();
                ViewBag.Classes = new SelectList(classes);
                var positions = GetPositions();
                ViewBag.Positions = new SelectList(positions);
                var statuses = GetStatuses();
                ViewBag.Statuses = new SelectList(statuses);
                return View(member);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Password,Status,Class,Position")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (hasPrivlege())
            {
                if (id == null || _context.Members == null)
                {
                    return NotFound();
                }

                var member = await _context.Members
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (member == null)
                {
                    return NotFound();
                }

                return View(member);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Members == null)
            {
                return Problem("Entity set 'PktInternalContext.Members'  is null.");
            }
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
            message.Subject = "A Phi Kap Internal Account has been created";
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
