<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupperAlloyDeptInfo.aspx.cs" Inherits="FileUplode.SupperAlloyDeptInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


      
    <style>

       .list-item a {
    font-weight: bold;      /* Make text bold */
    font-style: italic;     /* Make text italic */
    color: cornflowerblue;           /* Set text color to black */
    text-decoration: none;  /* Remove underline */
    font-size: x-large;     /* Set font size to extra large */
}


    </style>
     <div class="page-header">
            <h1>Departmental Information</h1>
    <hr style="border: 3px solid blue; font-weight: bold;" />
        </div>

     <div class="container">
<ul class="list-group">
    <li class="list-item"><a href="hrm_display.aspx?dept=HRM">HRM - Dept. Info</a></li>
<li class="list-item"><a href="hrm_display.aspx?dept=HRMM">HRM - Magazines</a></li>
<li class="list-item"><a href="hrm_display.aspx?dept=HRMC">HRM - CSR</a></li>
<li class="list-item"><a href="drawing_display.aspx?dept=DRAWING">DRAWING CELL</a></li>
<li class="list-item"><a href="safety_display.aspx?dept=SAFETY">SAFETY</a></li>
<li class="list-item"><a href="training_display.aspx?dept=TRAINING">TPM Training Modules</a></li>
<li class="list-item"><a href="daksha_display.aspx?dept=DAKSHA">Project Daksh</a></li>

    
     


</ul>

         </div>
     

</asp:Content>
