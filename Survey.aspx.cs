using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PI {
    ///<summary>
    ///The main <c>Survey</c> class
    ///Contains all methods that are able to be called on Survey.aspx
    ///</summary>
    public partial class Survey : System.Web.UI.Page {
        
        /// <summary>
        /// Store our database connection string 
        /// </summary>
        string connectionString = "Data Source=ABYDOS-WSS-GOR\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";
        /// <summary>
        /// Store number of questions loaded form SQL querry
        /// </summary>
        int numberOfQuestions = 0;
        /// <summary>
        /// Store List of RadioButtonList
        /// </summary>
        List<RadioButtonList> RBL;
        /// <summary>
        /// Store List of strings. 
        /// Read the list form Session
        /// </summary>
        List<string> variablesList;


        /// <summary>
        /// Set specific variables when page loads
        /// </summary>
        /// <param name="sender">Object who calls this method</param>
        /// <param name="e">Arguments of the event</param>
        protected void Page_Load(object sender, EventArgs e) {
            LoadQuestions();
            RBL = GenerateRBL();
            if (Session["Variables"] != null) {
                variablesList = (List<string>)Session["Variables"];
                dataLabel.Text = "numer albumu: " + variablesList[0] + "\temail: " + variablesList[1];
                
            }

            

        }



        /// <summary>
        /// Load SQL querry restult into grid
        /// </summary>
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


        /// <summary>
        /// Generate RadioButtonList for each question
        /// </summary>
        /// <returns>List of RadioButtonList objects</returns>
        protected List<RadioButtonList> GenerateRBL() {
            List<RadioButtonList> RBL = new List<RadioButtonList>();
            for (int i = 0; i < numberOfQuestions; i++) {
                RadioButtonList radio = new RadioButtonList();
                for (int j = 1; j <=6; j++) {
                    radio.Items.Add(j.ToString());
                }

                radio.RepeatDirection = RepeatDirection.Horizontal;
                radio.CellSpacing = 0;

                test1.Controls.Add(radio);
                RBL.Add(radio);
            }
            return RBL;
            



        }

        /// <summary>
        /// Set flag 'saved' in Database for specific user and insert his answers,hash and generated password into database
        /// </summary>
        /// <param name="passwd">Generated unique passord</param>
        /// <param name="hash">Generated unique hash</param>
        protected void UserVoted(string passwd, string hash) {
            var connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "update dbo.Users set saved = 1 where mail = '"+ variablesList[1] + "'";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            List<string> selections = new List<string>();

            foreach (RadioButtonList radio in RBL) {
                if (radio.SelectedIndex >= 0) {
                    selections.Add(radio.SelectedItem.Text);
                }
            }
          
            command.CommandText = "insert into dbo.UsrAnswers(passwd,answr_1,answr_2,answr_3,hash) values ('"+passwd+"',"+selections[0]+","+selections[1]+","+selections[2]+",'"+hash+"')";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();


        }

        /// <summary>
        /// Transform string into hash array of bytes
        /// </summary>
        /// <param name="stringToHash">Text we want to hash</param>
        /// <returns>Hashe byte array</returns>
        public static byte[] GetHash(string stringToHash) {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
        }

        /// <summary>
        /// Transform string into hash string
        /// </summary>
        /// <param name="stringToHash">Text we want to hash</param>
        /// <returns>Hash as a string</returns>
        public static string GetHashString(string stringToHash) {
            StringBuilder builder = new StringBuilder();
            foreach (byte b in GetHash(stringToHash))
                builder.Append(b.ToString("X2"));

            return builder.ToString();
        }

        /// <summary>
        /// Redirect to page login.aspx after button click
        /// </summary>
        /// <param name="sender">Object who calls this method</param>
        /// <param name="e">Arguments of the event</param>
        protected void returnButton_Click(object sender, EventArgs e) {
            Response.Redirect("login.aspx");
        }


        /// <summary>
        /// Save selected data form each RadioButton into database, send email with generated password and hash to user and redirect to check.aspx
        /// </summary>
        /// <param name="sender">Object who calls this method</param>
        /// <param name="e">Arguments of the event</param>
        protected void SaveButton_Click(object sender, EventArgs e) {
            List<string> selections = new List<string>();

            foreach (RadioButtonList radio in RBL) {
                if (radio.SelectedIndex >= 0) {
                    selections.Add(radio.SelectedItem.Text);
                }
            }


            string hash = variablesList[0] + variablesList[1] + selections[0] + selections[1] + selections[2];
            string passwd = Membership.GeneratePassword(14, 6);
            UserVoted(passwd, GetHashString(hash)); 

            var fromAddress = new MailAddress("projekt.anieta.anonimowa@gmail.com", "Ankieta");
            var toAddress = new MailAddress(variablesList[1], "test");
            const string fromPassword = "ABC123!@#";
            const string subject = "Temat Haslo";
            string body = "Twoje haslo to: " + passwd + "\n Twoj hash to: " + GetHashString(hash);

            var smtp = new SmtpClient {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress) {
                Subject = subject,
                Body = body
            }) {
                smtp.Send(message);
            }

            Session["Variables"] = variablesList;
            Response.Redirect("check.aspx");
        }
    }
}