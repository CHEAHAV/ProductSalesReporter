using System;
using System.ComponentModel.DataAnnotations;

namespace ProductSalesReporter
{
    public class SaleDto
    {
        [Display(Name = "Sale ID")]
        public int SaleID { get; set; }

        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal Total { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
