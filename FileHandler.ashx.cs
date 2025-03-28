using System;
using System.IO;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace FileUplode
{
    public class FileHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string filePathFromDb = context.Request.QueryString["file"];

                if (string.IsNullOrEmpty(filePathFromDb))
                {
                    context.Response.StatusCode = 400;
                    context.Response.Write("<h3 style='color:red;'>Invalid request: No file specified.</h3>");
                    return;
                }

                string connString = ConfigurationManager.ConnectionStrings["con"]?.ConnectionString;
                string storedFilePath = null;

                using (SqlConnection con = new SqlConnection(connString))
                {
                    string query = "SELECT FilePath FROM UploadedFiles WHERE FilePath = @FilePath";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@FilePath", filePathFromDb);
                        con.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            storedFilePath = result.ToString();
                        }
                    }
                }

                if (string.IsNullOrEmpty(storedFilePath))
                {
                    context.Response.StatusCode = 404;
                    context.Response.Write("<h3 style='color:red;'>Error: File record not found in database.</h3>");
                    return;
                }

                // Ensure stored file path is relative and then resolve it
                string fullFilePath = context.Server.MapPath("~/" + storedFilePath);

                if (!File.Exists(fullFilePath))
                {
                    context.Response.StatusCode = 404;
                    context.Response.Write($"<h3 style='color:red;'>Error: File not found at {fullFilePath}</h3>");
                    return;
                }

                context.Response.Clear();
                context.Response.ContentType = GetContentType(fullFilePath);
                context.Response.AddHeader("Content-Disposition", $"inline; filename=\"{HttpUtility.UrlEncode(Path.GetFileName(fullFilePath))}\"");
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.TransmitFile(fullFilePath);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.Write($"<h3 style='color:red;'>Error: {HttpUtility.HtmlEncode(ex.Message)}</h3>");
            }
        }

        private string GetContentType(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();

            var mimeTypes = new Dictionary<string, string>
    {
        { ".pdf", "application/pdf" },
        { ".txt", "text/plain" },
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".png", "image/png" },
        { ".doc", "application/msword" },
        { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        { ".xls", "application/vnd.ms-excel" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".ppt", "application/vnd.ms-powerpoint" },
        { ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
        { ".zip", "application/zip" },
        { ".mp3", "audio/mpeg" },
        { ".mp4", "video/mp4" }
    };

            return mimeTypes.ContainsKey(extension) ? mimeTypes[extension] : "application/octet-stream";
        }


        public bool IsReusable => false;
    }
}
