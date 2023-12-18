using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLSelectInsertUpdate
{
    public partial class FrmUpdateMember : Form
    {
        public FrmUpdateMember()
        {
            InitializeComponent();
        }

        string value;
        private ClubRegistrationQuery clubRegistrationQuery;
        SqlCommand cmd;
        SqlDataReader reader;

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            cmbProgram.Items.Add("BS Information Technology");
            cmbProgram.Items.Add("BS Computer Science");
            cmbProgram.Items.Add("BS Information Systems");
            cmbProgram.Items.Add("BS in Accountancy");
            cmbProgram.Items.Add("BS in Hospitality Management");
            cmbProgram.Items.Add("BS in Tourism Management");
            cmbGender.Items.Add("Female");
            cmbGender.Items.Add("Male");

            clubRegistrationQuery = new ClubRegistrationQuery();

            using (SqlConnection sqlConnect = new SqlConnection(clubRegistrationQuery.connectionString))
            {
                sqlConnect.Open();
                string query = "SELECT StudentId From ClubMembers";
                cmd = new SqlCommand(query, sqlConnect);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cmbID.Items.Add(reader.GetValue(0));
                }
                reader.Close();
                sqlConnect.Close();
            }
            cmbID.SelectedIndex = 0;
        }

        private void cmbID_SelectedIndexChanged(object sender, EventArgs e)
        {
            value = cmbID.SelectedItem.ToString();

            clubRegistrationQuery = new ClubRegistrationQuery();

            using (SqlConnection sqlConn = new SqlConnection(clubRegistrationQuery.connectionString))
            {
                sqlConn.Open();
                string query = "SELECT * FROM ClubMembers WHERE StudentID = '" + value + "'";
                cmd = new SqlCommand(query, sqlConn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    txtFirstName.Text = " " + reader.GetValue(2);
                    txtMiddleName.Text = " " + reader.GetValue(3);
                    txtLastName.Text = " " + reader.GetValue(4);
                    txtAge.Text = " " + reader.GetValue(5);
                    cmbGender.Text = " " + reader.GetValue(6);
                    cmbProgram.Text = " " + reader.GetValue(7);
                }
                reader.Close();
                sqlConn.Close();

            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            clubRegistrationQuery = new ClubRegistrationQuery();

            using(SqlConnection sqlConnect = new SqlConnection(clubRegistrationQuery.connectionString))
            {
                sqlConnect.Open();
                string updateQuery = "UPDATE ClubMembers SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Age = @Age, Gender = @Gender, Program = @Program WHERE StudentId = '" + value + "'";
                cmd = new SqlCommand(updateQuery, sqlConnect);
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@MiddleName", txtMiddleName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                cmd.Parameters.AddWithValue("@Gender", cmbGender.Text);
                cmd.Parameters.AddWithValue("@Program", cmbProgram.Text);
                MessageBox.Show("Your Information are Successfully Updated.");
                cmd.ExecuteNonQuery();
                sqlConnect.Close();
                this.Close();
            }
                         
        }

    }
}
