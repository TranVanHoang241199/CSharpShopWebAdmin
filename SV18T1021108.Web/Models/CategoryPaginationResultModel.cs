using SV18T1021108.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021108.Web.Models
{
    /// <summary>
    /// kết quả tìm kiếm và lấy dữ liệu loại mặt hàng dưới dạng phân trang
    /// </summary>
    public class CategoryPaginationResultModel : PaginationResultModel
    {
        /// <summary>
        /// Danh sách loại mặt hàng
        /// </summary>
        public List<Category> Data { get; set; }
    }
}