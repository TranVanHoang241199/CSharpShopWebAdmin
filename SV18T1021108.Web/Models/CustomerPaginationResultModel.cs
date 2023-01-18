using SV18T1021108.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021108.Web.Models
{
    /// <summary>
    /// kết quả tìm kiếm và lấy dữ liệu khách hàng dưới dạng phân trang
    /// </summary>
    public class CustomerPaginationResultModel : PaginationResultModel
    {
        /// <summary>
        /// Danh sách khách hàng
        /// </summary>
        public List<Customer> Data { get; set; }
    }
}