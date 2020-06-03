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
    ///The main <c>Login</c> class
    ///Contains all methods that are able to be called on Login.aspx
    /// </summary>
    public partial class Login : System.Web.UI.Page {
        string connectionString = "Data Source=ABYDOS-WSS-GOR\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";

        /// <summary>
        /// Set specific variables when page loads. On this specific page this method did not do anything.
        /// </summary>
        /// <param name="sender">Object who calls this method</param>
        /// <param name="e">Arguments of the event</param>
        protected void Page_Load(object sender, EventArgs e) {
            
        }


        /// <summary>
        /// Validate email and login, save them in Session and redirect to page survey.aspx after button click
        /// </summary>
        /// <param name="sender">Object who calls this method</param>
        /// <param name="e">Arguments of the event</param>
        protected void Button1_Click(object sender, EventArgs e) {
            LoginValidator emailExist = new EmailPresent();
            LoginValidator emailValid = new EmailValid();
            LoginValidator albumExist = new AlbumNumberPresent();
            LoginValidator albumValid = new AlbumNumberValid();

            emailExist.SetNextHandler(emailValid);
            emailValid.SetNextHandler(albumExist);
            albumExist.SetNextHandler(albumValid);

            List<string> ErrorList = emailExist.HandleRequest(this);
            foreach (string error in ErrorList) {
                ErrorLabel.Text = error;
            }
            
            if(ErrorList.Count==0) {
                ErrorLabel.Text = "";
                if (userExist()) {
                    List<string> list = new List<string>();
                    list.Add(nralbumu.Text);
                    list.Add(email.Text);
                    Session["Variables"] = list;

                    Response.Redirect("Survey.aspx");
                }
            }




        }

        /// <summary>
        /// Checks if user already exist in database and if he already voted.
        /// </summary>
        /// <returns>
        /// Return true if user did not vote yet but exist.
        /// Return false if user exist and he voted.
        /// </returns>
        protected bool userExist() {
            var select = "SELECT saved FROM dbo.Users where album = '" + nralbumu.Text + "' and mail = '" + email.Text + "'";
            var c = new SqlConnection(connectionString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            var dt = ds.Tables[0];

            //User exist in database
            if (dt.Rows.Count > 0) {
                ErrorLabel.Text = dt.Rows[0][0].ToString();
                //User already voted (display message in label)
                if (dt.Rows[0][0].ToString() == "True") {
                    ErrorLabel.Text = "Uzytkownik juz oddal glos! Sprawdz swoj mail.";
                }
                //User can vote (so redirect)
                else if (dt.Rows[0][0].ToString() == "False") {
                    return true;
                }


            }
            //User does not exist (create new user)
            else if (dt.Rows.Count == 0) {
                addUser();
                ErrorLabel.Text = "Uzytkownik dodany do bazy nacisnij zaloguj ponownie by przejsc do ankiety";
            }


            return false;
        }

        /// <summary>
        /// Add save user in our database
        /// </summary>
        protected void addUser() {
            var connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into Users(album,mail) values('" + nralbumu.Text + "','" + email.Text + "')";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
           
        }


        /// <summary>
        /// Check if email and album number are valid.
        /// </summary>
        /// <param name="sender">Object who calls this method</param>
        /// <param name="e">Arguments of the event</param>
        protected void Sprawdz(object sender, EventArgs e) {
            LoginValidator emailExist = new EmailPresent();
            LoginValidator emailValid = new EmailValid();
            LoginValidator albumExist = new AlbumNumberPresent();
            LoginValidator albumValid = new AlbumNumberValid();

            emailExist.SetNextHandler(emailValid);
            emailValid.SetNextHandler(albumExist);
            albumExist.SetNextHandler(albumValid);

            List<string> ErrorList = emailExist.HandleRequest(this);
            foreach (string error in ErrorList) {
                ErrorLabel.Text = error;
            }

            if (ErrorList.Count == 0) {
                ErrorLabel.Text = "";
                List<string> list = new List<string>();
                list.Add(nralbumu.Text);
                list.Add(email.Text);
                Session["Variables"] = list;
                Response.Redirect("check.aspx");

            }
            

        }

        /// <summary>
        /// Get album number from textbox
        /// </summary>
        /// <returns>Return string with album number</returns>
        public string getAlbum() {
            return nralbumu.Text;
        }

        /// <summary>
        /// Get email from textbox
        /// </summary>
        /// <returns>Return string with email</returns>
        public string getEmail() {
            return email.Text;
        }

    }




}

    