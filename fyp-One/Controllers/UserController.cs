using fyp_One.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace fyp_One.Controllers
{
    public class UserController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public UserController()
        {
            //asd
        }

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        // GET: User
        private db_fypEntities db = new db_fypEntities();
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "User");
        }
        public ActionResult LogIn()
        {
            if (Session["User"] == null)
            {
                return View();

            }
            string type = ((fyp_One.Models.User)Session["User"]).type;
            if (type.Equals("admin")) {
                return RedirectToAction("Profile", "Dashboard");
            }
            return RedirectToAction("Index", "Home");


        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "User", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(UserLogInVM model)
        {
            if (ModelState.IsValid)
            {

                var user = db.Users.Where(u => u.email.Equals(model.LogInEmail) && u.password.Equals(model.LogInPassword)).SingleOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("", "Login Failed with given Information");
                }
                else
                {
                    Session["User"] = user;
                    if (user.type.Equals("admin"))
                    {
                        return RedirectToAction("Profile", "Dashboard");
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            UserIndex userIndexModel = new UserIndex();
            userIndexModel.logIn = model;
            ViewBag.activeTab = "login";

            return View("Index", userIndexModel);


        }
        public ActionResult SignUp()
        {
            if (Session["User"] == null)
            {
                return View();

            }
            else { return RedirectToAction("Index", "Home"); }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserSignUpVM model)
        {
            if (Session["User"] == null)
            {
                if (ModelState.IsValid)
                {

                    var email = db.Users.Where(e => e.email.ToLower().Equals(model.SignUpEmail.ToLower())).SingleOrDefault();
                    if (email != null)
                    {
                        ModelState.AddModelError("SignUpEmail", "Email already exists. Please enter a different Email.");
                    }
                    if (ModelState.IsValid)
                    {
                        User user = new User
                        {   
                            first_name=model.FirstName,
                            last_name=model.LastName,
                            email = model.SignUpEmail.ToLower(),
                            password = model.SignUpPassword,
                            status = true
                        };
                        db.Users.Add(user);
                        db.SaveChanges();
                        Session["User"] = user;

                        return RedirectToAction("Index", "Home");

                    }

                }

            }
            else
            {

                return RedirectToAction("Index", "Home");
            }
            UserIndex userIndexModel = new UserIndex();
            userIndexModel.signUp = model;
            ViewBag.activeTab = "signup";
            return View("Index", userIndexModel);
        }

        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                UserIndex model = new UserIndex();
                return View(model);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}