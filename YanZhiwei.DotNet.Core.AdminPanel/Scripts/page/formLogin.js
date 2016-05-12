/// <reference path="../jquery-1.9.1.js" />
/// <reference path="../jqUtils.js" />

var FormLogin = function () {
    /// <summary>
    /// 用户登录业务Js
    /// </summary>
    /// <returns type=""></returns>
    var hanlderLogin = function () {
        /// <summary>
        /// 用户登录
        /// </summary>
        $('#btnLogin').click(function () {
            var _name = $("#txtUserName").val(),
                _pwd = $("#txtUserPwd").val(),
                _code = $("#txtCode").val();
            if (checkedUserLoginData(_name, _pwd, _code)) {
                var _parm = 'action=login&userName=' + escape(_name) + '&userPassword=' + escape(_pwd) + '&verifyCode=' + escape(_code);
                jqUtils.Ajax.post('/BackHandler/BaseHandler.ashx', _parm, function (data) {
                    if (data.StatusCode == 200) {
                        window.location.href = 'Admin/FormAdminIndex.aspx';
                    }
                    else {
                        if (data.ErrorCode == 1) {
                            $("#txtCode").focus();
                            $("#errorMsg2").html(data.Message);
                            createVerifyCode();
                        }
                        else if (data.ErrorCode == 2) {
                            $("#txtUserName").focus();
                            $("#errorMsg0").html("账户被锁,联系管理员");
                        }
                        else if (data.ErrorCode == 4) {
                            $("#txtUserName").focus();
                            $("#errorMsg0").html("账户或密码有错误");
                        }
                        else if (data.ErrorCode == 6) {
                            $("#txtUserName").focus();
                            $("#errorMsg0").html("该用户已经登录");
                        }
                        else {
                            alert(data.Message);
                            window.location.href = window.location.href.replace('#', '');
                        }
                    }
                });
            }
        });
    }
    var checkedUserLoginData = function (name, pwd, code) {
        /// <summary>
        /// 检查用户登录数据完整性
        /// </summary>
        /// <param name="name" type="type"></param>
        /// <param name="pwd" type="type"></param>
        /// <param name="code" type="type"></param>
        /// <returns type=""></returns>
        $("#errorMsg0").html("");
        $("#errorMsg1").html("");
        $("#errorMsg2").html("");
        if (name == "") {
            $("#txtUserName").focus();
            $("#errorMsg0").html("账户不能为空");
            return false;
        } else if (pwd == "") {
            $("#txtUserPwd").focus();
            $("#errorMsg1").html("密码不能为空");
            return false;
        } else if (code == "") {
            $("#txtCode").focus();
            $("#errorMsg2").html("验证码不能为空");
            return false;
        } else {
            return true;
        }
    }
    var hanlderBasicInit = function () {
        /// <summary>
        /// 基本初始化
        /// </summary>
        document.onkeydown = function (e) {
            if (!e) e = window.event; //火狐中是 window.event
            if ((e.keyCode || e.which) == 13) {
                var _obtnSearch = document.getElementById("btnLogin")
                _obtnSearch.focus(); //让另一个控件获得焦点就等于让文本输入框失去焦点
                _obtnSearch.click();
            }
        }
    }
    var createVerifyCode = function () {
        /// <summary>
        /// 更新验证码
        /// </summary>
        $("#txtCode").val("");
        $("#imgVerifyCode").attr("src", $("#imgVerifyCode").attr("src") + "?time=" + Math.random());
    }
    var hanlderVerifyCode = function () {
        /// <summary>
        /// 处理验证码控件事件订阅
        /// </summary>
        $('#imgVerifyCode').click(function () {
            createVerifyCode();
        });
    }
    return {
        init: function () {
            hanlderBasicInit();
            hanlderVerifyCode();
            hanlderLogin();
        },
        createVerifyCode: function () {
            createVerifyCode();
        }
    }
}();