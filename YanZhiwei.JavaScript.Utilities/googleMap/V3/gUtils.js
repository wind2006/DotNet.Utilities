/// <reference path="https://maps.googleapis.com/maps/api/js?key=AIzaSyBKumCcWi9uXR7SHtoQYFMnLhVPKNWVG64&sensor=true" />
/*
 *@description 谷歌地图 JAVASCRIPT API V3.0  工具类 
 *@author YanZhiwei
 *@see https://developers.google.com/maps/documentation/javascript/reference?hl=zh-cn
 *@email Yan.Zhiwei@hotmail.com
 */
(function () {
    window.gmap = {};
    window.overlay = {};
    window.panorama = {};
    window.infoWindow = {};
    window.overlays = [];
    gUtils = {
        CONSTANT: {
            CONTAINER: "googlemap",
            DEFAULT_ZOOM: 13,
            DEFAULT_INIT_ZOOM: 12,
            DEFAULT_MAX_ZOOM: 21,
            DEFAULT_MIN_ZOOM: 0,
            GRID_SIZE: 60
        },
        initNormalMap: function (lon, lat, zoom, maxzoom, minzoom, div) {
            /// <summary>
            /// 基本地图初始化
            /// </summary>
            /// <param name="lon">地理纬度</param>
            /// <param name="lat">地理经度</param>
            /// <param name="zoom">显示级别</param>
            /// <param name="maxzoom">地图最大级别</param>
            /// <param name="minzoom">地图最小级别,默认0</param>
            /// <param name="div">地图承载DIV</param>
            var gmap_div = document.getElementById(div || this.CONSTANT.CONTAINER);
            if (gmap_div) {
                var options = {
                    zoom: zoom || this.CONSTANT.DEFAULT_INIT_ZOOM,
                    scaleControl: true,
                    overviewMapControl: true,
                    overviewMapControlOptions: { opened: true },
                    center: new google.maps.LatLng(lat || 39.915, lon || 116.404),//默认中心 在上海
                    mapTypeId: google.maps.MapTypeId.ROADMAP,
                    maxZoom: maxzoom || this.CONSTANT.DEFAULT_MAX_ZOOM,
                    minZoom: minzoom || this.CONSTANT.DEFAULT_MIN_ZOOM
                }
                window.gmap = new google.maps.Map(gmap_div, options);
                window.overlay = new google.maps.OverlayView();
                overlay.draw = function (e, ee) { };
                overlay.setMap(window.gmap);
            }
        },
        addMarker: function (marker) {
            /// <summary>
            /// 将标记添加到地图
            /// </summary>
            /// <param name="marker">标记</param>
            if (marker) {
                marker.setMap(window.gmap);
                overlays.push(marker);
            }
        },
        addMapView: function () {
            /// <summary>
            /// 添加地图视图_普通街道_卫星视图_卫星和路网的混合
            /// </summary>
            //     if (window.gmap)

        },
        removeMarker: function (marker) {
            /// <summary>
            /// 从地图上移出标记
            /// </summary>
            /// <param name="marker">标记</param>
            if (marker) {
                overlays.remove(marker);
                marker.setVisible(false);
                marker.setMap(null);
            }
        },
        createBounds: function (points) {
            if (points) {
                var bounds = new google.maps.LatLngBounds();
                for (var i = 0; i < points.length; i++) {
                    var point = points[i];
                    if (point) {
                        bounds.extend(point);
                    }
                }
                return bounds;
            }
            return null;
        },
        focused: function (point, zoom) {
            /// <summary>
            /// 聚焦点
            /// </summary>
            /// <param name="point">point</param>
            /// <param name="zoom">级别</param>
            if (point) {
                var bounds = window.gmap.getBounds();
                window.gmap.setCenter(point);
                window.gmap.setZoom(zoom || this.CONSTANT.DEFAULT_ZOOM);
                if (!bounds.contains(point)) {
                    setTimeout(function () {
                        window.gmap.panTo(point);
                    }, 100);
                }
            }
        },
        addMenu: function (mapId, rightMenuId, callback) {
            /// <summary>
            /// 为地图添加右键菜单
            /// 注意：需要引用jQuery
            /// eg:
            ///<div id="rightMenu" class="contextMenu">
            ///<a id='menu1'>
            ///    <div class="context">
            ///         menu item 1
            ///     </div>
            /// </a>
            ///<a id='menu2'>
            ///    <div class="context">
            ///        menu item 2
            ///    </div>
            /// </a>
            /// </div>
            /// -----------------------------------
            ///.contextMenu {
            ///    position: absolute;
            ///    min-width: 100px;
            ///    z-index: 1000; /* 必要 */
            ///    background: #fff;
            ///    border-top: solid 1px #CCC;
            ///    border-left: solid 1px #CCC;
            ///    border-bottom: solid 1px #676767;
            ///    border-right: solid 1px #676767;
            ///    padding: 0px;
            ///    margin: 0px;
            ///    display: none; 
            ///    text-align: center;
            ///    /*font-size: smaller;*/
            ///}
            ///.contextMenu a:hover {
            ///    cursor: pointer;
            /// }
            /// </summary>
            /// <param name="mapId">google map承载DIV</param>
            /// <param name="rightMenuId">右键菜单承载DIV</param>
            mapId = mapId || this.CONSTANT.CONTAINER;
            var contextMenu = $("#" + rightMenuId);
            $("#" + mapId).append(contextMenu);
            google.maps.event.addListener(window.gmap, "rightclick", function (e) {
                contextMenu.hide();
                var mapcontainer = $("#" + mapId).append("#" + contextMenu)
                var x = e.pixel.x;
                var y = e.pixel.y;
                if (x > mapcontainer.width() - contextMenu.width()) {
                    x -= contextMenu.width();
                }
                if (y > mapcontainer.height() - contextMenu.height()) {
                    y -= contextMenu.height();
                }
                contextMenu.css({ top: y, left: x }).fadeIn(100);
                callback(e.LatLng);
            });
            //-----------------------------------------------------------
            $.each('click dragstart zoom_changed maptypeid_changed'.split(' '),
             function (i, name) {
                 google.maps.event.addListener(window.gmap, name, function () {
                     contextMenu.hide()
                 });
             });
        },
        fromLatLngToDivPixel: function (latLng) {
            /// <summary>
            /// 将经纬度转换为屏幕像素值
            /// </summary>
            /// <param name="latLng">经纬度</param>
            /// <returns type="">屏幕像素值</returns>
            //----------------------------------------------------------------------------
            /* convert LatLng object to actual pixels 参考：http://krasimirtsonev.com/blog/article/google-maps-api-v3-convert-latlng-object-to-actual-pixels-point-object
            var topRight = gmap.getProjection().fromLatLngToPoint(gmap.getBounds().getNorthEast());
            var bottomLeft = gmap.getProjection().fromLatLngToPoint(gmap.getBounds().getSouthWest());
            var scale = Math.pow(2, gmap.getZoom());
            var worldPoint = gmap.getProjection().fromLatLngToPoint(latLng);
            return new google.maps.Point((worldPoint.x - bottomLeft.x) * scale, (worldPoint.y - topRight.y) * scale);
            */
            var project = overlay.getProjection();
            if (project) {
                return project.fromLatLngToDivPixel(latLng);
            }
        },
        fromDivPixelToLatLng: function (pixels) {
            /// <summary>
            /// 将屏幕像素值转换为经纬度
            /// </summary>
            /// <param name="pixels">屏幕像素值</param>
            /// <returns type="">经纬度</returns>
            var project = overlay.getProjection();
            if (project) {
                return project.fromDivPixelToLatLng(pixels);
            }
        },
        latlngToPoint: function (latlng) {
            /// <summary>
            /// 可将 LatLng 值转换为世界坐标
            /// </summary>
            /// <param name="latlng">经纬度</param>
            /// <returns type="">世界坐标</returns>
            var normalizedPoint = gmap.getProjection().fromLatLngToPoint(latlng); // returns x,y normalized to 0~255
            //Projection.fromLatLngToPoint() 方法可将 LatLng 值转换为世界坐标。此方法用于在地图上放置叠加层（同时放置地图本身）。
            var scale = Math.pow(2, gmap.getZoom());
            var pixelCoordinate = new google.maps.Point(normalizedPoint.x * scale, normalizedPoint.y * scale);
            return pixelCoordinate;
        },
        pointToLatlng: function (point) {
            /// <summary>
            /// 将世界坐标转换为 LatLng 值
            /// </summary>
            /// <param name="point">世界坐标</param>
            /// <returns type="">经纬度</returns>
            var scale = Math.pow(2, gmap.getZoom());
            var normalizedPoint = new google.maps.Point(point.x / scale, point.y / scale);
            var latlng = gmap.getProjection().fromPointToLatLng(normalizedPoint);
            //Projection.fromPointToLatLng() 方法可将世界坐标转换为 LatLng 值。此方法用于将地图上发生的事件（如点击）转换为地理坐标。
            return latlng;
        }
    };
    gUtils.bounds = {
        CONSTANT: {
            GRID_SIZE: 60,
            OFF_SET: 0
        },
        setCenter: function (points) {
            /// <summary>
            /// 设置一组地理坐标点的中心点
            /// </summary>
            /// <param name="points">地理坐标点</param>
            if (points) {
                var bounds = new google.maps.LatLngBounds();
                for (var i = 0; i < points.length; i++) {
                    var point = points[i];
                    bounds.extend(point);
                }
                var point = bounds.getCenter();
                window.gmap.setCenter(point);
            }
        },
        getExtendedBounds: function (bounds, gridSize) {
            /// <summary>
            /// 获取一个扩展的视图范围，把上下左右都扩大一样的像素值。
            /// </summary>
            /// <param name="bounds">google.maps.LatLngBounds的实例化对象</param>
            /// <param name="gridSize">要扩大的像素值</param>
            /// <returns type="">返回扩大后的视图范围</returns>
            gridSize = gridSize || this.CONSTANT.GRID_SIZE;
            bounds = this.cutBoundsInRange(bounds);
            var pixelNE = gUtils.fromLatLngToDivPixel(bounds.getNorthEast());
            var pixelSW = gUtils.fromLatLngToDivPixel(bounds.getSouthWest());
            pixelNE.x += gridSize;
            pixelNE.y -= gridSize;
            pixelSW.x -= gridSize;
            pixelSW.y += gridSize;
            var newNE = gUtils.fromDivPixelToLatLng(pixelNE);
            var newSW = gUtils.fromDivPixelToLatLng(pixelSW);
            return new google.maps.LatLngBounds(newSW, newNE);
        },
        cutBoundsInRange: function (bounds) {
            /// <summary>
            /// 按照地图支持的世界范围对bounds进行边界处理
            /// </summary>
            /// <param name="bounds">BMap.Bounds</param>
            /// <returns type="BMap.Bounds">返回不越界的视图范围</returns>
            var maxX = this.getRange(bounds.getNorthEast().lng(), -180, 180);
            var minX = this.getRange(bounds.getSouthWest().lng(), -180, 180);
            var maxY = this.getRange(bounds.getNorthEast().lat(), -74, 74);
            var minY = this.getRange(bounds.getSouthWest().lat(), -74, 74);
            var sw = new google.maps.LatLng(minY, minX);
            var ne = new google.maps.LatLng(maxY, maxX);
            return new google.maps.LatLngBounds(sw, ne);
        },
        getRange: function (i, mix, max) {
            /// <summary>
            /// 对单个值进行边界处理。
            /// </summary>
            /// <param name="i">要处理的数值</param>
            /// <param name="mix">下边界值</param>
            /// <param name="max">上边界值</param>
            /// <returns type="">返回不越界的数值</returns>
            mix && (i = Math.max(i, mix));
            max && (i = Math.min(i, max));
            return i;
        }
    };
    gUtils.marker = {
        add: function (lon, lat) {
            /// <summary>
            /// 创建地图标记
            /// 说明：需要marker.setMap(window.gmap);或MarkerManger进行管理
            /// </summary>
            /// <param name="lon">地理纬度</param>
            /// <param name="lat">地理经度</param>
            /// <returns type="">google.maps.Marker</returns>
            var marker = new google.maps.Marker({
                animation: false, //google.maps.Animation.DROP,
                position: new google.maps.LatLng(lat, lon)
            });
            return marker;
        },
        addWithIcon: function (lon, lat, iconpath, size) {
            /// <summary>
            /// 创建标记并且定义图标
            /// 说明：需要marker.setMap(window.gmap);或MarkerManger进行管理
            /// </summary>
            /// <param name="lon">地理纬度</param>
            /// <param name="lat">地理经度</param>
            /// <param name="iconpath">图标路径</param>
            /// <param name="size">google.maps.Size</param>
            /// <returns type="">google.maps.Marker</returns>
            var image = {
                url: iconpath,
                size: size,
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(0, 32)
            };
            var marker = new google.maps.Marker({
                animation: false, //google.maps.Animation.DROP,
                position: new google.maps.LatLng(lat, lon),
                icon: image,
            });
            return marker;
        },
        addWithTitle: function (lon, lat, title) {
            /// <summary>
            ///创建标记并且设置toolTip文字
            ///说明：需要marker.setMap(window.gmap);或MarkerManger进行管理
            /// </summary>
            /// <param name="lon">地理纬度</param>
            /// <param name="lat">地理经度</param>
            /// <param name="title">toolTip文字</param>
            /// <returns type="">google.maps.Marker</returns>
            var marker = new google.maps.Marker({
                animation: false, //google.maps.Animation.DROP,
                position: new google.maps.LatLng(lat, lon),
                title: title
            });
            return marker;
        },
        finded: function (point) {
            /// <summary>
            /// 根据经纬度查找标记
            /// </summary>
            /// <param name="point">google.maps.LatLng</param>
            /// <returns type="">google.maps.Marker</returns>
            if (point instanceof google.maps.LatLng) {
                for (var i = 0; i < overlays.length; i++) {
                    var overlay = overlays[i];
                    if (overlay instanceof google.maps.Marker) {
                        var position = overlay.getPosition();
                        if (position.equals(point)) {
                            return overlay;
                        }
                    }
                }
            }
        },
        findInBounds: function (properties, value) {
            /// <summary>
            /// 查找可视范围内标记
            /// </summary>
            /// <param name="properties">键</param>
            /// <param name="value">值</param>
            /// <returns type="">若查找到则返回google.maps.Marker；若查找不到则返回NULL</returns>
            var bounds = window.gmap.getBounds();
            for (var i = 0; i < overlays.length; i++) {
                var overlay = overlays[i];
                if (overlay instanceof google.maps.Marker) {
                    if (bounds.contains(overlay.getPosition())) {
                        if (overlay[properties] == value)
                            return overlay;
                    }
                }
            }
            return null;
        },
        findAllInBounds: function (properties, value) {
            /// <summary>
            /// 查找可视范围内符合条件标记的集合
            /// </summary>
            /// <param name="properties">键</param>
            /// <param name="value">值</param>
            /// <returns type=""></returns>
            var finded = new Array;
            var bounds = window.gmap.getBounds();
            for (var i = 0; i < overlays.length; i++) {
                var overlay = overlays[i];
                if (overlay instanceof google.maps.Marker) {
                    if (bounds.contains(overlay.getPosition())) {
                        if (overlay[properties] == value)
                            finded.push(overlay);
                    }
                }
            }
            return finded;
        },
        focused: function (marker, zoom) {
            /// <summary>
            /// 标记聚焦
            /// </summary>
            /// <param name="marker">标记</param>
            /// <param name="zoom">缩放级别</param>
            if (marker instanceof google.maps.Marker) {
                var point = marker.getPosition();
                gUtils.focused(point, zoom);
            }
        },
        inBound: function (marker) {
            /// <summary>
            /// 判断标记是否在可是范围
            /// </summary>
            /// <param name="marker">标记</param>
            /// <returns type="bool">是否在可是范围内</returns>
            if (marker instanceof google.maps.Marker) {
                var bounds = gmap.getBounds();
                return bounds.contains(marker.getPosition());
            }
            return false;
        },
        setInfoWindow: function (marker, htmlElement) {
            /// <summary>
            /// 设置Marker点击的时候，显示的InfoWindow
            /// </summary>
            /// <param name="marker">需要设置的MARKER标记</param>
            /// <param name="htmlElement">htmlElement内容</param>
            google.maps.event.addListener(marker, 'click', function () {
                if (infoWindow instanceof google.maps.InfoWindow)
                    infoWindow.close();
                infoWindow = gUtils.infoWindow.create(htmlElement);
                infoWindow.open(window.gmap, marker);
            });
        },
        showInfoWindow: function (marker, htmlElement) {
            /// <summary>
            /// 显示InfoWindow
            /// </summary>
            /// <param name="marker">标记</param>
            /// <param name="htmlElement">htmlElement</param>
            if (marker) {
                gUtils.infoWindow.add(marker, htmlElement, true);
            }
        },
        show: function (marker) {
            /// <summary>
            /// 显示Marker
            /// </summary>
            /// <param name="marker">Marker</param>
            if (marker instanceof google.maps.Marker) {
                marker.setVisible(true);
            }
        },
        hide: function (maker) {
            /// <summary>
            /// 隐藏Marker
            /// </summary>
            /// <param name="marker">Marker</param>
            if (marker instanceof google.maps.Marker) {
                marker.setVisible(false);
            }
        },
        release: function (marker) {
            /// <summary>
            /// 在地图上释放掉marker标记，但是不从overlays中删除；
            /// </summary>
            /// <param name="marker">marker标记</param>
            if (marker instanceof google.maps.Marker) {
                marker.setMap(null);
            }
        },
        load: function (marker) {
            /// <summary>
            /// 将标记重新加载到地图上，主要针对之前在地图上标记释放过操作
            /// </summary>
            /// <param name="marker">marker标记</param>
            if (marker instanceof google.maps.Marker) {
                marker.setMap(window.gmap);
            }
        }
    };
    gUtils.tool = {
        CONSTANT: {
            RANDOM_NUMER: 100
        },
        addRandomInViewRange: function (number) {
            /// <summary>
            /// 在可视区域内添加随机标记
            /// 默认100个
            /// </summary>
            /// <param name="number">标记个数</param>
            google.maps.event.addListener(window.gmap, 'tilesloaded', function () {
                if (window.gmap) {
                    var bounds = window.gmap.getBounds();
                    var sw = bounds.getSouthWest();
                    var ne = bounds.getNorthEast();
                    var lngSpan = Math.abs(ne.lng() - sw.lng());
                    var latSpan = Math.abs(sw.lat() - ne.lat());
                    for (var i = 0; i < 100 ; i++) {
                        var marker = gUtils.marker.addWithTitle(sw.lng() + lngSpan * (Math.random() * 0.9), ne.lat() - latSpan * (Math.random() * 0.9), i + 'Google Map');
                        marker.id = i;
                        gUtils.marker.setInfoWindow(marker, '上海啊上海');
                        gUtils.addMarker(marker);
                    }
                }
            });
        }
    };
    gUtils.overlays = {
        add: function (overlay) {
            /// <summary>
            /// 新增覆盖物，将覆盖物增加到overlays数组中
            /// </summary>
            /// <param name="overlay">覆盖物</param>
            if (overlay) {
                overlays.push(overlay);
            }
        },
        find: function (properties, value) {
            /// <summary>
            /// 查找覆盖物
            /// </summary>
            /// <param name="properties">键</param>
            /// <param name="value">值</param>
            /// <returns type="">找到则返回覆盖物；若没找到则返回NULL</returns>
            for (var i = 0; i < overlays.length; i++) {
                var overlay = overlays[i];
                if (overlay.hasOwnProperty(properties)) {
                    if (overlay[properties] == value)
                        return overlay;
                }
            }
        },
        findAll: function (properties, value) {
            /// <summary>
            /// 查找符合条件覆盖物的集合
            /// </summary>
            /// <param name="properties">键</param>
            /// <param name="value">值</param>
            /// <returns type="">Array</returns>
            var finded = new Array;
            for (var i = 0; i < overlays.length; i++) {
                var overlay = overlays[i];
                if (overlay.hasOwnProperty(properties)) {
                    if (overlay[properties] == value)
                        finded.push(overlay);
                }
            }
            return finded;
        },
        count: function () {
            return overlays.length;
        },
        show: function (properties, value) {
            /// <summary>
            /// 查找覆盖物并显示
            /// </summary>
            /// <param name="properties">键</param>
            /// <param name="value">值</param>
            var overlay = this.find(properties, value);
            if (overlay) {
                overlay.setVisible(true);
            }
        },
        remove: function (properties, value) {
            /// <summary>
            /// 查找覆盖物并删除
            /// </summary>
            /// <param name="properties">键</param>
            /// <param name="value">值</param>
            var overlay = this.find(properties, value);
            if (overlay) {
                overlay.setVisible(false);
                overlay.setMap(null);
                markers.remove(overlay);
            }
        },
        removeAll: function (properties, value) {
            /// <summary>
            /// 查找所有符合条件的覆盖物并删除
            /// </summary>
            /// <param name="properties">键</param>
            /// <param name="value">值</param>
            var finded = this.findAll(properties, value);
            for (var i = 0; i < finded.length; i++) {
                var overlay = finded[i];
                overlay.setVisible(false);
                overlay.setMap(null);
                markers.remove(overlay);
            }
        },
        hide: function (properties, value) {
            /// <summary>
            /// 查找覆盖物并隐藏
            /// </summary>
            /// <param name="properties">键</param>
            /// <param name="value">值</param>
            var overlay = this.find(properties, value);
            if (overlay) {
                overlay.setVisible(false);
            }
        },
        clearAll: function () {
            /// <summary>
            /// 清除地图上所有的覆盖物
            /// </summary>
            for (var i = 0; i < overlays.length; i++) {
                overlays[i].setVisible(false);
                overlays[i].setMap(map);
            }
            overlays = [];
        }
    };
    gUtils.infoWindow =
        {
            CONSTANT: {
                WIDTH: 250,
                HEIGHT: 80
            },
            create: function (htmlElement) {
                /// <summary>
                ///  创建infoWindow
                /// </summary>
                /// <param name="htmlElement">显示内容HTML</param>
                /// <returns type="">google.maps.InfoWindow</returns>
                infoWindow = new google.maps.InfoWindow({
                    content: htmlElement,
                });
                return infoWindow;
            },
            add: function (point, htmlElement, focused) {
                /// <summary>
                /// 为point添加infoWindow对象
                /// </summary>
                /// <param name="point"></param>
                /// <param name="htmlElement">infowindows 内容</param>
                /// <param name="focused">是否聚焦点</param>
                if (point) {
                    if (infoWindow instanceof google.maps.InfoWindow) {
                        infoWindow.close();
                        infoWindow.setContent(htmlElement);
                    }
                    else {
                        infoWindow = this.create(htmlElement);
                    }
                    infoWindow.open(window.gmap, point);
                    //if (focused) {
                    //    gUtils.focused(point, window.gmap.getZoom());
                    //    //window.gmap.setCenter(point);
                    //    //map.setCenter(marker.getPosition());
                    //}
                }
            }
        };
    window.gUtils = gUtils;
})();