﻿using System;
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

        protected void returnButton_Click(object sender, EventArgs e) {
            Response.Redirect("login.aspx");
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

            query = "select hash from UsrAnswers where passwd = '" + TypePass.Text + "';";
            command = new SqlCommand(query, conn);

            conn.Open();
            using (SqlDataReader reader = command.ExecuteReader()) {
                if (reader.Read()) {
                    Label1.Text = "Zapisany hash to: \n" + reader.GetString(0);
                }
            }
            conn.Close();


            string hash = Request.QueryString["album"] + Request.QueryString["email"] + GridView1.Rows[0].Cells[0].Text + GridView1.Rows[0].Cells[1].Text + GridView1.Rows[0].Cells[2].Text;
            Label2.Text = "Swiezo wygenerowany hash: \n" + Survey.GetHashString(hash);


        }


    }
}