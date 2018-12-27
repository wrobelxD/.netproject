using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Komunikator1.ViewModels;
using Komunikator1.Models;
using System.Data.Entity.Validation;

namespace Komunikator1.Controllers
{
    public class AccountController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: Account/Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // GET: Account/Register
        [HttpPost]
        public ActionResult Register(RegisterVMcs obj)
        {
            bool UserExist = db.Users.Any(x => x.Username == obj.Username);
            if (UserExist) // nazwa w uzyciu
            {
                ViewBag.UserNameMessage = "Nazwa zajęta!!!!";
                return View();

            }

            bool EmailExistis = db.Users.Any(y => y.Email == obj.Email);
            if (EmailExistis) // email w uzyciu
            {
                ViewBag.EmailMessage = "Email zajęty :( ";
                return View();

                // jesli email i haslo jest unikalne wtedy rejestrujemy
            }

            User u = new User();
            u.Username = obj.Username;
            u.Password = obj.Password;
            u.Email = obj.Email;
            u.ImageUrl = "";
            u.CreatedOn = DateTime.Now;

            db.Users.Add(u);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index", "ChatRoom");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
                ViewBag.Message = "Nie udalo sie";
                return View();
            }
            
                
        }

        // GET: Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // GET: Account/Login
        [HttpPost]
        public ActionResult Login(LoginVM obj)
        {
            bool exists = db.Users.Any(u => u.Username == obj.Username && u.Password == obj.Password);
            
            if (exists)
            {
                Session["UserId"] = db.Users.Single(x => x.Username == obj.Username).Id;
                Session["ImageUrl"] = db.Users.Single(x => x.Username == obj.Username).ImageUrl;
                return RedirectToAction("Index", "ChatRoom");

            }

            ViewBag.Message = "Zle dane";
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["UserId"] = 0;
            Session["ImageUrl"] = null;
            return RedirectToAction("Index", "Home");

        }
    }
}