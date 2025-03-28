<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectDepartment.aspx.cs" Inherits="FileUplode.SelectDepartment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        @keyframes slideIn {
            from {
                width: 0;
                opacity: 0;
            }
            to {
                width: 100%;
                opacity: 1;
            }
        }

        hr {
            border: 2px solid blue;
            font-weight: bold;
            width: 0;
            opacity: 0;
            animation: slideIn 1.5s ease-out forwards;
        }

        .alert {
            display: none;
            text-align: center;
            font-weight: bold;
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
     background-color: #0056b3;
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

       <div class="button-container">
     <a href="#" class="home-button">🏠 Home</a>
 </div>

    <h2 style="text-align: center;">Login</h2>
    <hr />
    <br />

    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control">
        <asp:ListItem Text="Select Department" Value="" />
        <asp:ListItem Text="Quality" Value="Quality" />
        <asp:ListItem Text="Process" Value="Process" />
      
        <asp:ListItem Text="R & D" Value="RD" />
        <asp:ListItem Text="Maintenance" Value="maintenance" />
    </asp:DropDownList>
    <br />

    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
    <br />

    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" Text="Login" OnClick="btnLogin_Click" />
    <br />

    <!-- Styled Error Message -->
    <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger d-block mt-3" Visible="false"></asp:Label>
</asp:Content>
