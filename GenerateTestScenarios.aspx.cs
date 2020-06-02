using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PI {
    public partial class GenerateTestScenarios : System.Web.UI.Page {
        string firstAlbum = "917161";
        string firstEmail = "tester@emailer.domainer";
        string secondAlbum = "115511";
        string secondEmail = "test@email.domain";
        string connectionString = "Data Source=ABYDOS-WSS-GOR\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";


        protected void Page_Load(object sender, EventArgs e) {

        }

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