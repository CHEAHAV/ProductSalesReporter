using DevExpress.Drawing.Internal.Fonts.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductSalesReporter
{
    public partial class frmNewProductSales : Form
    {

        DatabaseConnection db = new DatabaseConnection();
        SqlCommand com;
        public frmNewProductSales()
        {
            InitializeComponent();
            db.SystemConnection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            db.con.Open();
            var uprice = Decimal.Parse(txtUprice.Text, NumberStyles.Currency);
            com = new SqlCommand("spSetNewProducts", db.con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@pCode", txtpCode.Text);
            com.Parameters.AddWithValue("@pName", txtpName.Text);
            com.Parameters.AddWithValue("@qty", txtqty.Text);
            com.Parameters.AddWithValue("@uPrice", uprice);
            com.Parameters.AddWithValue("@saleDate", dtSaleDate.Value);
            com.ExecuteNonQuery();
            MessageBox.Show("Product Sale added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
            frmProductSalesReporter psr = new frmProductSalesReporter();
            psr.Show();
        }

        private void txtpCode_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtpCode.Text)) return;

            try
            {
                if (db.con.State != ConnectionState.Open)
                    db.con.Open();

                com = new SqlCommand("spGetNameProduct", db.con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@pCode", txtpCode.Text.Trim());

                object result = com.ExecuteScalar();
                txtpName.Text = result != null ? result.ToString() : string.Empty;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product name: " + ex.Message);
            }
            finally
            {
                if (db.con.State == ConnectionState.Open)
                    db.con.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProductSalesReporter psr = new frmProductSalesReporter();
            psr.Show();
        }
    }
}
