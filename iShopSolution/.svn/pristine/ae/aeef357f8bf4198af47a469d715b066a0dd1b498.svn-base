<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Products.ascx.cs" Inherits="Website.UserControl.Products" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div>
            <div class="tille_content">
                <label id="lblCatePro" runat="server">
                    Sản Phẩm</label>
            </div>
            <div class="clear">
            </div>
            <div class="table_center">
                <!-- Main contetn -->
                <asp:ListView ID="lvProducts" runat="server" GroupItemCount="3">
                    <ItemTemplate>
                        <td id="Td1" valign="top" align="center" runat="server">
                            <div class="Product">
                                <div class="images" onmouseover="Tip('<div class=\'div_tootip_content\'><div class=\'div_ttp_title\'><%# Eval("Name") %>  <div class=\'ttp_price\'><%# Eval("StrPrice") %></div></div><div class=\'div_ttp_pre_title\'>Đặt hàng online<br/> giá rẻ hơn <span class=\'number\'>500,000 VNĐ</span></div><div class=\'div_fun_title\'>Chi tiết<br /></div><div class=\'div_fun_content\'><ul><li> - <b>Loại sản phẩm:</b> <%# Eval("Category.Name") %></li><li> - <b>Bảo hành:</b> <%# Eval("Warranty") %></li><li> - <b>Trong kho:</b> <%# Eval("Inventory","{0} Sản phẩm") %></li><li> - <b>Cập nhật:</b> <%# Eval("UpdateTime","{0:dd/MM/yyyy HH:mm:ss}") %></li></ul</div></div>', WIDTH, 250, ABOVE, true)"
                                    onmouseout="UnTip();">
                                    <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# Eval("Id","~/ProductDetails.aspx?id={0}") %>'
                                        ImageUrl='<%# Eval("ImageHost","{0}") %>' runat="server"></asp:HyperLink>
                                </div>
                                <div class="name">
                                    <asp:HyperLink ID="HyperLink1" Text='<%# Eval("Name") %>' NavigateUrl='<%# Eval("Id","~/ProductDetails.aspx?id={0}") %>'
                                        runat="server"></asp:HyperLink></div>
                                <div class="price">
                                    <asp:Label ID="lblPricePro" runat="server" Text='<%# Eval("StrPrice") %>'></asp:Label></div>
                                <div class="buynow_spec" onclick="location='<%# Eval("Id","ShoppingCart.aspx?ProductID={0}") %>'">
                                </div>
                            </div>
                        </td>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table cellpadding="2" runat="server" id="tblProducts">
                            <tr runat="server" id="groupPlaceholder">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr runat="server" id="productRow">
                            <td runat="server" id="itemPlaceholder">
                            </td>
                        </tr>
                    </GroupTemplate>
                    <EmptyDataTemplate>
                        <div style="text-align: center; font-weight: bold; color: Red; margin: 10px;">
                            Đang cập nhật</div>
                    </EmptyDataTemplate>
                </asp:ListView>
                <div class="ConlectionPage">
                    <asp:DataPager ID="DataPager1" PagedControlID="lvProducts" runat="server" OnPreRender="DataPager1_PreRender"
                        PageSize="9">
                        <Fields>
                            <asp:NumericPagerField ButtonCount="9" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="but_center">
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
