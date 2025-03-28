using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileUplode
{
    public partial class UploadFile : System.Web.UI.Page
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


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                try
                {
                    string connString = ConfigurationManager.ConnectionStrings["con"]?.ConnectionString;
                    if (string.IsNullOrEmpty(connString))
                    {
                        lblMessage.Text = "Database connection is not configured.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    string department = Session["Department"]?.ToString();
                    string documentNo = txtDocumentNo.Text.Trim();
                    string documentDescription = txtDocumentDescription.Text.Trim();

                    if (string.IsNullOrEmpty(documentNo))
                    {
                        lblMessage.Text = "Document No is required.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    string folderPath = Server.MapPath($"~/Uploads/{department}/");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fileName = Path.GetFileName(fileUpload.FileName);
                    string filePath = Path.Combine(folderPath, fileName);
                    fileUpload.SaveAs(filePath);

                    string dbFilePath = $"Uploads/{department}/{fileName}";

                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        string query = @"INSERT INTO UploadedFiles 
                                        (DocumentNo, DocumentDescription, FileName, FilePath, DepartmentName, UploadDate) 
                                        VALUES (@docNo, @docDesc, @name, @path, @dept, GETDATE())";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@docNo", documentNo);
                            cmd.Parameters.AddWithValue("@docDesc", documentDescription);
                            cmd.Parameters.AddWithValue("@name", fileName);
                            cmd.Parameters.AddWithValue("@path", dbFilePath);
                            cmd.Parameters.AddWithValue("@dept", department);

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    lblMessage.Text = "File uploaded successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    LoadFiles();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error uploading file: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblMessage.Text = "Please select a file to upload.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
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
                        GridViewFiles.DataSource = cmd.ExecuteReader();
                        GridViewFiles.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading files: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void GridViewFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int fileID = Convert.ToInt32(GridViewFiles.DataKeys[e.RowIndex].Value);
                string filePath = "";

                string connString = ConfigurationManager.ConnectionStrings["con"]?.ConnectionString;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();

                    string query = "SELECT FilePath FROM UploadedFiles WHERE FileID=@id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", fileID);
                        filePath = cmd.ExecuteScalar()?.ToString();
                    }

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        string fullFilePath = Server.MapPath(filePath);
                        if (File.Exists(fullFilePath))
                        {
                            File.Delete(fullFilePath);
                        }
                    }

                    query = "DELETE FROM UploadedFiles WHERE FileID=@id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", fileID);
                        cmd.ExecuteNonQuery();
                    }
                }

                lblMessage.Text = "File deleted successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                LoadFiles();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error deleting file: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
