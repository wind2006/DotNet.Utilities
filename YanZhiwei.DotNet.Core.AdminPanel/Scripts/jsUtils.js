(function (window) {
    window.console = window.console || (function () {
        var c = {}; c.log = c.warn = c.debug = c.info = c.error = c.time = c.dir = c.profile = c.clear = c.exception = c.trace = c.assert = function () { };
        return c;
    })();

    //String对象的拓展
    if (typeof String.prototype.trim == 'undefined') {
        String.prototype.trim = function () {
            var str = this,
whitespace = ' \n\r\t\f\x0b\xa0\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u200b\u2028\u2029\u3000';
            for (var i = 0, len = str.length; i < len; i++) {
                if (whitespace.indexOf(str.charAt(i)) == -1) {
                    str = str.substring(i);
                    break;
                }
            }
            for (i = str.length - 1; i >= 0; i--) {
                if (whitespace.indexOf(str.charAt(i)) == -1) {
                    str = str.substring(0, i + 1);
                    break;
                }
            }
            return whitespace.indexOf(str.charAt(0)) == -1 ? str : '';
        }
    }
    if (typeof String.prototype.ltrim == 'undefined') {
        String.prototype.ltrim = function () {
            return this.replace(/(^\s*)/g, "");
        }
    }
    if (typeof String.prototype.rtrim == 'undefined') {
        String.prototype.rtrim = function () {
            return this.replace(/(\s*$)/g, "");
        }
    }
    if (typeof String.prototype.htmlEncode == 'undefined') {
        String.prototype.htmlEncode = function (encodeNewLine) {//encodeNewLine:是否encode换行符
            var s = this;
            s = s.replace(/&/g, '&amp;');
            s = s.replace(/</g, '&lt;');
            s = s.replace(/>/g, '&gt;');
            s = s.replace(/'/g, '&quot;');
            if (encodeNewLine) {
                s = s.replace(/\r\n/g, '<br />');
                s = s.replace(/\r/g, '<br />');
                s = s.replace(/\n/g, '<br />');
            }
            return s;
        }
    }
    if (typeof String.prototype.htmlDecode == 'undefined') {
        String.prototype.htmlDecode = function (decodeNewLine) {//decodeNewLine：是否decode换行符
            var s = this;
            if (decodeNewLine) {
                s = s.replace(/<br\s*\/?>/gi, '\r\n');
            }
            s = s.replace(/&quot;/g, '\'');
            s = s.replace(/&gt;/g, '>');
            s = s.replace(/&lt;/g, '<');
            s = s.replace(/&amp;/g, '&');
            return s;
        }
    }
    if (typeof String.prototype.startsWith == 'undefined') {
        String.prototype.startsWith = function (start, ignoreCase) {//start：欲判断字符， ignoreCase：是否忽略大小写
            var s = this;
            if (ignoreCase) {
                s = s.toLowerCase();
                end = end.toLowerCase();
            }
            if (s.substr(0, start.length) == start)
                return true;
            return false;
        }
    }
    if (typeof String.prototype.endsWith == 'undefined') {
        String.prototype.endsWith = function (end, ignoreCase) {//end：欲判断字符， ignoreCase：是否忽略大小写
            var s = this;
            if (ignoreCase) {
                s = s.toLowerCase();
                end = end.toLowerCase();
            }
            if (s.substr(s.length - end.length) == end)
                return true;
            return false;
        }
    }
    //Array对象的拓展
    if (typeof Array.prototype.clear == 'undefined') {
        Array.prototype.clear = function () {
            this.length = 0;
        }
    }
    if (typeof Array.prototype.insertAt == 'undefined') {
        Array.prototype.insertAt = function (index, value) {
            this.splice(index, 0, value);
        }
    }
    if (typeof Array.prototype.removeAt == 'undefined') {
        Array.prototype.removeAt = function (index) {
            /// <summary>
            ///指定位置移除
            /// </summary>
            /// <param name="index">位置</param>
            this.splice(index, 1);
        }
    }
    if (typeof Array.prototype.remove == 'undefined') {
        Array.prototype.remove = function (value) {
            var index = this.indexOf(value);
            if (index >= 0) {
                this.removeAt(index);
            }
        }
    }
    if (typeof Array.prototype.indexOf == 'undefined') {
        Array.prototype.indexOf = function (obj, start) {
            for (var i = (start || 0), j = this.length; i < j; i++) {
                if (this[i] == obj) { return i; }
            }
            return -1;
        }
    }
    if (typeof Array.prototype.deleteRepeat == 'undefined') {
        Array.prototype.deleteRepeat = function () {
            /// <summary>
            /// 删除数组里的重复项
            /// </summary>
            /// <returns type=""></returns>
            var retArr = [], tempObj = {};
            for (var i = 0, tmpItem; (tmpItem = this[i]) != null; i++) {
                if (!tempObj[tmpItem]) {
                    retArr.push(tmpItem);
                    tempObj[tmpItem] = true;
                }
            }
            return retArr;
        }
    }

    if (typeof Date.prototype.addDays == 'undefined') {
        Date.prototype.addDays = function (days) {
            /// <summary>
            /// 当前时间增加天数
            /// </summary>
            /// <param name="days">需要增加天数</param>
            var dat = new Date(this.valueOf());
            dat.setDate(dat.getDate() + days);
            return dat;
        }
    }
    jsUtils = {
        selectedRange: function (el) {
            /// <summary>
            /// 选中内容
            /// eg: selectedRange(document.getElementById('123'));
            /// </summary>
            /// <param name="el">需要选中对象</param>
            var body = document.body, range, sel;
            if (document.createRange && window.getSelection) {
                //Range 对象表示文档的连续范围区域，如用户在浏览器窗口中用鼠标拖动选中的区域。
                //selection是对当前激活选中区（即高亮文本）进行操作。
                range = document.createRange();
                sel = window.getSelection();
                sel.removeAllRanges();
                try {
                    range.selectNodeContents(el);
                    sel.addRange(range);
                } catch (e) {
                    range.selectNode(el);
                    sel.addRange(range);
                }
            } else if (body.createTextRange) {
                range = body.createTextRange();
                range.moveToElementText(el);
                range.select();
            }
        },
        delegate: function (client, clientMethod) {
            /// <summary>
            /// JavaScript 委托
            /// </summary>
            /// <param name="client">目标对象</param>
            /// <param name="clientMethod">委托方法</param>
            return function () {
                return clientMethod.apply(client, arguments);
            }
        },
        isString: function (obj) {
            /// <summary>
            ///判断是不是String类型
            /// </summary>
            /// <param name="obj">对象</param>
            /// <returns type="">是不是String类型</returns>
            return (obj != null) && (obj != undefined) && (typeof obj == 'string') && (obj.constructor == String);
        },
        isNumber: function (obj) {
            /// <summary>
            /// 判断是否是数字
            /// </summary>
            /// <param name="obj">对象</param>
            /// <returns type="">是否是数字</returns>
            return (typeof obj == 'number') && (obj.constructor == Number);
        },
        isDate: function (obj) {
            /// <summary>
            /// 判断是否是日期
            /// </summary>
            /// <param name="obj">对象</param>
            /// <returns type="">是否是日期</returns>
            return obj && (typeof obj == 'object') && (obj.constructor == Date);
        },
        isArray: function (obj) {
            /// <summary>
            /// 判断是否是数组
            /// </summary>
            /// <param name="obj">对象</param>
            /// <returns type="">是否是数组</returns>
            return obj && (typeof obj == 'object') && (obj.constructor == Array);
        },
        isObject: function (obj) {
            /// <summary>
            /// 判断是否是对象
            /// </summary>
            /// <param name="obj">对象</param>
            /// <returns type="">是否是对象</returns>
            return obj && (typeof obj == 'object') && (obj.constructor == Object)
        },
        isPc: function () {
            /// <summary>
            /// 判断是否是pc端浏览
            /// </summary>
            /// <returns type=""></returns>
            var userAgentInfo = navigator.userAgent;
            var Agents = ["Android", "iPhone",
                        "SymbianOS", "Windows Phone",
                        "iPad", "iPod"];
            var flag = true;
            for (var v = 0; v < Agents.length; v++) {
                if (userAgentInfo.indexOf(Agents[v]) > 0) {
                    flag = false;
                    break;
                }
            }
            return flag;
        },
        loadScript: function (url, callback) {
            /// <summary>
            /// 动态加载js
            /// </summary>
            /// <param name="url" type="type">js路径</param>
            /// <param name="callback" type="type">回调函数</param>
            var script = document.createElement("script")
            script.type = "text/javascript";
            script.src = url;
            if (script.readyState) {  //IE
                script.onreadystatechange = function () {
                    if (script.readyState == "loaded" ||
                            script.readyState == "complete") {
                        script.onreadystatechange = null;
                        callback();
                    }
                };
            } else {  //Others
                script.onload = function () {
                    callback();
                };
            }

            document.body.appendChild(script);
        }
    };
    jsUtils.string = {
        isNullOrEmpty: function (data) {
            /// <summary>
            /// 判断NULL或者空
            /// 参考：http://www.sitepoint.com/testing-for-empty-values/
            /// </summary>
            /// <param name="data">需要判断数据</param>
            /// <returns type="bool">是否是NULL或者空</returns>
            if (typeof (data) == 'number' || typeof (data) == 'boolean') {
                return false;
            }
            if (typeof (data) == 'undefined' || data == null) {
                return true;
            }
            if (typeof (data.length) != 'undefined') {
                return data.length == 0;
            }
            var count = 0;
            for (var i in data) {
                if (data.hasOwnProperty(i)) {
                    count++;
                }
            }
            return count == 0;
        }
    };
    jsUtils.datetime = {
        CONSTANT:
            {
                DEFAULT_FORMAT: 'yyyy-MM-dd hh:mm:ss'
            },
        formart: function (date, format) {
            /// <summary>
            /// 时间格式化
            /// 参考：http://yaniswang.com/frontend/2013/02/16/dateformat-performance/
            /// </summary>
            /// <param name="date"></param>
            /// <param name="format"></param>
            /// <returns type=""></returns>
            if (date instanceof Date) {
                var map = {
                    "M": date.getMonth() + 1, //月份
                    "d": date.getDate(), //日
                    "h": date.getHours(), //小时
                    "m": date.getMinutes(), //分
                    "s": date.getSeconds(), //秒
                    "q": Math.floor((date.getMonth() + 3) / 3), //季度
                    "S": date.getMilliseconds() //毫秒
                };
                format = format || this.CONSTANT.DEFAULT_FORMAT;
                format = format.replace(/([yMdhmsqS])+/g, function (all, t) {
                    var v = map[t];
                    if (v !== undefined) {
                        if (all.length > 1) {
                            v = '0' + v;
                            v = v.substr(v.length - 2);
                        }
                        return v;
                    }
                    else if (t == 'y') {
                        return (date.getFullYear() + '').substr(4 - all.length);
                    }
                    return all;
                });
                return format;
            }
        },
        now: function (format) {
            /// <summary>
            /// 获取当前时间
            /// </summary>
            /// <param name="format">时间格式化类型，默认：yyyy-MM-dd hh:mm:ss</param>
            /// <returns type="">当前时间</returns>
            return this.formart(new Date(), format);
        },
        parseTime: function (time) {
            /// <summary>
            /// 将字符串时间转换时间json
            /// eg:parseTime("12:00 AM"); // {hh:  0, mm: 0}
            ///    parseTime("12:00 PM"); // {hh: 12, mm: 0}
            ///    parseTime("01:00 PM"); // {hh: 13, mm: 0}
            ///    parseTime("23:00");    // {hh: 23, mm: 0}
            /// </summary>
            /// <param name="time">字符串时间</param>
            var _part = time.match(/(\d+):(\d+)(?: )?(am|pm)?/i);
            var _hh = parseInt(_part[1], 10);
            var _mm = parseInt(_part[2], 10);
            var _ap = _part[3] ? _part[3].toUpperCase() : null;
            if (_ap === "AM") {
                if (_hh == 12) {
                    _hh = 0;
                }
            }
            if (_ap === "PM") {
                if (_hh != 12) {
                    hh += 12;
                }
            }
            return { hh: _hh, mm: _mm };
        },
        parseDateTime: function (date) {
            /// <summary>
            /// 将日期字符串转换成时间
            /// eg:parseDateTime('2006-1-1') return new Date(2006,0,1)
            ///    parseDateTime('2006-1-1 15:14:16') return new Date(2006,0,1,15,14,16)
            ///    parseDate('2006-1-1 15:14:16.254') return new Date(2006,0,1,15,14,16,254)
            ///    parseDate('不正确的格式') retrun null
            /// </summary>
            /// <param name="date">时间 date</param>
            if (typeof date == 'string') {
                var results = date.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) *$/);
                if (results && results.length > 3)
                    return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]));
                results = date.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2}) *$/);
                if (results && results.length > 6)
                    return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]), parseInt(results[4]), parseInt(results[5]), parseInt(results[6]));
                results = date.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2})\.(\d{1,9}) *$/);
                if (results && results.length > 7)
                    return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]), parseInt(results[4]), parseInt(results[5]), parseInt(results[6]), parseInt(results[7]));
            }
            return null;
        },
        addDays: function (date, days, format) {
            /// <summary>
            /// 增加天数
            /// <para>eg:jsUtils.datetime.addDays(new Date(), 2);</para>
            /// </summary>
            /// <param name="date">目标日期</param>
            /// <param name="days">增加天数</param>
            /// <param name="format">格式</param>
            var _newDate = new Date(date.getTime() + days * 24 * 60 * 60 * 1000);
            return this.formart(_newDate, format);
        },
        getFriendlyString: function (time) {
            /// <summary>
            /// 获取时间差友好提示
            /// </summary>
            /// <param name="time" type="type">date</param>
            /// <returns type="">时间差友好提示</returns>
            var _friendlyDate = '';
            var _nowdate = new Date();
            var _now = _nowdate.getTime();
            var _delay = _now - time;
            var _year = 365 * 24 * 60 * 60 * 1000,
                _day = 24 * 60 * 60 * 1000,
                _hour = 60 * 60 * 1000;
            if (_delay >= _year) {
                _delay = (_delay / _year);
                var _year = Math.floor(_delay);
                if (_year == 1) {
                    _friendlyDate = "去年";
                }
                else if (_year == 2) {
                    _friendlyDate = "前年";
                }
                else {
                    _friendlyDate = _year + "年前";
                }
            } else if (_delay >= _day) {
                _delay = (_delay / _day);
                var _day = Math.floor(_delay);
                if (_day == 1) {
                    _friendlyDate = "昨天";
                } else if (_day == 2) {
                    _friendlyDate = "前天";
                } else {
                    _friendlyDate = _day + "天前";
                }
            } else if (_delay >= (_hour)) {
                _delay = (_delay / _hour)
                _friendlyDate = Math.floor(_delay) + "小时前";
            } else if (_delay >= (60 * 1000)) {
                _delay = (_delay / (60 * 1000))
                _friendlyDate = Math.floor(_delay) + "分钟前";
            } else if (_delay >= (1000)) {
                _delay = (_delay / (1000))
                _friendlyDate = Math.floor(_delay) + "秒前";
            } else {
                _friendlyDate = "刚刚";
            }
            return _friendlyDate;
        }
    };
    jsUtils.log = {
        CONSTANT: {
            DIALOG_WITH: 400,
            DIALOG_HIGHT: 200
        },
        add: function (message) {
            /// <summary>
            /// 添加窗口日志信息
            /// </summary>
            /// <param name="message">日志信息</param>
            if (!this.window_ || this.window_.closed) {
                var win = window.open("", null, "width=600,height=200,scrollbars=yes,resizable=yes,status=no,location=no,menubar=no,toolbar=no");
                if (!win) return;
                var doc = win.document;
                doc.write("<html><head><title>Debug Log</title></head>" +
                          "<body></body></html>");
                doc.close();
                this.window_ = win;
            }
            var logLine = this.window_.document.createElement("div");
            message = jsUtils.datetime.now() + ": " + message;
            logLine.appendChild(this.window_.document.createTextNode(message));
            this.window_.document.body.appendChild(logLine);
        }
    };
    jsUtils.ie = {
        copyTextToClipboard: function (text) {
            /// <summary>
            ///仅支持IE浏览器的复制操作
            /// </summary>
            /// <param name="text">复制到剪切板内容</param>
            var copyFrom = $('<textarea/>');
            copyFrom.text(text);
            $('body').append(copyFrom);
            copyFrom.select();
            document.execCommand('copy', true);
            copyFrom.remove();
        },
        copyElecToClipboard: function (elecId) {
            /// <summary>
            /// 仅支持IE浏览器的对HTML元素复制到剪切板
            /// </summary>
            /// <param name="elecId">需要复制ID</param>
            var obj = document.getElementById(elecId);
            if (obj) {
                JsUtils.selectedRange(obj);
                document.execCommand('copy', true);
            }
        }
    };
    jsUtils.dialog =
    {
        CONSTANT:
          {
              WIDTH: 300,
              HEIGTH: 200
          },
        show: function (url, width, heigth) {
            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="url">链接</param>
            /// <param name="width">宽</param>
            /// <param name="heigth">高</param>
            width = width | this.CONSTANT.WIDTH;
            heigth = heigth | this.CONSTANT.HEIGTH;
            if (document.all)//IE
            {
                var feature = "width:" + width + ";Height:" + heigth + ";status:no;help:no;center:yes;";
                window.showModalDialog(url, null, feature);
            }
            else {
                var x = window.screen.width;
                var y = window.screen.height;
                x = (x - width) / 2;
                y = (y - heigth) / 2;
                var feature = "width=" + width + ",height=" + heigth + ",menubar=no,toolbar=no,location=no,top=" + y + ",left=" + x;
                feature += ",scrollbars=no,status=no,modal=yes";
                window.open(url, null, feature);
            }
        },
        showModel: function (url, width, heigth) {
            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="url">链接</param>
            /// <param name="width">宽</param>
            /// <param name="heigth">高</param>
            document.body.style.backgroundColor = "#999999";
            document.body.style.filter = 'Alpha(Opacity = 40)';
            this.show(url, width, heigth);
            document.body.style.backgroundColor = '';
            document.body.style.filter = '';
        },
        refreshParent: function () {
            /// <summary>
            /// 弹出页面刷新父窗体
            /// </summary>
            window.opener.location.href = window.opener.location.href;
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }
    };
    jsUtils.event = {
        add: function (element, type, handler, key) {
            /// <summary>
            /// 为元素绑定事件处理程序
            /// </summary>
            /// <param name="element"></param>
            /// <param name="type"></param>
            /// <param name="handler"></param>
            /// <param name="key"></param>
            /// <returns type=""></returns>
            var key = key || handler;
            if (element[type + key])
                return false;
            if (typeof element.addEventListener != "undefined") {
                element[type + key] = handler;
                element.addEventListener(type, handler, false);
            }
            else {
                element['e' + type + key] = handler;
                element[type + key] = function () {
                    element['e' + type + key](window.event);
                };
                element.attachEvent('on' + type, element[type + key]);
            }
            return true;
        },
        remove: function (element, type, key) {
            /// <summary>
            /// 移除元素事件处理程序
            /// </summary>
            /// <param name="element"></param>
            /// <param name="type"></param>
            /// <param name="key"></param>
            /// <returns type=""></returns>
            if (!element[type + key])
                return false;
            if (typeof element.removeEventListener != "undefined") {
                element.removeEventListener(type, element[type + key], false);
            }
            else {
                element.detachEvent("on" + type, element[type + key]);
                element['e' + type + key] = null;
            }

            element[type + key] = null;
            return true;
        }
    };
    jsUtils.style = {
        has: function (element, className) {
            /// <summary>
            /// 检查元素是否包含某个class
            /// </summary>
            /// <param name="element"></param>
            /// <param name="className"></param>
            /// <returns type=""></returns>
            var re = new RegExp("(^|\\s)" + className + "(\\s|$)", "i");
            if (element.className.match(re))
                return true;
            return false;
        },
        add: function (element, className) {
            /// <summary>
            /// 为元素添加一个class
            /// </summary>
            /// <param name="element"></param>
            /// <param name="className"></param>
            var re = new RegExp("(^|\\s)" + className + "(\\s|$)", "gi");
            var eleClass = element.className;
            if (!eleClass.match(re))
                element.className = eleClass + " " + className;
        },
        remove: function (element, className) {
            /// <summary>
            /// 为元素删除一个class
            /// </summary>
            /// <param name="element"></param>
            /// <param name="className"></param>
            var re = new RegExp("(^|\\s)" + className + "(\\s|$)", "gi");
            var eleClass = element.className;
            eleClass = eleClass.replace(re, "");
            if (element.className != eleClass)
                element.className = eleClass;
        },
        toggle: function (element, class1, class2) {
            /// <summary>
            /// 切换元素的class， 如果只传入一个class，则切换这个class的有无；如果传入两个class，则元素删除当前class，替换为另一个
            /// </summary>
            /// <param name="element"></param>
            /// <param name="class1"></param>
            /// <param name="class2"></param>
            if (class2 != undefined) {
                if (ssLib.hasClass(element, class1)) {
                    ssLib.removeClass(element, class1);
                    ssLib.addClass(element, class2);
                } else if (ssLib.hasClass(element, class2)) {
                    ssLib.removeClass(element, class2);
                    ssLib.addClass(element, class1);
                }
            } else {
                if (ssLib.hasClass(element, class1)) {
                    ssLib.removeClass(element, class1);
                } else {
                    ssLib.addClass(element, class1);
                }
            }
        }
    };
    jsUtils.listbox = {
        addItem: function (listboxId, key, value) {
            /// <summary>
            /// ListBox 添加
            /// </summary>
            /// <param name="listboxId">ListBox ID</param>
            /// <param name="key">键</param>
            /// <param name="value">值</param>
            var lsbox = document.getElementById(listboxId);
            if (lsbox) {
                var index = this.itemCount(lsbox);
                lsbox.options[index] = new Option(value, key, true, true);;
                lsbox.options[index].title = lsbox.options[index].text;
            }
        },
        itemCount: function (listbox) {
            /// <summary>
            /// 获取Listbox Item数量
            /// </summary>
            /// <param name="listbox">listbox</param>
            /// <returns type="">Item数量</returns>
            var count = 0;
            if (listbox) {
                count = listbox.options.length == null ? 0 : listbox.options.length;
            }
            return count;
        },
        isExitItem: function (listboxId, key) {
            /// <summary>
            /// 判断是否存在
            /// </summary>
            /// <param name="listboxId">ListBox ID</param>
            /// <param name="key">键</param>
            /// <returns type="bool">是否选中</returns>
            var lsbox = document.getElementById(listboxId);
            var isSelected = false;
            var count = this.itemCount(lsbox);
            for (var i = 0; i < count; i++) {
                if (lsbox.options[i].value == key) {
                    isSelected = true;
                    break;
                }
            }
            return isSelected;
        },
        removeItem: function (listboxId, key) {
            /// <summary>
            /// 移除一项
            /// </summary>
            /// <param name="listboxId">ListBox ID</param>
            /// <param name="key">键</param>
            var lsbox = document.getElementById(listboxId);
            var count = this.itemCount(lsbox);
            for (var i = 0; i < count; i++) {
                if (lsbox.options[i].value == key) {
                    lsbox.options[i].parentNode.removeChild(lsbox.options[i]);
                }
            }
        }
    };
    jsUtils.url = {
        get: function (url, key) {
            /// <summary>
            /// 获取连接参数
            ///<para>适用于：jsUtilsTest.html?Name='测试'，一类链接.</para>
            /// </summary>
            /// <param name="url">连接</param>
            /// <param name="key">参数名称</param>
            /// <returns type="">参数值</returns>
            var reg = new RegExp("(^|\\?|&)" + key + "=([^&]*)(\\s|&|$)", "i");
            if (reg.test(url)) {
                return unescape(RegExp.$2.replace(/\+/g, " "));
            }
            return "";
        },
        openInNewTab: function (url) {
            /// <summary>
            /// 在新Tab打开链接
            /// </summary>
            /// <param name="url"></param>
            var _win = window.open(url, '_blank');
            _win.focus();
        },
        checked: function (url) {
            /// <summary>
            /// 检查是否是合法的URL
            /// </summary>
            /// <param name="url"></param>
            var pattern = new RegExp('^(https?:\/\/)?' + '((([a-z\d]([a-z\d-]*[a-z\d])*)\.)+[a-z]{2,}|' +
                                     '((\d{1,3}\.){3}\d{1,3}))' +
                                     '(\:\d+)?(\/[-a-z\d%_.~+]*)*' +
                                          '(\?[;&a-z\d%_.~+=-]*)?' +
                                          '(\#[-a-z\d_]*)?$', 'i');
            return pattern.test(url);
        }
    }
    jsUtils.iframe = {
        getDynamicIframe: function (id) {
            /// <summary>
            /// 获取动态iframe
            /// <pare>参考：http://www.zomeoff.com/javascript-how-to-load-dynamic-contents-html-string-json-to-iframe/ </pare>
            /// </summary>
            /// <param name="id">ID</param>
            /// <returns type=""></returns>
            var iframe = document.getElementById(id);
            if (iframe) {
                var iframedoc;
                if (iframe.contentDocument)
                    iframedoc = iframe.contentDocument;
                else if (iframe.contentWindow)
                    iframedoc = iframe.contentWindow.document;
                return iframedoc;
            }
        }
    }
    jsUtils.cookie = {
        create: function (name, value, days) {
            /// <summary>
            /// 创建cookie
            /// </summary>
            /// <param name="name"></param>
            /// <param name="value"></param>
            /// <param name="days"></param>
            var _expires = "";
            if (days) {
                var _date = new Date();
                _date.setTime(_date.getTime() + (days * 24 * 60 * 60 * 1000));
                _expires = "; expires=" + _date.toGMTString();
            }
            document.cookie = name + "=" + value + _expires + "; path=/";
        },
        read: function (name) {
            /// <summary>
            /// 读取cookie
            /// </summary>
            /// <param name="name"></param>
            var nameEQ = name + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
            }
            return null;
        },
        remove: function (name) {
            /// <summary>
            /// 移除cookie
            /// </summary>
            /// <param name="name"></param>
            jsUtils.cookie.create(name, "", -1);
        }
    }
    jsUtils.json = {
        hanlderObj: function (obj) {
            /// <summary>
            /// 处理需要序列化json字符串内嵌对象
            /// </summary>
            /// <param name="obj" type="type"></param>
            /// <returns type=""></returns>
            for (var i in obj) {
                if (i.indexOf("Fixed") > 0) {
                    var key = i.substr(0, i.indexOf("Fixed"));
                    delete obj[key];
                    delete obj[i];
                }
                else if (i.indexOf("Specified") > 0) {
                    if (!obj[i]) {
                        var key = i.substr(0, i.indexOf("Specified"));
                        delete obj[key];
                    }
                    delete obj[i];
                }
                if (is.Object(obj[i])) {
                    obj[i] = this.hanlderObj(obj[i]);
                }
                if (is.Array(obj[i])) {
                    for (var j = 0; j < obj[i].length; j++) {
                        obj[i][j] = this.hanlderObj(obj[i][j]);
                    }
                }
            }
            return obj;
        },
        serialize: function (obj) {
            /// <summary>
            /// 序列化成Json字符串
            /// </summary>
            /// <param name="obj" type="type"></param>
            /// <returns type=""></returns>
            obj = this.hanlderObj(obj);
            return JSON.stringify(obj);
        }
    }
    window.jsUtils = jsUtils;
})(window);

