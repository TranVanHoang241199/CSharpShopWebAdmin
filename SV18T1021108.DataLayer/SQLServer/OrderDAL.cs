using SV18T1021108.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021108.DataLayer.SQLServer
{
    /// <summary>
    /// xử lý hóa đơn
    /// </summary>
    public class OrderDAL : _BaseDAL, ICommonDAL<Order>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public OrderDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// thêm mới một hóa đơn
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Order data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// đếm số lượng hóa đơn
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            int count = 0;
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT  COUNT(*)
                                    FROM    Employees
                                    WHERE    (@searchValue = N'')
                                        OR    (
                                                (FirstName LIKE @searchValue)
                                                OR (Email LIKE @searchValue)
                                            )";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return count;
        }

        /// <summary>
        /// xóa hóa hóa đơn
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public bool Delete(int OrderID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// lấy ra một hóa đơn cụ thể dựa vòa id
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public Order Get(int OrderID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// kiểm tra hóa đơn có tồn tại không
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public bool InUsed(int OrderID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// lấy ra danh sách hóa đơn 
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang </param>
        /// <param name="searchValue">tên hoặc địa chỉ tìm(tương đối)
        /// <returns></returns>
        public IList<Order> List(int page, int pageSize, string searchValue)
        {
            List<Order> data = new List<Order>();
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM
                                    (
                                        SELECT    *, ROW_NUMBER() OVER (ORDER BY OrderTime) AS RowNumber
                                        FROM    Orders
                                        WHERE    (@searchValue = N'')
                                            OR    (
                                                    (CustomerID LIKE @searchValue)
                                                 OR (ShipperID LIKE @searchValue)
                                                )
                                    ) AS t
                                    WHERE (@PageSize = 0) OR  (t.RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                
                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (result.Read())
                {
                    data.Add(new Order()
                    {
                        OrderID = Convert.ToInt32(result["OrderID"]),
                        OrderTime = Convert.ToDateTime(result["OrderTime"]),
                        AcceptTime = Convert.ToDateTime(result["AcceptTime"]),
                        ShippedTime = Convert.ToDateTime(result["ShippedTime"]),
                        FinishedTime = Convert.ToDateTime(result["FinishedTime"]),
                        Status = Convert.ToInt32(result["Status"])

                    });
                }
                result.Close();
                cn.Close();
            }
            return data;
        }

        /// <summary>
        /// cập nhật một hóa đơn
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Order data)
        {
            throw new NotImplementedException();
        }
    }
}
