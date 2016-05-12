<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bootstrapvalidatorDemo.aspx.cs" Inherits="YanZhiwei.JavaScript.Utilities.bs.v3.bootstrapvalidatorDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>bootstrapvalidator Demo</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="bootstrapvalidator/css/bootstrapValidator.min.css" rel="stylesheet" />
    <script src="../../jquery/jquery-1.9.1.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="bootstrapvalidator/js/bootstrapValidator.min.js"></script>
    <script type="text/javascript">

        $(function () {
            $('form').bootstrapValidator({
                message: 'This value is not valid',
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                submitHandler: function (validator, form, submitButton) {
                    alert('ok');
                },
                fields: {
                    username: {
                        message: '用户名验证失败',
                        validators: {
                            notEmpty: {
                                message: '用户名不能为空'
                            },
                            stringLength: {
                                min: 6,
                                max: 18,
                                message: '用户名长度必须在6到18位之间'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9_]+$/,
                                message: '用户名只能包含大写、小写、数字和下划线'
                            }
                        }
                    },
                    email: {
                        validators: {
                            notEmpty: {
                                message: '邮箱不能为空'
                            },
                            emailAddress: {
                                message: '邮箱地址格式有误'
                            }
                        }
                    }
                }

            });
        });
        //$(function () {
        //    $('form').bootstrapValidator({
        //        message: 'This value is not valid',
        //        feedbackIcons: {
        //            valid: 'glyphicon glyphicon-ok',
        //            invalid: 'glyphicon glyphicon-remove',
        //            validating: 'glyphicon glyphicon-refresh'
        //        },
        //        fields: {
        //            username: {
        //                message: '用户名验证失败',
        //                validators: {
        //                    notEmpty: {
        //                        message: '用户名不能为空'
        //                    }
        //                }
        //            },
        //            email: {
        //                validators: {
        //                    notEmpty: {
        //                        message: '邮箱地址不能为空'
        //                    }
        //                }
        //            }
        //        }
        //    });
        //});
    </script>
</head>
<body>
    <div class="container">
        <div class="row">
            <form>
                <div class="form-group">
                    <label>Username</label>
                    <input type="text" class="form-control" name="username" />
                </div>
                <div class="form-group">
                    <label>Email address</label>
                    <input type="text" class="form-control" name="email" />
                </div>
                <div class="form-group">
                    <button type="submit" name="submit" class="btn btn-primary">Submit</button>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
