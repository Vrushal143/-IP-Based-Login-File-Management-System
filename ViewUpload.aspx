<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewUpload.aspx.cs" Inherits="FileUplode.ViewUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
        <style>

       .list-item a {
    font-weight: bold;      /* Make text bold */
    font-style: italic;     /* Make text italic */
    color: cornflowerblue;           /* Set text color to black */
    text-decoration: none;  /* Remove underline */
    font-size: x-large;     /* Set font size to extra large */
}



  
     .home-button {
         display: inline-block;
         background-color: #007bff;
         color: white;
         text-decoration: none;
         font-size: 16px;
         font-weight: bold;
         padding: 10px 20px;
         border-radius: 8px;
         text-align: center;
         transition: background-color 0.3s ease;
         border: none;
     }

     .home-button:hover {
         background-color: blue;
     }

     .button-container {
         display: flex;
         justify-content: flex-end;
         padding: 10px;
     }

     /* Responsive Design */
     @media (max-width: 600px) {
         .home-button {
             font-size: 14px;
             padding: 8px 15px;
         }
     }


 </style>
    
  <div class="page-header">

    
      <div class="button-container">
        <a href="#" class="home-button">🏠 Home</a>
    </div>




         <h1>View & Upload Documents </h1>
 <hr style="border: 3px solid blue; font-weight: bold;" />
     </div>
    
  
         <div class="container">
             <ul class="list-group">
   
    <li class="list-item">

         <asp:LinkButton ID="lbDepartmentUpload" runat="server" OnClick="lbDepartmentUpload_Click"
             
     style="font-style: italic; text-decoration: none; color: black; font-weight: bold;">
   Upload Documents
 </asp:LinkButton>
      
    <br />
<br />
    </li>
      <asp:Label ID="lblError2" runat="server" ForeColor="Red" Visible="false"></asp:Label>



     <li class="list-item">
     <asp:LinkButton ID="lbDepartmentView"  runat="server" OnClick="lbDepartmentView_Click" 
         style="font-style: italic; text-decoration: none; color: black; font-weight: bold;">
        View Documents
     </asp:LinkButton>
         <br />
 <br />
 </li>

 <asp:Label ID="lblError1" runat="server" ForeColor="Red" Visible="false"></asp:Label>

</ul>

          </div>
</asp:Content>

