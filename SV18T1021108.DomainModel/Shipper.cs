using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021108.DomainModel
{
    /// <summary>
    /// người giao hàng
    /// </summary>
    public class Shipper
    {
        /// <summary>
        /// id người giao hàng
        /// </summary>
        public int ShipperID { get; set; }
        /// <summary>
        /// tên người giao hàng
        /// </summary>
        public string ShipperName { get; set; }
        /// <summary>
        /// số điện thoại
        /// </summary>
        public string Phone { get; set; }
    }
}
