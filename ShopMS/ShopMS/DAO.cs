using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace ShopMS
{
    class EmployeeDAO
    {
        private string connString;
        private SqlConnection conn;
        
        //Constructor
        public EmployeeDAO()
        {
            connString = @"Data Source=DESKTOP-0HHSV58\SQLEXPRESS;Initial Catalog=ShopMS;Integrated Security=True";
        }
        public void WriteLog(string log)
        {
            string path = @"Log.txt";
            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    var dt = DateTime.Now;
                    sw.WriteLine(dt.ToString("dd/MM/yyyy h:mm tt"));
                    sw.WriteLine(log);
                }
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(path))
            {
                var dt = DateTime.Now;
                sw.WriteLine(dt.ToString("dd/MM/yyyy h:mm tt"));
                sw.WriteLine(log);
            }       
        }

        public bool IsUserIDAvailable(string userID)
        {
            SqlDataReader dreader = null;
            SqlCommand cmd = null;
            bool isUserAvailable = false;
            try
            {
                conn = new SqlConnection(connString);
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = @"SELECT * FROM tblEmployees WHERE user_id = '" + userID + "' COLLATE SQL_Latin1_General_Cp1_CS_AS;";
                    this.WriteLog("Query : " + sql);
                    cmd = new SqlCommand(sql, conn);
                    dreader = cmd.ExecuteReader();
                    if (dreader.Read())
                        isUserAvailable = false;
                    else
                        isUserAvailable = true;
                }
            }
            catch (Exception ex)
            {
                this.WriteLog("(Exeception) " + ex.Message);
                throw new Exception("Exception : " + ex.Message);
            }
            finally
            {
                try
                {
                    dreader.Close();
                    cmd.Dispose();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    this.WriteLog("(Exeception) " + ex.Message);
                    throw new Exception("Exception : " + ex.Message);
                }
            }
            return isUserAvailable;
        }

        public int addEmployee(Employee employee)
        {
            int successFlag = 0;

            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connString);
                conn.Open();
                string sql = @"INSERT INTO tblEmployees 
                        (first_name, middle_name, last_name, user_id, password, role, salary, phone_no, email_id, dob, uid, address, sex)
                        VALUES ('" + employee.FirstName + "','" + employee.MiddleName + "','" + employee.LastName + "','" + employee.UserID + "','" + employee.Password + "','" + employee.Role + "'," + employee.Salary + "," + employee.PhoneNo + ",'" + employee.EmailID + "','" + employee.Dob + "'," + employee.Uid + ",'" + employee.Address + "','" + employee.Sex + "');";
                this.WriteLog("Query : " + sql);
                cmd = new SqlCommand(sql, conn);
                successFlag = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.WriteLog("(Exeception) " + ex.Message);
                throw new Exception("Exception : " + ex.Message);
            }
            finally
            {
                try
                {
                    cmd.Dispose();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    this.WriteLog("(Exeception) " + ex.Message);
                    throw new Exception("Exception : " + ex.Message);
                }
            }
            return successFlag;
        }

        public bool ValidateUser(string userID, string password)
        {
            bool validUser = false;
            SqlDataReader dreader = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connString);
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = @"SELECT * FROM tblEmployees WHERE user_id = '" + userID + "' COLLATE SQL_Latin1_General_Cp1_CS_AS and password = '" + password + "' COLLATE SQL_Latin1_General_Cp1_CS_AS;";
                    this.WriteLog("Query : " + sql);
                    cmd = new SqlCommand(sql, conn);
                    dreader = cmd.ExecuteReader();
                    if (dreader.Read())
                    {
                        validUser = true;
                        using (StreamWriter writer = new StreamWriter("employee_id.txt"))
                        {
                            writer.WriteLine(dreader.GetInt32(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteLog("(Exeception) " + ex.Message);
                throw new Exception("Exception : " + ex.Message);
            }
            finally
            {
                try
                {
                    dreader.Close();
                    cmd.Dispose();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    this.WriteLog("(Exeception) " + ex.Message);
                    throw new Exception("Exception : " + ex.Message);
                }
            }
            return validUser;
        }

        public Employee getUser(string userID)
        {
            SqlDataReader dreader = null;
            SqlCommand cmd = null;
            Employee employee = null;
            try
            {
                conn = new SqlConnection(connString);
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = @"SELECT * FROM tblEmployees WHERE user_id = '" + userID + "' COLLATE SQL_Latin1_General_Cp1_CS_AS and email_id <> '' COLLATE SQL_Latin1_General_Cp1_CS_AS;";
                    this.WriteLog("Query : " + sql);
                    cmd = new SqlCommand(sql, conn);
                    dreader = cmd.ExecuteReader();
                    if (dreader.Read())
                    {
                        employee = new Employee();
                        employee.EmailID = dreader.GetString(8);
                    }                        
                }
            }
            catch (Exception ex)
            {
                this.WriteLog("(Exeception) " + ex.Message);
                throw new Exception("Exception : " + ex.Message);
            }
            finally
            {
                try
                {
                    dreader.Close();
                    cmd.Dispose();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    this.WriteLog("(Exeception) " + ex.Message);
                    throw new Exception("Exception : " + ex.Message);
                }
            }
            return employee;
        }

        public bool sendMail(string recipient,int otp)
        {
            bool sent= false;
            try
            {
                SmtpClient clientDetails = new SmtpClient();
                //Smpt Client Details
                //gmail >> smtp server : smtp.gmail.com, port : 587 , ssl required
                clientDetails.Port = 587;
                clientDetails.Host = "smtp.gmail.com";
                clientDetails.EnableSsl = true;
                clientDetails.DeliveryMethod = SmtpDeliveryMethod.Network;
                clientDetails.UseDefaultCredentials = false;
                clientDetails.Credentials = new NetworkCredential("manojspopalghat@gmail.com", "password");
                //Message Details
                MailMessage mailDetails = new MailMessage();
                mailDetails.From = new MailAddress("manojspopalghat@gmail.com");
                mailDetails.To.Add(recipient);

                mailDetails.Subject = "One Time Password";
                mailDetails.IsBodyHtml = true; ;
                mailDetails.Body = " Your OTP for Reset Password is " + otp + ", Please don't share it with someone else. Thanks for Using SHOP MANAGEMENT SYSTEMS.";
                clientDetails.Send(mailDetails);
                sent = true;
            }
            catch (Exception ex)
            {
                this.WriteLog("(Email Exception) " + ex.Message);
                throw new Exception("Exception : " + ex.StackTrace);
            }
            return sent;
        }

        public int updatePassword(string userID, string Password)
        {
            int successFlag = 0;

            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connString);
                conn.Open();
                string sql = @"UPDATE tblEmployees SET password = '" + Password + "' WHERE user_id='" + userID + "' COLLATE SQL_Latin1_General_Cp1_CS_AS;";
                       
                this.WriteLog("Query : " + sql);
                cmd = new SqlCommand(sql, conn);
                successFlag = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.WriteLog("(Exeception) " + ex.Message);
                throw new Exception("Exception : " + ex.Message);
            }
            finally
            {
                try
                {
                    cmd.Dispose();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    this.WriteLog("(Exeception) " + ex.Message);
                    throw new Exception("Exception : " + ex.Message);
                }
            }
            return successFlag;
        }
    }
}
