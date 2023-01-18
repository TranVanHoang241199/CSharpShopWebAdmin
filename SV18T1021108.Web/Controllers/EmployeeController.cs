using SV18T1021108.BusinessLayer;
using SV18T1021108.DataLayer.SQLServer;
using SV18T1021108.DomainModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021108.Web.Controllers
{
    /// <summary>
    /// controller nhân viên
    /// </summary>
    [Authorize]
    [RoutePrefix("employee")]
    public class EmployeeController : Controller
    {
        #region CRUD
        /// <summary>
        /// lấy thông tin nhân viên
        /// </summary>
        /// <param name="page">số trang</param>
        /// <param name="searchValue">thông tin cần tìm kiếm</param>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchImput model = Session["EMPLOYEE_SEARCH"] as Models.PaginationSearchImput;

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
            var data = CommonDataService.ListOfEmployees(input.Page, input.PageSize, input.SearchValue, out rowCount);

            Models.EmployeePaginationResultModel model = new Models.EmployeePaginationResultModel
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                SearchValue = input.SearchValue,
                Data = data
            };

            Session["EMPLOYEE_SEARCH"] = input;

            return View(model);
        }

        /// <summary>
        /// tạo mới một nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhân viên";
            Employee model = new Employee()
            {
                EmployeeID = 0,
                BirthDate = new DateTime(2000, 01, 01)
            };
            return View(model);
        }

        /// <summary>
        /// chỉnh sửa nhân viên 
        /// </summary>
        /// <param name="employeeID">id nhân viên</param>
        /// <returns></returns>
        [Route("edit/{employeeID}")]
        public ActionResult Edit(string employeeID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(employeeID);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Cập nhật thông tin nhân viên";

            Employee model = CommonDataService.GetEmployee(id);

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
        public ActionResult Save(Employee model, string BirthDateString, HttpPostedFileBase uploadPhoto)
        {
            try
            {
                DateTime d = DateTime.ParseExact(BirthDateString, "dd/MM/yyyy",
                                  CultureInfo.InvariantCulture);
                model.BirthDate = d;
            }
            catch
            {
                ModelState.AddModelError("BirthDate", "Ngày sinh không hợp lệ");
            }

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Employee");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);

                model.Photo = $"/Images/Employee/{fileName}";
            }

            if (string.IsNullOrWhiteSpace(model.FirstName))
                ModelState.AddModelError("FirstName", "Tên không được để trống");

            if (string.IsNullOrWhiteSpace(model.LastName))
                ModelState.AddModelError("LastName", "họ không được để trống");

            if (string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError("Email", "Email không được để trống");
            
            else if (CommonDataService.InEmailEmployee(model.Email, model.EmployeeID))
                ModelState.AddModelError("Email", "Email đã tồn tại");

            if (string.IsNullOrWhiteSpace(model.Photo))
                ModelState.AddModelError("Photo", "Ảnh không được để trống");

            if (string.IsNullOrWhiteSpace(model.Notes))
                model.Notes = "";

            if (!ModelState.IsValid)
            {
                if (model.EmployeeID > 0)
                    ViewBag.Title = "Cập nhật nhân viên";
                else
                    ViewBag.Title = "Bổ sung";
                return View("Create", model);
            }

            if (model.EmployeeID > 0)
            {
                CommonDataService.UpdateEmployee(model);
            }
            else
                CommonDataService.AddEmployee(model);

            Session["EMPLOYEE_SEARCH"] = new Models.PaginationSearchImput()
            {
                Page = 1,
                PageSize = 10,
                SearchValue = model.FirstName
            };

            return RedirectToAction("Index");

            /*  return Json(new
          {
              Model = model
          });*/
        }

        /// <summary>
        /// xóa nhân viên
        /// </summary>
        /// <param name="customerID">id nhân viên</param>
        /// <returns></returns>
        [Route("delete/{employeeID}")]
        public ActionResult Delete(string employeeID)
        {
            int id = 0;

            try
            {
                id = Convert.ToInt32(employeeID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetEmployee(id);

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        #endregion
    }
}