var is =
{
    types: ["Array", "Boolean", "Date", "Number", "Object", "RegExp", "String", "Window", "HTMLDocument", "HTMLLIElement"]
}
for (var i = 0, c; c = is.types[i++];) {
    is[c] = (function (type) {
        return function (obj) {
            return Object.prototype.toString.call(obj) == "[object " + type + "]";
        }
    }
    )(c);
}
function Uri(uri) {
    /// <signature>
    /// <summary>网络连接字符串</summary>
    /// <field name='Origin' type='String'>原始 URI 字符串。</field>
    /// <field name='AbsoluteUri' type='String'>绝对 URI。</field>
    /// <field name='Scheme' type='String'>方案名称</field>
    /// <field name='UserInfo' type='String'>用户名、密码或其他与指定 URI 关联的特定于用户的信息。</field>
    /// <field name='Authority' type='String'>域名系统 (DNS) 主机名或 IP 地址和端口号</field>
    /// <field name='Port' type='Int'>端口号</field>
    /// <field name='Host' type='String'>主机</field>
    /// <field name='PathAndQuery' type='String'>用问号 (?) 分隔的 AbsolutePath 和 Query</field>
    /// <field name='AbsolutePath' type='String'>URI 的绝对路径</field>
    /// <field name='Query' type='String'>URI 中包括的任何查询信息</field>
    /// <field name='Querys' type='Object'>URI 中包括的任何查询信息集合</field>
    /// <field name='Segments' type='String'>构成指定 URI 的路径段的数组</field>
    /// </signature>

    uri = uri.toString();
    if (uri.lastIndexOf("#") == uri.length - 1)
        uri = uri.substr(0, uri.length - 1);
    var temp;
    var schemeIndex = uri.indexOf(":") + 3;
    var userInfoIndex = uri.indexOf("@");
    var authorityIndex = uri.indexOf("/", schemeIndex);
    if (authorityIndex < 0)
        authorityIndex = uri.length;
    var queryIndex = uri.indexOf("?");

    this.Origin = uri;
    if (queryIndex > 0) {
        this.Origin = uri.substr(0, queryIndex);
    }
    this.AbsoluteUri = uri;
    this.Scheme = uri.substr(0, schemeIndex - 3);
    this.UserInfo = null;
    this.Authority = null;
    if (userInfoIndex > 0) {
        this.UserInfo = uri.substr(schemeIndex, userInfoIndex - schemeIndex);
        this.Authority = uri.substr(userInfoIndex + 1, authorityIndex - userInfoIndex - 1);
    } else {
        this.Authority = uri.substr(schemeIndex, authorityIndex - schemeIndex);
    }
    this.Port = 80;
    this.Host = this.Authority;
    if (this.Authority.indexOf(":") > 0) {
        temp = this.Authority.split(":");
        this.Host = temp[0];
        this.Port = temp[1];
    }

    this.PathAndQuery = uri.substr(authorityIndex);

    this.AbsolutePath = this.PathAndQuery;

    this.Query = "";
    this.Querys = {};
    if (queryIndex > 0) {
        temp = this.PathAndQuery.split("?");
        this.AbsolutePath = temp[0];
        this.Query = temp[1];
    }
    if (this.Query) {
        var items = this.Query.split("&");
        for (var i = 0; i < items.length; i++) {
            var index = items[i].indexOf("=");
            this.Querys[items[i].substr(0, index)] = items[i].substr(index + 1);
        }
    }

    this.Segments = null;
    if (this.AbsolutePath != "") {
        this.Segments = this.AbsolutePath.split("/");
    }
}
Uri.prototype.toString = function () {
    var param = "";
    if (this.Querys) {
        var i = 0;
        for (var q in this.Querys) {
            if (i == 0)
                param = "?";
            if (i++ > 0)
                param += "&";
            param += (q + "=" + this.Querys[q]);
        }
    }

    var result = this.Scheme + "://";
    if (this.UserInfo)
        result += this.UserInfo + "@";
    result += this.Host;
    if (this.Origin.indexOf(":", 6) > 0)
        result += ":" + this.Port;

    result += this.AbsolutePath + param;
    return result;
    //return this.Origin + param;
}
function Dictionary() {
}
Dictionary.prototype.toArray = function () {
    var array = new Array();
    var temp = this;
    temp.forIn(function (e) { array.push(temp[e]) })
    return array;
}

Dictionary.prototype.forIn = function (code) {
    for (var key in this) {
        if (this[key].constructor.toString().indexOf("Function") > 0)
            continue;
        code(key, this[key]);
    }
}