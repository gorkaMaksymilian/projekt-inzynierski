using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PI {
    public partial class Survey : System.Web.UI.Page {
        //Zmien mnie mihau
        string connectionString = "Data Source=ABYDOS-WSS-GOR\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";
        int numberOfQuestions = 0;

        protected void Page_Load(object sender, EventArgs e) {
            LoadQuestions();
            GenerateRBL();
            dataLabel.Text = "numer albumu: " + Request.QueryString["album"] + "\temail: " + Request.QueryString["email"];

        }


        //Wczytuje pytania do QuestionsGrid
        protected void LoadQuestions() {
            var select = "select question from dbo.Questions";
            var c = new SqlConnection(connectionString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            var dt = ds.Tables[0];
            numberOfQuestions = dt.Rows.Count;

            QuestionsGrid.DataSource = dt;
            QuestionsGrid.DataBind();

        }

        protected void GenerateRBL() {
            //HtmlTableRow row = new HtmlTableRow();

            for (int i = 0; i < numberOfQuestions; i++) {
                //HtmlTableCell cell = new HtmlTableCell();
                RadioButtonList radio = new RadioButtonList();
                for (int j = 1; j <=6; j++) {
                    radio.Items.Add(j.ToString());
                }

                radio.RepeatDirection = RepeatDirection.Horizontal;
                radio.CellSpacing = 0;

                //cell.Controls.Add(radio);
                test1.Controls.Add(radio);
            }
           
            



        }
    }
}