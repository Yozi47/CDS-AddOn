<%@ Page Title="Plan Tree Diagram" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlanTreeDiagram.aspx.cs" Inherits="CDS_AddOn.PlanTreeDiagram" %>

<%-- I am using this page to use graphics for making a tree diagram TODO: Rename the page --%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position:relative">
        <div style="height: 548px; margin-left: 19px; margin-right: 64px; margin-top: 11px;">
    <h3> Showcase of the plan as a Tree Diagram.</h3>
        <div style="width: 1255px; margin-left: 12px; height: 438px; overflow:auto">
            <asp:Image ID="TreeDiagramImage" runat="server" ImageUrl="~/tree.svg" />
        </div>    
       
       </div>
    <div style="clear:both; position:relative">
         <div style="height: 62px; margin-top: 10px; width: 1336px;">
             <div style="width: 226px">
                <asp:Button class="btn btn-primary btn-lg" ID="GoBackButton" runat="server" Text="&laquo; GO Back" OnClick="GoBackButton_Click" />            
             </div>
            
            <div style="width: 328px; margin-left: 397px; margin-top: -55px">
                <asp:Button class="btn btn-primary btn-lg" ID="RealityVsYourPlanPlanButton" runat="server" Text="Reality Vs Your Plan &raquo;" OnClick="RealityVsYourPlanPlanButton_Click" />
            </div>

        </div>
    </div>
    </div>

</asp:Content>
