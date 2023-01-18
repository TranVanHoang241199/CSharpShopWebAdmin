using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021108.DomainModel
{
    /// <summary>
    /// nhà cung cấp
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// id nhà cung cấp
        /// </summary>
        public int SupplierID { get; set; }
        /// <summary>
        /// tên nhà cung cấp
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// tên liên lạc
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// địa chỉ liên lạc
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// thành phố của nhà cung cấp
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Mã bưu điện 
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// quốc gia
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// số điện thoại liên lạc
        /// </summary>
        public string Phone { get; set; }
    }
}
