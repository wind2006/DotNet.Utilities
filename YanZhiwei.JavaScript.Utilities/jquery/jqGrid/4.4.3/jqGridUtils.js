/// <reference path="jquery.jqGrid-4.4.3/js/jquery.jqGrid.src.js" />
$.jgrid.extend({
    initBase: function (height, width, colNames, colModel, caption) {
        /// <summary>
        /// 初始化基本的本地数据jqGird
        /// </summary>
        /// <param name="height">高度</param>
        /// <param name="width">宽度</param>
        /// <param name="colNames">列名称集合</param>
        /// <param name="colModel">列名称绑定集合</param>
        /// <param name="caption">标题</param>
        var jGrid = $(this);
        jGrid.jqGrid({
            datatype: "json",
            height: height,
            width: width,
            colNames: colNames,/*列名称*/
            colModel: colModel,
            multiselect: false,
            caption: caption
        });
    },
    blinkRow: function (rowId, blinks, changeColor) {
        /// <summary>
        /// 闪烁行
        /// </summary>
        /// <param name="rowId">行索引</param>
        /// <param name="blinks">闪烁次数</param>
        /// <param name="changeColor">闪烁颜色</param>
        var jGrid = $(this);
        var delay = 500;
        var blinkCnt = 0;
        var curr = false;
        var rr = setInterval(function () {
            var color;
            if (curr === false) {
                color = changeColor;
                curr = color;
            } else {
                color = '';
                curr = false;
            }
            jGrid.setRowData(rowId, false, { background: color });
            if (blinkCnt >= blinks * 2) {
                blinkCnt = 0;
                clearInterval(rr);
                jGrid.setRowData(rowId, false, { background: '' });
            } else {
                blinkCnt++;
            }
        }, delay);
    },
    updateBlinkRow: function (rowId, data, blinks, changeColor) {
        /// <summary>
        /// 更新行，并闪烁
        /// <para>参考：http://blog.valqk.com/archives/jqGrid-update-row-and-blink-highlight-it-58.html </para>
        /// </summary>
        /// <param name="rowId">行索引</param>
        /// <param name="data">行数据</param>
        /// <param name="blinks">闪烁次数</param>
        /// <param name="changeColor">闪烁颜色</param>
        var jGrid = $(this);
        jGrid.setRowData(rowId, data);
        var delay = 500;
        var blinkCnt = 0;
        var curr = false;
        var rr = setInterval(function () {
            var color;
            if (curr === false) {
                color = changeColor;
                curr = color;
            } else {
                color = '';
                curr = false;
            }
            jGrid.setRowData(rowId, false, { background: color });
            if (blinkCnt >= blinks * 2) {
                blinkCnt = 0;
                clearInterval(rr);
                jGrid.setRowData(rowId, false, { background: '' });
            } else {
                blinkCnt++;
            }
        }, delay);
    },
    addList: function (jsondb) {
        /// <summary>
        /// 添加行数据集合
        /// </summary>
        /// <param name="jsondb">json数据</param>
        var jGrid = $(this);
        jGrid.setGridParam({ data: jsondb }).trigger('reloadGrid');
    },
    highlightRow: function (rowIndex, color) {
        /// <summary>
        /// 为行增加highlight效果，默认2000毫米
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="color">颜色</param>
        var jGrid = $(this);
        var id = jGrid.selector;
        jQuery("#" + rowIndex, id).effect("highlight", { color: color }, 2000);
    },
    getRowDataByColNameValue: function (colName, colValue) {
        /// <summary>
        /// 根据列名称以及列值查找行数据
        /// </summary>
        /// <param name="colName">列名称</param>
        /// <param name="colValue">列值</param>
        /// <returns type="">行数据</returns>
        var jGrid = $(this);
        var data = jGrid.getRowData();
        if (data) {
            for (var i = 0; i < data.length; i++) {
                var rowData = data[i];
                if (rowData.hasOwnProperty(colName) && rowData[colName] == colValue) {
                    return rowData;
                }
            }
        }
    },
    getRowIndex: function (colName, colValue) {
        /// <summary>
        /// 根据列名称以及列值查找所在行
        /// </summary>
        /// <param name="colName">列名称</param>
        /// <param name="colValue">列值</param>
        /// <returns type="">所在行，若没有找到则返回-1</returns>
        var jGrid = $(this);
        var ids = jGrid.getDataIDs();
        for (var i = 0; i < ids.length; i++) {
            var rowId = ids[i];
            var rowData = jGrid.getRowData(rowId);
            if (rowData.hasOwnProperty(colName) && rowData[colName] == colValue) {
                return rowId;
            }
        }
        return -1;
    },
    getCellIndex: function (cellName) {
        /// <summary>
        /// 根据列名称获取列索引
        /// </summary>
        /// <param name="cellName">列名称</param>
        /// <returns type="">列名称对应的索引，若没找到则返回-1</returns>
        var jGrid = $(this);
        var cells = jGrid.getGridParam('colModel');
        if (cells) {
            for (var i = 0; i < cells.length; i++) {
                var cell = cells[i];
                if (cell.name == cellName) {
                    return i;
                }
            }
        }
        return -1;
    },
    blinkCell: function (rowIndex, cellIndex, blinks, changeColor) {
        /// <summary>
        /// 闪烁单元格
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="cellIndex">列索引</param>
        /// <param name="blinks">闪烁次数</param>
        /// <param name="changeColor">闪烁颜色</param>
        var jGrid = $(this);
        var delay = 500;
        var blinkCnt = 0;
        var curr = false;
        var rr = setInterval(function () {
            var color;
            if (curr === false) {
                color = changeColor;
                curr = color;
            } else {
                color = '';
                curr = false;
            }
            jGrid.setCell(rowIndex, cellIndex, '', { background: color });
            if (blinkCnt >= blinks * 2) {
                blinkCnt = 0;
                clearInterval(rr);
                jGrid.setCell(rowIndex, cellIndex, '', { background: '' });
            } else {
                blinkCnt++;
            }
        }, delay);
    },
    clearAllRows: function () {
        /// <summary>
        /// 删除所有行
        /// </summary>
        var jGrid = $(this);
        var rowIds = jGrid.jqGrid('getDataIDs');
        for (var i = 0, len = rowIds.length; i < len; i++) {
            var currRow = rowIds[i];
            jGrid.jqGrid('delRowData', currRow);
        }
    }
});