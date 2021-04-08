<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="CDS_AddOn.About" %>

<%-- I am usning this page to use graphics for making a tree diagram TODO: Rename the page --%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <div>
    <h3> Showcase of the plan as a Tree Diagram.</h3>
        <div style="width: 193px; margin-left: 113px; height: 44px;">
            <%-- TODO: The graphics goes here --%>
        </div>
        <div style="height: 62px; margin-top: 10px;">
             <p>
                <asp:Button class="btn btn-primary btn-lg" ID="GoBackButton" runat="server" Text="GO Back &raquo;" />
                 <%-- TODO: Add a backwards arrow --%>
            </p>
            
            <p>
                <asp:Button class="btn btn-primary btn-lg" ID="SeeFourYearPlanButton" runat="server" Text="See Four Year Plan  &raquo;" />
            </p>

        </div>
       </div>

</asp:Content>
