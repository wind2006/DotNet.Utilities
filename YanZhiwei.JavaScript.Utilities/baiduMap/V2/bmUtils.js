/// <reference path="MarkerManager.js" />
(function () {
    BMapLib.MarkerManager.prototype.count = function () {
        /// <summary>
        /// 获取MarkerManager管理标记数量
        /// </summary>
        /// <returns type="">管理标记数量</returns>
        return this._numMarkers.length;
    }
    BMapLib.MarkerManager.prototype.find = function (properties, value) {
        /// <summary>
        ///查找标记 
        /// </summary>
        /// <param name="properties">键</param>
        /// <param name="value">值</param>
        /// <returns type="BMap.Marker">若没查找到，则返回NULL</returns>
        for (var i = 0; i < this._numMarkers.length; i++) {
            var marker = this._numMarkers[i];
            if (marker[properties] == value)
                return marker;
        }
    }
    BMapLib.MarkerManager.prototype.findAllInBounds = function (properties, value) {
        /// <summary>
        /// 查找符合条件的可视范围内标记
        /// </summary>
        /// <param name="properties">键</param>
        /// <param name="value">值</param>
        /// <returns type="Array">符合条件的数组</returns>
        var bounds = this._map.getBounds();
        var finded = new Array;
        for (var i = 0; i < this._numMarkers.length; i++) {
            var marker = this._numMarkers[i];
            if (bounds.containsPoint(marker.getPosition())) {
                if (marker[properties] == value) {
                    finded.push(marker);
                }
            }
        }
        return finded;
    }
    BMapLib.MarkerManager.prototype.findVisualMarkers = function () {
        /// <summary>
        /// 查找可视范围内标记
        /// </summary>
        /// <returns type="Array">可视范围内标记数组</returns>
        var bounds = this._map.getBounds();
        var finded = new Array;
        for (var i = 0; i < this._numMarkers.length; i++) {
            var marker = this._numMarkers[i];
            if (bounds.containsPoint(marker.getPosition())) {
                finded.push(marker);
            }
        }
        return finded;
    }
    yBMUtils = {
    }
    window.yBMUtils = yBMUtils;
})();