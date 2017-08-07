
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Settings
{
    public partial class Area : Page
    {
        private DatabaseAccess dba = new DatabaseAccess();
        helperAcc LoadDll = new helperAcc();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDivision();
                LoadGrid();
                rdbtnIsActive.SelectedValue = "1";
            }

        }
        private void Clear()
        {
            
            txtAreaName.Text = string.Empty;
            txtAreaName.ReadOnly = false;
            txtAreaCode.Text = string.Empty;
          
            rdbtnIsActive.SelectedValue = "1";
            ddlDivision.SelectedIndex = 0;
            
            btnSubmit.Text = "Submit";
            lblmessage.Text = "";
            HIDAreaID.Value = string.Empty;
        }
        protected void LoadDivision()
        {
            string qry = "Select * from DivisionMaster where IsActive='Y' order by DivisionName";

            DataSet ds = dba.GetDataSet(qry, "ConnDB230");
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];

                LoadDll.LoadDll(ddlDivision, dt, "DivisionID", "DivisionName", "Select Division");
            }
            else
            {

                ddlDivision.Items.Clear();
                ddlDivision.Items.Add(new ListItem("No Division Available", "0"));

            }
        }
        private void LoadGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = "select t.*,d.DivisionName from AreaMaster t left outer join DivisionMaster d on t.DivisionID=d.DivisionID order by t.AreaName";
                ds = dba.GetDataSet(query, "ConnDB230");
                GRVArea.DataSource = ds;
                GRVArea.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void Save()
        {
            string IsActive = "";
            string AreaName = txtAreaName.Text.ToString();
            string AreaCode = txtAreaCode.Text.ToString();
            string Division = ddlDivision.SelectedValue;
            if (rdbtnIsActive.SelectedValue == "0")
            {
                IsActive = "N";
            }
            else
            {
                IsActive = "Y";
            }
            string UserID = Session["UserID"].ToString();

            try
            {

                if (btnSubmit.Text == "Submit")
                {
                    string sqlchk = "select * from AreaMaster where AreaName='" + txtAreaName.Text + "'";
                    DataTable dt = dba.GetDataTable(sqlchk);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                            "alert('The Area *" + txtAreaName.Text + "* Already Exist...');", true);
                    }
                    else
                    {
                        string sqlCat = "insert into AreaMaster(DivisionID,AreaName,AreaCode,CreatedBy,IsActive) values('" + Division + "','" + AreaName + "','" + AreaCode + "','" + UserID + "','" + IsActive + "')";

                        dba.ExecuteQuery(sqlCat, "ConnDB230");
                        LoadGrid();
                        Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
                    }
                }
                if (btnSubmit.Text == "Update")
                {
                    string sqlCat = "update AreaMaster set AreaCode='" + AreaCode + "', DivisionID='" + Division + "', IsActive='" + IsActive + "',ModifiedBy='" + UserID + "' where AreaID='" + HIDAreaID.Value + "'";

                    dba.ExecuteQuery(sqlCat, "ConnDB230");
                    btnSubmit.Text = "Submit";
                    LoadGrid();
                    Clear();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Updated...');", true);
                }

            }

            catch { }


        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Save();

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void GRVArea_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GRVArea.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void GRVArea_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            HIDAreaID.Value = GRVArea.Rows[e.NewSelectedIndex].Cells[1].Text.ToString();
            txtAreaName.Text = GRVArea.Rows[e.NewSelectedIndex].Cells[3].Text.ToString();
            txtAreaCode.Text = GRVArea.Rows[e.NewSelectedIndex].Cells[4].Text.ToString().Replace("&nbsp;", "");
            

            if (GRVArea.Rows[e.NewSelectedIndex].Cells[5].Text.ToString().Replace("&nbsp;", "") == "Y")
            {
                rdbtnIsActive.SelectedValue = "1";
            }
            else
            {
                rdbtnIsActive.SelectedValue = "0";
            }
            ddlDivision.SelectedValue = GRVArea.Rows[e.NewSelectedIndex].Cells[6].Text.ToString();
            lblmessage.Text = "";
            btnSubmit.Text = "Update";
            txtAreaName.ReadOnly = true;
        }

        protected void GRVArea_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            //{
            //    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA';this.style.cursor='pointer';");
            //    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

            //}
            for (int i = 0; i <= GRVArea.Rows.Count - 1; i++)
            {
                //GRVArea.Rows[i].BackColor = i % 2 != 0 ? Color.Gainsboro : Color.LightSkyBlue;
                GRVArea.Rows[i].Cells[3].Text = GRVArea.Rows[i].Cells[3].Text.Replace(" ", "&nbsp;");

            }
            //try
            //{
            //    switch (e.Row.RowType)
            //    {
            //        case DataControlRowType.Header:
            //            //...
            //            break;
            //        case DataControlRowType.DataRow:

            //            e.Row.Attributes.Add("onclick",
            //                                 Page.ClientScript.GetPostBackEventReference(GRVArea, "Select$" +
            //                                                                             e.Row.RowIndex.ToString()));

            //            break;
            //    }

            //}
            //catch (Exception ex) { }
           // e.Row.Cells(4).Text = e.Row.Cells(4).Text.Replace(" ", "&nbsp;")
        }
    }
}