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
    ///The main <c>GenerateTestScenarios</c> class
    ///Contains all methods that are able to be called on GenerateTestScenarios.aspx
    ///This page is only used by the testing enviroment.
    ///</summary>
    public partial class GenerateTestScenarios : System.Web.UI.Page {
        /// <summary>
        /// Stores album number for first test user
        /// </summary>
        string firstAlbum = "917161";
        /// <summary>
        /// Stores email for first test user
        /// </summary>
        string firstEmail = "tester@emailer.domainer";
        /// <summary>
        /// Stores album number for second test user
        /// </summary>
        string secondAlbum = "115511";
        /// <summary>
        /// Stores email for first second user
        /// </summary>
        string secondEmail = "test@email.domain";
        /// <summary>
        /// Stores our database connection string 
        /// </summary>
        string connectionString = "Data Source=ABYDOS-WSS-GOR\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";


        /// <summary>
        /// Set specific variables when page loads. On this specific page this method did not do anything.
        /// </summary>
        /// <param name="sender">Object who calls this method</param>
        /// <param name="e">Arguments of the event</param>
        protected void Page_Load(object sender, EventArgs e) {

        }

        /// <summary>
        /// Edit info label after clicking button. Call method <c>userCreate</c> to create users.
        /// </summary>
        /// <param name="sender">Object who calls this method</param>
        /// <param name="e">Arguments of the event</param>
        protected void GenerateButton_Click(object sender, EventArgs e) {
            bool first = userCreate(firstAlbum, firstEmail, 0);
            bool second = userCreate(secondAlbum, secondEmail, 1);

            if (first==false && second==false) {
                InfoLabel.Text = "Testowi uzytkowicy istenija juz w bazie";
            }
            else{
                InfoLabel.Text = "Dodano";

            }
        }

        /// <summary>
        /// Create users and save them in database.
        /// </summary>
        /// <param name="album">Test user album number</param>
        /// <param name="email">Test user email</param>
        /// <param name="voted">Flag if user voted</param>
        /// <returns>
        /// False if user is already in database
        /// True if user was created (set flag based on 'voted' value)
        /// </returns>
        protected bool userCreate(string album, string email, int voted) {
            var select = "SELECT saved FROM dbo.Users where album = '" + album + "' and mail = '" + email + "'";
            var c = new SqlConnection(connectionString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            var dt = ds.Tables[0];

            // User exist in database
            if (dt.Rows.Count > 0) {
                return false;
            }
            // User does not exist (create new user)
            else if (dt.Rows.Count == 0) {
                // second user
                if (voted == 1) {
                    var connection = new SqlConnection(connectionString);
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "insert into Users(album,mail,saved) values('" + album + "','" + email + "','" + voted + "')";

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    // Add test hash and answers
                    command.CommandText = "insert into dbo.UsrAnswers(passwd,answr_1,answr_2,answr_3,hash) values ('" + "unitTest" + "'," + 4 + "," + 4 + "," + 4 + ",'" + "DF0B1A55BBF510A1115DACE4408667689E159B981F7E0A6562AA48CD93C91C40" + "')";
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
                else {
                    var connection = new SqlConnection(connectionString);
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "insert into Users(album,mail) values('" + album + "','" + email + "')";

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                

            }

            return true;

        }


    }
}