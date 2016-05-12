<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormPanel.aspx.cs" Inherits="YanZhiwei.JavaScript.Learn.Bootstrap.V3.FormPanel" %>

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
    <form id="form1" runat="server">
        <div class="container">
            <h1>面板</h1>
            <div class="panel panel-default">
                <div class="panel-body">Basic panel example </div>
            </div>
            <h1>带标题的面版</h1>
            <div class="panel panel-default">
                <div class="panel-heading">Panel heading without title</div>
                <div class="panel-body">Panel content </div>
            </div>
            <h1>带脚注的面版</h1>
            <div class="panel panel-default">
                <div class="panel-heading">Panel heading without title</div>
                <div class="panel-body">Panel content </div>
                <div class="panel-footer">Panel footer</div>
            </div>
            <h1>有意义的替换</h1>
            <div class="panel panel-primary">
                <div class="panel-heading">Panel heading without title</div>
                <div class="panel-body">Panel content </div>
            </div>
            <div class="panel panel-success">
                <div class="panel-heading">Panel heading without title</div>
                <div class="panel-body">Panel content </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">Panel heading without title</div>
                <div class="panel-body">Panel content </div>
            </div>
            <div class="panel panel-warning">
                <div class="panel-heading">Panel heading without title</div>
                <div class="panel-body">Panel content </div>
            </div>
            <h1>带表格的面版</h1>
            <div class="panel panel-default">
                <!-- Default panel contents -->
                <div class="panel-heading">Panel heading</div>
                <div class="panel-body">
                    <p>Some default panel content here. Nulla vitae elit libero, a pharetra augue. Aenean lacinia bibendum nulla sed consectetur. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
                    <table class="table">
                        <thead>
                            <tr>
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>User Name</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>aehyok</td>
                                <td>leo</td>
                                <td>@aehyok</td>
                            </tr>
                            <tr>
                                <td>lynn</td>
                                <td>thl</td>
                                <td>@lynn</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <h1>带列表组的面版</h1>
            <div class="panel panel-default">
                <!-- Default panel contents -->
                <div class="panel-heading">Panel heading</div>
                <div class="panel-body">
                    <p>Some default panel content here. Nulla vitae elit libero, a pharetra augue. Aenean lacinia bibendum nulla sed consectetur. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
                </div>
                <ul class="list-group">
                    <li class="list-group-item">Cras justo odio</li>
                    <li class="list-group-item">Dapibus ac facilisis in</li>
                    <li class="list-group-item">Morbi leo risus</li>
                    <li class="list-group-item">Porta ac consectetur ac</li>
                    <li class="list-group-item">Vestibulum at eros</li>
                </ul>
            </div>
        </div>
    </form>
</body>
</html>
