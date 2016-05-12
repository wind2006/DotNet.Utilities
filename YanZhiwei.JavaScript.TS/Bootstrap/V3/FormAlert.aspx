<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormAlert.aspx.cs" Inherits="YanZhiwei.JavaScript.Learn.Bootstrap.V3.FormAlert" %>

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
    <script type="text/javascript">
        function closeAlert() {
            $("#alert2").alert('close');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>警示框</h1>
            <div class="alert alert-success">Well done! You successfully read this important alert message.</div>
            <div class="alert alert-info">Well done! You successfully read this important alert message.</div>
            <div class="alert alert-warning">Well done! You successfully read this important alert message.</div>
            <div class="alert alert-danger">Well done! You successfully read this important alert message.</div>
            <h1>可关闭的警告框 </h1>
            <div class="alert alert-warning alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <strong>Warning!</strong> Best check yo self, you're not looking too good.
            </div>
            <div class="alert alert-danger fade in">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <h4>Oh snap! You got an error!</h4>
                <p>Change this and that and try again. Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Cras mattis consectetur purus sit amet fermentum.</p>
                <p>
                    <button type="button" class="btn btn-danger">Take this action</button>
                    <button type="button" class="btn btn-success">Or do this</button>
                </p>
            </div>
            <div id="alert2" class="alert alert-warning fade in">
                <button type="button" class="close" onclick="closeAlert()" aria-hidden="true">&times;</button>
                <strong>Holy guacamole!</strong> Best check yo self, you're not looking too good.
            </div>
            <h1>警告框中的链接 </h1>
            <div class="alert alert-success">
                Well done! 
             <a href="#" class="alert-link">You successfully read this important alert message.</a>
            </div>
            <div class="alert alert-info">
                Well done! 
             <a href="#" class="alert-link">You successfully read this important alert message.</a>
            </div>
            <div class="alert alert-warning">
                Well done! 
             <a href="#" class="alert-link">You successfully read this important alert message.</a>
            </div>
            <div class="alert alert-danger">
                Well done!
             <a href="#" class="alert-link">You successfully read this important alert message.</a>
            </div>

        </div>
    </form>
</body>
</html>
