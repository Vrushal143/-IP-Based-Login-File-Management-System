using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileUplode
{
    public partial class SelectDepartment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Any page load logic can go here if required
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["con"]?.ConnectionString;

                // Check if the connection string is null or empty
                if (string.IsNullOrEmpty(connString))
                {
                    throw new Exception("Database connection string is missing in web.config.");
                }

                string department = ddlDepartment.SelectedValue;
                string password = txtPassword.Text;
                string mode = Request.QueryString["mode"];

                // Validate that mode is valid and not null
                if (string.IsNullOrEmpty(mode) || (mode != "upload" && mode != "view"))
                {
                    lblMessage.Text = "Invalid mode!";
                    lblMessage.CssClass = "alert alert-warning d-block mt-3";
                    lblMessage.Visible = true;
                    return;
                }

                // Choose the correct password column based on mode
                string query = (mode == "upload") ?
                    "SELECT UploadPassword FROM DepartmentPasswords WHERE DepartmentName=@dept" :
                    "SELECT ViewPassword FROM DepartmentPasswords WHERE DepartmentName=@dept";

                using (SqlConnection con = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@dept", department);

                    con.Open();
                    // Get the password from the database
                    string dbPassword = cmd.ExecuteScalar()?.ToString();

                    // Check if a password was returned
                    if (dbPassword == null)
                    {
                        lblMessage.Text = "No password found for the selected department!";
                        lblMessage.CssClass = "alert alert-warning d-block mt-3";
                        lblMessage.Visible = true;
                        return;
                    }

                    // Compare the entered password with the one retrieved from the database
                    if (dbPassword == password)
                    {
                        // Set session and redirect based on the mode
                        Session["Department"] = department;
                        if (mode == "upload")
                        {
                            Response.Redirect("UploadFile.aspx");
                        }
                        else if (mode == "view")
                        {
                            Response.Redirect("ViewFiles.aspx");
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Invalid Password!";
                        lblMessage.CssClass = "alert alert-danger d-block mt-3";
                        lblMessage.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "alert alert-warning d-block mt-3";
                lblMessage.Visible = true;
            }
        }
    }
}
