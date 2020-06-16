using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PI {
    /// <summary>
    ///The main <c>Summary</c> class
    ///Contains all methods that are able to be called on Summary.aspx
    /// </summary>
    public partial class Summary : System.Web.UI.Page {
        /// <summary>
        /// Stores our database connection string 
        /// </summary>
        string connectionString = "Data Source=ABYDOS-WSS-GOR\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";

        /// <summary>
        /// Load data from SQL database and display it. 
        /// </summary>
        /// <param name="sender">Object who calls this method</param>
        /// <param name="e">Arguments of the event</param>
        protected void Page_Load(object sender, EventArgs e) {
            var select = "SELECT saved FROM dbo.Users";
            var c = new SqlConnection(connectionString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            var dt = ds.Tables[0];

            int voteCounter = 0;
            int userCounter = dt.Rows.Count;

            // There are users in database
            if (userCounter > 0) {
                for (int iterator = 0; iterator < userCounter; iterator++) { 
                    if (dt.Rows[iterator][0].ToString() == "True") {
                        voteCounter++;
                    }
                }
                dataLabel.Text = "Wszystkich uzytkownikow jest: " + userCounter + "<br/>Zaglosowalo: " + voteCounter + "<br/>Procentowo: " + ((float)voteCounter / (float)userCounter * 100).ToString("0.00") + "%";
            }
            // Table is empty
            else if (userCounter == 0) {
                errorLabel.Text = "W bazie nie ma zadnego uzytkownika.";
            }


        }
    }
}