/// <reference path="../jquery-1.6.4.js" />

(function (window) {
    jQuery.fn.extend({
        disable: function (state) {
            return this.each(function () {
                this.disabled = state;
            });
        },
        attrs: function () {
            /// <summary>
            /// 获得所有属性
            /// </summary>
            var _attributes = {};
            $.each(this.get(0).attributes, function (i, attrib) {
                _attributes[attrib.name] = attrib.value;
            });
            return _attributes;
        }
    });
    jqUtils = {}
    jqUtils.UISelect = {
        getText: function (id, hanlder) {
            /// <summary>
            /// 获取html select 选中文本
            /// </summary>
            /// <param name="id"></param>
            var _length = arguments.length;
            var _text = $("#" + id).find("option:selected").text();
            if (_length == 1) {
                return _text;
            }
            if (_length == 2) {
                return hanlder(_text);
            }
        },
        setOptionByText: function (id, filterText, fireEvent) {
            /// <summary>
            /// 根据文本选中option
            /// </summary>
            /// <param name="id">Id</param>
            /// <param name="filterText">需要选中的文本</param>
            /// <param name="fireEvent">是否触发change事件；布尔类型</param>
            var _argument = arguments.length;
            if (_argument == 2)
                $("#" + id + " option:contains(" + filterText + ")").attr('selected', 'selected');
            if (_argument == 3 && fireEvent == true)
                $("#" + id + " option:contains(" + filterText + ")").attr('selected', 'selected').trigger('change');
        }
    }
    jqUtils.UICheckbox = {
        isChecked: function (id) {
            /// <summary>
            /// 获取html checkbox是否选中
            /// </summary>
            /// <param name="id">若选中则返回true;否则则返回false</param>
            return $("#" + id).attr("checked") == 'checked';
        },
        getAllStatus: function (array) {
            /// <summary>
            /// 获取一组CheckedBox选中状态
            /// 若选中则返回1,否则返回0
            /// </summary>
            /// <param name="array">eg:var _array=new Array();_array.push('gp1Ck1');_array.push('gp1Ck2');_array.push('gp1Ck3')</param>
            var _statusArray = new Array();
            for (var i = 0; i < array.length; i++) {
                var _ckId = array[i];
                _statusArray.push(jqUtils.UICheckbox.isChecked(_ckId) == true ? 1 : 0);
            }
            return _statusArray;
        },
        getAllStatusByPrefix: function (prefix, count) {
            /// <summary>
            /// 获取一组CheckedBox选中状态
            /// 若选中则返回1,否则返回0
            /// </summary>
            /// <param name="prefix">ckeckbox前缀，譬如gp1ck</param>
            /// <param name="count">需要获取checkbox状态数量</param>
            var _ckGroup = new Array();
            for (var i = 1; i <= count; i++) {
                _ckGroup.push(prefix + i);
            }

            return jqUtils.UICheckbox.getAllStatus(_ckGroup);
        }
    }
    jqUtils.UIRadio = {
        getValueByName: function (name) {
            /// <summary>
            /// 根据Name获取一组Radio中选中值
            /// </summary>
            /// <param name="name"></param>
            var _radioGroup = $('input[name=' + name + ']');
            var _checkedValue = _radioGroup.filter(':checked').val();
            return _checkedValue;
        }
    }
    jqUtils.UITable = {
        getObj: function (id) {
            /// <summary>
            /// 获取Table对象
            /// </summary>
            /// <param name="id">Table ID</param>
            var _table = $('#' + id + ' > tbody > tr').map(function () {
                return $(this).children().map(function () {
                    return $(this);
                });
            });
            return _table;
        },
        setCellValue: function (id, rowIndex, cellIndex, cellValue) {
            /// <summary>
            /// 设置单元格的值
            /// </summary>
            /// <param name="id">Table ID</param>
            /// <param name="rowIndex">行索引，从零开始</param>
            /// <param name="cellIndex">列索引，从零开始</param>
            /// <param name="cellValue">列值</param>
            var _tableObj = jqUtils.UITable.getObj(id);
            if (_tableObj.length > 0) {
                _tableObj[rowIndex][cellIndex].html(cellValue);
            }
        }
    }
    jqUtils.Ajax = {
        post: function (url, parm, callBack) {
            /// <summary>
            /// Post请求
            /// </summary>
            /// <param name="url" type="type"></param>
            /// <param name="parm" type="type"></param>
            /// <param name="callBack" type="type"></param>
            $.ajax({
                type: 'post',
                dataType: "json",
                url: url,
                data: parm,
                cache: false,
                async: false,
                success: function (msg) {
                    callBack(msg);
                }
            });
        }
    }
    window.jqUtils = jqUtils;
})(window);