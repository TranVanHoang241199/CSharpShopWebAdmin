using SV18T1021108.BusinessLayer;
using SV18T1021108.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021108.Web
{
    /// <summary>
    /// cung cấp một số tiện ích các hàm liên quan đến danh sách chọn trong Seclect
    /// </summary>
    public class SelectListHeplper
    {
        /// <summary>
        /// danh sách các quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Coutries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "", Text = "--Chọn quốc gia--" });
            foreach (var item in CommonDataService.ListOfCountrys())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }

            return list;
        }

        /// <summary>
        /// loại sản phẩm
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "", Text = "-- Chọn loại hàng --" });
            foreach (var item in CommonDataService.ListOfCategorys())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.CategoryID),
                    Text = item.CategoryName
                });
            }
            return list;
        }

        /// <summary>
        /// nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "", Text = "-- Chọn nhà cung cấp --" });
            foreach (var item in CommonDataService.ListOfSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.SupplierID),
                    Text = item.SupplierName
                });
            }
            return list;
        }
    }
}