<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zTree_example1.aspx.cs" Inherits="YanZhiwei.JavaScript.Utilities.jquery.zTree._3._5.zTree_example1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <link href="ztree_v3-master/css/ztreestyle/ztreestyle.css" rel="stylesheet" />
    <link href="ztree_v3-master/css/demo.css" rel="stylesheet" />
    <script src="../../jquery-1.9.1.js" type="text/javascript"></script>
    <script src="ztree_v3-master/js/jquery.ztree.core-3.5.js" type="text/javascript"></script>
    <script src="zTree_v3-master/js/jquery.ztree.excheck-3.5.min.js" type="text/javascript"></script>
    <script src="zTreeUtils.js" type="text/javascript"></script>

    <script>
        function getFont(treeId, node) {
            return node.font ? node.font : {};
        }
        $(document).ready(function () {
            var setting = {
                view: {
                    dblClickExpand: false,
                    fontCss: getFont,
                    nameIsHTML: true,

                },
                check: {
                    enable: true
                },
                callback: {
                    onCheck: onCheck
                },
                data: {
                    key: {
                        title: "tooltip"
                    }
                }
            };
            var zNodes = [
            {
                id: 1, name: "无右键菜单 1", open: true, noR: true, type: 'cab',
                children: [
                       { id: 11, name: "节点 1-1", noR: true, type: 'pole', chkDisabled: true, tooltip: "测试...\r\nhello wrold" },
                       { id: 12, name: "节点 1-2", noR: true, type: 'pole', font: { 'background-color': 'gray', 'color': 'white' } }
                ]
            },
            {
                id: 2, name: "右键操作 2", open: true, type: 'cab',
                children: [
                       { id: 21, name: "节点 2-1", type: 'pole' },
                       { id: 22, name: "节点 2-2", type: 'pole' },
                       { id: 23, name: "节点 2-3", type: 'pole' },
                       { id: 24, name: "节点 2-4", type: 'pole' }
                ]
            },
            {
                id: 3, name: "右键操作 3", open: true, type: 'cab',
                children: [
                       {
                           id: 31, name: "节点 3-1", type: 'pole',
                           children: [
                               {
                                   id: 35, name: "节点 3-1-1", type: 'lamp',
                                   children: [
                                          { id: 38, name: "节点 3-1-1-1", type: 'lamp' },
                                          { id: 39, name: "节点 3-1-1-2", type: 'lamp' }
                                   ]
                               },
                               { id: 36, name: "节点 3-2-2", type: 'lamp' },
                               { id: 37, name: "节点 3-2-3", type: 'lamp' },
                           ]
                       },
                       { id: 32, name: "节点 3-2", type: 'pole' },
                       { id: 33, name: "节点 3-3", type: 'pole' },
                       { id: 34, name: "节点 3-4", type: 'pole' }
                ]
            }
            ];

            $.fn.zTree.initBase($("#treeDemo"), setting, zNodes, true);

        });
        function onCheck(e, treeId, treeNode) {
            alert("onCheck " + treeNode.name);
        }
        function addMenu() {
            var _zTreeObj = $.fn.zTree.getZTreeObj('treeDemo');
            _zTreeObj.addMenu('rMenu');
        }
        function selectNode() {
            var _zTreeObj = $.fn.zTree.getZTreeObj('treeDemo');
            _zTreeObj.selectedNode('id', 31);
        }
        function updateIcon() {
            var _zTreeObj = $.fn.zTree.getZTreeObj('treeDemo');
            _zTreeObj.updateIcon('id', 31, 'zTree/3.5/cab_0_0_0.png');

        }
        function updateFont() {
            var _zTreeObj = $.fn.zTree.getZTreeObj('treeDemo');
            _zTreeObj.updateFont('id', 32, { 'background-color': 'black', 'color': 'white' });
        }
        function restoreFont() {

            var _zTreeObj = $.fn.zTree.getZTreeObj('treeDemo');
            _zTreeObj.restoreFont('id', 32);
        }
        function addMenuByNodeDemo() {
            var _zTreeObj = $.fn.zTree.getZTreeObj('treeDemo');
            _zTreeObj.addMenuByNode(function (node) {
                var _name = node.name;
                if (_name == '节点 3-4')
                    return "rMenu";
                else if (_name == '节点 2-1')
                    return "rMenu2";
                else
                    return '#';
            });
        }
        function recursiveDemo() {
            var _zTreeObj = $.fn.zTree.getZTreeObj('treeDemo');
            _zTreeObj.recursiveNode(null, function (node) {
                console.log(node.name);
                if (node.name == '右键操作 3')
                    return true;
                return false;
            })
            console.log("---------------------------");
            var _findedNode = _zTreeObj.getNodeByParam('name', '右键操作 3', null);
            _zTreeObj.recursiveNode(_findedNode, function (node) {
                console.log(node.name);
                return false;
            })

        }
        function testDemo() {
            alert('test');
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="content_wrap">
                <div class="zTreeDemoBackground left">
                    <ul id="treeDemo" class="ztree"></ul>
                </div>
            </div>
            <br />
            <input id="Button1" type="button" value="addMenu" onclick="addMenu()" />
            <input id="Button2" type="button" value="selectNode" onclick="selectNode()" />
            <input id="Button3" type="button" value="updateIcon" onclick="updateIcon()" />
            <input id="Button7" type="button" value="updateFont" onclick="updateFont()" />
            <input id="Button4" type="button" value="restoreFont" onclick="restoreFont()" />
            <input id="Button5" type="button" value="addMenuByNode" onclick="addMenuByNodeDemo()" />
            <input id="Button6" type="button" value="recursive" onclick="recursiveDemo()" />
            <div id="rMenu" style="visibility: hidden">
                <ul>
                    <li id="m_add" onclick="testDemo()">节点 3-4</li>
                    <li id="m_del" onclick="testDemo()">节点 3-4</li>
                    <li id="m_check" onclick="testDemo()">节点 3-4</li>
                    <li id="m_unCheck" onclick="testDemo()">节点 3-4</li>
                    <li id="m_reset" onclick="testDemo()">节点 3-4</li>
                </ul>
            </div>
            <div id="rMenu2" style="visibility: hidden">
                <ul>
                    <li id="m_add2" onclick="testDemo()">节点 1-1</li>
                    <li id="m_del2" onclick="testDemo()">节点 1-1</li>
                    <li id="m_check2" onclick="testDemo()">节点 1-1</li>
                    <li id="m_unCheck2" onclick="testDemo()">节点 1-1</li>
                    <li id="m_reset2" onclick="testDemo()">节点 1-1</li>
                </ul>
            </div>
        </div>
    </form>
</body>
</html>
