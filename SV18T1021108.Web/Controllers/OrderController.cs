using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021108.Web.Controllers
{
    /// <summary>
    /// controller người giao hàng
    /// </summary>
    [Authorize]
    public class OrderController : Controller
    {
        #region CRUD
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Order()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
        #endregion
    }
}