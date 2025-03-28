using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Web;


namespace FileUplode
{
    public partial class ViewORUplode : System.Web.UI.Page
    {
        private string clientIP = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            // No IP validation here, only when buttons are clicked
        }

        /// <summary>
        /// Retrieves the Client's IPv4 Address
        /// </summary>
        private string GetClientIPv4Address()
        {
            string ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipList))
            {
                ipList = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (!string.IsNullOrEmpty(ipList))
            {
                string[] ipArray = ipList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string ip in ipArray)
                {
                    string trimmedIP = ip.Trim();
                    if (IPAddress.TryParse(trimmedIP, out IPAddress ipAddr) && ipAddr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return trimmedIP; // Return First Valid IPv4 Address
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Checks if the given IP exists in the database.
        /// </summary>
        private bool IsIPInDatabase(string ip)
        {
            bool isAllowed = false;
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM AllowedIPs WHERE IPAddress = @IP", con))
                    {
                        cmd.Parameters.AddWithValue("@IP", ip);
                        int count = (int)cmd.ExecuteScalar();
                        isAllowed = count > 0; // IP Exists if count > 0
                    }
                }
                catch (Exception ex)
                {
                    lblError1.Text = "⚠️ Database Error: " + ex.Message;
                    lblError1.ForeColor = System.Drawing.Color.Orange;
                    lblError1.Visible = true;
                }
            }
            return isAllowed;
        }

        protected void lbDepartmentView_Click(object sender, EventArgs e)
        {
            clientIP = GetClientIPv4Address(); // Get Client's IP Address

            if (!string.IsNullOrEmpty(clientIP))
            {
                if (IsIPInDatabase(clientIP))
                {
                    // Redirect to SelectDepartment.aspx with query string mode=view
                    Response.Redirect("SelectDepartment.aspx?mode=view");
                }
                else
                {
                    // Hide the LinkButton and show error message
                    lbDepartmentView.Visible = false;
                    lblError1.Text = $"❌ Access Denied: Your IP {clientIP} is not authorized.";
                    lblError1.Visible = true;
                }
            }
            else
            {
                // Hide the LinkButton and show error message for invalid IP
                lbDepartmentView.Visible = false;
                lblError1.Text = "⚠️ Unable to retrieve a valid IPv4 address.";
                lblError1.ForeColor = System.Drawing.Color.Orange;
                lblError1.Visible = true;
            }
        }

        protected void lbDepartmentUpload_Click(object sender, EventArgs e)

        {
            clientIP = GetClientIPv4Address(); // Get Client's IP Address

            if (!string.IsNullOrEmpty(clientIP))
            {
                if (IsIPInDatabase(clientIP))
                {

                    // Redirect to SelectDepartment.aspx with query string mode=upload
                    Response.Redirect("SelectDepartment.aspx?mode=upload");
                }
                else
                {
                    // Hide the LinkButton and show error message
                    lbDepartmentUpload.Visible = false;
                    lblError2.Text = $"❌ Access Denied: Your IP {clientIP} is not authorized.";
                    lblError2.Visible = true;
                }
            }
            else
            {
                // Hide the LinkButton and show error message for invalid IP
                lbDepartmentView.Visible = false;
                lblError2.Text = "⚠️ Unable to retrieve a valid IPv4 address.";
                lblError2.ForeColor = System.Drawing.Color.Orange;
                lblError2.Visible = true;
            }


        }

    }

}

