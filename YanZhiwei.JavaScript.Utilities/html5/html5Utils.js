/// <reference path="excanvas.js" />
(function () {

    html5Utils =
        {

        };
    html5Utils.canvas = {
        fixgetContext: function (canvas) {
            /// <summary>
            /// 修复动态生成的Canvas对象将不支持getContext方法
            /// </summary>
            /// <param name="canvas">canvas</param>
            if (!canvas.getContext) {
                G_vmlCanvasManager.initElement(canvas);
            }
        }
    };
})();