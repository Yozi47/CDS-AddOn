using CDS_AddOn.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CDS_AddOn.Models
{
    public class DBReader
    {
        private string sqlCommand;

        public DBReader(string query)
        {
            this.sqlCommand = query;
        }

        public DataSet GetTableData()
        {
            SqlConnection it4a_conn = new SqlConnection(SelectionSingleton.it4a_connStr);
            it4a_conn.Open();

            SqlCommand cmd = new SqlCommand(this.sqlCommand, it4a_conn);
            // table name   
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet tableDS = new DataSet();
            da.Fill(tableDS);  // fill dataset
            return tableDS;
        }
    }
}