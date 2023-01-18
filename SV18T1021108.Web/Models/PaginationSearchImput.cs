using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021108.Web.Models
{
    /// <summary>
    /// tìm kiếm phân trang
    /// </summary>
    public class PaginationSearchImput
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SearchValue { get; set; }

        //-------Product
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }

    }
}