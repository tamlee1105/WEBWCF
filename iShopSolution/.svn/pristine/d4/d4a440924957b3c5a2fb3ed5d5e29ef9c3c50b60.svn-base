<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Manager.Master" AutoEventWireup="true"
    CodeBehind="login.aspx.cs" Inherits="Website.admin.login1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .center-cont
        {
            margin: 100px auto;
            padding: 30px;
            background: white;
            border-radius: 10px;
            box-shadow: 0px 0px 20px #aaa;
            -moz-box-shadow: 0 0 20px #aaa;
            -webkit-box-shadow: 0 0 20px #aaa;
            width: 500px;
            height: 100%;
        }
        h2#tille-login
        {
            font-size: 20px;
            font-weight: bold;
        }
        .login-cont table
        {
            padding: 30px;
        }
        span.lable
        {
            font-weight: bold;
        }
        .login-cont input
        {
            height: 25px;
            width: 80%;
        }
        #btnSubmit, #ContentPlaceHolder1_btnSubmit
        {
            height: 35px;
            width: 100px;
            margin: 20px auto 0px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-cont">
        <div class="center-cont">
            <div>
                <h2 id="tille-login">
                    Đăng nhập</h2>
            </div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label CssClass="lable" ID="Label1" runat="server" Text="Tên đăng nhập"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass="lable" ID="Label2" runat="server" Text="Mật khẩu"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Button ID="btnSubmit" runat="server" Text="Login" OnClick="btnSubmit_Click" />
                        <br/>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
