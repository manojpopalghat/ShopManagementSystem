using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace ShopMS
{
    public partial class frmLogin : Form
    {
        private EmployeeDAO dao = new EmployeeDAO();
        public frmLogin()
        {
            InitializeComponent();
        }

        ~frmLogin()
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSignup frm = new frmSignup();
            this.Hide();
            frm.ShowDialog();
            this.ShowDialog();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //code
            if(dao.ValidateUser(txtUserID.Text,txtPassword.Text))
                MessageBox.Show("Log in Successful", "LoggedIn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("User Id or Password is Incorrect", "Invalid", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmForgotPassword frm = new frmForgotPassword();
            this.Hide();
            frm.ShowDialog();
            this.ShowDialog();
        }

    }
}
