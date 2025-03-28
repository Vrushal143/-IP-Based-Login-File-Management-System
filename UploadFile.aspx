<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="FileUplode.UploadFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .table-centered th,
        .table-centered td {
            text-align: center;
            vertical-align: middle;
        }
    </style>

    <h2 class="text-center">Upload File</h2>
    <hr class="mt-4 mb-4" style="border-top: 3px solid blue;">


    <div class="form-group">
        <label>Document No:</label>
        <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <br />
    <div class="form-group">
        <label>Document Description:</label>
        <asp:TextBox ID="txtDocumentDescription" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <br />

    <div class="form-group">
        <label>Select File:</label>
        <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" onchange="validateFileSize(this)" />
    </div>

    <br />
    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Upload" OnClientClick="return checkFile()" OnClick="btnUpload_Click" />
    <br />
    <br />

    <asp:GridView ID="GridViewFiles" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-centered" DataKeyNames="FileID"
        OnRowDeleting="GridViewFiles_RowDeleting">
        <Columns>
            <asp:BoundField DataField="DocumentNo" HeaderText="Document No" />
            <asp:BoundField DataField="FileName" HeaderText="File Name" />
            <asp:BoundField DataField="DocumentDescription" HeaderText="Description" />
            <asp:BoundField DataField="UploadDate" HeaderText="Uploaded On" DataFormatString="{0:dd-MM-yyyy HH:mm}" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("FileID") %>' CssClass="btn btn-danger" Text="Delete" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>


    <script>
        function validateFileSize(input) {
            var file = input.files[0]; // Get selected file
            if (file) {
                var maxSize = 100 * 1024 * 1024; // 100MB in bytes
                if (file.size > maxSize) {
                    alert("File size exceeds 100 MB limit. Please select a smaller file.");
                    input.value = ''; // Clear file input
                }
            }
        }

        function checkFile() {
            var fileInput = document.getElementById('<%= fileUpload.ClientID %>');
            if (fileInput.files.length === 0) {
                alert("Please select a file to upload.");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
