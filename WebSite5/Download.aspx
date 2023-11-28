<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Download.aspx.cs" Inherits="Download" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1">
                <tr>
                    <td>Name</td>
                    <td>Email</td>
                    <td>Phone No</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Image ID="QRCodeImg" runat="server" Width="200px" Height="200px" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:Button ID="Print" runat="server" Text="Print" />
    </form>
    <p>
        &nbsp;
    </p>
</body>
</html>
