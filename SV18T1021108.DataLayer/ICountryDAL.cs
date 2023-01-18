using SV18T1021108.DomainModel;
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
    public interface ICountryDAL
    {

        /// <summary>
        /// lấy danh sách quốc gia
        /// </summary>
        /// <returns></returns>
        List<Country> ListOfCountry();
    }
}
