<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileViewer.aspx.cs" Inherits="FileUplode.FileViewer" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <iframe id="fileViewer" runat="server" width="100%" height="700px" style="border: none;"></iframe>

    <script>
        document.addEventListener("contextmenu", function (e) {
            e.preventDefault();
        });

        document.addEventListener("keydown", function (e) {
            if (e.ctrlKey && (e.key === "s" || e.key === "p" || e.key === "u" || e.key === "j" || e.key === "i" || e.key === "c") || e.key === "F12") {
                e.preventDefault();
                alert("Action not allowed!");
            }
        });
    </script>
</asp:Content>
