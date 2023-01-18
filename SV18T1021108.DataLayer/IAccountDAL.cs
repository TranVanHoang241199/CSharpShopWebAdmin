using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021108.DataLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountDAL
    {
        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool logIn(string userName, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool EditPassword(string userName, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useName"></param>
        /// <returns></returns>
        bool InUsed(string useName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool InPassword(string userName, string password);
    }
}
