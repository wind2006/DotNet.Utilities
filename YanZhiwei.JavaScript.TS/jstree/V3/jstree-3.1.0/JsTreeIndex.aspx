<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JsTreeIndex.aspx.cs" Inherits="YanZhiwei.JavaScript.Learn.jstree.V3.jstree_3._1._0.JsTreeIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>jsTree</title>
    <style>
        html {
            margin: 0;
            padding: 0;
            font-size: 62.5%;
        }

        body {
            max-width: 800px;
            min-width: 300px;
            margin: 0 auto;
            padding: 20px 10px;
            font-size: 14px;
            font-size: 1.4em;
        }

        h1 {
            font-size: 1.8em;
        }

        .demo {
            overflow: auto;
            border: 1px solid silver;
            min-height: 100px;
        }
    </style>
    <link href="jstree/dist/themes/default/style.css" rel="stylesheet" />
    <script src="jstree/dist/jquery.min.js" type="text/javascript"></script>
    <script src="jstree/dist/jstree.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // html demo
            $('#html').jstree();

            // inline data demo
            $('#data').jstree({
                'core': {
                    'data': [
                        {
                            "text": "Root node", "icon": "../../icon/chrome.png", "children": [
                                  { "text": "Child node 1", "icon": "../../icon/chrome.png" },
                                  { "text": "Child node 2", "icon": "../../icon/chrome.png" }
                            ]
                        }
                    ]
                }
            });

            // data format demo
            $('#frmt').jstree({
                'core': {
                    'data': [
                        {
                            "text": "Root node",
                            "state": { "opened": true },
                            "children": [
                                {
                                    "text": "Child node 1",
                                    "state": { "selected": true },
                                    "icon": "jstree-file"
                                },
                                { "text": "Child node 2", "state": { "disabled": true } }
                            ]
                        }
                    ]
                }
            });

            // ajax demo 1
            $('#ajax').jstree({
                'core': {
                    'data': {
                        "url": "../../../BackHandler/BaseHandler.ashx?action=initJsTree",
                        "dataType": "json" // needed only if you do not supply JSON headers
                    }
                }
            });



            // lazy demo
            $('#lazy').jstree({
                'core': {
                    'data': {
                        "url": "//www.jstree.com/fiddle/?lazy",
                        "data": function (node) {
                            return { "id": node.id };
                        }
                    }
                }
            });
            // lazy demo 2
            $('#lazy2').jstree({
                'core': {
                    'data': {
                        "url": "../../../BackHandler/BaseHandler.ashx?action=optJsTree",
                        "data": function (node) {
                            return { "parentId": node.id };
                        }
                    }
                }
            });
            // data from callback
            $('#clbk').jstree({
                'core': {
                    'data': function (node, cb) {
                        if (node.id === "#") {
                            cb([{ "text": "Root", "id": "1", "children": true }]);
                        }
                        else {
                            cb(["Child"]);
                        }
                    }
                }
            });

            // interaction and events
            $('#evts_button').on("click", function () {
                var instance = $('#evts').jstree(true);
                instance.deselect_all();
                instance.select_node('1');
            });
            $('#evts')
                .on("changed.jstree", function (e, data) {
                    if (data.selected.length) {
                        alert('The selected node is: ' + data.instance.get_node(data.selected[0]).text);
                    }
                })
                .jstree({
                    'core': {
                        'multiple': false,
                        'data': [
                            {
                                "text": "Root node", "children": [
                                      { "text": "Child node 1", "id": 1 },
                                      { "text": "Child node 2" }
                                ]
                            }
                        ]
                    }
                });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <h1>HTML demo</h1>
        <div id="html" class="demo">
            <ul>
                <li data-jstree='{ "opened" : true }'>Root node
			
                    <ul>
                        <li data-jstree='{ "selected" : true }'>Child node 1</li>
                        <li>Child node 2</li>
                    </ul>
                </li>
            </ul>
        </div>
        <h1>Inline data demo</h1>
        <div id="data" class="demo"></div>

        <h1>Data format demo</h1>
        <div id="frmt" class="demo"></div>

        <h1>AJAX demo1</h1>
        <div id="ajax" class="demo"></div>
        <h1>Lazy loading demo1</h1>
        <div id="lazy2" class="demo"></div>
        <h1>Lazy loading demo2</h1>
        <div id="lazy" class="demo"></div>

        <h1>Callback function data demo</h1>
        <div id="clbk" class="demo"></div>
        <h1>Interaction and events demo</h1>
        <button id="evts_button">select node with id 1</button>
        <em>either click the button or a node in the tree</em>
        <div id="evts" class="demo"></div>
    </form>
</body>
</html>
