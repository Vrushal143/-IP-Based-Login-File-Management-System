using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Web;
using System.Web.UI;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string clientIP = GetClientIPv4Address();

            if (!string.IsNullOrEmpty(clientIP))
            {
                // Store whether IP is allowed in session
                Session["IsIPAllowed"] = IsIPInDatabase(clientIP);
            }
            else
            {
                Session["IsIPAllowed"] = false; // Default to not allowed
            }
        }
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
                IPAddress ipAddr; // Declare variable before using it
                if (IPAddress.TryParse(trimmedIP, out ipAddr) && ipAddr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return trimmedIP;
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
                    isAllowed = count > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        return isAllowed;
    }

    protected void lbDepartmentView_Click(object sender, EventArgs e)
    {
        if (Session["IsIPAllowed"] != null && Convert.ToBoolean(Session["IsIPAllowed"]))
        {
            Response.Redirect("SelectDepartment.aspx?mode=view");
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("SupperAlloyDeptInfo.aspx");
    }
}
