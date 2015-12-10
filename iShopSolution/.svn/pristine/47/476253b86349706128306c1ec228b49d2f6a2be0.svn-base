<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPage.Master" AutoEventWireup="true"
    CodeBehind="Manager.aspx.cs" Inherits="Website.Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHGioHang" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPHSearch" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript">
        function checkall() {
            alert('ok');
            $(':checked').each(function () {
                this.checked = true;
            });
        }
    </script>
    <div>
        <div class="tille_content">
            <label id="lblCatePro" runat="server">
                Quản lý đơn đặt hàng</label>
        </div>
        <div class="clear">
        </div>
        <div class="table_center">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="ListCart">
                        <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                            GridLines="None">
                            <Columns>
                                <asp:TemplateField HeaderText="Loại sản phẩm">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCateName" runat="server" Text='<%# Bind("CateName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tên sản phẩm">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("ProductName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Giá">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("ProductPrice") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Số lượng">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("Unit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EmptyDataTemplate>
                                Chưa chọn đơn đặt hàng
                            </EmptyDataTemplate>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                    </div>
                    <div class="ListCart">
                        <asp:GridView ID="gvLstCart" runat="server" AutoGenerateColumns="False" Width="100%"
                            DataKeyNames="Id" OnRowCommand="gvLstCart_RowCommand" GridLines="None">
                            <Columns>
                                <asp:TemplateField HeaderText="Khách hàng">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("Customer.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Thời gian">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy HH:mm:ss}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tổng sản phẩm">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalProduct" runat="server" Text='<%# Eval("TotalProduct") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tổng số lượng">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalUnit" runat="server" Text='<%# Eval("TotalUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Trạng thái">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chi tiết">
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:CheckBox ID="chbSelected" runat="server" />
                                            <asp:ImageButton ID="imgBtnDetails" runat="server" ImageUrl="~/images/detail.png"
                                                CommandArgument='<%# Eval("Id") %>' CommandName="details" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EmptyDataTemplate>
                                Chưa có đơn đặt hàng
                            </EmptyDataTemplate>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                    </div>
                    <div>
                        <input type="button" name="select-all" value="Chọn hết" id="select-all" onclick="checkall()" />
                        <asp:Button ID="btnUnSelected" runat="server" Text="Bỏ chọn" />
                        <asp:Button ID="btnSendRequest" runat="server" Text="Gởi phiếu đặt hàng" 
                            onclick="btnSendRequest_Click" />

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="clear">
        </div>
        <div class="but_center">
        </div>
    </div>
</asp:Content>
