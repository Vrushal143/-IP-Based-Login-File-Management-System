<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewFiles.aspx.cs" Inherits="FileUplode.ViewFiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .table-centered th,
        .table-centered td {
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <h2 class="text-center">View Uploaded Files</h2>
    <hr class="mt-4 mb-4" style="border-top: 3px solid blue;">


    <asp:GridView ID="GridViewViewFiles" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-centered"
        EmptyDataText="No files found for this department.">
        <Columns>
            <asp:BoundField DataField="DocumentNo" HeaderText="Document No" />
            <asp:BoundField DataField="FileName" HeaderText="File Name" />
            <asp:BoundField DataField="DocumentDescription" HeaderText="Description" />
            <asp:BoundField DataField="UploadDate" HeaderText="Uploaded On" DataFormatString="{0:dd-MM-yyyy HH:mm}" />

            <asp:TemplateField HeaderText="View Document">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkView" runat="server"
                        NavigateUrl='<%# "FileViewer.aspx?FileID=" + Eval("FileID") %>'
                        Text="View" Target="_blank" CssClass="btn btn-primary">
                </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

     <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>


</asp:Content>
