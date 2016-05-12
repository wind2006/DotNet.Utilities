/// <reference path="bootbox.js" />

(function (window) {
    bootboxUtils = {
        CONSTANT:
           {
               TIMEOUT: 3000//默认超时时间
           },
        allAutoColse: function (timeout) {
            /// <summary>
            /// 自动关闭所有弹出框，适用于：bootbox.alert， bootbox.dialog；
            /// </summary>
            /// <param name="timeout" type="type">毫秒，默认3秒钟</param>
            timeout = timeout || this.CONSTANT.TIMEOUT;
            window.setTimeout(function () {
                bootbox.hideAll();
            }, timeout);
        },
        showAutoCloseDialog: function (title, content, timeout) {
            /// <summary>
            /// 自动延时关闭bootbox.dialog
            /// </summary>
            /// <param name="title" type="type">bootbox.dialog 标题</param>
            /// <param name="content" type="type">bootbox.dialog 内容</param>
            /// <param name="timeout" type="type">毫秒，默认3秒钟</param>

            if (typeof (title) == 'undefined')
                throw new Error('缺少bootbox.dialog 标题.');
            if (typeof (content) == 'undefined')
                throw new Error('缺少bootbox.dialog 内容.');
            timeout = timeout || this.CONSTANT.TIMEOUT;
            var _modal = bootbox.dialog({
                message: content,
                title: title,
                buttons: [
                  {
                      label: '关闭',
                      callback: function () {
                          _modal.modal('hide');
                      }
                  }
                ],
                show: false,
                onEscape: function () {
                    _modal.modal('hide');
                }
            });

            _modal.on("shown.bs.modal", function () {
                setTimeout(function () {
                    _modal.modal('hide');
                }, timeout);
            });

            _modal.modal('show');
        }
    }
    window.bootboxUtils = bootboxUtils;
})(window);