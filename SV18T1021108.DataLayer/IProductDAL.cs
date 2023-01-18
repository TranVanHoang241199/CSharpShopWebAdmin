using SV18T1021108.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021108.DataLayer
{
    public interface IProductDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng mỗi trang (0 nếu không phân trang)</param>
        /// <param name="searchValue">Giá trị tìm kiếm (rỗng nếu bỏ qua )</param>
        /// <returns></returns>
        IList<Product> List(int page = 1, int pageSize = 0, string searchValue = "", int CategoryID = 0, int SupplierID = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue = "", int categoryID = 0, int supplierID = 0);
        /// <summary>
        /// Lấy 1 bản ghi (1 dòng dữ liệu) dựa vào id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product Get(int productID);
        /// <summary>
        /// Bổ sung dữ liệu T, hàm trả về id(IDENTITY) của dữ liệu được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Product data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Product data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// Kiểm 
        /// </summary>
        bool InUsed(int id);

        //-----------------------------------------------

        IList<ProductPhoto> ListOfPhoto(int productID);

        ProductPhoto GetOfPhoto(int ProductPhotoID);

        int AddOfPhoto(ProductPhoto data);

        bool UpdateOfPhoto(ProductPhoto data);

        bool DeleteOfPhoto(int ProductPhotoID);

        //-----------------------------------------------

        IList<ProductAttribute> ListOfAttribute(int productID);

        ProductAttribute GetOfAttribute(int AttributeID);

        int addOfAttribute(ProductAttribute data);

        bool UpdateOfAttribute(ProductAttribute data);

        bool DeleteOfAttribute(int AttributeID);
    }
}
