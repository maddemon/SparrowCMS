<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetUp.aspx.cs" Inherits="ExampleSite.Setup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>安装SparrowCMS</h2>
        <fieldset>
            <legend>数据配置</legend>
            <table>
                <tr>
                    <th>数据库类型：</th>
                    <td>
                        <select name="DbType">
                            <option>SqlServer</option>
                            <option>Access</option>
                            <option>MySql</option>
                            <option>SQLite</option>
                        </select></td>
                </tr>
                <tr>
                    <th>主机名：</th>
                    <td>
                        <input type="text" name="DbAddress" /></td>
                </tr>
                <tr>
                    <th>用户名：</th>
                    <td>
                        <input type="text" name="DbUsername" /></td>
                </tr>
                <tr>
                    <th>密码：</th>
                    <td>
                        <input type="text" name="DbPassword" /></td>
                </tr>
                <tr>
                    <th>端口：</th>
                    <td>
                        <input type="text" name="BbPort" /></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>站点设置：</legend>
            <table>
                <tr>
                    <th>网站名称：</th>
                    <td>
                        <input type="text" name="SiteName" /></td>
                </tr>
            </table>
        </fieldset>
        <input type="submit" />
    </form>
</body>
</html>
