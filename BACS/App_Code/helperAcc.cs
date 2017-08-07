using System;
using System.Data;
using System.Web.UI.WebControls;
using Saplin.Controls;

namespace PNF
{
    class helperAcc
    {
        public void LoadDll(DropDownList ddl, DataTable dt, string valueField, string textField, string blankField)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddl.DataTextField = textField;
                    ddl.DataValueField = valueField;
                    ddl.DataSource = dt;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("--" + blankField + "--", ""));
                    ddl.SelectedIndex = 0;
                }
                else
                {
                    ddl.Items.Clear();
                    ddl.Items.Insert(0, new ListItem("...No Data Found...", ""));
                }
            }
            catch (Exception ex)
            {
                ddl.Items.Clear();
                ddl.Items.Add(new ListItem("--" + blankField + "--", "0"));
                //ddlProduct.Enabled = false;
            }
        }
        public void LoadListbx(ListBox ddl, DataTable dt, string valueField, string textField, string blankField)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddl.DataTextField = textField;
                    ddl.DataValueField = valueField;
                    ddl.DataSource = dt;
                    ddl.DataBind();
                    //ddl.Items.Insert(0, new ListItem("--" + blankField + "--", ""));
                }
                else
                {
                    ddl.Items.Clear();
                    //ddl.Items.Insert(0, new ListItem("...No Data Found...", ""));
                }
            }
            catch (Exception ex)
            {
                ddl.Items.Clear();
                //ddl.Items.Add(new ListItem("--" + blankField + "--", "0"));
                //ddlProduct.Enabled = false;
            }
        }
        public void LoadCheckbx(CheckBoxList ddl, DataTable dt, string valueField, string textField, string blankField)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddl.DataTextField = textField;
                    ddl.DataValueField = valueField;
                    ddl.DataSource = dt;
                    ddl.DataBind();
                    //ddl.Items.Insert(0, new ListItem("--" + blankField + "--", ""));
                }
                else
                {
                    ddl.Items.Clear();
                    //ddl.Items.Insert(0, new ListItem("...No Data Found...", ""));
                }
            }
            catch (Exception ex)
            {
                ddl.Items.Clear();
                //ddl.Items.Add(new ListItem("--" + blankField + "--", "0"));
                //ddlProduct.Enabled = false;
            }
        }
        public void LoadDropDownCheckBoxes(DropDownCheckBoxes ddl, DataTable dt, string valueField, string textField, string blankField)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddl.DataTextField = textField;
                    ddl.DataValueField = valueField;
                    ddl.DataSource = dt;
                    ddl.DataBind();
                    //ddl.Items.Insert(0, new ListItem("--" + blankField + "--", ""));
                }
                else
                {
                    ddl.Items.Clear();
                    //ddl.Items.Insert(0, new ListItem("...No Data Found...", ""));
                }
            }
            catch (Exception ex)
            {
                ddl.Items.Clear();
                //ddl.Items.Add(new ListItem("--" + blankField + "--", "0"));
                //ddlProduct.Enabled = false;
            }
        }
    }
}
