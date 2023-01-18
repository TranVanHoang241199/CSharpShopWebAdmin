using SV18T1021108.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021108.Web.Models
{
    /// <summary>
    /// kết quả tìm kiếm và lấy dữ liệu đơn hàng dưới dạng phân trang
    /// </summary>
    public class OrderPaginationResultModel : PaginationResultModel
    {
        /// <summary>
        /// Danh sách đơn hàng
        /// </summary>
        public List<Order> Data { get; set; }
    }
}