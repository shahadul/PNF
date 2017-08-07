using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Selling_Process
{
    public partial class rtOrderCollection : Page
    {
        DatabaseAccess dba = new DatabaseAccess();
        helperAcc LoadDll = new helperAcc();
        private string uid = string.Empty;
        public static string ProductGroupID = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                uid = Session["UserID"].ToString();
            }
            txtDealerCode.Text = HIDDealerCode.Value;
            txtAreaName.Text = HIDDealerArea.Value;
            //txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
           
            if (!IsPostBack)
            {

              //  LoadParty();
                LoadProductGroup();
               // LoadCategory();
                pnlOrderDetails.Visible = false;
                pnlOrderShow.Visible = false;
                pnlOrderDetails.Visible = false;
                Session["sgvOrderDetail)"] = null;
                Session["ProductGroupID"] = String.Empty;
                //ProductGroupID=String.Empty;
            }
            else if (IsPostBack)
            {
               
            }
            

        }

        [WebMethod]
        public static List<string> GetAutoCompleteData(string Party)
       {
           // string dealer = txtDealer.Text;

            DatabaseAccess dba=new DatabaseAccess();
            List<string> dealers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = dba.getconnectionSting();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select t.PartyID,lu.UserName,t.PartyName,lu.UserName + '  =  '  + t.PartyName  as Party,AreaID from PartyMaster t,Login_Users lu 
                           where t.PartyID=lu.UserID and t.IsActive='Y' and lu.UserName + '  =  '  + t.PartyName LIKE '%'+@SearchText+'%' order by lu.UserName";
                    cmd.Parameters.AddWithValue("@SearchText", Party);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            dealers.Add(string.Format("{0}//{1}", dr["Party"], dr["PartyID"]));
                        }
                    }
                    conn.Close();
                }
            }
            return dealers;

        }

        [WebMethod]
        public static List<string> GetDealercode(string Party)
        {
            // string dealer = txtDealer.Text;
            DatabaseAccess dba = new DatabaseAccess();
            List<string> dealers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = dba.getconnectionSting();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select t.PartyID,lu.UserName,am.AreaName from PartyMaster t
                join Login_Users lu on t.PartyID=lu.UserID
                left join AreaMaster am on t.AreaID=am.AreaID
                where t.PartyID LIKE '%'+@SearchText+'%'";
                    cmd.Parameters.AddWithValue("@SearchText", Party);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            dealers.Add(string.Format("{0}//{1}", dr["UserName"], dr["AreaName"]));
                        }
                    }
                    conn.Close();
                }
            }
            return dealers;
           
        }



        //protected void LoadParty()
        //{
        //    string qry = "EXEC get_dealer ''";
        //    DataSet ds = dba.GetDataSet(qry, "ConnDB230");
        //    if (ds != null)
        //    {
        //        DataTable dt = ds.Tables[0];
        //        LoadDll.LoadDll(ddlPartyName, dt, "PartyID", "Party", " Select Dealer");
        //    }
        //    else
        //    {

        //        ddlPartyName.Items.Clear();
        //        ddlPartyName.Items.Add(new ListItem("No Dealer Available", "0"));

        //    }
        //}
        protected void LoadProductGroup()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select ProductGroupID,GroupName from ProductGroup where IsActive='Y' order by GroupName", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                LoadDll.LoadDll(ddlProductGroup, dt, "ProductGroupID", "GroupName", "Select Product Group");
            }
            else
            {

                ddlProductGroup.Items.Clear();
                ddlProductGroup.Items.Add(new ListItem("No Product Group Available", "0"));

            }
        }
        protected void LoadCategory()
        {
            string qry = "select CategoryID,Category from Product_Category where ProductGroupID='" + ddlProductGroup.SelectedValue + "' order by Category";
            DataSet ds = dba.GetDataSet(qry, "ConnDB230");
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                LoadDll.LoadDll(ddlCategory, dt, "CategoryID", "Category", "Select Category");
            }
            else
            {

                ddlCategory.Items.Clear();
                ddlCategory.Items.Add(new ListItem("No Category Available", "0"));

            }
        }
        protected void LoadProduct()
        {
            string qry = "select ProdID,ProdName from Product_Master where CategoryID='" + ddlCategory.SelectedValue + "' and IsActive='Y' order by ProdName ";
            DataSet ds = dba.GetDataSet(qry, "ConnDB230");
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                LoadDll.LoadDll(ddlProductName, dt, "ProdID", "ProdName", "Select Product");
            }
            else
            {

                ddlProductName.Items.Clear();
                ddlProductName.Items.Add(new ListItem("No Product Available", "0"));

            }
        }
       protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            if (Session["sgvOrderDetail)"] != null)
            {
                gvOrderDetail.AllowPaging = false;
                gvOrderDetail.DataSource = (DataTable)Session["sgvOrderDetail)"];
                gvOrderDetail.DataBind();
            }
            
            try
            {
               
                if (gvOrderDetail.Rows.Count > 0)
                {
                    if (ddlProductName.SelectedIndex != 0)
                    {
                        double Totl = 0;                       
                        Int32 totalQTY = 0;
                       
                        foreach (GridViewRow row in gvOrderDetail.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                Int32 qty = Convert.ToInt32(nullChecker(row.Cells[1].Text));
                                totalQTY += qty;
                                Totl += Convert.ToDouble(nullChecker(row.Cells[4].Text));
                            }
                        }


                        string resOut = string.Empty;
                        string orderDt = DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        
                        string addedby = uid;
                        string PartyID = HIDDealerID.Value;
                        
                        string qryOrder =
                            "insert into Local_Order_Master (Date,PartyID,AddedByFK,DeliveryStatus,IsApprove,IsActive,IsInvoice,TotalAmount,OrderQty,ProductGroupID) values " +
                            "('" + orderDt + "','" + PartyID + "','" + addedby + "','N','N'," +
                            "'Y','N','"+Totl+"','"+totalQTY+"','"+ddlProductGroup.SelectedValue+"') SELECT CAST(scope_identity() AS varchar(36))";
                        string result = dba.GetObjectDataId(qryOrder, "ConnDB230");
                        string OrderNo = string.Empty;
                        if (result != string.Empty)
                        {
                           // string dateYear = DateTime.ParseExact(DateTime.Today.ToString("yy"), "yy", CultureInfo.InvariantCulture).ToString("yy", CultureInfo.InvariantCulture);
                            string dateYear = "16";
                            string OrderNumber = "ODR-" + dateYear + "-" + Convert.ToInt64(result).ToString("000000000");
                            string sqlordno = "update Local_Order_Master set OrderNo='" + OrderNumber + "' where OrderID='" + result + "'";
                            dba.ExecuteQuery(sqlordno, "ConnDB230");

                            if (result != null)
                            {
                                foreach (GridViewRow row in gvOrderDetail.Rows)
                                {
                                    string productid = nullChecker(row.Cells[5].Text);
                                    Int32 qty = Convert.ToInt32(nullChecker(row.Cells[1].Text));
                                    double unitPric = Convert.ToDouble(nullChecker(row.Cells[2].Text));
                                    double Discount = Convert.ToDouble(nullChecker(row.Cells[3].Text));
                                    double TotaL = Convert.ToDouble(nullChecker(row.Cells[4].Text));

                                    string QryOrderDetail = "insert into Local_Order_Master_Details (OrderID, ProdID, Qty, Price,Discount, Total) values " +
                                                            "('" + result + "','" + productid + "','" + qty + "','" + unitPric + "','" + Discount + "','" + TotaL + "')";

                                    resOut = dba.ExecuteQuery(QryOrderDetail, "ConnDB230");
                                }
                                if (resOut == "1")
                                {
                                    clearInitialize();
                                    //pnlOrderDetails.Visible = false;
                                    //pnlOrderShow.Visible = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');",
                                        true);

                                  //  EnabledisableTop(true);
                                    pnlOrderDetails.Visible = false;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Saved...');",
                                        true);
                                }
                            }
                        }
                        //Response.Redirect(Request.RawUrl);
                        //Page.Response.Redirect(Page.Request.Url.ToString(), true);
                       
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Product Added...');", true);
                }

            }
            catch (Exception ex)
            {
                var lineNumber = new StackTrace(ex, true).GetFrame(0).GetFileLineNumber();

                lblMsg.Text = ex.Message + "   Line No:" + lineNumber;

            }

        }
       protected void GrandTotalCal()
       {
           if (Session["sgvOrderDetail)"] != null)
           {
               gvOrderDetail.AllowPaging = false;
               gvOrderDetail.DataSource = (DataTable)Session["sgvOrderDetail)"];
               gvOrderDetail.DataBind();
           }
           double GrandTotl = 0;
           if (gvOrderDetail.Rows.Count > 0)
           {
                   foreach (GridViewRow Row in gvOrderDetail.Rows)
                   {

                       GrandTotl += Convert.ToDouble(nullChecker(Row.Cells[4].Text));
                   }
              
           }
           txtGrandTotal.Text = Convert.ToString(GrandTotl);
           if (Session["sgvOrderDetail)"] != null)
           {
               gvOrderDetail.AllowPaging = true;
               gvOrderDetail.DataSource = (DataTable)Session["sgvOrderDetail)"];
               gvOrderDetail.DataBind();
           }
       }
        protected void TotalCal()
       {
           double vdiscount = 0;
         
           if (!string.IsNullOrEmpty(txtDiscount.Text))
           {
               vdiscount = (Convert.ToDouble(txtDiscount.Text) / 100) ;
 
           }
            if (txtOrderQty.Text == string.Empty || txtUnitPrice.Text == string.Empty)
            {
                txtTotalPrice.Text = string.Empty;
            }
            else
            {
                double TotalPr = (Convert.ToDouble(txtUnitPrice.Text) * Convert.ToDouble(txtOrderQty.Text)) - (vdiscount * Convert.ToDouble(txtUnitPrice.Text) * Convert.ToDouble(txtOrderQty.Text));                
                txtTotalPrice.Text = Convert.ToString(TotalPr);
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            TotalCal();
        }

        protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            TotalCal();
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (!(Convert.ToDecimal(txtDiscount.Text.ToString()) < 100))
            {
                txtDiscount.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Discount should less than 100% ...');", true);
            }
            else
            {
                TotalCal();
            }
            
        }

        // ADD GRID SECTION

        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    if (gvOrderDetail.Rows.Count > 0)
        //    {
        //        pnlOrderDetails.Visible = true;
        //        //pnlOrderShow.Visible = false;

        //        DataTable dt = new DataTable();
        //        dt = gvTOdt(gvOrderDetail);

        //        if (dt.Rows.Count > 0)
        //        {
        //            DataView dv = new DataView(dt);
        //            dv.RowFilter = "ProductId=" + ddlProductName.SelectedValue;

        //            DataTable dtChk = dv.ToTable();

        //            if (dtChk.Rows.Count > 0)
        //            {
        //                ScriptManager.RegisterStartupScript(this, GetType(), "Save",
        //                    "alert('The Product Allready Added...');", true);
        //            }
        //            else
        //            {
        //                DataTableBuilder();
        //                //pnlOrderShow.Visible = false;
        //                //pnlOrderDetails.Visible = true;
        //                //disableTop();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        DataTableBuilder();
        //        pnlOrderShow.Visible = false;
        //        //pnlOrderDetails.Visible = true;
        //        //disableTop();
        //    }
        //    GrandTotalCal();
        //    ChildClear();
        //    //enabledisableTop(false);
        //}
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ProductGroupID = Session["ProductGroupID"].ToString();
            if (String.IsNullOrEmpty(ProductGroupID) || (ProductGroupID== ddlProductGroup.SelectedValue.ToString())|| (ddlProductGroup.SelectedIndex==0))
            {
                Session["ProductGroupID"] = ddlProductGroup.SelectedValue.ToString();
                if (Session["sgvOrderDetail)"] != null)
                {
                    pnlOrderDetails.Visible = true;
                    //pnlOrderShow.Visible = false;

                    DataTable dt = (DataTable)Session["sgvOrderDetail)"];
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "ProductId=" + ddlProductName.SelectedValue;

                    DataTable dtChk = dv.ToTable();

                    if (dtChk.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                            "alert('The Product Allready Added...');", true);
                    }
                    else
                    {
                        DataTableBuilder();
                        //pnlOrderShow.Visible = false;
                        //pnlOrderDetails.Visible = true;
                        //disableTop();
                    }
                }
                else
                {
                    DataTableBuilder();
                    pnlOrderShow.Visible = false;
                    pnlOrderDetails.Visible = true;
                }


                GrandTotalCal();
                ChildClear();
                //enabledisableTop(false);
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                              "alert('Please select same product group and then try again...');", true);
                
            }
            
        }
        protected DataTable gvTOdt(GridView gv)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                //dt.Columns.Add("column" + i.ToString());
                dt.Columns.Add(gv.Columns[i].ToString());
            }
            foreach (GridViewRow row in gv.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < gv.Columns.Count; j++)
                {
                    dr[gv.Columns[j].ToString()] = row.Cells[j].Text;
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }

        protected string nullChecker(string str)
        {
            string outut = string.Empty;
            if (str != "&nbsp;" && str != "")
            {
                outut = str;
            }
            else
            {
                outut = "0";
            }
            return outut;
        }
        protected void ChildClear() 
        {
            txtUnitPrice.Text = string.Empty;
            txtOrderQty.Text = string.Empty;
            txtTotalPrice.Text = string.Empty;
            txtDiscount.Text = string.Empty;
           // ProductGroupID=String.Empty;
        }
        protected void clearInitialize()
        {

            ddlProductName.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            ddlProductGroup.SelectedIndex = 0;
            Session["ProductGroupID"] = String.Empty;
          
            txtOrderQty.Text = string.Empty;
            txtUnitPrice.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtTotalPrice.Text = string.Empty;
            txtGrandTotal.Text = string.Empty;
            txtDealer.Text=String.Empty;
            txtDealerCode.Text = String.Empty;
            txtAreaName.Text = String.Empty;
            HIDDealerCode.Value = String.Empty;
            HIDDealerArea.Value=String.Empty;
            gvOrderDetail.AllowPaging = true;
            DataTable ds = new DataTable();
            ds = null;
            gvOrderDetail.DataSource = ds;
            gvOrderDetail.DataBind();

            pnlOrderDetails.Visible = false;
            Session["sgvOrderDetail)"] = null;
          //  txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
        }

        protected void DataTableBuilder()
        {
           // DataTable dtG = GetDataTable(gvOrderDetail);
            DataTable dtG = new DataTable();
            if (Session["sgvOrderDetail)"] != null)
            {
                dtG = (DataTable)Session["sgvOrderDetail)"];
            }
            else 
            {
                dtG.Columns.Add("ProductName");
                dtG.Columns.Add("Qty");
                dtG.Columns.Add("UnitPrice");
                dtG.Columns.Add("Discount");
                dtG.Columns.Add("Total");
                dtG.Columns.Add("ProductId");
            }
        

            if (true)
            {
                object[] o =
                {ddlProductName.SelectedItem.Text.Trim(),txtOrderQty.Text.Trim(),Convert.ToDouble(txtUnitPrice.Text.Trim()),txtDiscount.Text.Trim(),
                    txtTotalPrice.Text.Trim(), ddlProductName.SelectedValue.Trim()};
                dtG.Rows.Add(o);

                if (dtG.Rows.Count > 0)
                {
                    gvOrderDetail.DataSource = null;
                    gvOrderDetail.DataBind();
                    gvOrderDetail.DataSource = dtG;
                    gvOrderDetail.DataBind();
                    
                    pnlOrderDetails.Visible = true;
                  //  EnabledisableTop(false);
                }
                else
                {
                    pnlOrderDetails.Visible = false;
                }
            }
            Session["sgvOrderDetail)"] = dtG;
        }

        DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {

                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }
            else
            {
                dt.Columns.Add("ProductName");
                dt.Columns.Add("Qty");
                dt.Columns.Add("UnitPrice");
                dt.Columns.Add("Discount");
                dt.Columns.Add("Total");
               
                dt.Columns.Add("ProductId");
               
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Text.Trim() != "&nbsp;")
                    {
                        dr[i] = row.Cells[i].Text.Trim();
                    }
                    else
                    {
                        dr[i] = string.Empty;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

    
        protected void gvOrderDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = new DataTable();
            if (Session["sgvOrderDetail)"] != null)
            {
                dt = (DataTable)Session["sgvOrderDetail)"];
            }
           
            dt.Rows[index].Delete();
            //ViewState["dt"] = dt;
            gvOrderDetail.DataSource = dt;
            gvOrderDetail.DataBind();
            if (dt.Rows.Count > 0)
            {

            }
            else
            {
              //  EnabledisableTop(true);
                pnlOrderDetails.Visible = false;
            }
            GrandTotalCal();
        }

        protected void gvOrderDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrderDetail.PageIndex = e.NewPageIndex;
            DataTable dt = new DataTable();
            dt = (DataTable)Session["sgvOrderDetail)"];
            gvOrderDetail.DataSource = null;
            gvOrderDetail.DataSource = dt;
            gvOrderDetail.DataBind();
           
        }

        protected void gvOrderDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            
            clearInitialize();
            //Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select t.DealerPrice,IsNull(Discount, 0) as Discount,t.Short1 as ProductCode from Product_Master t where t.ProdID='" + ddlProductName.SelectedValue + "'";
                DataTable dt = dba.GetDataTable(sql);

                if (dt != null)
                {
                    txtUnitPrice.Text = dt.Rows[0]["DealerPrice"].ToString();
                   txtProductCode.Text=dt.Rows[0]["ProductCode"].ToString();
                }
                else
                {
                    txtUnitPrice.Text = string.Empty;
                    txtProductCode.Text = string.Empty;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory();
           
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

     
    }
}