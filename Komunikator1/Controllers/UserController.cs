using Komunikator1.Models;
using Komunikator1.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Komunikator1.Controllers
{
    public class UserController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: User
        public ActionResult UserProfile()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View(db.Users.Find(userId));
            }
        }
        [HttpPost]
        public ActionResult UpdatePicture(PictureVM obj)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }else
            {

                User user = db.Users.Find(userId);

                var file = obj.Picture;

                if (file != null)
                {
                    string extension = Path.GetExtension(file.FileName);
                    string idAndExtension = userId + extension;
                    string imageUrl = "~/Profile Images/" + idAndExtension;
                    user.ImageUrl = imageUrl;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();

                    var path = Server.MapPath("~/Profile Images/");
                    if (Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (System.IO.File.Exists(path + idAndExtension))
                    {
                        System.IO.File.Delete(path + idAndExtension);

                    }

                    file.SaveAs(path + idAndExtension);
                }
                    return RedirectToAction("UserProfile");

                
            }
        }
    }
}