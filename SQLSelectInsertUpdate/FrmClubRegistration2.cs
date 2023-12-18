using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLSelectInsertUpdate
{
    public partial class FrmClubRegistration2 : Form
    {
        public FrmClubRegistration2()
        {
            InitializeComponent();
            clubRegistrationQuery = new ClubRegistrationQuery();
            RefreshListOfClubMembers();
        }

        private ClubRegistrationQuery clubRegistrationQuery;
        private int ID, Age, count;
        private string FirstName, MiddleName, LastName, Gender, Program;
        private long StudentID;

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtStudentID.Text) && (String.IsNullOrEmpty(txtFirstName.Text) && 
                (String.IsNullOrEmpty(txtMiddleName.Text) && (String.IsNullOrEmpty(txtLastName.Text) && 
                (String.IsNullOrEmpty(txtAge.Text) && (cmbGender.SelectedIndex == -1) && (cmbProgram.SelectedIndex == -1))))))
            {
                MessageBox.Show("Please fill up the Form to Register!!");
            }
            else if (String.IsNullOrEmpty(txtStudentID.Text))
            {
                MessageBox.Show("Please Enter your Student ID!!");
            }
            else if (String.IsNullOrEmpty(txtFirstName.Text))
            {
                MessageBox.Show("Please Enter your First Name!!");
            }
            else if (String.IsNullOrEmpty(txtMiddleName.Text))
            {
                MessageBox.Show("Please Enter your Middle Name!!");
            }
            else if (String.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Please Enter your Last Name!!");
            }
            else if (String.IsNullOrEmpty(txtAge.Text))
            {
                MessageBox.Show("Please Input your Age!!");
            }
            else if (cmbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select a Gender!!");
            }
            else if (cmbProgram.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select a Program!!");
            }
            else
            {
                ID = RegistrationID();
                StudentID = Convert.ToInt64(txtStudentID.Text);
                FirstName = txtFirstName.Text;
                MiddleName = txtMiddleName.Text;
                LastName = txtLastName.Text;
                Age = Convert.ToInt32(txtAge.Text);
                Gender = cmbGender.Text;
                Program = cmbProgram.Text;

                clubRegistrationQuery.RegisterStudent(ID, StudentID, FirstName, MiddleName, LastName, Age, Gender, Program);
                Clear();
                RefreshListOfClubMembers();

                MessageBox.Show("Congrats! You are Succesfully Registered.");
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmUpdateMember update = new FrmUpdateMember();
            update.ShowDialog();
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }


        private void FrmClubRegistration2_Load(object sender, EventArgs e)
        {

            string[] ListOfPrograms = new string[] {
                "BS in Information Technology",
                "BS in Computer Science",
                "BS in Information Systems",
                "BS in Accountancy",
                "BS in Hospitality Management",
                "BS in Tourism Management"
            };

            for (int i = 0; i < ListOfPrograms.Length; i++)
            {
                cmbProgram.Items.Add(ListOfPrograms[i].ToString());
            }

            string[] listOfGender = new string[] {
                "Male",
                "Female"
            };

            for (int i = 0; i < 2; i++)
            {
                cmbGender.Items.Add(listOfGender[i].ToString());
            }

        }


        public void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView1.DataSource = clubRegistrationQuery.bindingSource;
        }

        
        public int RegistrationID()
        {
            count++;
            return count;
        }

        public void Clear()
        {
            txtStudentID.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            cmbGender.SelectedIndex = -1;
            cmbProgram.SelectedIndex = -1;  
        }
    }
}
