<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CDS_AddOn._Default" %>

<%-- This is a start page --%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<%--        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
   <%--                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>--%>
 
    <div style="height: 251px">
        <h3> Select your plan to inspect.</h3>
        <div style="width: 193px; margin-left: 113px; height: 44px;">
            <asp:DropDownList ID="PlanSelectorDropDownList"  runat="server" Width="174px"></asp:DropDownList> <%--TODO: Put the text in dropdown saying maybe " Choose your plan here" --%>
        </div>
        <div style="height: 62px; margin-top: 10px;">
            <p>
                <asp:Button class="btn btn-primary btn-lg" ID="PlanInspectorButton" runat="server" Text="Inspect your Plan  &raquo;" />
            </p>

        </div>
    </div>

</asp:Content>
