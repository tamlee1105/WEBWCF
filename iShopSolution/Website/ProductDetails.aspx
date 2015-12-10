<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPage.Master" AutoEventWireup="true"
    CodeBehind="ProductDetails.aspx.cs" Inherits="Website.ProductDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <div class="tille_content">
                    Chi tiết sản phẩm
                </div>
                <div class="clear">
                </div>
                <div class="table_center">
                    <div style="padding: 5px 5px 5px 5px">
                        <table cellpadding="0" cellspacing="5" width="100%">
                            <tr>
                                <td style="width: 100px" valign="top">
                                    <div class="image_laptop">
                                        <asp:Image ID="lblPic" runat="server" />
                                    </div>
                                </td>
                                <td valign="top">
                                    <div class="laptop_detail_text">
                                        <div style="padding: 5px; margin: 5px" class="mytextarea">
                                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                        </div>
                                        <table class="details" id="info_product" width="100%" cellpadding="0" cellspacing="0"
                                            style="margin: 5px;">
                                            <tr>
                                                <td class="description">
                                                    <b>Loại sản phẩm</b>
                                                </td>
                                                <td class="info">
                                                    <asp:Label ID="lblCateName" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="description">
                                                    <b>Bảo hành</b>
                                                </td>
                                                <td class="info">
                                                    <asp:Label ID="lblWarranty" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="description">
                                                    <b>Trong kho</b>
                                                </td>
                                                <td class="info">
                                                    <div class="quantity">
                                                        <asp:Label ID="lblSoTon" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="description">
                                                    <b>Giá</b>
                                                </td>
                                                <td class="info">
                                                    <div class="price">
                                                        <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="description">
                                                    <b>Cập nhật</b>
                                                </td>
                                                <td class="info">
                                                    <div class="quantity">
                                                        <asp:Label ID="lblUpdate" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="Sale">
                                            <table>
                                                <tr>
                                                    <td style="font-size: 13px; color: Red; font-weight: bold;">
                                                        <asp:TextBox ID="txtSoLuong" CssClass="SoLuong" Text="1" runat="server" MaxLength="2"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"
                                                            Text="*" ControlToValidate="txtSoLuong">*</asp:RequiredFieldValidator>
                                                        <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtSoLuong"
                                                            ValidChars="0123456789">
                                                        </cc2:FilteredTextBoxExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnBuy" runat="server" CssClass="btnSale" Text="" 
                                                            onclick="btnBuy_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <b class="mytextarea">» Mô tả sản phẩm</b><br />
                        <div style="margin: 10px;">
                        </div>
                        <asp:Label ID="lblDes" runat="server" Text=""></asp:Label>
                        <div class="box do">
                            <ul class="box">
                                <li class="tmore gotop"><a href="javascript:window.scrollTo(0,500)">Đầu trang</a></li>
                                <li class="tmore back"><a href="javascript:history.go(-1);">Trở về</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
                <div class="but_center">
                </div>
            </div>
            <div style="padding-top: 5px">
                <div class="tille_content">
                    Sản phẩm cùng loại</div>
                <div class="clear">
                </div>
                <div class="table_center">
                    <div style="padding: 5px 5px 5px 5px">
                        <asp:ListView ID="lvSamePros" runat="server" GroupItemCount="3">
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
                                <tr runat="server" id="productRow" style="height: 80px">
                                    <td runat="server" id="itemPlaceholder">
                                    </td>
                                </tr>
                            </GroupTemplate>
                        </asp:ListView>
                        <div class="ConlectionPage">
                            <asp:DataPager ID="DataPager1" PagedControlID="lvSamePros" runat="server" OnPreRender="DataPager1_PreRender"
                                PageSize="3">
                                <Fields>
                                    <asp:NumericPagerField />
                                </Fields>
                            </asp:DataPager>
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
                <div class="but_center">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
