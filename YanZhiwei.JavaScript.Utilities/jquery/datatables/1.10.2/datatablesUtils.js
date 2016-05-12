/// <reference path="media/js/jquery.js" />
/// <reference path="media/js/jquery.dataTables.js" />

(function (jQuery) {
    $.fn.extend($.fn.dataTable(), {
        executePageQuery: function (columns, sAjaxSource, order, badRequest) {
            /// <summary>
            /// 分页查询处理
            /// </summary>
            /// <param name="columns" type="type">列名称</param>
            /// <param name="sAjaxSource" type="type">Ajax数据源</param>
            /// <param name="order" type="type">排序，默认第0列，desc</param>
            /// <param name="badRequest" type="type">发生错误的时候提示处理，默认alert</param>
            /// <returns type=""></returns>
            order = order || [[0, "desc"]];
            badRequest = badRequest || function badRequest(message) {
                alert(message);
            }
            var _table = $(this).dataTable({
                "bPaginate": true,
                "bLengthChange": false,
                "bFilter": false,
                "bSort": true,
                "bInfo": true,
                'bProcessing': true,
                'bServerSide': true,
                'sAjaxSource': sAjaxSource,
                "autoWidth": false,
                "bStateSave": true,
                "oLanguage": {
                    "sZeroRecords": "抱歉， 没有找到",
                    "sInfoEmpty": "表中数据为空",
                    "sZeroRecords": "没有检索到数据",
                    "sProcessing": "处理中...",
                    "sProcessing": "处理中...",
                    "sLengthMenu": "显示 _MENU_ 项结果",
                    "sZeroRecords": "没有匹配结果",
                    "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
                    "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
                    "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
                    "sInfoPostFix": "",
                    "sSearch": "搜索:",
                    "sUrl": "",
                    "sEmptyTable": "表中数据为空",
                    "sLoadingRecords": "载入中...",
                    "sInfoThousands": ",",
                    "oPaginate": {
                        "sFirst": "首页",
                        "sPrevious": "上页",
                        "sNext": "下页",
                        "sLast": "末页"
                    },
                    "oAria": {
                        "sSortAscending": ": 以升序排列此列",
                        "sSortDescending": ": 以降序排列此列"
                    }
                },
                "columns": columns,
                "order": order,
                "fnServerData": function (sSource, aoData, fnCallback) {
                    $.ajax({
                        "dataType": 'json',
                        "contentType": "application/json; charset=utf-8",
                        "type": "GET",
                        "url": sSource,
                        "data": aoData,
                        "success": function (msg) {
                            if (msg != null || msg != undefined) {
                                if (msg.StatusCode == 200) {
                                    var _json = msg.Message;
                                    fnCallback(_json);
                                    $(this).show();
                                }
                                else {
                                    fnCallback(msg.Message);
                                    badRequest(msg.Message.ExecuteMessage);
                                }
                            }
                        },
                        error: function (xhr, textStatus, error) {
                            if (typeof console == "object") {
                                console.log(xhr.status + "," + xhr.responseText + "," + textStatus + "," + error);
                            }
                        }
                    });
                }
            });
            return _table;
        },
        executeQuery: function (columns, order, pagelength) {
            /// <summary>
            /// 初始化
            /// </summary>
            /// <param name="columns">列名称</param>
            /// <param name="order">排序，默认第0列，desc</param>
            /// <param name="pagelength">每页展现数据，默认10行</param>
            /// <returns type=""></returns>

            order = order || [[0, "desc"]];
            pagelength = pagelength || 10;
            // var _columns = [
            //{ "data": "time", "title": "时间", "class": "center", "width": "20%" },
            //{ "data": "log", "title": "日志", "class": "center", "width": "70%" },
            //{ "data": "status", "title": "状态", "class": "center", "width": "10%" }
            // ];
            var _table = $(this).dataTable({
                language: {
                    "sProcessing": "处理中...",
                    "sLengthMenu": "显示 _MENU_ 项结果",
                    "sZeroRecords": "没有匹配结果",
                    "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
                    "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
                    "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
                    "sInfoPostFix": "",
                    "sSearch": "搜索:",
                    "sUrl": "",
                    "sEmptyTable": "表中数据为空",
                    "sLoadingRecords": "载入中...",
                    "sInfoThousands": ",",
                    "oPaginate": {
                        "sFirst": "首页",
                        "sPrevious": "上页",
                        "sNext": "下页",
                        "sLast": "末页"
                    },
                    "oAria": {
                        "sSortAscending": ": 以升序排列此列",
                        "sSortDescending": ": 以降序排列此列"
                    }
                },
                "autoWidth": false,
                "bStateSave": true,
                "columns": columns,
                "order": order,
                "lengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "全部"]
                ],
                "pageLength": pagelength
            });
            return _table;
        },
        addJson: function (json) {
            /// <summary>
            /// 添加json数据源
            /// </summary>
            /// <param name="json">json对象或者json字符串</param>
            var _jsonObj = '';
            if (jsUtils.isString(json)) {
                _jsonObj = $.parseJSON(json);
            }
            else if (jsUtils.isArray(json)) {
                _jsonObj = json;
            }
            else if (jsUtils.isObject(json)) {
                _jsonObj = json;
            }
            if (!jsUtils.string.isNullOrEmpty(_jsonObj)) {
                this.dataTable().fnAddData(_jsonObj);
                this.DataTable().page('first').draw('page');//每次绑定数据后，强制设置选中第一页数据；
            }
        },
        hightSingleRow: function () {
            /// <summary>
            /// 高亮选中行并只能选中一行
            /// </summary>
            /*
            *高亮css代码：
            * table.dataTable tbody tr.selected {
              background-color: #B0BED9;
            }
            */
            var _id = $(this).attr('id');
            var _table = $('#' + _id).DataTable();
            $('#' + _id + ' tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    _table.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });
        },
        hightMutilRow: function () {
            /// <summary>
            /// 高亮选中行并可以选择多行
            /// </summary>

            /*
            *高亮css代码：
            * table.dataTable tbody tr.selected {
              background-color: #B0BED9;
            }
            */
            var _id = $(this).attr('id');
            var _table = $('#' + _id).DataTable();
            $('#' + _id + ' tbody').on('click', 'tr', function () {
                $(this).toggleClass('selected');
            });
        },
        getAllRowsToArray: function () {
            /// <summary>
            /// 将获取行数据转换为array
            /// </summary>
            /// <returns type="">Array</returns>
            var _array = new Array();
            var _allrows = $(this).DataTable().rows().data();
            if (_allrows != null && _allrows.length > 0) {
                for (var i = 0; i < _allrows.length; i++) {
                    _array.push(_allrows[i]);
                }
            }
            return _array;
        },
        getSelectedRowsData: function () {
            /// <summary>
            /// 获取选中行数据，若要函数生效可用，请先调用
            /// hightSingleRow()或hightMutilRow()函数；
            /// </summary>

            var _id = $(this).attr('id');
            var _table = $('#' + _id).DataTable();
            var _data = _table.rows('.selected').data();
            return _data;
        },
        getSelectedRowIndex: function () {
            /// <summary>
            /// 获取选中行索引，若要函数生效可用，请先调用
            /// hightSingleRow()或hightMutilRow()函数；
            /// </summary>

            var _id = $(this).attr('id');
            var _table = $('#' + _id).DataTable();
            var _index = _table.row('.selected').index();
            return _index;
        },
        updateRowByIndex: function (rowIndex, rowData) {
            /// <summary>
            /// 根据行索引更新行数据
            /// </summary>
            /// <param name="rowIndex">行索引</param>
            /// <param name="rowData">新的行数据</param>
            $(this).DataTable().row(rowIndex).data(rowData).draw();
        },
        deleteRowByIndex: function (rowIndex) {
            /// <summary>
            /// 根据行索引删除行
            /// </summary>
            /// <param name="rowIndex"></param>
            $(this).DataTable().row(rowIndex).remove().draw(false);
        },
        clearAllRows: function () {
            /// <summary>
            /// 删除所有行
            /// </summary>
            $(this).DataTable().clear().draw();
        },
        getAllRows: function () {
            /// <summary>
            /// 获取所有行
            /// </summary>
            return $(this).DataTable().rows().data();
        },
        getRowCount: function (id) {
            /// <summary>
            /// 获取行总数
            /// </summary>
            var _allRowsData = $(this).getAllRows();
            if (jsUtils.isObject(_allRowsData))
                return _allRowsData.length;
            else
                return -1;
        },
        rowClickEvent: function (hanlder) {
            /// <summary>
            /// jquery Datatable 行点击事件
            /// </summary>
            /// <param name="hanlder">方法委托，参数：行数据</param>
            var _id = $(this).attr('id');
            $('#' + _id + ' tbody').on('click', 'tr', function () {
                var _table = $('#' + _id).DataTable();
                hanlder(_table.row(this).data());
            });
        },
        applySyncCheckstate: function (parentCkId) {
            /// <summary>
            /// 同步checkbox 选中状态
            /// </summary>
            /// <param name="parentCkId">主checkbox id</param>
            var _id = $(this).attr('id');
            $('#' + parentCkId).on('click', function () {
                var _table = $('#' + _id).DataTable();
                var rows = _table.rows({ 'search': 'applied' }).nodes();
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
            });

            $('#' + _id + ' tbody').on('change', 'input[type="checkbox"]', function () {
                if (!this.checked) {
                    var el = $('#' + parentCkId).get(0);
                    if (el && el.checked && ('indeterminate' in el)) {
                        el.indeterminate = true;
                    }
                }
            });
        },
        updateSettings: function (option) {
            /// <summary>
            /// 更新参数
            /// </summary>
            /// <param name="option"></param>

            //var oTable = $('#tableCabChTotalView').dataTable();
            //_visible = !_visible;
            //oTable.updateSettings({
            //    "bRetrieve": true,
            //    "bProcessing": true,
            //    "autoWidth": false,
            //    "bLengthChange": _visible,
            //    "bStateSave": true,
            //    "bFilter": _visible,
            //    "bInfo": true,
            //    "columns": [
            //           { "data": "CabId", "title": "箱的名称", "class": "center", "visible": false },
            //           { "data": "CabName", "title": "箱的名称", "class": "center" },
            //           { "data": "ChTotalCount", "title": "回路总数", "class": "center", "visible": false },
            //            { "data": "TheoryState", "title": "复核状态", "class": "center" },
            //            { "data": "ReviewTime", "title": "复核时间", "class": "center" },
            //           { "data": "CabChStatusBase64", "title": "回路状态", "class": "center" },
            //           { "data": "ChStatusLastTime", "title": "回路更新时间", "class": "center" },
            //           { "data": "IncomeAvgVoltage", "title": "进线平均电压", "class": "center", "visible": true },
            //           { "data": "IncomeSumCurrent", "title": "进线合计电流", "class": "center", "visible": true },
            //           { "data": "IncomeSumPower", "title": "进线合计功率", "class": "center", "visible": false },
            //           { "data": "IncomeAvgPowerFactor", "title": "进线平均功率因子", "class": "center", "visible": false },
            //           { "data": "IncomeSumPowerFactor", "title": "进线合计功率因子", "class": "center", "visible": false },
            //           { "data": "OutcomeAvgVoltage", "title": "出线平均电压", "class": "center", "visible": true },
            //           { "data": "OutcomeSumCurrent", "title": "出线合计电流", "class": "center", "visible": true },
            //           {
            //               "title": "进出线详细",
            //               "className": 'details-control',
            //               "orderable": false,
            //               "data": null,
            //               "defaultContent": ''
            //           },
            //           { "data": "OutcomeSumPower", "title": "出线合计功率", "class": "center", "visible": false },
            //           { "data": "InLineLastUpdate", "title": "进线更新时间", "class": "center", "visible": false },
            //           { "data": "OutLineLastUpdate", "title": "出线更新时间", "class": "center", "visible": false },
            //           { "data": "OutcomeAvgPowerFactor", "title": "出线平均功率因子", "class": "center", "visible": false },
            //           { "data": "OutcomeSumPowerFactor", "title": "出线合计功率因子", "class": "center", "visible": false },
            //           { "data": "Region", "title": "隶属区域", "class": "center", "visible": false },
            //           { "data": "InComeElec", "title": "InComeElec", "class": "center", "visible": false },
            //            { "data": "OutComeElec", "title": "OutComeElec", "class": "center", "visible": false }
            //    ],
            //    "lengthMenu": [
            //        [5, 15, 20, -1],
            //        [5, 15, 20, "全部"]
            //    ],
            //    "pageLength": 10,
            //    "pagingType": "bootstrap_full_number",
            //    "columnDefs": [{
            //        'orderable': false,
            //        'targets': [1]
            //    }, {
            //        "searchable": false,
            //        "targets": [1]
            //    }],
            //    "order": [
            //        [1, "desc"]
            //    ],
            //    "fnRowCallback": function (row, data, displayIndex, displayIndexFull) {
            //        var _imgTag = '<img src="data:image/png;base64,' + data.CabChStatusBase64 + '" title="回路状态"/>';
            //        $('td:eq(3)', row).html(_imgTag);
            //        return row;
            //    }
            //});
            var _table, _otable, _selector = this.selector;//datatables Id
            if ($.fn.dataTable.isDataTable(_selector)) {
                _table = $(_selector).DataTable(),
                _otable = $(_selector).dataTable();
                var _allrows = _otable.fnGetData();
                _otable.fnClearTable();
                _table.destroy();

                var _language = {
                    "language": {
                        "sProcessing": "处理中...",
                        "sLengthMenu": "显示 _MENU_ 项结果",
                        "sZeroRecords": "没有匹配结果",
                        "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
                        "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
                        "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
                        "sInfoPostFix": "",
                        "sSearch": "搜索:",
                        "sUrl": "",
                        "sEmptyTable": "表中数据为空",
                        "sLoadingRecords": "载入中...",
                        "sInfoThousands": ",",
                        "oPaginate": {
                            "sFirst": "首页",
                            "sPrevious": "上页",
                            "sNext": "下页",
                            "sLast": "末页"
                        },
                        "oAria": {
                            "sSortAscending": ": 以升序排列此列",
                            "sSortDescending": ": 以降序排列此列"
                        }
                    }
                }
                option = $.extend(_language, option);
                _table = $(_selector).DataTable(option);
                _otable.fnAddData(_allrows);
            }
        },
        foreach: function (callback) {
            /// <summary>
            /// 遍历行
            /// </summary>
            /// <param name="callback">回调函数；参数 rowData</param>
            var _id = $(this).attr('id');
            var _table = $('#' + _id).DataTable();
            _table.rows().flatten().each(function (idx, i) {
                var _data = _table.row(idx).data();
                callback(_data);
            });
        },
        getRowByParam: function (key, value) {
            /// <summary>
            /// 根据行数据的属性搜索
            /// </summary>
            /// <param name="key">需要精确匹配的属性名称</param>
            /// <param name="value">需要精确匹配的属性值，可以是任何类型，只要保证与 key 指定的属性值保持一致即可</param>
            var _id = $(this).attr('id'),
                _table = $('#' + _id).DataTable(),
                _rows = new Object();
            _table.rows().flatten().each(function (idx, i) {
                var _data = _table.row(idx).data();
                if (_data[key] === value) {
                    _rows = _table.row(idx);
                    return false;
                }
            });
            return _rows;
        },
        deleteRowByParam: function (key, value) {
            /// <summary>
            /// 根据键值删除行数据
            /// </summary>
            /// <param name="key" type="type">需要精确匹配的属性名称</param>
            /// <param name="value" type="type">需要精确匹配的属性值，可以是任何类型，只要保证与 key 指定的属性值保持一致即可</param>
            var _id = $(this).attr('id'),
            _table = $('#' + _id).DataTable();
            _table.rows().flatten().each(function (idx, i) {
                var _data = _table.row(idx).data();
                if (_data != undefined && _data[key] === value) {
                    var _row = _table.row(idx);
                    _table.row(_row).remove().draw(false);
                    //  return false;//跳出循环
                }
            });
        },
        getRowIndexByParam: function (key, value) {
            /// <summary>
            /// 根据行数据的属性搜索索引
            /// </summary>
            /// <param name="key">需要精确匹配的属性名称</param>
            /// <param name="value">需要精确匹配的属性值，可以是任何类型，只要保证与 key 指定的属性值保持一致即可</param>
            var _id = $(this).attr('id'),
                _table = $('#' + _id).DataTable(),
                _rowIndex = -1;
            var _allDatas = _table.rows().data();
            for (var i = 0; i < _allDatas.length; i++) {
                var _data = _allDatas[i];
                if (_data[key] === value) {
                    _rowIndex = i;
                    break;
                }
            }
            return _rowIndex;
        }
    });
})(jQuery);