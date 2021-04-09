<%@ Page Title="Plan Tree Diagram" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlanTreeDiagram.aspx.cs" Inherits="CDS_AddOn.PlanTreeDiagram" %>

<%-- I am using this page to use graphics for making a tree diagram TODO: Rename the page --%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 290px">
    <h3> Showcase of the plan as a Tree Diagram.</h3>
        <div style="width: 193px; margin-left: 113px; height: 44px;">
            <%-- TODO: The graphics goes here --%>
        </div>
        <div style="height: 62px; margin-top: 10px;">
             <div style="width: 226px">
                <asp:Button class="btn btn-primary btn-lg" ID="GoBackButton" runat="server" Text="GO Back &raquo;" OnClick="GoBackButton_Click" />
                 <%-- TODO: Add a backwards arrow --%>
            
             </div>
            
            <div style="width: 328px; margin-left: 397px; margin-top: -55px">
                <asp:Button class="btn btn-primary btn-lg" ID="SeeFourYearPlanButton" runat="server" Text="See Four Year Plan  &raquo;" OnClick="SeeFourYearPlanButton_Click" />
            </div>

        </div>
       </div>

</asp:Content>
