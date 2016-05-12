<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormTable.aspx.cs" Inherits="YanZhiwei.JavaScript.Learn.Bootstrap.V3.FormTable" %>

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

            <h1>Table</h1>
            <h4>创建响应式表格<small> 将任何.table包裹在.table-responsive中即可创建响应式表格，其会在小屏幕设备上（小于768px）水平滚动。当屏幕大于768px宽度时，水平滚动条消失。</small></h4>
            <h4>紧缩表格<small> 利用.table-condensed可以让表格更加紧凑，单元格中的内部（padding）均会减半。 </small></h4>
            <div class="table-responsive">
                <table class="table table-hover table-bordered table-condensed">
                    <caption>基本表格</caption>
                    <thead>
                        <tr>
                            <th>名称</th>
                            <th>年龄</th>
                            <th>备注</th>
                        </tr>
                    </thead>
                    <tr class="active">
                        <td>张三</td>
                        <td>18</td>
                        <td>上海</td>
                    </tr>
                    <tr class="warning">
                        <td>李四</td>
                        <td>28</td>
                        <td>北京</td>
                    </tr>
                    <tr class="danger">
                        <td>王五</td>
                        <td>38</td>
                        <td>广州</td>
                    </tr>
                    <tr class="success">
                        <td>赵六</td>
                        <td>48</td>
                        <td>厦门</td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
