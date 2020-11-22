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
    public partial class frmForgotPassword : Form
    {
        EmployeeDAO dao = new EmployeeDAO();
        Random rad = new Random();
        int otp;
        public frmForgotPassword()
        {
            InitializeComponent();
        }
        //Destructor
        ~frmForgotPassword()
        {
            this.Close();
        }

        private void btnSendOTP_Click(object sender, EventArgs e)
        {
            Employee emp = dao.getUser(txtUserID.Text);
            if(emp!=null)
            {
                string recipient = emp.EmailID;
                //Genrate OTP
                Random rad = new Random();
                otp = rad.Next(100000, 1000000);
                if (dao.sendMail(recipient, otp))
                {
                    MessageBox.Show("Mail Sent to " + recipient, "Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblOTP.Visible = true;
                    txtOTP.Visible = true;
                    lblNewPassword.Visible = true;
                    txtNewPassword.Visible = true;
                    lblConfirmPassword.Visible = true;
                    txtConfirmPassword.Visible = true;
                    txtUserID.ReadOnly = true;
                    btnSubmit.Visible = true;
                }
                else
                    MessageBox.Show("Something Went Wrong ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("No email id found with this user id", "MailNotFound", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(txtOTP.Text) == otp)
                {
                    if (txtConfirmPassword.Text == txtNewPassword.Text)
                    {
                        //update password
                        int successFlag = dao.updatePassword(txtUserID.Text, txtNewPassword.Text);
                        if (successFlag > 0)
                        {
                            MessageBox.Show("Password changed successfully !", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                            MessageBox.Show("Something Went Wrong ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("New Password and Confirm Password are not Matching", "Password", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                    MessageBox.Show("You Entered Wrong OTP\nPlease Enter Correct OTP", "OTPNotMatched", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (Exception ex)
            {
                dao.WriteLog("(Exeception) " + ex.Message);
                throw new Exception("Exception : " + ex.Message);
            }
        }
    }
}
