using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PI {
    public partial class Check : System.Web.UI.Page {
        static string connectionString = "Data Source=ABYDOS-WSS-GOR\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectionString);
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void TypePass_TextChanged(object sender, EventArgs e) {

        }

        protected void ButtonPass_Click(object sender, EventArgs e) {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();

            String query = "select UsrAnswers.answr_1,UsrAnswers.answr_2,UsrAnswers.answr_3 from UsrAnswers where passwd = '" + TypePass.Text + "';";
            SqlCommand command = new SqlCommand(query, conn);
            //command.CommandText = "select UsrAnswers.answr_1,UsrAnswers.answr_2,UsrAnswers.answr_3 from UsrAnswers where passwd = '"+TypePass.Text+"';";
            // conn.Open();
            adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(ds);


            GridView1.DataSource = ds;
            GridView1.DataBind();
            conn.Close();

        }
    }
}