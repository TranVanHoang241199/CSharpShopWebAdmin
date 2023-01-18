using SV18T1021108.BusinessLayer;
using SV18T1021108.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021108.Web.Controllers
{
    /// <summary>
    /// người giao hàng
    /// </summary>
    [Authorize]
    [RoutePrefix("shipper")]
    public class ShipperController : Controller
    {
        #region CRUD
        /// <summary>
        /// lấy ra danh sách người giao hàng
        /// </summary>
        /// <param name="page">số trang</param>
        /// <param name="searchValue">thông tin cần tìm kiếm</param>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchImput model = Session["SHIPPER_SEARCH"] as Models.PaginationSearchImput;

            if (model == null)
            {
                model = new Models.PaginationSearchImput()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = ""
                };
            }

            return View(model);
        }

        public ActionResult Search(Models.PaginationSearchImput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfShippers(input.Page, input.PageSize, input.SearchValue, out rowCount);

            Models.ShipperPaginationResultModel model = new Models.ShipperPaginationResultModel
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                SearchValue = input.SearchValue,
                Data = data
            };

            Session["SHIPPER_SEARCH"] = input;

            return View(model);
        }

        /// <summary>
        /// tạo mới một người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung người giao hàng";
            Shipper model = new Shipper()
            {
                ShipperID = 0
            };
            return View(model);
        }

        /// <summary>
        /// chỉnh sửa một người giao hàng
        /// </summary>
        /// <param name="shipperID">id người giao hàng</param>
        /// <returns></returns>
        [Route("edit/{shipperID}")]
        public ActionResult Edit(string shipperID)
        {
            int id = 0;

            try
            {
                id = Convert.ToInt32(shipperID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Cập nhật thông tin người giao hàng";

            Shipper model = CommonDataService.GetShipper(id);
            if (model == null)
            {
                RedirectToAction("Index");
            }
            return View("Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Shipper model)
        {
            //kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(model.ShipperName))
                ModelState.AddModelError("ShipperName", "tên người giao hàng không được để trống");

            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError("Phone", "số điện thoại giao hàng không được để trống");

            //nếu dữ liệu đầu vào không hợp lệ trả lại giao điện nhận
            //ModelState giúp ta kiểm soát dc lỗi
            if (!ModelState.IsValid)
            {
                if (model.ShipperID > 0)
                    ViewBag.Title = "Cập nhật thông tin người giao hàng";
                else
                    ViewBag.Title = "bổ sung thông tin người giao hàng";
                return View("Create", model);
            }

            if (model.ShipperID > 0)
                CommonDataService.UpdateShipper(model);
            else
                CommonDataService.AddShipper(model);

            Session["SHIPPER_SEARCH"] = new Models.PaginationSearchImput()
            {
                Page = 1,
                PageSize = 10,
                SearchValue = model.ShipperName
            };

            return RedirectToAction("index");
        }

        /// <summary>
        /// xóa một người giao hàng
        /// </summary>
        /// <param name="shipperID">id người giao hàng</param>
        /// <returns></returns>
        [Route("delete/{shipperID}")]
        public ActionResult Delete(string shipperID)
        {
            int id = 0;

            try
            {
                id = Convert.ToInt32(shipperID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteShipper(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetShipper(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion
    }
}