using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ShopMS
{
    public partial class frmSignup : Form
    {
        private EmployeeDAO dao = new EmployeeDAO();
        private bool isUserIDAvailable = false;
        public frmSignup()
        {
            InitializeComponent();
        }
        ~frmSignup()
        {
            this.Close();
        }

        private void frmSignup_Load(object sender, EventArgs e)
        {

        }

        
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            isUserIDAvailable = dao.IsUserIDAvailable(txtUserID.Text);
            if (isUserIDAvailable)
            {
                lblAvailabilty.ForeColor = Color.Lime;
                lblAvailabilty.Text = "Available !";
            }
            else
            {
                lblAvailabilty.ForeColor = Color.Red;
                lblAvailabilty.Text = "Not Available !";
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //Check all Required Fields
            if (txtUserID.Text == null || txtUserID.Text == "")
            {
                MessageBox.Show("Please Enter UserID !", "Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (txtFirstName.Text == null || txtFirstName.Text == "")
            {
                MessageBox.Show("Please Enter First Name !", "Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (txtLastName.Text == null || txtLastName.Text == "")
            {
                MessageBox.Show("Please Enter Last Name !", "Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (txtPassword.Text == null || txtPassword.Text == "")
            {
                MessageBox.Show("Please Enter Password", "Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (txtCnfPassword.Text == null || txtCnfPassword.Text == "")
            {
                MessageBox.Show("Please Enter Confirm Password", "Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (txtPassword.Text != txtCnfPassword.Text)
            {
                MessageBox.Show("Password and Confirm Password are not Matching !", "Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (txtSalary.Text == null || txtSalary.Text == "")
            {
                MessageBox.Show("Please Enter Salary !", "Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (lstRole.Text == null || lstRole.Text == "")
            {
                MessageBox.Show("Please Select Role !", "Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                isUserIDAvailable = dao.IsUserIDAvailable(txtUserID.Text);
                if (!isUserIDAvailable)
                    MessageBox.Show("This User ID is Not Available.\nThis User ID already Exists.\nPlease insert another User ID", "Not Available", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);                   
                else
                {
                    if (txtPhoneNo.Text == null || txtPhoneNo.Text == "")
                        txtPhoneNo.Text = "-1";
                    if (txtUID.Text == null || txtUID.Text == "")
                        txtUID.Text = "-1";
                    int successFlag = 0;
                    Employee employee = new Employee();
                    
                    employee.Address = txtAddress.Text;
                    employee.Dob = dtpDOB.Text;
                    employee.EmailID = txtEmailID.Text;
                    employee.FirstName = txtFirstName.Text;
                    employee.MiddleName = txtMiddleName.Text;
                    employee.LastName = txtLastName.Text;
                    employee.UserID = txtUserID.Text;
                    employee.Password = txtPassword.Text;
                    employee.PhoneNo = long.Parse(txtPhoneNo.Text);
                    employee.Role = lstRole.Text;
                    employee.Salary = float.Parse(txtSalary.Text);
                    employee.Sex = lstSex.Text;
                    employee.Uid = long.Parse(txtUID.Text);

                    successFlag = dao.addEmployee(employee);
                    if(successFlag == 1)
                    {
                        MessageBox.Show("Your account request sent for approval.\nAfter request approved by Admin, You can Log In.", "On Approval", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                        MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    
                    
                }
            }
        }
    }
}
