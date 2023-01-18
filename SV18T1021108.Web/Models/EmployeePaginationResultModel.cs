using SV18T1021108.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021108.Web.Models
{
    /// <summary>
    /// kết quả tìm kiếm và lấy dữ liệu nhân viên dưới dạng phân trang
    /// </summary>
    public class EmployeePaginationResultModel : PaginationResultModel
    {
        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public List<Employee> Data { get; set; }
    }
}