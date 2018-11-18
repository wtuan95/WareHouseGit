﻿using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Warehouse.Models;
using Warehouse.Services.Interface;

namespace Warehouse.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IProvinceService _proviceService;
        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IProvinceService proviceService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _proviceService = proviceService;
        }

       

        public string UserId
        {
            get
            {
                return User.Identity.GetUserId();
            }
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

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {

            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : message == ManageMessageId.UpdateInfoSuccess ? "Cập nhật thông tin thành công"
                : "";

           
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(UserId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(UserId),
                Logins = await UserManager.GetLoginsAsync(UserId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(UserId)
            };
            ViewBag.ProvinceId = new SelectList(_proviceService.GetAll(), "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditInformation(UpdateInfoViewModel updateInfoViewModel)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser applicationUser = UserManager.FindById(UserId);
                applicationUser.Email = updateInfoViewModel.Email;
                applicationUser.FullName = updateInfoViewModel.FullName;
                applicationUser.PhoneNumber = updateInfoViewModel.PhoneNumber;
                applicationUser.Address = updateInfoViewModel.Address;
                await UserManager.UpdateAsync(applicationUser);
                return RedirectToAction("Index", new { message = ManageMessageId.UpdateInfoSuccess });

            }

            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(UserId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(UserId),
                Logins = await UserManager.GetLoginsAsync(UserId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(UserId)
            };
            return View("Index", model);
        }
        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

     

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

       
        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ChangePassword(ChangePasswordViewModel changepassword)
        {
            string errors = "";
            if (!ModelState.IsValid)
            {
                errors = Functions.GetAllErrorsPage(this.ModelState);
                return Json(new { status = 0, message = errors }, JsonRequestBehavior.AllowGet);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), changepassword.OldPassword, changepassword.NewPassword);
            if (result.Succeeded)
            {
                return Json(new { status = 1, message = "Success" }, JsonRequestBehavior.AllowGet);
            }
            AddErrors(result);
            errors = Functions.GetAllErrorsPage(this.ModelState);
            return Json(new { status = 0, message = errors }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> SetPassword(SetPasswordViewModel setpassword, string Id)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.RemovePasswordAsync(Id);
                if (result.Succeeded)
                {
                    result = await UserManager.AddPasswordAsync(Id, setpassword.NewPassword);
                    if (result.Succeeded)
                    {
                        return Json(new { status = 1, message = "Success" }, JsonRequestBehavior.AllowGet);
                    }
                }
                AddErrors(result);
            }
            string error = Functions.GetAllErrorsPage(ModelState);
            // If we got this far, something failed, redisplay form
            return Json(new { status = 0, message = error }, JsonRequestBehavior.AllowGet);
        }


        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
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

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            UpdateInfoSuccess,
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}