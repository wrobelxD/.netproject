using Komunikator1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Komunikator1.ViewModels;

namespace Komunikator1.Controllers
{
    public class ChatRoomController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: ChatRoom
        public ActionResult Index()
        {
            var comments = db.Comments.Include(x => x.Replies).OrderByDescending(x => x.CreatedOn).ToList();
            return View(comments);
        }

        [HttpPost]

        public ActionResult PostReply(ReplyVM obj)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            if(userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            Reply reply = new Reply();
            reply.Text = obj.Reply;
            reply.CommentId = obj.CID;
            reply.UserId = userId;
            reply.CreatedOn = DateTime.Now;
            db.Replies.Add(reply);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PostComment(string CommentText)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            Comment com = new Comment();
            com.Text = CommentText;
            com.CreatedOn = DateTime.Now;
            com.UserId = userId;
            db.Comments.Add(com);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}