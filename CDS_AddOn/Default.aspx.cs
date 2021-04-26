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
                string constr = ConfigurationManager.ConnectionStrings["It4a_org"].ToString();
                // connection string  
                SqlConnection it4a_conn = new SqlConnection(constr);
                it4a_conn.Open();

                SqlCommand prog_name_cmd = new SqlCommand("Select distinct PROG_NAME, PROG_NUM from PROGRAM ", it4a_conn);
                // table name   
                SqlDataAdapter da = new SqlDataAdapter(prog_name_cmd);
                DataSet prog_name_container_ds = new DataSet();
                da.Fill(prog_name_container_ds);  // fill dataset  
                PlanSelectorDropDownList.DataTextField = prog_name_container_ds.Tables[0].Columns["PROG_NAME"].ToString(); // text field name of table dispalyed in dropdown       
                PlanSelectorDropDownList.DataValueField = prog_name_container_ds.Tables[0].Columns["PROG_NUM"].ToString();
                // to retrive specific  textfield name   
                PlanSelectorDropDownList.DataSource = prog_name_container_ds.Tables[0];      //assigning datasource to the dropdownlist  
                PlanSelectorDropDownList.DataBind();  //binding dropdownlist  
            }
            

        }

        protected void PlanInspectorButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("PlanTreeDiagram.aspx");
        }

        protected void PlanSelectorDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Controllers.SelectionSingleton.DropdownSelection = PlanSelectorDropDownList.SelectedValue; 
        }
    }
}