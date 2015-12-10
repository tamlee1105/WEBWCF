<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryMenu.ascx.cs" Inherits="Website.UserControl.CategoryMenu" %>
<asp:Label ID="lblMsg" runat="server" Text="" Font-Bold="True" 
    ForeColor="Red"></asp:Label>
<asp:DataList ID="dListMenu" runat="server" Width="100%" CssClass="CategoryMenu">
    <ItemTemplate>
        <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Eval("Id","~/Products.aspx?cateId={0}") %>'
            Text='<%# Eval("Name") %>' runat="server">HyperLink</asp:HyperLink>
    </ItemTemplate>
</asp:DataList>