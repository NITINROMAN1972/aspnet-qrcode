<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QRCodeGenerator.aspx.cs" Inherits="QRCodeGenerator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td>Name</td>
                <td>Email</td>
                <td>Phone No</td>
                <td>Password</td>
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
                <td>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <div>
        </div>
        <div class="table-responsive">
            <!--begin: Datatable -->
            <asp:GridView ShowHeaderWhenEmpty="true" ID="GrdUser" runat="server" AutoGenerateColumns="false" OnRowCommand="GrdUser_RowCommand">
                <HeaderStyle CssClass="kt-shape-bg-color-3" />
                <Columns>
                    <asp:TemplateField ControlStyle-CssClass="col-xs-1" HeaderText="Sr.No">
                        <ItemTemplate>
                            <asp:HiddenField ID="id" runat="server" Value='<% #Eval("id") %>' />
                            <span>
                                <%#Container.DataItemIndex + 1%>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" SortExpression="MobileNo" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="btnedit" CommandArgument='<%# Eval("id") %>' CommandName="lnkDownload" Text="download" ToolTip="Print" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <!--end: Datatable -->
        </div>
    </form>
</body>
</html>
