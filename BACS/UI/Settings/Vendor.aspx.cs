using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Settings
{
    public partial class Vendor : Page
    {
        private DatabaseAccess dba = new DatabaseAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rdbtnIsActive.SelectedValue = "1";
                LoadGrid();
            }

        }
        private void Clear()
        {
            txtVendor.Text = string.Empty;
            HIDVendorID.Value = string.Empty;
            rdbtnIsActive.SelectedValue = "1";

            txtVendor.Text=String.Empty;
            txtAddress.Text = String.Empty;
            txtLandline.Text = String.Empty;
            txtMobile.Text = String.Empty;
            txtCreditLimit.Text = "0";
            txtEmailAddress.Text = String.Empty;
            txtFax.Text = String.Empty;
            txtTin.Text = String.Empty;
            txtWebsite.Text = String.Empty;
            txtAccountNo.Text = String.Empty;

            txtStartingBalance.Text = "0";

            btnSubmit.Text = "Submit";

        }
        private void LoadGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = "Select * from Vendor order by CompanyName";
                ds = dba.GetDataSet(query, "ConnDB230");
                gvVendor.DataSource = ds;
                gvVendor.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string UserID = Session["UserID"].ToString();
            string Vendor = txtVendor.Text.ToString();
            string VAddress = txtAddress.Text;
            string VLandline = txtLandline.Text.ToString();
            string VMobile = txtMobile.Text;
            double VCreditLimit = Convert.ToDouble(txtCreditLimit.Text);
            string VEmailAddress = txtEmailAddress.Text;
            string vFax = txtFax.Text;
            string VTin = txtTin.Text;
            string vWebsite = txtWebsite.Text;
            string vAccountNo = txtAccountNo.Text;
           
            double VStartingBalance =Convert.ToDouble(txtStartingBalance.Text);
            string IsActive = "";
            if (rdbtnIsActive.SelectedValue == "0")
            {
                IsActive = "N";
            }
            else
            {
                IsActive = "Y";
            }

            try
            {
                if (btnSubmit.Text == "Submit")
                {
                    string sqlchk = "select * from Vendor where CompanyName='" + txtVendor.Text + "'";
                    DataTable dt = dba.GetDataTable(sqlchk);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                            "alert('The Vendor *" + txtVendor.Text + "* Already Exist...');", true);
                    }
                    else
                    {
                        string sqlCat = "insert into Vendor (CompanyName,Address,Mobile,Landline,CreditLimit,Email,Fax,Tin,Website,AccountNo,StartingBalance,IsActive,CreatedBy) values ('" + Vendor + "','" + VAddress + "','" + VMobile + "','" + VLandline + "','" + VCreditLimit + "','" + VEmailAddress + "','" + vFax + "','" + VTin + "','" + vWebsite + "','" + vAccountNo + "','" + VStartingBalance + "','" + IsActive + "', '" +
                                        UserID +
                                        "')";
                        dba.ExecuteQuery(sqlCat, "ConnDB230");
                        txtVendor.Text = string.Empty;
                        LoadGrid();
                        Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
                    }
                }
                if (btnSubmit.Text == "Update")
                {
                    string sqlCat = "update Vendor set Address='" + VAddress + "', Mobile='" + VMobile + "',Landline='" + VLandline + "', CreditLimit='" + VCreditLimit + "',Email='" + VEmailAddress + "', Fax='" + vFax + "',Tin='" + VTin + "', Website='" + vWebsite + "',AccountNo='" + vAccountNo + "', StartingBalance='" + VStartingBalance + "',IsActive='" + IsActive + "' " +
                                    "where VendorID='" + HIDVendorID.Value + "'";
                    dba.ExecuteQuery(sqlCat, "ConnDB230");

                    btnSubmit.Text = "Submit";
                    LoadGrid();
                    Clear();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Updated...');", true);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void GvRefresh()
        {
            string sqlCat = "Select * from Vendor order by CompanyName";
            DataSet ds = dba.GetDataSet(sqlCat, "ConnDB230");
            gvVendor.DataSource = ds;
            gvVendor.DataBind();
        }

        protected void gvVendor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVendor.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void gvVendor_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
           
            HIDVendorID.Value = string.Empty;
            HIDVendorID.Value = gvVendor.Rows[e.NewSelectedIndex].Cells[1].Text.Trim().Replace("&nbsp;", "");
            txtVendor.Text = gvVendor.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
            txtAddress.Text = gvVendor.Rows[e.NewSelectedIndex].Cells[3].Text.Trim();
            txtMobile.Text = gvVendor.Rows[e.NewSelectedIndex].Cells[4].Text.Trim();
            txtLandline.Text = gvVendor.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();
            txtFax.Text = gvVendor.Rows[e.NewSelectedIndex].Cells[6].Text.Trim();
            txtEmailAddress.Text = gvVendor.Rows[e.NewSelectedIndex].Cells[7].Text.Trim();
            txtCreditLimit.Text = gvVendor.Rows[e.NewSelectedIndex].Cells[8].Text.Trim();
            txtTin.Text = gvVendor.Rows[e.NewSelectedIndex].Cells[9].Text.Trim();
            txtAccountNo.Text = gvVendor.Rows[e.NewSelectedIndex].Cells[10].Text.Trim();
            txtWebsite.Text = gvVendor.Rows[e.NewSelectedIndex].Cells[11].Text.Trim();
            txtStartingBalance.Text = gvVendor.Rows[e.NewSelectedIndex].Cells[12].Text.Trim();
           
            if (gvVendor.Rows[e.NewSelectedIndex].Cells[13].Text.ToString().Replace("&nbsp;", "") == "Y")
            {
                rdbtnIsActive.SelectedValue = "1";
            }
            else
            {
                rdbtnIsActive.SelectedValue = "0";
            }
            btnSubmit.Text = "Update";
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();

        }


    }
}