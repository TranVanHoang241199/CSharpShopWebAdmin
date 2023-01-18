using SV18T1021108.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021108.Web.Models
{
    /// <summary>
    /// kết quả tìm kiếm và lấy dữ liệu người giao hàng dưới dạng phân trang
    /// </summary>
    public class ShipperPaginationResultModel : PaginationResultModel
    {
        /// <summary>
        /// Danh sách người giao hàng
        /// </summary>
        public List<Shipper> Data { get; set; }
    }
}