using SV18T1021108.DataLayer;
using SV18T1021108.DataLayer.SQLServer;
using SV18T1021108.DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021108.BusinessLayer
{
    /// <summary>
    /// các chức năng nghiệp vụ liên quan đến dữ liệu chung
    /// (nhà cung cấp, khách hàng, người giao hàng, thành viên, loại hàng)
    /// </summary>
    public static class CommonDataService
    {
        private static readonly ICommonDAL<Category> categoryDB;
        private static readonly ICommonDAL<Customer> customerDB;
        private static readonly ICommonDAL<Supplier> supplierDB;
        private static readonly ICommonDAL<Employee> employeeDB;
        private static readonly ICommonDAL<Shipper> shipperDB;
        private static readonly ICommonDAL<Order> orderDB;
        private static readonly EmployeeDAL employeeDBB;
        private static readonly IProductDAL productDB;
        private static readonly ICountryDAL countryDB;
        private static readonly IAccountDAL accountDB;

        /// <summary>
        /// contrusctor
        /// </summary>
        static CommonDataService()
        {
            string provider = ConfigurationManager.ConnectionStrings["DB"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            switch (provider)
            {
                case "SQLServer":
                    categoryDB = new DataLayer.SQLServer.CategoryDAL(connectionString);
                    customerDB = new DataLayer.SQLServer.CustomerDAL(connectionString);
                    supplierDB = new DataLayer.SQLServer.SupplierDAL(connectionString);
                    employeeDB = new DataLayer.SQLServer.EmployeeDAL(connectionString);
                    shipperDB = new DataLayer.SQLServer.ShipperDAL(connectionString);
                    orderDB = new DataLayer.SQLServer.OrderDAL(connectionString);
                    productDB = new DataLayer.SQLServer.ProductDAL(connectionString);
                    countryDB = new DataLayer.SQLServer.CountryDAL(connectionString);
                    employeeDBB = new DataLayer.SQLServer.EmployeeDAL(connectionString);
                    accountDB = new DataLayer.SQLServer.AccountDAL(connectionString);

                    break;
                default:
                    categoryDB = new DataLayer.FakeDB.CategoryDAL();
                    break;
            }

        }

        #region List

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountrys()
        {
            return countryDB.ListOfCountry().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Category> ListOfCategorys()
        {
            return categoryDB.List(1, 0, "").ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers()
        {
            return supplierDB.List(1, 0, "").ToList();
        }

        #endregion

        //-------0o0------

        #region Processing Category

        /// <summary>
        /// lấy ra danh sách loại mặt hàng
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang </param>
        /// <param name="searchValue">tên hoặc địa chỉ tìm(tương đối)
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Category> ListOfCategorys(int page,
                                                   int pageSize,
                                                   string searchValue,
                                                   out int rowCount)

        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();

        }

        /// <summary>
        /// Lấy thông tin loại hàng theo id
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }
        /// <summary>
        /// Thêm loại hàng theo data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }
        /// <summary>
        /// Update loại hàng theo data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }
        /// <summary>
        /// Xóa loại hàng theo ID,(kiểm tra InUsed)
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryID)
        {
            if (categoryDB.InUsed(categoryID))
                return false;
            return categoryDB.Delete(categoryID);
        }
        public static bool InUsedCategory(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }
        #endregion

        #region Processing Customer
        /// <summary>
        /// lấy ra danh sách khách hàng
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang </param>
        /// <param name="searchValue">tên hoặc địa chỉ tìm(tương đối)
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page,
                                                   int pageSize,
                                                   string searchValue,
                                                   out int rowCount)
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();

        }

        /// <summary>
        /// Lấy thông tin khách hàng theo mã khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int customerID)
        {
            return customerDB.Get(customerID);
        }
        /// <summary>
        /// Thêm khách hàng theo data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }
        /// <summary>
        /// Update khách hàng theo data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }
        /// <summary>
        /// Xóa khách hàng theo ID,(kiểm tra InUsed)
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerID)
        {
            if (customerDB.InUsed(customerID))
                return false;
            return customerDB.Delete(customerID);
        }
        public static bool InUsedCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }
        #endregion

        #region Processing Employee
        /// <summary>
        /// lấy ra danh sách nhà nhân viên
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang </param>
        /// <param name="searchValue">tên hoặc địa chỉ tìm(tương đối)
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(int page,
                                                   int pageSize,
                                                   string searchValue,
                                                   out int rowCount)

        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();

        }

        /// <summary>
        /// Lấy thông tin nhân viên theo id
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }

        /// <summary>
        /// Thêm nhân viên theo data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }

        /// <summary>
        /// Update nhân viên theo data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        /// <summary>
        /// Xóa nhân viên theo ID,(kiểm tra InUsed)
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int employeeID)
        {
            if (employeeDB.InUsed(employeeID))
                return false;
            return employeeDB.Delete(employeeID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool InUsedEmployee(int employeeID)
        {
            return employeeDB.InUsed(employeeID);
        }

        public static bool InEmailEmployee(string email, int id)
        {
            return employeeDBB.InEmail(email, id);
        }
        #endregion

        #region Processing Shipper
        /// <summary>
        /// lấy ra danh sách người giao hàng
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang </param>
        /// <param name="searchValue">tên hoặc địa chỉ tìm(tương đối)
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(int page,
                                                   int pageSize,
                                                   string searchValue,
                                                   out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();

        }

        /// <summary>
        /// Lấy thông tin người giao hàng theo id
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }

        /// <summary>
        /// Thêm shipper theo data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }

        /// <summary>
        /// Update shipper theo data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }

        /// <summary>
        /// Xóa shipper theo ID,(kiểm tra InUsed)
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int shipperID)
        {
            if (shipperDB.InUsed(shipperID))
                return false;
            return shipperDB.Delete(shipperID);
        }

        /// <summary>
        /// Kiểm tra shipper có thông tin liên quan
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }
        #endregion

        #region Processing Supplier
        /// <summary>
        /// lấy ra danh sách nhà cung cấp
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang </param>
        /// <param name="searchValue">tên hoặc địa chỉ tìm(tương đối)
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(int page,
                                                   int pageSize,
                                                   string searchValue,
                                                   out int rowCount)

        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();

        }

        /// <summary>
        /// Lấy thông tin nhà cung cấp theo id
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }

        /// <summary>
        /// Thêm nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }

        /// <summary>
        /// Update nhà cung cấp theo data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }

        /// <summary>
        /// Xóa nhà cung cấp theo ID,(kiểm tra InUsed)
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            if (supplierDB.InUsed(supplierID))
                return false;
            return supplierDB.Delete(supplierID);
        }

        /// <summary>
        /// Kiểm tra nhà cung cấp có thông tin liên quan
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }
        #endregion

        //-------0o0------

        #region Processing Order
        /// <summary>
        /// lấy ra danh sách hóa đơn
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang </param>
        /// <param name="searchValue">tên hoặc địa chỉ tìm(tương đối)
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Order> ListOfOrders(int page,
                                                   int pageSize,
                                                   string searchValue,
                                                   out int rowCount)
        {
            rowCount = orderDB.Count(searchValue);
            return orderDB.List(page, pageSize, searchValue).ToList();

        }
        #endregion

        #region Processing Product
        /// <summary>
        /// lấy ra danh sách mặt hàng
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang (nếu 0 thì lấy toàn bộ)</param>
        /// <param name="searchValue">tên hoặc địa chỉ tìm(tương đối)
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Product> ListOfProducts(int page,
                                                   int pageSize,
                                                   string searchValue,
                                                   out int rowCount, int categoryID, int supplierID)

        {
            rowCount = productDB.Count(searchValue, categoryID, supplierID);
            return productDB.List(page, pageSize, searchValue, categoryID, supplierID).ToList();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static List<ProductPhoto> ListOfProductPhotos(int supplierID)

        {
            return productDB.ListOfPhoto(supplierID).ToList();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static List<ProductAttribute> ListOfProductAttributes(int supplierID)

        {
            return productDB.ListOfAttribute(supplierID).ToList();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Product GetProduct(int productID)
        {
            return productDB.Get(productID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static ProductPhoto GetProductPhoto(int productID)
        {
            return productDB.GetOfPhoto(productID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static ProductAttribute GetProductAttribute(int productID)
        {
            return productDB.GetOfAttribute(productID);
        }

        /// <summary>
        /// Thêm nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProducs(Product data)
        {
            return productDB.Add(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProducPhoto(ProductPhoto data)
        {
            return productDB.AddOfPhoto(data);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProducAttribute(ProductAttribute data)
        {
            return productDB.addOfAttribute(data);
        }

        /// <summary>
        /// Update nhà cung cấp theo data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProduct(Product data)
        {
            return productDB.Update(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProductAttribute(ProductAttribute data)
        {
            return productDB.UpdateOfAttribute(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProductPhoto(ProductPhoto data)
        {
            return productDB.UpdateOfPhoto(data);
        }

        /// <summary>
        /// Xóa nhà cung cấp theo ID,(kiểm tra InUsed)
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteProduct(int productID)
        {
            if (supplierDB.InUsed(productID))
                return false;
            return productDB.Delete(productID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static bool DeleteOfProductAttribute(int attributeID)
        {
            /*if (productDB.InUsed(attributeID))
                return false;*/

            return productDB.DeleteOfAttribute(attributeID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static bool DeleteOfProductPhoto(int photoID)
        {
            /*if (productDB.InUsed(photoID))
                return false;*/

            return productDB.DeleteOfPhoto(photoID);
        }

        /// <summary>
        /// Kiểm tra nhà cung cấp có thông tin liên quan
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedProduct(int productID)
        {
            return productDB.InUsed(productID);
        }


        #endregion

        //-------0o0------

        #region Account
        public static bool LoginAccount(string email, string password)
        {
            return accountDB.logIn(email, password);
        }

        public static bool UpdatePassword(string email, string password)
        {
            return accountDB.EditPassword(email, password);
        }

        public static bool InUserNameACcount(string email)
        {
            return accountDB.InUsed(email);
        }

        public static bool InPasswordAccount(string email, string password)
        {
            return accountDB.InPassword(email, password);
        }

        public static bool UpdateAccount(string email, string password)
        {
            return accountDB.EditPassword(email, password);
        }
        #endregion
    }
}