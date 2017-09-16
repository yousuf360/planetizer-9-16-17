using fyp_One.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fyp_One.Controllers
{
    public class VenueManageController : Controller
    {
        // GET: VenueManage
        public ActionResult Index()
        {

            return View();
        }
        db_fypEntities db = new db_fypEntities();


        public ActionResult CheckDate(int venueId, DateTime date)
        {

            var data = db.VenueDates.Where(x => x.Venue_Id == venueId && x.VenueDate1 == date).SingleOrDefault();
            var data2 = db.VenueOrders.Where(x => x.Venue_Id == venueId && x.EventDate == date && x.Status.Equals("2")).SingleOrDefault();
            
            if (data2 == null && data==null) {
                return Content("free");
            }
            return Content("occupied");
        }

        public ActionResult Rate(int? venueId, decimal rate)
        {
            if (Session["User"] == null)
            {
                return Content("login");
            }
            int UserId =((fyp_One.Models.User)Session["User"]).user_id;

            venue_rate venue_rating = db.venue_rate.Where(x => x.user_Id == UserId && x.venue_Id == venueId).SingleOrDefault();
            if (venue_rating != null)
            {
                venue_rating.rate = rate;
                db.Entry(venue_rating).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.venue_rate.Add(new venue_rate
                {
                    venue_Id = venueId.Value,
                    user_Id = UserId,
                    rate = rate
                });
                db.SaveChanges();
            }
           
            return Content("success");
        }
        public ActionResult book(int id) {

            return View(db.VenueInfoes.Find(id));
        }
        
        public ActionResult VenueOrder(int v_id,DateTime date)
        {
            var venueOrder = new VenueOrderViewModel();
            venueOrder.Venue_Id = v_id;
            venueOrder.EventDate = date;
            return View(venueOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VenueOrder( VenueOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                VenueOrder venueorder = new VenueOrder
                {
                    Name = model.Name,
                    ContactNo = model.ContactNo,
                    Email = model.Email,
                    Guests = model.Guests, 
                    EventDate = model.EventDate,
                    Venue_Id = model.Venue_Id,
                    Status="1"
                };
                db.VenueOrders.Add(venueorder);
                db.SaveChanges();
                return RedirectToAction("Venue", "Home");
            }
           
            return View(model);

        }
    }
}