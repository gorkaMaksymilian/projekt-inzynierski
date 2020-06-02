using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace PI {
    public partial class Login : System.Web.UI.Page {
        string connectionString = "Data Source=ABYDOS-WSS-GOR\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e) {
            
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e) {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e) {

        }

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

        protected void addUser() {
            var connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into Users(album,mail) values('" + nralbumu.Text + "','" + email.Text + "')";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
           
        }


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

        public string getAlbum() {
            return nralbumu.Text;
        }

        public string getEmail() {
            return email.Text;
        }

    }




}

    