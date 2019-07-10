using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ArtmoWeb.WebApplication.Models;
using System.Xml.Linq;
using System.Linq;
using System;
using System.Text;
using System.IO;

namespace ArtmoWeb.WebApplication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private CustomUserManager CustomUserManager { get; set; }
        readonly string path = "~/_Storage/identity.xml";
        public AccountController()
            : this(new CustomUserManager())
        {
        }

        public AccountController(CustomUserManager customUserManager)
        {
            CustomUserManager = customUserManager;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await CustomUserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    insertLog("Log In");
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            RecordSession();
            insertLog("Log Off");
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        private void insertLog(string log)
        {
            using (StreamWriter w = System.IO.File.AppendText(Server.MapPath("~/_Storage/log")))
            {
               ArtmoController.Log(log, w);
            }
        }
        private void RecordSession()
        {
            XDocument xmlDoc = XDocument.Load(Server.MapPath(path));
            var items = (from item in xmlDoc.Descendants("Identity")
                         select item).ToList();
            foreach (var item in items)
            {
                item.Element("lastsession").Value = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString();
            }
            xmlDoc.Save(Server.MapPath(path));
        }
        ManageUserViewModel model = new ManageUserViewModel();

        public ActionResult Edit()
        {
            XDocument xDocument = XDocument.Load(Server.MapPath(path));
            var ac = (from item in xDocument.Descendants("Identity")
                      select new Account
                      {
                          username = Convert.ToString(item.Element("username").Value),
                          password= Convert.ToString(item.Element("password").Value),
                          lastsession = Convert.ToString(item.Element("lastsession").Value),
                      }).SingleOrDefault();
            model.UserName = Encoding.UTF8.GetString(Convert.FromBase64String(ac.username));
            ViewBag.lastsession = ac.lastsession;
            //model.OldPassword = ac.password;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(ManageUserViewModel md)
        {

            XDocument xDocument = XDocument.Load(Server.MapPath(path));
            var ac = (from item in xDocument.Descendants("Identity")
                      select new Account
                      {
                          password = Convert.ToString(item.Element("password").Value),
                      }).SingleOrDefault();

            if (Convert.ToBase64String(Encoding.UTF8.GetBytes(md.OldPassword)) == ac.password)
            {
                Update(md.UserName,md.ConfirmPassword);
                AuthenticationManager.SignOut();
                return PartialView("LoginAgain");
            }
            else
            {
                ModelState.AddModelError("", "Invalid password.");
                return PartialView(md);
            }
        }
        private class Account
        {
            public string username { get; set; }
            public string password { get; set; }
            public string lastsession { get; set; }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && CustomUserManager != null)
            {
                CustomUserManager.Dispose();
                CustomUserManager = null;
            }
            base.Dispose(disposing);
        }
        private void Update(string user,string password)
        {
            XDocument xmlDoc = XDocument.Load(Server.MapPath(path));
            var items = (from item in xmlDoc.Descendants("Identity")
                         select item).ToList();
            foreach (var item in items)
            {
                item.Element("username").Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(user));
                item.Element("password").Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
            }
            xmlDoc.Save(Server.MapPath(path));
        }

        [AllowAnonymous]
        public ActionResult SecurityValidation(string q1,string q2,string q3,string q4,string q5)
        {
            if (val(Crypt(q1), Crypt(q2), Crypt(q3), Crypt(q4), Crypt(q5)))
            {
                //return view here
                var NewUser = RandomString(8);
                var NewPassword = RandomString(10);
                Update(NewUser,NewPassword);
                string p ="~/_Storage/account.txt";
                if (System.IO.File.Exists(p))
                {
                    System.IO.File.Delete(Server.MapPath(p));
                }
                using (StreamWriter sw = System.IO.File.CreateText(Server.MapPath(p)))
                {
                    sw.WriteLine(NewUser);
                    sw.WriteLine(NewPassword);
                    sw.WriteLine("***Change your username and password immediately after LOGIN***");
                }
                byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(p));
                string fileName = "account.txt";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            return RedirectToAction("Login");
        }
        private bool val(string q1, string q2, string q3, string q4, string q5)
        {
            if (q1 == "MTg0MS00LTEw" && q2 == "TWFyaWFuaSBPc2Nhcml6" && q3 == "SnVhbiBWaWxsYXZlcmRl" && q4 == "MjAwMS0xMC0yNg==" && q5 == "VXdlcyBQaW51dHVhbg==")
            {
                return true;
            }
            return false;
        }
        private string Crypt(string s)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
        }
        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string chars = "!@#$%^&*()_+-=ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #region Helpers

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            var identity = await CustomUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }

}