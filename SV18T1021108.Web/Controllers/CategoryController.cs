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
    /// controller loại sản phẩm
    /// </summary>
    [Authorize]
    [RoutePrefix("category")]
    public class CategoryController : Controller
    {
        #region CRUD
        /// <summary>
        /// lấy thông tin loại sản phẩm
        /// </summary>
        /// <param name="page">số trang</param>
        /// <param name="searchValue">thông tin cần tìm kiếm</param>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchImput model = Session["CATEGORY_SEARCH"] as Models.PaginationSearchImput;

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
            var data = CommonDataService.ListOfCategorys(input.Page, input.PageSize, input.SearchValue, out rowCount);

            Models.CategoryPaginationResultModel model = new Models.CategoryPaginationResultModel
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                SearchValue = input.SearchValue,
                Data = data
            };

            Session["CATEGORY_SEARCH"] = input;

            return View(model);
        }

        /// <summary>
        /// tạo mới loại sản phẩm
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung khách hàng";
            Category model = new Category()
            {
                CategoryID = 0
            };
            return View(model);
        }

        /// <summary>
        /// chỉnh sửa một loại sản phẩm
        /// </summary>
        /// <param name="categoryID">id loại sản phẩm</param>
        /// <returns></returns>
        [Route("edit/{categoryID}")]
        public ActionResult Edit(string categoryID)
        {
            int id = 0;

            try
            {
                id = Convert.ToInt32(categoryID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Cập nhật thông tin khách hàng";

            Category model = CommonDataService.GetCategory(id);

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
        public ActionResult Save(Category model)
        {
            //kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(model.CategoryName))
                ModelState.AddModelError("CategoryName", "tên loại sản phẩm không được để trống");

            if (string.IsNullOrWhiteSpace(model.Description))
                ModelState.AddModelError("Description", "Mô tả không được để trống");

            //nếu dữ liệu đầu vào không hợp lệ trả lại giao điện nhận
            //ModelState giúp ta kiểm soát dc lỗi
            if (!ModelState.IsValid)
            {
                if (model.CategoryID > 0)
                    ViewBag.Title = "Cập nhật thông tin loại sản phẩm";
                else
                    ViewBag.Title = "bổ sung thông tin loại sản phẩm";

                return View("Create", model);
            }

            if (model.CategoryID > 0)
            {
                CommonDataService.UpdateCategory(model);
            }
            else
            {
                CommonDataService.AddCategory(model);
            }

            Session["CATEGORY_SEARCH"] = new Models.PaginationSearchImput()
            {
                Page = 1,
                PageSize = 10,
                SearchValue = model.CategoryName
            };

            return RedirectToAction("Index");
        }

        /// <summary>
        /// xóa một loại sản phẩm
        /// </summary>
        /// <param name="categoryID">id loại sản phẩm</param>
        /// <returns></returns>
        [Route("delete/{categoryID}")]
        public ActionResult Delete(string categoryID)
        {
            int id = 0;

            try
            {
                id = Convert.ToInt32(categoryID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetCategory(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion
    }
}