/// <reference path="D:\工作\代码\LHLMSMetronic\LHLMSMetronic.UI\assets/global/plugins/bootstrap/js/bootstrap.js" />
(function (window) {
    bs3Utils = {}
    bs3Utils.div = {
        automaticHeight: function (divId) {
            /// <summary>
            /// div自适应页面高度，适用于页面带navbar时候使用，须调用  $(window).resize();
            /// </summary>
            /// <param name="divId" type="type"></param>
            $(window).resize(function () {
                $('#' + divId).height($(window).height() - $(".navbar").height()
                                - parseInt($(".navbar").css("margin-bottom"))
                                - parseInt($(".navbar").css("margin-top"))
                                - parseInt($('#' + divId).css("margin-bottom"))
                                );
            })
        }
    }
    bs3Utils.nav = {
        activeTab: function (tabId) {
            /// <summary>
            /// 设置需要展现的tab
            /// </summary>
            /// <param name="tabId"></param>
            $('.nav-tabs a[href="#' + tabId + '"]').tab('show');
        },
        getActive: function (navId) {
            /// <summary>
            /// 获取nav-tabs选中tab的href
            /// </summary>
            /// <param name="navId">navId</param>
            return $('#' + navId + ' .active > a').attr('href');
        },
        activatedEvent: function (navId, hanlder) {
            /// <summary>
            /// nav activated事件
            /// </summary>
            /// <param name="navId">navId</param>
            /// <param name="hanlder">委托，参数：href</param>
            $('#' + navId + ' a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                var _target = $(e.target).attr("href")
                hanlder(_target);
            });
        }
    }
    window.bs3Utils = bs3Utils;
})(window);