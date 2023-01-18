using SV18T1021108.BusinessLayer;
using SV18T1021108.DomainModel;
using SV18T1021108.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021108.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[Route("{CategoryID}/{SupplierID}")]
        public ActionResult Index(int CategoryID = 0, int SupplierID = 0)
        {
            Models.PaginationSearchImput model = Session["PRODUCT_SEARCH"] as Models.PaginationSearchImput;

            if (model == null)
            {
                model = new Models.PaginationSearchImput()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = ""
                };
            }

            if(CategoryID > 0 || SupplierID > 0)
            {
                model.CategoryID = CategoryID;
                model.SupplierID = SupplierID;
                model.SearchValue = "";
            }

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchImput input)
        {

            int rowCount = 0;
            var data = CommonDataService.ListOfProducts(input.Page, input.PageSize, input.SearchValue, out rowCount, input.CategoryID, input.SupplierID);

            Models.ProductPaginationResultModel model = new Models.ProductPaginationResultModel
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                SearchValue = input.SearchValue,
                Data = data,
                CateloryID = input.CategoryID,
                SupplierID = input.SupplierID
            };

            Session["PRODUCT_SEARCH"] = input;

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung mặt hàng";
            Product model = new Product()
            {
                ProductID = 0
            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("edit/{productID}")]
        public ActionResult Edit(int productID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(productID);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Cập nhật thông tin mặt hàng";

            Product model = CommonDataService.GetProduct(id);

            if (model == null)
            {
                RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("delete/{productID}")]
        public ActionResult Delete(int productID)
        {
            int id = 0;

            try
            {
                id = Convert.ToInt32(productID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteProduct(id);
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetProduct(id);

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Product model, HttpPostedFileBase uploadPhoto)
        {

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Products");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);

                model.Photo = $"/Images/Products/{fileName}";
            }

            if (string.IsNullOrWhiteSpace(model.ProductName))
                ModelState.AddModelError("ProductName", "Tên không được để trống");

            if (string.IsNullOrEmpty(model.Unit))
                ModelState.AddModelError("Unit", "đơn vị không được để trống");

            if (string.IsNullOrEmpty(model.SupplierID.ToString()))
                ModelState.AddModelError("SupplierID", "Nhà cung cấp không được để trống");

            if (string.IsNullOrEmpty(model.CategoryID.ToString()))
                ModelState.AddModelError("CategoryID", "Loại sản phẩm không được để trống");

            if (model.Price <= 0)
            {
                ModelState.AddModelError("Price", "giá bán không được để trống");
            }

            if (string.IsNullOrWhiteSpace(model.Photo))
                ModelState.AddModelError("Photo", "Ảnh không được để trống");

            if (!ModelState.IsValid)
            {
                if (model.ProductID > 0) {  
                    ViewBag.Title = "Cập nhật mặt hàng";
                    return View("edit", model);
                }
                else
                {
                    ViewBag.Title = "Bổ sung";
                    return View("create", model);
                }
            }

            if (model.ProductID > 0)
                CommonDataService.UpdateProduct(model);
            else
                CommonDataService.AddProducs(model);

            Session["PRODUCT_SEARCH"] = new Models.PaginationSearchImput()
            {
                Page = 1,
                PageSize = 10,
                SearchValue = model.ProductName
            };

            return RedirectToAction("Index");

            /*  return Json(new
          {
              Model = model
          });*/
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveAttribute(ProductAttribute model)
        {

            if (string.IsNullOrWhiteSpace(model.AttributeName))
                ModelState.AddModelError("ProductName", "Tên không được để trống");

            if (string.IsNullOrWhiteSpace(model.AttributeValue))
                ModelState.AddModelError("Unit", "đơn vị không được để trống");

            /*if (model.DisplayOrder)
                ModelState.AddModelError("Price", "giá bán không được để trống");*/

            if (!ModelState.IsValid)
            {
                if (model.AttributeID > 0)
                    ViewBag.Title = "Cập nhật thông tin";
                else
                    ViewBag.Title = "Bổ sung thông tin";

                    return View("Attribute", model);
                    
            }

            if (model.AttributeID > 0)
                CommonDataService.UpdateProductAttribute(model);
            else
                CommonDataService.AddProducAttribute(model);

            return RedirectToAction("edit/"+model.ProductID);

            /*  return Json(new
          {
              Model = model
          });*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="uploadPhoto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SavePhoto(ProductPhoto model, HttpPostedFileBase uploadPhoto)
        {

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/ProductPhotos");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);

                model.Photo = $"/Images/ProductPhotos/{fileName}";
            }

            if (string.IsNullOrWhiteSpace(model.Description))
                ModelState.AddModelError("Description", "nội dung không được để trống");

            /*if (string.IsNullOrWhiteSpace(model.DisplayOrder))
                ModelState.AddModelError("Unit", "đơn vị không được để trống");*/

            if (string.IsNullOrWhiteSpace(model.Photo))
                ModelState.AddModelError("Photo", "Ảnh không được để trống");

            if (!ModelState.IsValid)
            {
                if (model.PhotoID > 0)
                    ViewBag.Title = "Cập nhật ảnh";
                   
                else
                    ViewBag.Title = "Bổ sung ảnh";

                    return View("Photo", model);
                    
            }

            if (model.PhotoID > 0)
                CommonDataService.UpdateProductPhoto(model);
            else
                CommonDataService.AddProducPhoto(model);

            return RedirectToAction("edit/" + model.ProductID);

            /*return Json(new
            {
                Model = model
            });*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method}/{productID}/{photoID?}")]
        public ActionResult Photo(string method, int productID, int? photoID)
        {
            ProductPhoto model = new ProductPhoto();
            int id = 0;
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";

                    model.PhotoID = 0;
                    try
                    {
                        id = Convert.ToInt32(productID);
                    }
                    catch
                    {
                        return RedirectToAction("Index");
                    }
                    model.ProductID = id;
                    break;
                case "edit":

                    try
                    {
                        id = Convert.ToInt32(photoID);
                    }
                    catch
                    {
                        return RedirectToAction("Index");
                    }

                    model = CommonDataService.GetProductPhoto(id);
                    ViewBag.Title = "Thay đổi ảnh";
                    break;
                case "delete":
                    try
                    {
                        id = Convert.ToInt32(photoID);
                    }
                    catch
                    {
                        return RedirectToAction("Index");
                    }

                    CommonDataService.DeleteOfProductPhoto(id);
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method, int productID, int? attributeID)
        {
            ProductAttribute model = new ProductAttribute();
            int id = 0;

            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";

                    model.AttributeID = 0;

                    try
                    {
                        id = Convert.ToInt32(productID);
                    }
                    catch
                    {
                        return RedirectToAction("Index");
                    }

                    model.ProductID = id;

                    break;
                case "edit":

                    try
                    {
                        id = Convert.ToInt32(attributeID);
                    }
                    catch
                    {
                        return RedirectToAction("Index");
                    }

                    model = CommonDataService.GetProductAttribute(id);

                    ViewBag.Title = "Thay đổi thuộc tính";

                    break;

                case "delete":

                    try
                    {
                        id = Convert.ToInt32(attributeID);
                    }
                    catch
                    {
                        return RedirectToAction("Index");
                    }

                    CommonDataService.DeleteOfProductAttribute(id);

                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}