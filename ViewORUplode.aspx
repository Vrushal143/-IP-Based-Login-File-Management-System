<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewORUplode.aspx.cs" Inherits="FileUplode.ViewORUplode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


  <div class="page-header">
         <h1>View Document / Uplode Document </h1>
 <hr style="border: 3px solid blue; font-weight: bold;" />
     </div>
    







    









  
         <div class="container">
<ul class="list-group">

           <li class="list-item">
              <asp:LinkButton ID="lbDepartmentview" runat="server" OnClick="lbDepartmentview_Click"
                  style="font-style: italic; text-decoration: none; color: cornflowerblue; font-weight: bold;">
                  View Documents
              </asp:LinkButton>
 <br />
<br />
 </li>

       <li class="list-item">
              <asp:LinkButton ID="lbDepartmentUpload" runat="server" OnClick="lbDepartment
                  Upload_Click"
                  style="font-style: italic; text-decoration: none; color: cornflowerblue; font-weight: bold;">
                  Upload Documents
              </asp:LinkButton>
 <br />
<br />
 </li>



 </ul>
          </div>

</asp:Content>
