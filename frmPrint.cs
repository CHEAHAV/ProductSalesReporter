using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProductSalesReporter
{
    public partial class frmPrint : XtraForm
    {
        public frmPrint()
        {
            InitializeComponent();
            dcvPrint.Zoom = 0.80f;
        }

        public void PrintInvoice(SaleDto saleDto, List<ProductSalesDetailDto> data)
        {
            try
            {
                ProductSalesXtraReport report = new ProductSalesXtraReport();

                foreach (DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
                    p.Visible = false;

                report.InitData(
                    saleDto.ProductCode,
                    saleDto.ProductName,
                    saleDto.StartDate,
                    saleDto.EndDate,
                    data
                );

                dcvPrint.DocumentSource = report;
                report.CreateDocument();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message,
                                "Report Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
