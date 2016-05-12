<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormLogin.aspx.cs" Inherits="YanZhiwei.DotNet.Core.AdminPanel.FormLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登陆</title>
    <meta charset="utf-8" />
    <link href="Content/login.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jsUtils.js" type="text/javascript"></script>
    <script src="Scripts/jqUtils.js" type="text/javascript"></script>
    <script src="Scripts/page/formLogin.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            FormLogin.init();
        });
    </script>
</head>
<body style="padding-top: 167px">
    <form id="form1" runat="server">
        <div class="boxLogin">
            <dl>
                <dd>
                    <div class="s1">
                        账&nbsp;&nbsp;&nbsp;户：
                    </div>
                    <div class="s2">
                        <input type="text" id="txtUserName" value="system" class="txt" style="width: 122px;" />
                        <span id="errorMsg0" class="errorMsg"></span>
                    </div>
                </dd>
                <dd>
                    <div class="s3">
                        密&nbsp;&nbsp;&nbsp;码：
                    </div>
                    <div class="s4">
                        <input type="password" onpaste="return false;" id="txtUserPwd" value="system" class="txt"
                            style="width: 122px;" />&nbsp;<span id="errorMsg1" class="errorMsg"></span>
                    </div>
                </dd>
                <dd>
                    <div class="s5">
                        验证码：
                    </div>
                    <div class="s6">
                        <input type="text" id="txtCode" maxlength="4" class="txt" style="ime-mode: disabled; width: 48px;" />
                        <img src="/BackHandler/VerifyCode.ashx" id="imgVerifyCode" width="70" height="22" alt="点击切换验证码"
                            title="点击切换验证码" style="margin-top: 0px; vertical-align: top; cursor: pointer;" />
                        <span id="errorMsg2" class="errorMsg"></span>
                    </div>
                </dd>
                <dd>
                    <div class="load">
                        <img src="../Themes/Images/Login/loading.gif" />
                    </div>
                </dd>
            </dl>
            <div class="s8">
                <input id="btnLogin" type="button" class="sign" />
            </div>
        </div>
        <div class="copyright">
            <p id="cp">
                最佳浏览环境：IE8.0～10.0或基于IE内核的浏览器，1280×800显示分辨率。
            </p>
        </div>
    </form>
</body>
</html>