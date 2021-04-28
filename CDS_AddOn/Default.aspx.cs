using CDS_AddOn.Controllers;
using CDS_AddOn.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDS_AddOn
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string progNameQuery = "Select distinct PROG_NAME, PROG_NUM from PROGRAM ";
                DBReader reader = new DBReader(progNameQuery);
                DataSet prog_name_container_ds = reader.GetTableData();

                PlanSelectorDropDownList.DataTextField = prog_name_container_ds.Tables[0].Columns["PROG_NAME"].ToString(); // text field name of table dispalyed in dropdown       
                PlanSelectorDropDownList.DataValueField = prog_name_container_ds.Tables[0].Columns["PROG_NUM"].ToString();
                // to retrive specific  textfield name   
                PlanSelectorDropDownList.DataSource = prog_name_container_ds.Tables[0];      //assigning datasource to the dropdownlist  
                PlanSelectorDropDownList.DataBind();  //binding dropdownlist  
                PlanSelectorDropDownList.Items[0].Selected = true;
                SelectionSingleton.DropdownSelection = PlanSelectorDropDownList.SelectedValue;

            }


        }

        protected void PlanInspectorButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("PlanTreeDiagram.aspx");
        }

        protected void PlanSelectorDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectionSingleton.DropdownSelection = PlanSelectorDropDownList.SelectedValue; 
        }
    }
}