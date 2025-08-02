using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;

namespace ProductSalesReporter
{
    public partial class ProductSalesXtraReport : XtraReport
    {
        public ProductSalesXtraReport()
        {
            InitializeComponent();

        }

        public void InitData(string productCode, string productName, DateTime startDate, DateTime endDate, List<ProductSalesDetailDto> data)
        {
            DataSource = data;

            xrlProductCode.Text = productCode;
            xrlProductName.Text = productName;
            xrlStartDate.Text = startDate.ToString("dd/MM/yyyy");
            xrlEndDate.Text = endDate.ToString("dd/MM/yyyy");

            xrtNo.ExpressionBindings.Clear();
            xrtNo.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "sumRecordNumber()"));
            xrtNo.Summary.Running = SummaryRunning.Report;

            xrtProductName.ExpressionBindings.Clear();
            xrtProductName.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[ProductName]"));

            xrtQuantity.ExpressionBindings.Clear();
            xrtQuantity.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[Quantity]"));

            xrtUnitPrice.ExpressionBindings.Clear();
            xrtUnitPrice.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "FormatString('{0:$ 0.00}', [UnitPrice])"));

            xrtTotal.ExpressionBindings.Clear();
            xrtTotal.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "FormatString('{0:$ 0.00}', [Total])"));

            xrtSalesDate.ExpressionBindings.Clear();
            xrtSalesDate.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "FormatString('{0:dd/MM/yyyy}', [SaleDate])"));

            xrlTotalQuantity.ExpressionBindings.Clear();
            xrlTotalQuantity.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "FormatString('{0}', sumSum([Quantity]))"));
            xrlTotalQuantity.Summary.Running = SummaryRunning.Report;

            xrlGrandTotal.ExpressionBindings.Clear();
            xrlGrandTotal.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "FormatString('{0:$ 0.00}', sumSum([Total]))"));
            xrlGrandTotal.Summary.Running = SummaryRunning.Report;
        }
    }
}
