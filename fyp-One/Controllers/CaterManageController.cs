using fyp_One.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fyp_One.Controllers
{
    public class CaterManageController : Controller
    {
        // GET: CaterManage
        //public ActionResult Index()
        //{
        //    return View();
        //}

        db_fypEntities db = new db_fypEntities();

        public ActionResult menu(int id)
        {

            return View(db.Caters.Find(id));
        }

        public ActionResult menuOrder(int id)
        {
            var CaterOrder = new CaterOrderViewModel();
            CaterOrder.Menu_Id = id;
            return View(CaterOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CaterOrder(CaterOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                CaterOrder Caterorder = new CaterOrder
                {
                    Name = model.Name,
                    ContactNo = model.ContactNo,
                    Email = model.Email,
                    Guests = model.Guests,
                    EventDate = model.EventDate,
                    Menu_Id = model.Menu_Id,
                    Status = "1"
                };
                db.CaterOrders.Add(Caterorder);
                db.SaveChanges();
                return RedirectToAction("Cater", "Home");
            }

            return View(model);

        }
    }
}