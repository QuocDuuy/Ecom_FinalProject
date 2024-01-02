using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult _Review(int productId)
        {
            ViewBag.ProductId = productId;
            var item = new ReviewProduct();
            if(User.Identity.IsAuthenticated)
            {
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = userManager.FindByName(User.Identity.Name);
                if (user != null) 
                {
                    item.Email = user.Email;
                    item.FullName = user.FullName;
                    item.UserName = user.UserName;
                }
                return PartialView(item);
            }
            return PartialView();
        }

        [AllowAnonymous]
        /*        public ActionResult _Load_Review(int productId)
                {
                    var item = _db.Reviews.Where(x => x.ProductId == productId).OrderByDescending(x => x.Id).ToList();
                    ViewBag.Count = item.Count;
                    return PartialView();
                }*/
        public ActionResult _Load_Review(int productId)
        {
            // Assuming Review has an Id property which is an int
            var item = _db.Reviews
                           .Where(x => x.ProductId == productId)
                           .OrderByDescending(x => x.Id)
                           .ToList();

            // Assuming you are trying to get the count of reviews and assign it to ViewBag.Count
            ViewBag.Count = item.Count;

            // It seems like you are returning a PartialView without passing the data to it
            // You may want to pass the 'item' to the PartialView like this:
            return PartialView(item);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostReview(ReviewProduct req)
        {
            if (ModelState.IsValid)
            {
                req.CreateDate = DateTime.Now;
                _db.Reviews.Add(req);
                _db.SaveChanges();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }



    }
}