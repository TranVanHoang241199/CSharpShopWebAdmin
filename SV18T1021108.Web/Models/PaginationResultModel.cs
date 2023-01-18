using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021108.Web.Models
{
    /// <summary>
    /// lớp cơ sở cho các model chứa các dữ liệu dưới dạng phân trang
    /// </summary>
    public abstract class PaginationResultModel
    {
        /// <summary>
        /// Số trang
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// kích cở của trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// giá trị cần tìm kiếm
        /// </summary>
        public string SearchValue { get; set; }
        /// <summary>
        /// tổng số cột trong csdl
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageCount { 
            get
            {
                int p = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    p += 1;
                return p;
            } 
        }

        //-------Product
        public int CateloryID { get; set; }
        public int SupplierID { get; set; }
    }
}