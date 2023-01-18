using SV18T1021108.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021108.Web.Models
{
    public class ProductUpdateModel
    {
        public Product Product { get; set; }
        public List<ProductAttribute> ProductAttribute { get; set; }

        public List<ProductPhoto> ProductPhoto { get; set; }
    }
}