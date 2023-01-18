using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021108.DomainModel
{
    /// <summary>
    /// khách hàng
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// địa chỉ khách hàng
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// tên khách hàng
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// tên lien lạc của khách hàng
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// địa chỉ khách hàng
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// thành phố của khách hàng
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// mã bưu điện của khác hàng
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// quốc gia của khách hàng
        /// </summary>
        public string Country { get; set; }
    }
}
