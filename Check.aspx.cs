using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PI {
    ///<summary>
    ///The main <c>Check</c> class
    ///Contains all methods that are able to be called on Check.aspx
    ///</summary>
    public partial class Check : System.Web.UI.Page {
        /// <summary>
        /// Stores our database connection string 
        /// </summary>
        static string connectionString = "Data Source=ABYDOS-WSS-GOR\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";
        /// <summary>
        /// Stores SQLConnection object
        /// </summary>
        SqlConnection conn = new SqlConnection(connectionString);
        /// <summary>
        /// Stores List of strings. 
        /// Reads the list form Session
        /// </summary>
        List<string> variablesList;


        /// <summary>
        /// Set specific variables when page loads
        /// </summary>
        /// <param name="sender">Object who calls this method</param>
        /// <param name="e">Arguments of the event</param>
        protected void Page_Load(object sender, EventArgs e) {
            if (Session["Variables"] != null) {
                variablesList = (List<string>)Session["Variables"];
          

            }
        }


        /// <summary>
        /// Redirect to login.aspx
        /// </summary>
        /// <param name="sender">Object who calls this method</param>
        /// <param name="e">Arguments of the event</param>
        protected void returnButton_Click(object sender, EventArgs e) {
            Response.Redirect("login.aspx");
        }

        /// <summary>
        /// Get answers saved with specific password from database and display them in grid, get saved hash and display it and also generate new hash (so user can compare all three hashes [email/saved in database/freshly generated]
        /// </summary>
        /// <param name="sender">Object who calls this method</param>
        /// <param name="e">Arguments of the event</param>
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

           
            string hash = variablesList[0] + variablesList[1] + GridView1.Rows[0].Cells[0].Text + GridView1.Rows[0].Cells[1].Text + GridView1.Rows[0].Cells[2].Text;
            Label2.Text = "Swiezo wygenerowany hash: \n" + Survey.GetHashString(hash);


        }


    }
}