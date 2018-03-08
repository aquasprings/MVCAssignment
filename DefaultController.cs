using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();


        }
        [HttpPost]
        public ActionResult Login(ALogin_ a)
        {
            DB01TEST01Entities db = new DB01TEST01Entities();
            ObjectParameter x = new ObjectParameter("flag", 0);
            db.ALogin_sp(a.username, a.password, x);

            if(Convert.ToInt32(x.Value)==1)
            {
               // Response.Write("<script>alert('hello')</script>");
                TempData["user"] = 1;
                return RedirectToAction("Register");
            }
            else
            {
                Response.Write("<script>alert('bye')</script>");
            }

            return View();
        }

        public ActionResult ViewRegister()
        {
            DB01TEST01Entities db = new DB01TEST01Entities();
            List<claim> lst = new List<claim>();
            lst = db.claims.ToList();
            return View(lst);
        }



        public ActionResult Register(int id)
        {
            DB01TEST01Entities db = new DB01TEST01Entities();

            //List<claim> lst = new List<claim>();
            claim c = db.claims.Find(id);
            TempData["id"] = id;
            return View(c);
        }

        [HttpPost]
        public ActionResult Register(claim a)
        {
            DB01TEST01Entities db = new DB01TEST01Entities();
            int id = Convert.ToInt32(TempData["id"]);

            db.Claimupdate_sp(id, a.ClaimStaus, a.MobileNumber);

            return RedirectToAction("ViewRegister");
        }

        }
}
