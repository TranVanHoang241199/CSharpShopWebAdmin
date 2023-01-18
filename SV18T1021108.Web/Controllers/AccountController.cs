using SV18T1021108.BusinessLayer;
using SV18T1021108.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021108.Web.Controllers
{
    /// <summary>
    /// Quy định controller ở chế độ Authorize
    /// tức user chưa đăng nhập => login
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        #region CRUD
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Xử lý yêu cầu đăng nhập
        /// </summary>
        /// <param name="username">tài khoản</param>
        /// <param name="password">mật khẩu</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]

        public ActionResult Login(string username, string password)
        {
            //TODO: kiểm tra tài khoản mật khẩu vs DB
            if (CommonDataService.LoginAccount(username, password))
            {
                //Ghi lại cookie phiên đăng nhập
                System.Web.Security.FormsAuthentication.SetAuthCookie(username, false);
                //Quay về trang chủ
                return RedirectToAction("Index", "Home");
            }

            ViewBag.UserName = username;
            ViewBag.Message = "Đăng nhập thất bại";

            return View();
        }

        /// <summary>
        /// đăng xuất
        /// </summary>
        /// <returns></returns>
        ///
        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            AccountPaginationResultModel model = new AccountPaginationResultModel();
            model.UserName = User.Identity.Name;
            
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(AccountPaginationResultModel model)
        {

            if(!string.IsNullOrEmpty(model.PasswordOld)&&!CommonDataService.InPasswordAccount(model.UserName, model.PasswordOld))
                ModelState.AddModelError("PasswordOld", "mật khẩu không đúng");

            if(string.IsNullOrEmpty(model.PasswordOld))
                ModelState.AddModelError("PasswordOld", "vui lòng nhập mật hiện tai");

            if (string.IsNullOrEmpty(model.Password))
                ModelState.AddModelError("Password", "nhập mật khẩu cần thây đổi");

            if (string.IsNullOrEmpty(model.PasswordAgain))
                ModelState.AddModelError("passwordAgain", "nhập lại mật khẩu đã thây đổi");

            if (model.Password != model.PasswordAgain 
                && !string.IsNullOrEmpty(model.Password) 
                && !string.IsNullOrEmpty(model.PasswordAgain))
                ModelState.AddModelError("pass", "mật khẩu không khớp");

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            if(CommonDataService.InUserNameACcount(model.UserName))
                CommonDataService.UpdatePassword(model.UserName, model.PasswordAgain);
            else
            {
                ViewBag.Message = "Đăng nhập thất bại";
            }
            
            return Logout();
        }

        #endregion
    }
}