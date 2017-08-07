
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Return_Process
{
    public partial class PurchaseReturn : Page
    {
        private DatabaseAccess dba = new DatabaseAccess();
        private helperAcc LoadDll = new helperAcc();
        private string uid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                uid = Session["UserID"].ToString();
            }
            if (!IsPostBack)

            {
                LoadParty();
                pnlOrderDetails.Visible = false;
                pnlReturnDetails.Visible = false;
            }
        }
         protected void LoadParty()
        {
            string qry = "EXEC get_dealer ''";
            DataSet ds = dba.GetDataSet(qry, "ConnDB230");
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                LoadDll.LoadDll(ddlPartyName, dt, "PartyID", "Party", " Select Dealer");
            }
            else
            {

                ddlPartyName.Items.Clear();
                ddlPartyName.Items.Add(new ListItem("No Dealer Available", "0"));

            }
        }
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
         protected void LoadProduct()
         {
             string qry=String.Empty;
             if (ddlProductGroup.SelectedIndex == 0)
             {
                 qry = @"select t.ProdID,t.ProdName from Product_Master t left join Product_Category pc on t.CategoryID=pc.CategoryID left join ProductGroup pg on pc.ProductGroupID=pg.ProductGroupID 
                          where t.IsActive='Y' and t.ProdID in(select distinct d.ProdID from InvoiceMaster t left join InvoiceDetails d on t.InvID=d.InvID where t.IsApprove='Y' and t.IsCancel<>'Y' and t.PartyID='" +
                              ddlPartyName.SelectedValue + "') order by ProdName";
             }
             else
             {
                 qry = @"select t.ProdID,t.ProdName from Product_Master t left join Product_Category pc on t.CategoryID=pc.CategoryID left join ProductGroup pg on pc.ProductGroupID=pg.ProductGroupID 
                          where t.IsActive='Y' and pg.ProductGroupID='" + ddlProductGroup.SelectedValue + "' and t.ProdID in(select distinct d.ProdID from InvoiceMaster t left join InvoiceDetails d on t.InvID=d.InvID where t.IsApprove='Y' and t.IsCancel<>'Y' and t.PartyID='" + ddlPartyName.SelectedValue + "') order by ProdName";
             }
            
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
         protected void LoadChallan()
         {
             string qry = String.Empty;

                     qry = @"select t.ChallanMasterID,t.ChallanNumber from ChallanMaster t,Delivery d
                             where t.ChallanMasterID=d.ChallanMasterID
                             and t.InvID='" + ddlInvoice.SelectedValue + "' order by t.ChallanMasterID ";
                 

             DataSet ds = dba.GetDataSet(qry, "ConnDB230");
             if (ds != null)
             {
                 DataTable dt = ds.Tables[0];
                 LoadDll.LoadDll(ddlChallan, dt, "ChallanMasterID", "ChallanNumber", " Select Challan Number");

             }
             else
             {

                 ddlChallan.Items.Clear();
                 ddlChallan.Items.Add(new ListItem("No Challan Number Available", "0"));

             }
         }
         protected void LoadInvoice()
         {   string qry=String.Empty;
             if (ddlProductGroup.SelectedIndex==0)
             {
                 if (ddlProductName.SelectedIndex == 0)
                 {
                     qry = @"select t.InvID,t.InvNumber from InvoiceMaster t,ChallanMaster cm,Delivery d                                     
                                    where 
                                    t.InvID=cm.InvID
                                    and cm.ChallanMasterID=d.ChallanMasterID
                                    and t.PartyID='" + ddlPartyName.SelectedValue + "' and t.IsApprove='Y' and t.IsCancel<>'Y' order by t.InvNumber";
                 }
                 else
                 {
                     qry = @"select t.InvID,t.InvNumber from InvoiceMaster t,ChallanMaster cm,Delivery d  
                            where
                             t.InvID=cm.InvID
                              and cm.ChallanMasterID=d.ChallanMasterID
                             and t.PartyID='" + ddlPartyName.SelectedValue + "' and t.IsApprove='Y' and t.IsCancel<>'Y' and t.InvID in(select d.InvID from InvoiceDetails d where d.ProdID='" + ddlProductName.SelectedValue + "') order by t.InvNumber";
                 }
             }
             else
             {
                 if (ddlProductName.SelectedIndex == 0)
                 {
                     qry = @" select t.InvID,t.InvNumber from InvoiceMaster t,ChallanMaster cm,Delivery d                                     
                                    where 
                                    t.InvID=cm.InvID
                                    and cm.ChallanMasterID=d.ChallanMasterID and t.PartyID='" + ddlPartyName.SelectedValue + "' and t.ProductGroupID='" + ddlProductGroup.SelectedValue + "' and t.IsApprove='Y' and t.IsCancel<>'Y' order by t.InvNumber";
                 }
                 else
                 {
                     qry =@" select t.InvID,t.InvNumber from InvoiceMaster t,ChallanMaster cm,Delivery d                                     
                                    where 
                                    t.InvID=cm.InvID
                                    and cm.ChallanMasterID=d.ChallanMasterID and t.PartyID='" + ddlPartyName.SelectedValue + "' and t.ProductGroupID='" + ddlProductGroup.SelectedValue + "' and t.IsApprove='Y' and t.IsCancel<>'Y'  and t.InvID in(select d.InvID from InvoiceDetails d where d.ProdID='" + ddlProductName.SelectedValue + "') order by t.InvNumber";
                 }  
             }
            
             
             DataSet ds = dba.GetDataSet(qry, "ConnDB230");
             if (ds != null)
             {
                 DataTable dt = ds.Tables[0];
                 LoadDll.LoadDll(ddlInvoice, dt, "InvID", "InvNumber", " Select Invoice Number");
                 
             }
             else
             {

                 ddlInvoice.Items.Clear();
                 ddlInvoice.Items.Add(new ListItem("No Invoice Number Available", "0"));

             }
         }
         private void LoadInvoiceDetails()
         {
             try
             {   String query=String.Empty;
                 DataSet ds = new DataSet();
                 if (ddlProductName.SelectedIndex == 0)
                 {
                     query = @"select t.InvDetailID,cd.ChallanDetailID,t.InvID,t.ProdID,(isnull(cd.ChalanQty,0)-isnull(t.ReturnQuantity,0)) Quantity,convert(float,t.UnitPrice) as UnitPrice,CONVERT(float,t.Discount) as Discount
                                ,((isnull(cd.ChalanQty,0)-isnull(t.ReturnQuantity,0))*convert(float,t.UnitPrice))-(isnull(cd.ChalanQty,0)-isnull(t.ReturnQuantity,0))*convert(float,t.UnitPrice)*(CONVERT(float,t.Discount)*0.01) as Total,t.ApprovedQty,t.ApprovedTotal,CONVERT(float,t.ApprovedDiscount) as ApprovedDiscount
                                ,p.ProdName,isnull(t.ReturnQuantity,0) as ReturnQuantity,isnull(t.ReturnTotal,0) as ReturnTotal 
                                from  InvoiceDetails t 
                                left join ChallanMaster cm on t.InvID=cm.InvID
                                left join ChallanDetail cd on cm.ChallanMasterID=cd.ChallanMasterID
                                left join Product_Master p on t.ProdID=p.ProdID                                   
                                where cm.IsDeliver='Y' and t.ProdID=cd.ProdID and cm.InvID='" + ddlInvoice.SelectedValue + "' order by t.InvDetailID";
                 }
                 else
                 {
                     query = @"select t.InvDetailID,cd.ChallanDetailID,t.InvID,t.ProdID,(isnull(cd.ChalanQty,0)-isnull(t.ReturnQuantity,0)) Quantity,convert(float,t.UnitPrice) as UnitPrice,CONVERT(float,t.Discount) as Discount
                                ,((isnull(cd.ChalanQty,0)-isnull(t.ReturnQuantity,0))*convert(float,t.UnitPrice))-(isnull(cd.ChalanQty,0)-isnull(t.ReturnQuantity,0))*convert(float,t.UnitPrice)*(CONVERT(float,t.Discount)*0.01) as Total,t.ApprovedQty,t.ApprovedTotal,CONVERT(float,t.ApprovedDiscount) as ApprovedDiscount
                                ,p.ProdName,isnull(t.ReturnQuantity,0) as ReturnQuantity,isnull(t.ReturnTotal,0) as ReturnTotal 
                                from  InvoiceDetails t 
                                left join ChallanMaster cm on t.InvID=cm.InvID
                                left join ChallanDetail cd on cm.ChallanMasterID=cd.ChallanMasterID
                                left join Product_Master p on t.ProdID=p.ProdID 
                                where cm.IsDeliver='Y' and t.ProdID=cd.ProdID and t.InvID='" + ddlInvoice.SelectedValue + "' and t.ProdID='" + ddlProductName.SelectedValue + "' order by t.InvDetailID";
                 }
                  
                 ds = dba.GetDataSet(query, "ConnDB230");
                 if (ds!=null)
                 {
                     gvInvoiceDetails.DataSource = ds;
                     gvInvoiceDetails.DataBind();
                     pnlOrderDetails.Visible = true;
                 }
                 else
                 {
                     pnlOrderDetails.Visible = false;
                     
                 }
                

             }
             catch (Exception ex)
             { throw ex; }

         }
         private void LoadReturnProducts()
         {
             String VpartyID=String.Empty;
             String vprductgroupID=String.Empty;
             String vproductID=String.Empty;
             String vInvID=String.Empty;
             if (ddlPartyName.SelectedIndex != 0)
             {
                 VpartyID = ddlPartyName.SelectedValue;
             }
             if (ddlPartyName.SelectedIndex != 0)
             {
                 vprductgroupID = ddlProductGroup.SelectedValue;
             }
             if (ddlPartyName.SelectedIndex != 0)
             {
                 vproductID = ddlProductName.SelectedValue;
             }
             if (ddlPartyName.SelectedIndex != 0)
             {
                 vInvID = ddlInvoice.SelectedValue;
             }
             try
             {
                 DataSet ds = new DataSet();
//                 String query = @"select t.InvDetailID,t.InvID,invm.InvNumber,t.ProdID,pm.ProdName,ivd.UnitPrice,ivd.Discount,t.ReturnQuantity,t.ReturnTotal,t.IsSellable,date from PurchaseReturn t
//                                inner join PartyMaster p on t.PartyID=p.PartyID
//                                inner join Product_Master pm on t.ProdID=pm.ProdID
//                                LEFT JOIN InvoiceDetails ivd on t.InvDetailID=ivd.InvDetailID
//                                inner join InvoiceMaster invm on t.InvID=invm.InvID
//                                  where t.PartyID='" +ddlPartyName.SelectedValue+"' order by t.ReturnID;";
                 String query = "EXEC Get_Return_details '" + VpartyID + "','" + vprductgroupID + "','" + vproductID + "','" + vInvID + "'";
                 ds = dba.GetDataSet(query, "ConnDB230");
                 if (ds != null)
                 {
                     gvReturnDetails.DataSource = ds;
                     gvReturnDetails.DataBind();
                     pnlReturnDetails.Visible = true;
                 }
                 else
                 {
                     pnlReturnDetails.Visible = false;

                 }
                
             }
             catch (Exception ex)
             { throw ex; }

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
         protected void txtReturnQuantity_TextChanged(object sender, EventArgs e)
         {
             try
             {
                 TextBox thisTextBox = (TextBox)sender;
                 GridViewRow currentRow = (GridViewRow)thisTextBox.Parent.Parent;
                 int rowindex = 0;
                 rowindex = currentRow.RowIndex;

                 Label lblInvoiceQuantiity = (Label)currentRow.FindControl("txtQuantity");
                 Int32 InvoiceQuantiity = Convert.ToInt32(nullChecker(lblInvoiceQuantiity.Text));

                 TextBox txtReturnQuantity = (TextBox)currentRow.FindControl("txtReturnQuantity");
                 Int32 ReturnQuantity = Convert.ToInt32(nullChecker(txtReturnQuantity.Text));

                 if (ReturnQuantity > InvoiceQuantiity)
                 {
                     txtReturnQuantity.Text = Convert.ToString(ReturnQuantity);
                     txtReturnQuantity.Focus();
                     ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Accept greater than Invoice Quantity');", true);
                 }
                 else
                 {
                     Label lblReturntotal = (Label)currentRow.FindControl("lblReturntotal");
                     double Returntotal = Convert.ToDouble(nullChecker(lblReturntotal.Text));
                     Label lblPrice = (Label)currentRow.FindControl("lblPrice");
                     double Price = Convert.ToDouble(nullChecker(lblPrice.Text));
                     Label txtDiscount = (Label)currentRow.FindControl("txtDiscount");
                     double Discount = Convert.ToDouble(nullChecker(txtDiscount.Text));
                     double vdiscount = 0;
                     vdiscount = (Discount / 100) * Price * ReturnQuantity;

                     Returntotal = (ReturnQuantity * Price) - vdiscount;
                     lblReturntotal.Text = Convert.ToString(Returntotal);
                 }
             }
             catch
             {

             }

         }

        protected void Clear()
        {
            ddlInvoice.SelectedIndex = 0;
            pnlOrderDetails.Visible = false;
        }
         protected void btnSubmit_Click(object sender, EventArgs e)
         {
             string resOut = string.Empty;
             Int32 totalQTY = 0;
             double totalReturnamount = 0;
             string addedby = uid;
             try
             {

                     foreach (GridViewRow row in gvInvoiceDetails.Rows)
                     {
                         TextBox txtReturnQuantity = (TextBox)row.FindControl("txtReturnQuantity");
                         Int32 ActualReturnQuantity = Convert.ToInt32(nullChecker(txtReturnQuantity.Text));

                         Int32 ReturnQuantity = Convert.ToInt32(nullChecker(txtReturnQuantity.Text));

                         if (ReturnQuantity >= 1)
                         {
                             string InvDetailID = nullChecker(row.Cells[8].Text);
                             int vprodId = Convert.ToInt32(nullChecker(row.Cells[7].Text));
                             int vPreviousReturnQuantity = Convert.ToInt32(nullChecker(row.Cells[9].Text));
                             double vPreviousReturnTotal = Convert.ToDouble(nullChecker(row.Cells[10].Text));
                             int vChallanDetailID = Convert.ToInt32(nullChecker(row.Cells[12].Text));

                             Label txtQuantity = (Label)row.FindControl("txtQuantity");
                             Int32 InvoiceQuantity = Convert.ToInt32(nullChecker(txtQuantity.Text));

                             Label txtDiscount = (Label)row.FindControl("txtDiscount");
                             Double Discount = Convert.ToDouble(nullChecker(txtDiscount.Text));

                             Label lblReturntotal = (Label)row.FindControl("lblReturntotal");
                             double ReturnTotal = Convert.ToDouble(nullChecker(lblReturntotal.Text));
                             double PresentReturnTotal = ReturnTotal;
                             ReturnTotal = ReturnTotal + vPreviousReturnTotal;

                             Label lblPrice = (Label)row.FindControl("lblPrice");
                             double Price = Convert.ToDouble(nullChecker(lblPrice.Text));
                             CheckBox vchkIsSellable = (CheckBox)row.FindControl("chkIsSellable");

                             Int32 PresentQuantity = InvoiceQuantity - ReturnQuantity;
                             Int32 PresentReturnQuantity = ReturnQuantity;
                             ReturnQuantity = ReturnQuantity + vPreviousReturnQuantity;

                          

                             int UpdatedQty = 0;
                             if (vchkIsSellable.Checked)
                             {
                                 string QryPurchaseReturn = @"insert into PurchaseReturn(InvID,InvDetailID,ProdID,ReturnQuantity,ReturnTotal,IsSellable,PartyID,Remarks) values('" + ddlInvoice.SelectedValue + "','" + InvDetailID + "','" + vprodId + "','" + PresentReturnQuantity + "','" + PresentReturnTotal + "','Y','" + ddlPartyName.SelectedValue + "','"+txtRemarks.Text+"')";
                                 resOut = dba.ExecuteQuery(QryPurchaseReturn, "ConnDB230");
                                 string QrySalesReturn = @"EXEC updateSalesReturn '" + ddlInvoice.SelectedValue + "','" + InvDetailID + "','" + ReturnQuantity + "','" + ReturnTotal + "','" + ActualReturnQuantity + "','" + vChallanDetailID + "'";
                                 resOut = dba.ExecuteQuery(QrySalesReturn, "ConnDB230");
                                 string StockQty = "select StockQty from IStock where ProdID='" + vprodId + "'";
                                 DataTable dt = dba.GetDataTable(StockQty);
                                 if (dt != null && dt.Rows.Count > 0)
                                 {
                                     UpdatedQty = Convert.ToInt32(dt.Rows[0][0].ToString()) + ReturnQuantity;
                                     string QryIStock = @"update IStock 
                                    set StockQty='" + UpdatedQty + "' where ProdID='" + vprodId + "'";
                                     dba.ExecuteQuery(QryIStock, "ConnDB230");
                                 }
                                 else
                                 {
                                     string QryDamageStock = @"insert into IStock(ProdID,StockQty) values('" + vprodId + "','" + ReturnQuantity + "')";

                                     dba.ExecuteQuery(QryDamageStock, "ConnDB230");
                                 }

                             }
                             else
                             {
                                 string QryPurchaseReturn = @"insert into PurchaseReturn(InvID,InvDetailID,ProdID,ReturnQuantity,ReturnTotal,IsSellable,PartyID,Remarks) values('" + ddlInvoice.SelectedValue + "','" + InvDetailID + "','" + vprodId + "','" + PresentReturnQuantity + "','" + PresentReturnTotal + "','N','" + ddlPartyName.SelectedValue + "','" + txtRemarks.Text + "')";
                                 resOut = dba.ExecuteQuery(QryPurchaseReturn, "ConnDB230");
//                                 string QryOrderDetail = @"update InvoiceDetails 
//                                    set IsReturn='Y',Quantity='" + PresentQuantity + "',ReturnQuantity='" + ReturnQuantity + "',ReturnTotal='" + ReturnTotal + "' where InvDetailID='" + InvDetailID + "' and InvID='" + ddlInvoice.SelectedValue + "'";
//                                 resOut = dba.ExecuteQuery(QryOrderDetail, "ConnDB230");
                                 string QrySalesReturn = @"EXEC updateSalesReturn '" + ddlInvoice.SelectedValue + "','" + InvDetailID + "','" + ReturnQuantity + "','" + ReturnTotal + "','"+ActualReturnQuantity+"','"+vChallanDetailID+"'";
                                 resOut = dba.ExecuteQuery(QrySalesReturn, "ConnDB230");
                                 string StockQty = "select StockQty from DamageStock where ProdID='" + vprodId + "'";
                                 DataTable dt = dba.GetDataTable(StockQty);
                                 if (dt != null && dt.Rows.Count > 0)
                                 {
                                     UpdatedQty = Convert.ToInt32(dt.Rows[0][0].ToString()) + ReturnQuantity;
                                     string QryDamageStock = @"update DamageStock 
                                    set StockQty='" + UpdatedQty + "' where ProdID='" + vprodId + "'";
                                     dba.ExecuteQuery(QryDamageStock, "ConnDB230");

                                 }
                                 else
                                 {

                                     string QryDamageStock = @"insert into DamageStock(ProdID,StockQty) values('" + vprodId + "','" + ReturnQuantity + "')";

                                     dba.ExecuteQuery(QryDamageStock, "ConnDB230");
                                 }

                             }
                         }
                     }
             }
             catch
             {

             }

             if (resOut == "1" || resOut == "2" || resOut == "3")
             {

                 ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Product Successfuly Returned  ...');",
                     true);
                 Clear();
                 LoadReturnProducts();
             }
             else
             {
                 ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Returned...');",
                     true);
             }

         }
        
        protected void ddlPartyName_TextChanged(object sender, EventArgs e)
         {
             LoadProductGroup();
             LoadProduct();
             LoadInvoice();
             LoadReturnProducts();
             pnlOrderDetails.Visible = false;

         }
         protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
         {
             LoadProduct();
             LoadInvoice();
             LoadReturnProducts();
             LoadInvoiceDetails();
             pnlOrderDetails.Visible = false;
         }
         protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
         {
             LoadInvoice();
             LoadReturnProducts();
             LoadInvoiceDetails();
             pnlOrderDetails.Visible = false;
         }
         protected void ddlInvoice_SelectedIndexChanged(object sender, EventArgs e)
         {
             LoadInvoiceDetails();
             LoadReturnProducts();
       
         }
         protected void ddlChallan_SelectedIndexChanged(object sender, EventArgs e)
         {
             //LoadInvoiceDetails();
             //LoadReturnProducts();
         }
         protected void btnCancel_Click(object sender, EventArgs e)
         {

         }
         protected void gvReturnDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
         {
             gvReturnDetails.PageIndex = e.NewPageIndex;
             LoadReturnProducts();
         }
         protected void gvReturnDetails_RowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA';this.style.cursor='pointer';");
                 e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

             }
             for (int i = 0; i <= gvReturnDetails.Rows.Count - 1; i++)
             {
                 gvReturnDetails.Rows[i].BackColor = i % 2 != 0 ? Color.Gainsboro : Color.LightSkyBlue;
             }
             try
             {
                 switch (e.Row.RowType)
                 {
                     case DataControlRowType.Header:
                         //...
                         break;
                     case DataControlRowType.DataRow:

                         e.Row.Attributes.Add("onclick",
                                              Page.ClientScript.GetPostBackEventReference(gvReturnDetails, "Select$" +
                                                                                          e.Row.RowIndex.ToString()));

                         break;
                 }

             }
             catch (Exception ex) { }
         }
        
    }

   
    }
