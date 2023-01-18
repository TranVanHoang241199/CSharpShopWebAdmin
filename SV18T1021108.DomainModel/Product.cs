using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021108.DomainModel
{
    /// <summary>
    /// Mặt hàng
    /// </summary>
    public class Product
    {
        /// <summary>
        /// id mặt hàng
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// tên mặt hàng
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// đơn vị mặt hàng 
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// giá mặt hàng
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// ảnh của mặt hàng
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// nhà cung cấp
        /// </summary>
        public int SupplierID { get; set; }
        /// <summary>
        /// loại sản phẩm
        /// </summary>
        public int CategoryID { get; set; }
    }
}
