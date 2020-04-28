using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class ProductSort
    {
        public int ProductSortID { get; set; }
        public string SortName { get; set; }
        public Comparison<CustomProduct> SortOperation { get; set; }
    }
}
