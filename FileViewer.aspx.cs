using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace FileUplode
{
    public partial class FileViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string fileId = Request.QueryString["FileID"];

                if (string.IsNullOrEmpty(fileId) || !int.TryParse(fileId, out _))
                {
                    Response.Write("<h3 style='color:red;'>Invalid file request.</h3>");
                    Response.End();
                    return;
                }

                string connString = ConfigurationManager.ConnectionStrings["con"]?.ConnectionString;
                string filePathFromDb = null;

                using (SqlConnection con = new SqlConnection(connString))
                {
                    string query = "SELECT FilePath FROM UploadedFiles WHERE FileID = @FileID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@FileID", fileId);
                        con.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            filePathFromDb = result.ToString();
                        }
                    }
                }

                if (string.IsNullOrEmpty(filePathFromDb))
                {
                    Response.Write("<h3 style='color:red;'>File not found.</h3>");
                    Response.End();
                    return;
                }

                // Pass file to iframe via FileHandler
                string fileUrl = ResolveUrl("~/FileHandler.ashx?file=" + HttpUtility.UrlEncode(filePathFromDb));
                fileViewer.Attributes["src"] = fileUrl;

            }
        }
    }
}
