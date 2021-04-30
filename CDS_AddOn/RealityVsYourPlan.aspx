<%@ Page Title="Reality Vs Your Plan" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RealityVsYourPlan.aspx.cs" Inherits="CDS_AddOn.RealityVsYourPlan" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 546px">
    <h3> Reality Vs Your Plan.</h3>
        <div style="width: 804px; margin-left: 146px; height: 394px;">
            <asp:Chart ID="PieChartCredits" runat="server" Width="483px">
            </asp:Chart>
        </div>
      <%-- <div style="width: 804px; margin-left: 146px; height: 137px;">
            <%-- TODO: The Plan Chart.js goes here --%>
<%--        </div>--%>
        <div style="height: 62px; margin-top: 10px;">
             <div style="width: 226px">
                <asp:Button class="btn btn-primary btn-lg" ID="GoBackButton" runat="server" Text="&laquo; GO Back" OnClick="GoBackButton_Click" />
                 <%-- TODO: Add a backwards arrow --%>
            
             </div>

        </div>
       </div>
</asp:Content>
