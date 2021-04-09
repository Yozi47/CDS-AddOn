<%@ Page Title="Reality Vs Your Plan" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RealityVsYourPlan.aspx.cs" Inherits="CDS_AddOn.RealityVsYourPlan" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 452px">
    <h3> Reality Vs Your Plan.</h3>
        <div style="width: 804px; margin-left: 146px; height: 137px;">
            <%-- TODO: The Plan Chart.js goes here --%>
        </div>
       <div style="width: 804px; margin-left: 146px; height: 137px;">
            <%-- TODO: The Plan Chart.js goes here --%>
        </div>
        <div style="height: 62px; margin-top: 10px;">
             <div style="width: 226px">
                <asp:Button class="btn btn-primary btn-lg" ID="GoBackButton" runat="server" Text="GO Back &raquo;" OnClick="GoBackButton_Click" />
                 <%-- TODO: Add a backwards arrow --%>
            
             </div>

        </div>
       </div>
</asp:Content>
