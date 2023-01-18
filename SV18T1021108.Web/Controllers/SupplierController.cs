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
    /// controller nhà cung cấp
    /// </summary>
    [Authorize]
    [RoutePrefix("supplier")]
    public class SupplierController : Controller
    {

        #region CRUD
        /// <summary>
        /// lấy ra danh sách người nhà cung cấp
        /// </summary>
        /// <param name="page">số trang</param>
        /// <param name="searchValue">thông tin cần tìm kiếm</param>
        /// <returns></returns>
        public ActionResult Index()    
        {
            Models.PaginationSearchImput model = Session["SUPPLIER_SEARCH"] as Models.PaginationSearchImput;

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
            var data = CommonDataService.ListOfSuppliers(input.Page, input.PageSize, input.SearchValue, out rowCount);

            Models.SupplierPaginationResultModel model = new Models.SupplierPaginationResultModel
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                SearchValue = input.SearchValue,
                Data = data
            };

            Session["SUPPLIER_SEARCH"] = input;

            return View(model);
        }

        /// <summary>
        /// tạo mới một nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung Nhà cung cấp";
            Supplier model = new Supplier()
            {
                SupplierID = 0
            };
            return View(model);

        }

        /// <summary>
        /// chỉnh sửa một nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        [Route("edit/{supplierID}")]
        public ActionResult Edit(string supplierID)
        {
            int id = 0;

            try
            {
                id = Convert.ToInt32(supplierID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Cập nhật thông tin Nhà cung cấp";

            Supplier model = CommonDataService.GetSupplier(id);

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
        public ActionResult Save(Supplier model)
        {
            //kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(model.SupplierName))
                ModelState.AddModelError("SupplierName", "tên nhà cung cấp không được để trống");

            if (string.IsNullOrWhiteSpace(model.ContactName))
                ModelState.AddModelError("ContactName", "tên giao dịch không được để trống");

            if (string.IsNullOrWhiteSpace(model.Address))
                ModelState.AddModelError("Address", "địa chỉ không được để trống");

            if (string.IsNullOrWhiteSpace(model.City))
                model.City = "";

            if (string.IsNullOrWhiteSpace(model.PostalCode))
                model.PostalCode = "";

            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError("Phone", "số điện thoại không được để trống");

            if (string.IsNullOrWhiteSpace(model.Country))
                ModelState.AddModelError("Country", "tên thành phố không được để trống");

            //nếu dữ liệu đầu vào không hợp lệ trả lại giao điện nhận
            //ModelState giúp ta kiểm soát dc lỗi
            if (!ModelState.IsValid)
            {
                if (model.SupplierID > 0)
                    ViewBag.Title = "Cập nhật thông tin nhà cung cấp";
                else
                    ViewBag.Title = "bổ sung thông tin nhà cung cấp";
                return View("Create", model);
            }

            if (model.SupplierID > 0)
                CommonDataService.UpdateSupplier(model);
            else
                CommonDataService.AddSupplier(model);

            Session["SUPPLIER_SEARCH"] = new Models.PaginationSearchImput()
            {
                Page = 1,
                PageSize = 10,
                SearchValue = model.SupplierName
            };

            return RedirectToAction("Index");
        }

        /// <summary>
        /// xóa một nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        [Route("delete/{supplierID}")]
        public ActionResult Delete(string supplierID)
        {
            int id = 0;

            try
            {
                id = Convert.ToInt32(supplierID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetSupplier(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion
    }
}