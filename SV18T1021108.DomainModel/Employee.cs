using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021108.DomainModel
{
    /// <summary>
    /// Nhân viên 
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Id nhân viên
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// họ của nhân viên
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// ngày sinh
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// ảnh
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// ghi chú
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }
    }
}
