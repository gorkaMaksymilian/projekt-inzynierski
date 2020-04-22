using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PI {
    public partial class Survey : System.Web.UI.Page {
        //Zmien mnie mihau
        string connectionString = "Data Source=ABYDOS-WSS-GOR\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";
        int numberOfQuestions = 0;
        List<RadioButtonList> RBL;

        protected void Page_Load(object sender, EventArgs e) {
            LoadQuestions();
            RBL = GenerateRBL();
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

        protected List<RadioButtonList> GenerateRBL() {
            //HtmlTableRow row = new HtmlTableRow();
            List<RadioButtonList> RBL = new List<RadioButtonList>();
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
                RBL.Add(radio);
            }
            return RBL;
            



        }

        protected void UserVoted(string passwd) {
            var connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "update dbo.Users set saved = 1 where mail = '"+ Request.QueryString["email"] + "'";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            List<string> selections = new List<string>();

            foreach (RadioButtonList radio in RBL) {
                if (radio.SelectedIndex >= 0) {
                    selections.Add(radio.SelectedItem.Text);
                }
            }
          
            command.CommandText = "insert into dbo.UsrAnswers(passwd,answr_1,answr_2,answr_3) values ('"+passwd+"',"+selections[0]+","+selections[1]+","+selections[2]+")";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();


        }






        protected void SaveButton_Click(object sender, EventArgs e) {
            string passwd = Membership.GeneratePassword(14, 6);
            UserVoted(passwd); 

            var fromAddress = new MailAddress("projekt.anieta.anonimowa@gmail.com", "Ankieta");
            var toAddress = new MailAddress(Request.QueryString["email"], "test");
            const string fromPassword = "passwd";
            const string subject = "Temat Haslo";
            string body = "Twoje haslo to: " + passwd;

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
        }
    }
}
