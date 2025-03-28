using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace FileUplode
{
    public partial class ViewFiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if session variable "Department" is null or not
            if (Session["Department"] == null)
            {
                // Redirect to a login or home page (in this case, ViewUpload.aspx)
                Response.Redirect("ViewUpload.aspx");
                return; // Ensure no further code is executed
            }

            // Continue to load files if the user is valid
            if (!IsPostBack)
            {
                LoadFiles();
            }
        }
        private void LoadFiles()
        {
            try
            {
                string department = Session["Department"]?.ToString();
                string connString = ConfigurationManager.ConnectionStrings["con"]?.ConnectionString;

                using (SqlConnection con = new SqlConnection(connString))
                {
                    string query = @"SELECT FileID, DocumentNo, DocumentDescription, FileName, FilePath, UploadDate 
                                     FROM UploadedFiles WHERE DepartmentName=@dept";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@dept", department);
                        con.Open();
                        GridViewViewFiles.DataSource = cmd.ExecuteReader();
                        GridViewViewFiles.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading files: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
