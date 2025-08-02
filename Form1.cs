using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ProductSalesReporter
{
    public partial class frmProductSalesReporter : XtraForm
    {
        DatabaseConnection db = new DatabaseConnection();

        public frmProductSalesReporter()
        {
            InitializeComponent();
            db.SystemConnection();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtStartDate.EditValue == null)
                {
                    MessageBox.Show("Please select a Start Date.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtStartDate.Focus();
                    return;
                }

                if (dtEndDate.EditValue == null)
                {
                    MessageBox.Show("Please select an End Date.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtEndDate.Focus();
                    return;
                }

                if (db.con.State == ConnectionState.Closed)
                    db.con.Open();

                DateTime startDate = (DateTime)dtStartDate.EditValue;
                DateTime endDate = (DateTime)dtEndDate.EditValue;

                var list = db.con.Query<SaleDto>(
                    "spGetProductSales",
                    new
                    {
                        StartDate = startDate,
                        EndDate = endDate
                    },
                    commandType: CommandType.StoredProcedure
                ).ToList();

                if (list.Count == 0)
                {
                    MessageBox.Show("No product found for the selected date range. Default data starts from 20 June 2025.");
                }

                grcProductSales.DataSource = list;

                var view = grcProductSales.MainView as GridView;
                if (view != null)
                {
                    if (view.Columns["StartDate"] != null)
                        view.Columns["StartDate"].Visible = false;

                    if (view.Columns["EndDate"] != null)
                        view.Columns["EndDate"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
            finally
            {
                if (db.con.State == ConnectionState.Open)
                    db.con.Close();
            }
        }


        private void bntGenerateReport_Click(object sender, EventArgs e)
        {
            var view = grcProductSales.MainView as GridView;
            if (view == null) return;

            SaleDto obj = view.GetFocusedRow() as SaleDto;

            if (obj == null)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            DateTime startDate = (DateTime)dtStartDate.EditValue;
            DateTime endDate = (DateTime)dtEndDate.EditValue;

            try
            {
                if (db.con.State == ConnectionState.Closed)
                    db.con.Open();

                var list = db.con.Query<ProductSalesDetailDto>(
                    "spGetProductSalesDetail",
                    new
                    {
                        ProductCode = obj.ProductCode,
                        StartDate = startDate,
                        EndDate = endDate
                    },
                    commandType: CommandType.StoredProcedure
                ).ToList();

                if (list.Count == 0)
                {
                    MessageBox.Show("No order details found for this order.");
                    return;
                }

                using (frmPrint frm = new frmPrint())
                {
                    frm.PrintInvoice(new SaleDto
                    {
                        ProductCode = obj.ProductCode,
                        ProductName = obj.ProductName,
                        StartDate = startDate,
                        EndDate = endDate
                    }, list);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error printing report: " + ex.Message);
            }
            finally
            {
                if (db.con.State == ConnectionState.Open)
                    db.con.Close();
            }
        }

        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNewProductSales nps = new frmNewProductSales();
            nps.Show();
        }
    }
}
