<%@ Page Title="Plan Breakdown" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlanBreakdown.aspx.cs" Inherits="CDS_AddOn.PlanBreakdown" %>


<%-- This page is showing 4 years plan break down in a table --%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 290px">
    <h3> Plan Breakdown into Four Years.</h3>
        <div style="width: 804px; margin-left: 146px; height: 137px;">
            <%-- TODO: The 4 yeal plan table goes here --%>
            <asp:GridView ID="FourYearPlanGridView" runat="server"></asp:GridView>
        </div>
        <div style="height: 62px; margin-top: 10px;">
             <div style="width: 226px">
                <asp:Button class="btn btn-primary btn-lg" ID="GoBackButton" runat="server" Text="GO Back &raquo;" OnClick="GoBackButton_Click" />
                 <%-- TODO: Add a backwards arrow --%>
            
             </div>
            
            <div style="width: 423px; margin-left: 397px; margin-top: -55px">
                <asp:Button class="btn btn-primary btn-lg" ID="RealityVsYourPlanButton" runat="server" Text="Reality Vs Your Plan Graph  &raquo;" OnClick="RealityVsYourPlanButton_Click" />
            </div>

        </div>
       </div>
</asp:Content>
