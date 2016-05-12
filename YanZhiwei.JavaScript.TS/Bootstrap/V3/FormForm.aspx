<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormForm.aspx.cs" Inherits="YanZhiwei.JavaScript.Learn.Bootstrap.V3.FormForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Bootstrap3 基本模板</title>
    <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="js/html5shiv.min.js"></script>
      <script src="js/respond.min.js"></script>
    <![endif]-->
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="js/jquery-1.11.1.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
</head>
<body>
    <div class="container">
        <h1>基本案例</h1>
        <h4><small>单独的表单控件会被自动赋予一些全局样式。所有设置了.form-control的'input'、'textarea'和'select'元素都将被默认设置为width: 100%;。将label和前面提到的这些控件包裹在.form-group中可以获得最好的排列。</small></h4>
        <form id="form1" role="form">
            <div class="form-group">
                <label for="inputEmail">电子邮箱</label>
                <input type="email" class="form-control" id="inputEmail" placeholder="请输入电子邮箱...">
            </div>
            <div class="form-group">
                <label for="inputPassword">用户密码</label>
                <input type="password" class="form-control" id="inputPassword" placeholder="请输入用户密码...">
            </div>
            <button type="submit" class="btn btn-default">登录</button>
        </form>
        <h1>内联表单</h1>
        <h4><small>为左对齐和inline-block级别的控件设置.form-inline，可以将其排布的更紧凑。需要设置宽度：在Bootstrap中,input、select和textarea默认被设置为100%宽度。为了使用内联表单，你需要专门为使用到的表单控件设置宽度。对于这些内联表单,你可以通过为label设置.sr-only已将其隐藏。</small></h4>
        <form role="form" class="form-inline">
            <div class="form-group">
                <label for="inputEmail2" class="sr-only">电子邮箱</label>
                <input type="email" class="form-control" id="inputEmail2" placeholder="请输入电子邮箱...">
            </div>
            <div class="form-group">
                <label for="inputPassword2" class="sr-only">用户密码</label>
                <input type="password" class="form-control" id="inputPassword2" placeholder="请输入用户密码...">
            </div>
            <div class="checkbox">
                <label>
                    <input type="checkbox">记住我
                </label>
            </div>
            <button type="submit" class="btn btn-default">登录</button>
        </form>
        <h1>水平排列的表单</h1>
        <h4><small>1.通过为表单添加.form-horizontal，并使用Bootstrap预置的栅格class可以将label和控件组水平并排布局。这样做将改变.form-group的行为，使其表现为栅格系统中的行（row），因此就无需再使用.row了。</small></h4>
        <h4><small>2.大部分表单控件、文本输入域控件。包括HTML5支持的所有类型：text、password、datetime、datetime-local、date、month、time、week、number、email、url、search、tel和color。</small></h4>
        <h4><small>3.注意：有正确设置了type的input控件才能被赋予正确的样式。</small></h4>
        <h4><small>4.为输入框设置disabled属性可以防止用户输入，并能改变一点外观，使其更直观。</small></h4>
        <h4><small>5.Bootstrap对表单控件的校验状态，如error、warning和success状态，都定义了样式。使用时，添加.has-warning、.has-error或.has-success到这些控件的父元素即可。任何包含在此元素之内的.control-label、.form-control和.help-block都将接受这些校验状态的样式。</small></h4>
        <h4><small>6.通过.input-lg之类的class可以为控件设置高度，通过.col-lg-*之类的class可以为控件设置宽度。</small></h4>
        <form role="form" class="form-horizontal">
            <div class="form-group has-success">
                <label for="inputEmail3" class="col-sm-2 control-label">电子邮箱</label>
                <div class="col-sm-10">
                    <input type="email" class="form-control" id="inputEmail3" placeholder="请输入电子邮箱...">
                </div>
            </div>
            <div class="form-group has-feedback">
                <label for="inputPassword3" class="col-sm-2 control-label">用户密码</label>
                <div class="col-sm-10">
                    <input type="password" class="form-control" id="inputPassword3" placeholder="请输入用户密码..." />
                </div>
            </div>
            <div class="form-group has-success">
                <label for="inputEmployee" class="col-sm-2 control-label">企业员工</label>
                <div class="col-sm-10">
                    <label class="form-control">
                        <input type="checkbox">
                        若本公司员工，请勾选
                    </label>
                </div>
            </div>
            <div class="form-group has-warning">
                <label for="inputGender" class="col-sm-2 control-label">用户性别</label>
                <div class="col-sm-10">
                    <div class="form-control">
                        <label>
                            <input type="radio" name="optGender" id="optMan" value="option1" checked>男
                        </label>
                        <label>
                            <input type="radio" name="optGender" id="optGirl" value="option2">女
                        </label>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label for="inputNative" class="col-sm-2 control-label">用户籍贯</label>
                <div class="col-sm-10">
                    <select class="form-control" id="inputNative">
                        <option>北京</option>
                        <option>上海</option>
                        <option>广州</option>
                        <option>株洲</option>
                        <option>珠海</option>
                    </select>
                </div>
            </div>
            <div class="form-group">
                <label for="inputRemark" class="col-sm-2 control-label">备注</label>
                <div class="col-sm-10">
                    <textarea class="form-control" rows="3"></textarea>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10">
                    <button type="submit" class="btn btn-primary">登录</button>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
