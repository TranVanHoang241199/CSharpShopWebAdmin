using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021108.Web.Models
{
    public class AccountPaginationResultModel
    {
        public string UserName { get; set; }
        public string PasswordOld { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
    }
}