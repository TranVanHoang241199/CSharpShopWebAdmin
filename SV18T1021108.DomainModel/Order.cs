using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021108.DomainModel
{
    /// <summary>
    /// lập đơn hàng
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Id
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// thời gian gọi món
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// thời gian nhân
        /// </summary>
        public DateTime AcceptTime { get; set; }
        /// <summary>
        /// thời gian giao hàng
        /// </summary>
        public DateTime ShippedTime { get; set; }
        /// <summary>
        /// thời gian hoàn thành
        /// </summary>
        public DateTime FinishedTime { get; set; }
        /// <summary>
        /// trạng thái đơn hàng
        /// </summary>
        public int Status { get; set; }
    }
}
