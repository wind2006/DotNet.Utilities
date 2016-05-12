/// <reference path="zTree_v3-master/js/jquery-1.4.4.min.js" />
/// <reference path="zTree_v3-master/js/jquery.zTree.all-3.5.js" />

(function (jQuery) {
    function recursiveNodes(node, callback) {
        if (node.children && node.children.length > 0) {
            for (var j = 0; j < node.children.length; j++) {
                var _findedNode = node.children[j];
                if (callback(_findedNode)) {
                    break;
                }
                recursiveNodes(_findedNode, callback);
            }
        }
    }
    $.fn.zTree.initBase = function (obj, zSetting, zNodes, extend) {
        /// <summary>
        /// Ztree初始化，加载扩展函数
        /// </summary>
        /// <param name="obj">obj</param>
        /// <param name="zSetting">zSetting</param>
        /// <param name="zNodes">zNodes</param>
        /// <param name="extend">是否加载扩展函数;true or false</param>
        $.fn.zTree.init(obj, zSetting, zNodes);
        if (arguments.length == 4 && extend == true) {
            /*
            *扩展方法
            */
            $.fn.extend($.fn.zTree._z.data.getZTreeTools(obj.attr('id')), {
                addMenu: function (menuId) {
                    /// <summary>
                    /// 附加右键菜单
                    /// </summary>
                    /// <param name="menuId"></param>
                    //<div id="rMenu">
                    //    <ul>
                    //        <li id="m_add" onclick="addTreeNode();">增加节点</li>
                    //        <li id="m_del" onclick="removeTreeNode();">删除节点</li>
                    //        <li id="m_check" onclick="checkTreeNode(true);">Check节点</li>
                    //        <li id="m_unCheck" onclick="checkTreeNode(false);">unCheck节点</li>
                    //        <li id="m_reset" onclick="resetTree();">恢复zTree</li>
                    //    </ul>
                    //</div>
                    var _rMenu = $("#" + menuId);
                    if (_rMenu) {
                        _rMenu.css({
                            "position": "absolute",
                            "visibility": "hidden",
                            "top": 0,
                            "background-color": "#555",
                            "text-align": "left",
                            "padding": "2px"
                        });
                        _rMenu.find('ul').css({
                            "margin": "1px 0",
                            "padding": "0 5px",
                            "cursor": "pointer",
                            "background-color": " #DFDFDF",
                            "list-style": "none outside none"
                        });
                        this.setting.callback.onRightClick = function (event, treeId, treeNode) {
                            if (treeNode && !treeNode.noR) {
                                this.getZTreeObj(treeId).selectNode(treeNode);
                                _rMenu.find('ul').show();
                                var x = event.clientX, y = event.clientY;
                                _rMenu.css({ "top": y + "px", "left": x + "px", "visibility": "visible" });
                            }
                        }
                        $("body").bind("mousedown", function (event) {
                            if (!(event.target.id == menuId || $(event.target).parents("#" + menuId).length > 0)) {
                                _rMenu.css({ "visibility": "hidden" });
                            }
                        });
                    }
                },
                addMenuByNode: function (callback) {
                    var menus = new Array;//存储右键菜单Id
                    this.setting.callback.onRightClick = function (event, treeId, treeNode) {
                        if (treeNode && !treeNode.noR) {
                            var menuId = callback(treeNode);
                            if (menuId != '#') {
                                menus.push(menuId);
                                var _rMenu = $("#" + menuId);
                                _rMenu.css({
                                    "position": "absolute",
                                    "visibility": "hidden",
                                    "top": 0,
                                    "background-color": "#555",
                                    "text-align": "left",
                                    "padding": "2px"
                                });
                                _rMenu.find('ul').css({
                                    "margin": "1px 0",
                                    "padding": "0 5px",
                                    "cursor": "pointer",
                                    "background-color": " #DFDFDF",
                                    "list-style": "none outside none"
                                });
                                this.getZTreeObj(treeId).selectNode(treeNode);
                                _rMenu.find('ul').show();
                                var x = event.clientX, y = event.clientY;
                                _rMenu.css({ "top": y + "px", "left": x + "px", "visibility": "visible" });
                            }
                        }
                    }
                    $("body").bind("mousedown", function (event) {
                        for (var i = 0; i < menus.length; i++) {
                            var _menuId = menus[i];
                            if (!(event.target.id == _menuId || $(event.target).parents("#" + _menuId).length > 0)) {
                                var _rMenu = $("#" + _menuId);
                                _rMenu.css({ "visibility": "hidden" });
                            }
                        }
                    });
                },
                selectedNode: function (filedId, fieldValue, expand) {
                    /// <summary>
                    /// 设置节点选中，并且触发onclick事件
                    /// </summary>
                    /// <param name="filedId">字段名称</param>
                    /// <param name="fieldValue">字段数值</param>
                    /// <param name="expand">是否展开</param>
                    expand = expand || true;
                    var _findedNode = this.getNodesByParam(filedId, fieldValue, null);
                    if (_findedNode != null) {
                        this.selectNode(_findedNode[0]);
                        if (expand == true)
                            this.expandNode(_findedNode[0], false, true);
                        $('#' + _findedNode[0].tId + '_a').trigger('click');
                    }
                },
                updateIcon: function (fieldId, fieldValue, icon) {
                    /// <summary>
                    /// 更新节点图标
                    /// </summary>
                    /// <param name="fieldId">更新依据字段名称</param>
                    /// <param name="fieldValue">更新依据字段数值</param>
                    /// <param name="icon">图标路径</param>
                    var _findedNode = this.getNodesByParam(fieldId, fieldValue, null);
                    if (_findedNode.length > 0) {
                        var _actualNode = _findedNode[0];
                        _actualNode.icon = window.location.protocol + "//" + window.location.host + "//" + icon;
                        this.updateNode(_actualNode);
                    }
                },
                updateFont: function (fieldId, fieldValue, fontCss) {
                    /// <summary>
                    /// 更新节点字体
                    ///view: {
                    /// fontCss: getFont,
                    /// nameIsHTML: true
                    ///},
                    /// </summary>
                    /// <param name="fieldId">更新依据字段名称</param>
                    /// <param name="fieldValue">更新依据字段数值</param>
                    /// <param name="fontCss">字体样式</param>
                    var _findedNode = this.getNodesByParam(fieldId, fieldValue, null);
                    if (_findedNode.length > 0) {
                        var _actualNode = _findedNode[0];
                        _actualNode.font = fontCss;
                        this.updateNode(_actualNode);
                    }
                },
                restoreFont: function (fieldId, fieldValue) {
                    /// <summary>
                    /// 还原节点默认样式
                    /// </summary>
                    /// <param name="fieldId" type="type"></param>
                    /// <param name="fieldValue" type="type"></param>
                    var _findedNode = this.getNodesByParam(fieldId, fieldValue, null);
                    if (_findedNode.length > 0) {
                        var _actualNode = _findedNode[0];
                        _actualNode.font = { 'background-color': 'transparent', 'color': 'black' };
                        this.updateNode(_actualNode);
                    }
                },
                recursiveNode: function (node, callback) {
                    /// <summary>
                    /// 递归节点
                    /// </summary>
                    /// <param name="node">需要开始递归的节点，若为NULL，则直接递归整棵树；</param>
                    /// <param name="callback">回调处理，参数node，若返回true，则挑出递归;</param>
                    if (node == null) {
                        var _parentNodes = this.getNodes();
                        for (var i = 0; i < _parentNodes.length; i++) {
                            var _nodes = _parentNodes[i];
                            if (callback(_nodes))
                                break;
                            recursiveNodes(_nodes, callback);
                        }
                    }
                    else {
                        var _findedNode = node;
                        if (_findedNode.children) {
                            for (var i = 0; i < _findedNode.children.length; i++) {
                                var _childNode = _findedNode.children[i];
                                recursiveNodes(_childNode, callback);
                            }
                        }
                    }
                }
            });
        }
    }
})(jQuery);