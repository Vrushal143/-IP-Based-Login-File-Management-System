using System;
using System.Web;
using System.Web.UI;

namespace FileUplode
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get the current page name
                string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath).ToLower();

                // Check if the user is logged in (session exists)
                bool isLoggedIn = Session["Department"] != null;

                // Show logout button only on UploadFile.aspx or ViewFiles.aspx when logged in
                btnLogout.Visible = isLoggedIn &&
                                    (currentPage == "uploadfile.aspx" || currentPage == "viewfiles.aspx");
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("ViewUpload.aspx");
        }


    }
}
