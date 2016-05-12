<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormSwitch.aspx.cs" Inherits="YanZhiwei.JavaScript.Learn.Bootstrap.V3.Switch.FormSwitch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Bootstrap3 基本模板</title>
    <!-- Bootstrap -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="bootstrap-switch-master/dist/css/bootstrap3/bootstrap-switch.css" rel="stylesheet" />
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="../js/html5shiv.min.js"></script>
      <script src="../js/respond.min.js"></script>
    <![endif]-->
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="../js/jquery-1.11.1.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="../js/bootstrap.min.js"></script>
    <script src="bootstrap-switch-master/dist/js/bootstrap-switch.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[name='my-checkbox']").bootstrapSwitch();
            $("[name='GroupedSwitches']").bootstrapSwitch();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <input type="checkbox" name="my-checkbox" data-off-text="全关" data-on-text="全开" checked>
        </div>
    </form>
</body>
</html>
