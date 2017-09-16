using fyp_One.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace fyp_One.Controllers
{
    public class HomeController : Controller
    {
        db_fypEntities db = new db_fypEntities();

        public ActionResult Index(string type)
        {
            //ViewBag.Venue_Id = new SelectList(db.VenueInfoes.Where(i => i.user_id == 3).ToList(), "venueInfo_Id", "venueType");
            //return View();
            var venueInfo = db.VenueInfoes.ToList();
           
                venueInfo = venueInfo.Where(x => x.venueType.Equals(type)).ToList();
            
          
            
            return View(venueInfo);

        }

        public ActionResult VenueSearch(string type, int from, int to, int capacity)
        {
            var venueInfo = db.VenueInfoes.ToList();
            if (!type.Equals("")) {
                venueInfo = venueInfo.Where(x => x.venueType.Equals(type)).ToList();
            }
            if (from > 0) {
                venueInfo = venueInfo.Where(x => x.venuePrice>=from).ToList();

            }
            if (to > 0) {
                venueInfo = venueInfo.Where(x => x.venuePrice <= to).ToList();
            }
            if (capacity > 0)
            {
                if (capacity == 1) {
                    venueInfo = venueInfo.Where(x => x.capacity <= 300).ToList();
                }
                else if (capacity == 2)
                {
                    venueInfo = venueInfo.Where(x => x.capacity >= 300 && x.capacity<=500).ToList();

                }
                else if (capacity == 3)
                {
                    venueInfo = venueInfo.Where(x => x.capacity >= 500 && x.capacity <= 800).ToList();
                }
                else if (capacity == 4)
                {
                    venueInfo = venueInfo.Where(x => x.capacity <= 800).ToList();
                }
            }
            return View(venueInfo);
        }
        public ActionResult Venue()
        {
            //ViewBag.Message = "Your application description page.";

            return View(db.VenueInfoes.Where(x=>x.verify!=3).ToList());
        }
        public ActionResult VenuBooking()
        {
            return View();
        }

        public ActionResult Cater()
        {
          // ViewBag.Message = "Cater page.";

            return View(db.Caters.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       

        }
}