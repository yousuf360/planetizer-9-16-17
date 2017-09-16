using fyp_One.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace fyp_One.Controllers
{
    public class DashboardController : Controller
    {
        db_fypEntities db = new db_fypEntities();
        // GET: Dashboard
        

        public static string SendMail(string from, string to, string subject, string body)
        {
            string commaDelimCCs = Resource.ToCCEmails;

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(from, to,
            subject, body);
            msg.IsBodyHtml = true;
            if (commaDelimCCs != "")
                msg.CC.Add(commaDelimCCs);

            System.Net.NetworkCredential cred = new
            System.Net.NetworkCredential(Resource.UserName, Resource.Password);
            System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient(Resource.SMTP, Resource.Port);
            mailClient.EnableSsl = true;
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = cred;
           // string attachmentFilename1 = @"C:\Users\BAGHAIRAT\Desktop\sumarBhaiEmail\AvanceraPresentation1.pptx";
            //if (attachmentFilename1 != null)
            //{
            //    Attachment attachment = new Attachment(attachmentFilename1, MediaTypeNames.Application.Octet);
            //    ContentDisposition disposition = attachment.ContentDisposition;
            //    disposition.CreationDate = File.GetCreationTime(attachmentFilename1);
            //    disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename1);
            //    disposition.ReadDate = File.GetLastAccessTime(attachmentFilename1);
            //    disposition.FileName = Path.GetFileName(attachmentFilename1);
            //    disposition.Size = new FileInfo(attachmentFilename1).Length;
            //    disposition.DispositionType = DispositionTypeNames.Attachment;
            //    msg.Attachments.Add(attachment);
            //}
            //string attachmentFilename2 = @"C:\Users\BAGHAIRAT\Desktop\sumarBhaiEmail\Company Profile and Portfolio.pdf";
            //if (attachmentFilename2 != null)
            //{
            //    Attachment attachment = new Attachment(attachmentFilename2, MediaTypeNames.Application.Octet);
            //    ContentDisposition disposition = attachment.ContentDisposition;
            //    disposition.CreationDate = File.GetCreationTime(attachmentFilename2);
            //    disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename2);
            //    disposition.ReadDate = File.GetLastAccessTime(attachmentFilename2);
            //    disposition.FileName = Path.GetFileName(attachmentFilename2);
            //    disposition.Size = new FileInfo(attachmentFilename2).Length;
            //    disposition.DispositionType = DispositionTypeNames.Attachment;
            //    msg.Attachments.Add(attachment);
            //}
            try
            {
                mailClient.Send(msg);
                return "Success";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static class Resource
        {
            public static string UserName = "faizan_khan999@live.com";
            public static string Password = "45223253.fak";
            // public static string SMTP = "smtp.zoho.com";
            public static string SMTP = "smtp.live.com";
            public static int Port = 587;
            public static string ToCCEmails = "";
        }


        [HttpPost]
        public ActionResult response(int id, int action) {

            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }

            db.VenueOrders.Find(id).Status = action.ToString();
            db.SaveChanges();
            var subject = "Planatizer booking confirmation";
            var to = db.VenueOrders.Find(id).Email;
            var body = "Vanue confirmation email.";
            Task sendEmailTask = new Task(delegate
            {
                SendMail("faizan_khan999@live.com", to, subject, body);
            });
            sendEmailTask.Start();
            return RedirectToAction("bookingList");
        }
        [HttpPost]
        public ActionResult responseCater(int id, int action)
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }

            db.CaterOrders.Find(id).Status = action.ToString();
            db.SaveChanges();
            var subject = "Planatizer booking confirmation";
            var to = db.CaterOrders.Find(id).Email;
            var body = "Order confirmation email.";
            Task sendEmailTask = new Task(delegate
            {
                SendMail("faizan_khan999@live.com", to, subject, body);
            });
            sendEmailTask.Start();
            return RedirectToAction("CaterbookingList");
        }

        public ActionResult Add()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }
          

            return View();
        }
        public ActionResult Profile()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }
            //string type = ((fyp_One.Models.User)Session["User"]).type;
            //if (!type.Equals("admin"))
            //{
            //    return Content("You Dont have Rights to access this page!");
            //}
            return View();
        }
        public ActionResult AddCater()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }
            return View();
        }
        public ActionResult VenueDate()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }
            int UserId = ((fyp_One.Models.User)Session["User"]).user_id;

            ViewBag.Venue_Id = new SelectList(db.VenueInfoes.Where(i => i.user_id == UserId).ToList(), "venueInfo_Id", "venueName");
            return View();
        }

        public ActionResult AddMenu()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }
            int UserId = ((fyp_One.Models.User)Session["User"]).user_id;
            return View();
        }
        public ActionResult MenuForm()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }
            int UserId = ((fyp_One.Models.User)Session["User"]).user_id;

            ViewBag.Cater_Id = new SelectList(db.Caters.Where(i => i.User_Id == UserId).ToList(), "Id", "CaterServiceName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MenuForm(MenuFormViewModel model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }
            int UserId = ((fyp_One.Models.User)Session["User"]).user_id;
            if (ModelState.IsValid)
            {
                Menu menu = new Menu
                {
                    Mname = model.mName,
                    Mprice = model.mPrice,
                    Cater_Id=model.Cater_Id
                };
                string[] ItemName = model.ItemName.Split(',');
                db.Menus.Add(menu);
                db.SaveChanges();
                foreach (var item in ItemName)
                {
                    if (item.Length > 0)
                    {
                        db.MenuItems.Add(new MenuItem

                        {
                            Menu_Id = menu.Id,
                            ItemName = item
                        });
                    }
                }


                db.SaveChanges();
               // ViewBag.Cater_Id = new SelectList(db.Caters.Where(i => i.User_Id == UserId).ToList(), "Id", "CaterServiceName");

                return RedirectToAction("MenuForm");
            }
            ViewBag.Cater_Id = new SelectList(db.Caters.Where(i => i.User_Id == UserId).ToList(), "Id", "CaterServiceName");
            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(DashBoardViewModel model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }
            int UserId = ((fyp_One.Models.User)Session["User"]).user_id;
            if (ModelState.IsValid)
            {
                VenueInfo venueinfo = new VenueInfo
                {
                    verify = 1,
                    venueName = model.venueName,
                    venueType = model.venueType,
                    capacity = Convert.ToInt32(model.capacity),
                    venueWebsite = model.venueWebsite,
                    venuePrice = Convert.ToInt32(model.venuePrice),
                    user_id = UserId
                };
                string[] features = model.venueFeature.Split(',');
                db.VenueInfoes.Add(venueinfo);
                db.SaveChanges();
                foreach(var item in features)
                {
                    if (item.Length > 0)
                    {
                        db.VenueFeatures.Add(new VenueFeature

                        {
                            Id_venueInfo = venueinfo.venueInfo_Id,
                            venuefeatures = item
                        });
                    }
                }
                db.SaveChanges();
                VenueAddress venueaddress = new VenueAddress
                {
                    vContactName = model.vContactName,
                    vCity = model.vCity,
                    vCountry = model.vCountry,
                    vStreetAddress = model.vStreetAddress,
                    vZipcode = Convert.ToInt32(model.vZipCode),
                    vContactnum1 = model.vContactNumber1,
                    vContactnum2 = model.vContactNumber2,
                    venueInfo_id = venueinfo.venueInfo_Id,
                    //,
                    //vlang = Convert.ToDecimal(model.vlang),
                    //vlong = Convert.ToDecimal(model.vlong)

                };
                db.VenueAddresses.Add(venueaddress);
                db.SaveChanges();
                string folderName = "venue-" + venueinfo.venueInfo_Id;
                string path = Server.MapPath("~/Uploads/" + folderName);
                Directory.CreateDirectory(path);
                int i = 1;
                foreach(var file in model.venuePhoto)
                {
                    if (file != null)
                    {
                        string extension = Path.GetExtension(file.FileName);
                        string namewithFullPath = Path.Combine(path + "/", i + extension);
                        file.SaveAs(namewithFullPath);
                        i++;
                    }
                }
                return RedirectToAction("Profile", "Dashboard");
            } 
            return View(model);
        }

        public ActionResult VenueList()
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }
            int UserId = ((fyp_One.Models.User)Session["User"]).user_id;
            string type = ((fyp_One.Models.User)Session["User"]).type;
            if(type.Equals("admin"))
            {
                return View("VenueListAdmin", db.VenueInfoes.ToList());
            }
            return View(db.VenueInfoes.Where(x=>x.user_id==UserId).ToList());
        }

        public ActionResult VenueEdit(int? id)
        {
            VenueEditVM vm = new VenueEditVM();
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            vm.venueinfo = db.VenueInfoes.Find(id);

            vm.venueaddress = db.VenueAddresses.Where(s => s.venueInfo_id == id).ToList();

            vm.venuefeature = db.VenueFeatures.Where(s => s.Id_venueInfo == id).ToList();

            
            return View(vm);
        }

        [HttpPost]
        public ActionResult VenueEdit(VenueEditVM vm)
        {
            if(ModelState.IsValid)

            {
                VenueInfo venueinfo = vm.venueinfo;
                //VenueFeature venuefeature = vm.venuefeature;


                db.Entry(vm.venueinfo).State = EntityState.Modified;
                db.SaveChanges();

                foreach (var item in vm.venueaddress)
                {
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();

                foreach (var item in vm.venuefeature)
                {
                    db.Entry(item).State = EntityState.Modified;
                }


                db.SaveChanges();
            }
            return RedirectToAction("VenueList");
        }

        public ActionResult CaterList()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }
            int UserId = ((fyp_One.Models.User)Session["User"]).user_id;
            return View(db.Caters.Where(x => x.User_Id == UserId).ToList());
        }

        public ActionResult CaterEdit(int? id)
        {

            CaterEditVM vm = new CaterEditVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            vm.cater = db.Caters.Find(id);

            vm.cateraddress = db.CaterAddres.Where(s => s.Cater_Id == id).ToList();

            return View(vm);
        }

        [HttpPost]
        public ActionResult CaterEdit(CaterEditVM vm)
        {
            if (ModelState.IsValid)

            {
                Cater cater = vm.cater;

                db.Entry(vm.cater).State = EntityState.Modified;
                db.SaveChanges();
                foreach(var item in vm.cateraddress)
                {
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();

            }
            return RedirectToAction("CaterList");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VenueDate(VenueDateViewModel model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }
            int UserId = ((fyp_One.Models.User)Session["User"]).user_id;

            if (ModelState.IsValid)
            {
                string[] dates = model.VenueDate.Split(',');
                foreach (var item in dates)
                {
                        db.VenueDates.Add(new VenueDate
                        {
                            Venue_Id = model.Venue_Id,
                            VenueDate1 = Convert.ToDateTime(item)
                        });
                    
                }
                db.SaveChanges();
                ViewBag.Venue_Id = new SelectList(db.VenueInfoes.Where(i => i.user_id == UserId).ToList(), "venueInfo_Id", "venueName");
                return View();
            }

            ViewBag.Venue_Id = new SelectList(db.VenueInfoes.Where(i => i.user_id == UserId).ToList(), "venueInfo_Id", "venueName");
            return View(model);
        }
          

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCater(AddCaterViewModel model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }
            int UserId = ((fyp_One.Models.User)Session["User"]).user_id;

            if (ModelState.IsValid)
            {
                Cater cater = new Cater
                {
                    CaterServiceName = model.CaterServiceName,
                    User_Id = UserId
                };
                db.Caters.Add(cater);
                db.SaveChanges();

                CaterAddre cateraddress = new CaterAddre
                {
                    cStreetAddress = model.cStreetAddress,
                    cCity = model.cCity,
                    cCountry = model.cCountry,
                    cZipCode = model.cZipCode,
                    cContactNumber = model.cContactNumber,
                    cContactNumber2 = model.cContactNumber2,
                    Cater_Id = cater.Id

                };
                db.CaterAddres.Add(cateraddress);
                db.SaveChanges();
                return RedirectToAction("AddMenu");
            }
                return View(model);

        }

        public ActionResult VenueVerification(int venueID, string task)
        {
            if (Session["User"] == null)
            {
                return Content("login");
            }
            string type = ((fyp_One.Models.User)Session["User"]).type;

            if (!type.Equals("admin"))
            {
                return Content("no rights");
            }

            if (task.Equals("verify"))
            {
                db.VenueInfoes.Find(venueID).verify=2;
            }
            else if (task.Equals("reject"))
                {
                db.VenueInfoes.Find(venueID).verify=3;
            }
            db.SaveChanges();
            return Content("success");
        }
        public ActionResult BookingList()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }
            int UserId = ((fyp_One.Models.User)Session["User"]).user_id;
            string type = ((fyp_One.Models.User)Session["User"]).type;
            var venueIds = db.VenueInfoes.Where(x => x.user_id == UserId).Select(x => x.venueInfo_Id).ToList();
            return View(db.VenueOrders.Where(x=>venueIds.Contains(x.Venue_Id)).ToList());
        }

        public ActionResult CaterBookingList()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("index", "User");
            }
            int UserId = ((fyp_One.Models.User)Session["User"]).user_id;
            string type = ((fyp_One.Models.User)Session["User"]).type;
            var MenuIds = db.Menus.Where(x => x.Cater_Id == UserId).Select(x => x.Id).ToList();
            return View(db.CaterOrders.Where(x => MenuIds.Contains(x.Menu_Id)).ToList());
        }

        public ActionResult AddNewVenue()
        {
            return View();
        } 
        //public ActionResult AddNewVenueCreate(string name, string type, Int32 capacity, string venueDetail, string website, Int32 price)
        //{
        //    try
        //    {
        //        Console.WriteLine();
        //        VenueInfo newVenue = new VenueInfo
        //        {
        //            venueDetail = venueDetail.Replace(".", ", "),
        //            venueName = name,
        //            //user_id = Convert.ToInt32(Session["User_ID"].ToString()),
        //            venueType = type,
        //            capacity = capacity,
        //            venuePrice = price,
        //            venueWebsite = website

        //        };
        //        if (ModelState.IsValid)
        //        {
        //            db.VenueInfoes.Add(newVenue);
        //            db.SaveChanges();
        //            return Content("yes");
        //        }
        //        else
        //        {
        //            return Content("no");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Content("You have already inserted this venue.");
        //    }
        //}
    }
}