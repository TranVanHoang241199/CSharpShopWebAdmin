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
    /// controller khách hàng
    /// </summary>
    [Authorize]
    [RoutePrefix("customer")]
    public class CustomerController : Controller
    {
        #region CRUD
        /// <summary>
        /// hiển thị giao diện
        /// </summary></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            /*int pageSize = 10;
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomers(page, pageSize, searchValue, out rowCount);

            Models.CustomerPaginationResultModel model = new Models.CustomerPaginationResultModel
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = data
            };*/

            /* 
             var model = CommonDataService.ListOfCategories(page, pageSize, searchValue, out rowCount);

             int pageCount = rowCount / pageSize;
             if (rowCount % pageSize > 0)
                 pageCount += 1;

             ViewBag.RowCount = rowCount;
             ViewBag.PageCount = pageCount;
             ViewBag.SearchValue = searchValue;
             ViewBag.Page = page;*/

            //trong session luôn lưu điều kiện tìm kiếm vừa thực hiện
            Models.PaginationSearchImput model = Session["CUSTOMER_SEARCH"] as Models.PaginationSearchImput;

            if(model == null)
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
            var data = CommonDataService.ListOfCustomers(input.Page, input.PageSize, input.SearchValue, out rowCount);

            Models.CustomerPaginationResultModel model = new Models.CustomerPaginationResultModel
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                SearchValue = input.SearchValue,
                Data = data
            };

            Session["CUSTOMER_SEARCH"] = input;

            return View(model);
        }

        /// <summary>
        /// Add thêm khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung khách hàng";
            Customer model = new Customer()
            {
                CustomerID = 0
            };
            return View(model);
        }

        /*[Route("edit1/{idKH}")]
        public ActionResult Edit1(int idKH = 0)
        {
            Console.Write(idKH);
            ViewBag.Title = "Khoa123456";
            return View("Create");
        }*/

        /// <summary>
        /// sửa khách hàng
        /// </summary>
        /// <param name="customerID">id khách hàng</param>
        /// <returns></returns>
        [Route("edit/{customerID}")]
        public ActionResult Edit(string customerID)
        {
            int id = 0;

            try
            {
                id = Convert.ToInt32(customerID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Cập nhật thông tin khách hàng";

            Customer model = CommonDataService.GetCustomer(id);

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
        public ActionResult Save(Customer model)
        {
            //kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(model.CustomerName))
                ModelState.AddModelError("CustomerName", "tên khách hàng không được để trống");

            if (string.IsNullOrWhiteSpace(model.ContactName))
                ModelState.AddModelError("ContactName", "tên giao dịch không được để trống");

            if (string.IsNullOrWhiteSpace(model.Address))
                ModelState.AddModelError("Address", "địa chỉ không được để trống");

            if (string.IsNullOrWhiteSpace(model.City))
                model.City = "";

            if (string.IsNullOrWhiteSpace(model.PostalCode))
                model.PostalCode = "";

            if (string.IsNullOrWhiteSpace(model.Country))
                ModelState.AddModelError("Country", "thành phố không được để trống");

            //nếu dữ liệu đầu vào không hợp lệ trả lại giao điện nhận
            //ModelState giúp ta kiểm soát dc lỗi
            if(!ModelState.IsValid)
            {
                if (model.CustomerID > 0)
                    ViewBag.Title = "Cập nhật thông tin khách hàng";
                else
                    ViewBag.Title = "bổ sung thông tin khách hàng";
                return View("Create", model);
            }

            //xử lý luwu dữ liệu vào csdl
            if (model.CustomerID > 0)
            {
                CommonDataService.UpdateCustomer(model);
            }
            else
            {
                CommonDataService.AddCustomer(model);/*
                Session["CUSTOMER_SEARCH"] = new Models.PaginationSearchImput() {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = model.CustomerName
                };*/
            }
            Session["CUSTOMER_SEARCH"] = new Models.PaginationSearchImput()
            {
                Page = 1,
                PageSize = 10,
                SearchValue = model.CustomerName
            };

            return RedirectToAction("index");
        }

        /// <summary>
        /// xóa khách hàng
        /// </summary>
        /// <param name="customerID">id khách hàng</param>
        /// <returns></returns>
        [Route("delete/{customerID}")]
        public ActionResult Delete(string customerID)
        {
            int id = 0;

            try
            {
                id = Convert.ToInt32(customerID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetCustomer(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion
    }
}   