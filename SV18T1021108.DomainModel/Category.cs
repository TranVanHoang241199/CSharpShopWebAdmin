using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021108.DomainModel
{
    /// <summary>
    /// loại hàng
    /// </summary>
    public class Category
    {
        /// <summary>
        /// mã loại hàng
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// tên loại hàng
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// miêu tả loại hàng
        /// </summary>
        public string Description { get; set; }
    }
}
