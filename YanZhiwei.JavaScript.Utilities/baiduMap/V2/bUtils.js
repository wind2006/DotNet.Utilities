/// <reference path="../../yUtils.js" />
/*
 *@description 百度地图 JAVASCRIPT API V2.0 大众版 工具类 
 *@author YanZhiwei
 *@see http://developer.baidu.com/window.bmap/reference/index.php
 *@email Yan.Zhiwei@hotmail.com
 */
(function () {
    window.bmap = {};
    window.panorama = {};
    window.bInfoWindow = {};
    bUtils = {
        CONSTANT: {
            DYNAMIC_CITY: "上海",
            CONTAINER: "baidumap",
            DEFAULT_ZOOM: 13,
            DEFAULT_INIT_ZOOM: 12,
            DEFAULT_MAX_ZOOM: 18,
            DEFAULT_MIN_ZOOM: 8,
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
            /// <param name="minzoom">地图最小级别</param>
            /// <param name="div">地图承载DIV</param>
            window.bmap = new BMap.Map(div || this.CONSTANT.CONTAINER, { enableMapClick: false });
            var point = new BMap.Point(lon || 116.404, lat || 39.915); // 默认地图初始化位置为北京
            window.bmap.centerAndZoom(point, zoom || this.CONSTANT.DEFAULT_INIT_ZOOM);
            window.bmap.enableDragging(); // 开启拖拽
            window.bmap.setMinZoom(minzoom || this.CONSTANT.DEFAULT_MIN_ZOOM);//地图最小级别
            window.bmap.setMaxZoom(maxzoom || this.CONSTANT.DEFAULT_MAX_ZOOM);//地图最小级别
            window.bmap.enableScrollWheelZoom(true); // 允许鼠标滚轮缩放地图
            window.bmap.addControl(new BMap.NavigationControl(BMAP_ANCHOR_TOP_LEFT)); // 添加默认缩放平移控件
            window.bmap.addControl(new BMap.ScaleControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT })); // 左下角比例尺控件
            window.bmap.addControl(new BMap.OverviewMapControl()); // 添加默认缩略地图控件(鹰眼)
            var cr = new BMap.CopyrightControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT });
            window.bmap.addControl(cr); // 添加版权控件（支持自定义版权控件）
        },
        addMapView: function () {
            /// <summary>
            /// 添加地图视图_普通街道_卫星视图_卫星和路网的混合
            /// </summary>
            if (window.bmap)
                window.bmap.addControl(new BMap.MapTypeControl({ mapTypes: [BMAP_NORMAL_MAP, BMAP_SATELLITE_MAP, BMAP_PERSPECTIVE_MAP] }));
        },
        addMarker: function (marker) {
            /// <summary>
            /// 将标记添加到地图
            /// </summary>
            /// <param name="marker">标记</param>
            if (window.bmap)
                window.bmap.addOverlay(marker);
        },
        addOverview: function () {
            /// <summary>
            /// 添加缩略地图
            /// </summary>
            window.bmap.addControl(new BMap.OverviewMapControl());
        },
        focused: function (point, zoom) {
            /// <summary>
            /// 聚焦点
            /// </summary>
            /// <param name="point">point</param>
            /// <param name="zoom">级别</param>
            if (point) {
                var bounds = window.bmap.getBounds();
                window.bmap.centerAndZoom(point, zoom || this.CONSTANT.DEFAULT_ZOOM);
                if (!bounds.containsPoint(point)) {
                    setTimeout(function () {
                        window.bmap.panTo(point);
                    }, 100);
                }
            }
        },
        setCenter: function (markers) {
            /// <summary>
            /// 设置一组标记的中心点
            /// </summary>
            /// <param name="markers">标记组</param>
            if (markers) {
                var points = new Array();
                for (var i = 0; i < markers.length; i++) {
                    var marker = markers[i];
                    points.push(marker.getPosition());
                }
                bUtils.bounds.setCenter(points);
            }
        },
        addMenu: function (menuItem) {
            /// <summary>
            /// 给地图添加右键菜单
            /// eg:
            ///var menuItem = [
            ///        {
            ///            text:'放大',
            ///            callback:function(){window.bmap.zoomIn()}
            ///        },
            ///        {
            ///            text:'缩小',
            ///            callback:function(){window.bmap.zoomOut()}
            ///        }
            ///    ];
            /// </summary>
            /// <param name="menuItem">menuItem</param>
            if (menuItem.length > 0) {
                var menu = new BMap.ContextMenu();
                for (var i = 0; i < menuItem.length; i++) {
                    menu.addItem(new BMap.MenuItem(menuItem[i].text, menuItem[i].callback, 100));
                }
                window.bmap.addContextMenu(menu);
            }
        }
    };
    bUtils.panorama = {
        CONSTANT: {
            CONTAINER: "panoramamap",
            POV_HEADING: 270,
            POV_PITCH: 0
        },
        initNormal: function (lon, lat, div) {
            /// <summary>
            /// 初始化街景地图
            /// </summary>
            /// <param name="lon">地理纬度</param>
            /// <param name="lat">地理经度</param>
            /// <param name="div">承载DIV</param>
            panorama = new BMap.Panorama(div || this.CONSTANT.CONTAINER);
            //panorama.setId('0100010000130501122416015Z1');
            var point = new BMap.Point(lon || 120.305456, lat || 31.570037);//默认无锡
            panorama.setPosition(point);

        },
        setPov: function (heading, pitch) {
            /// <summary>
            /// 设置全景的视角
            /// </summary>
            /// <param name="heading">水平方向的角度，正北方向为0，正东为90，正南为180，正西为270。</param>
            /// <param name="pitch">竖直方向的角度，向上最大到90度，向下最大到-90度。（在某些场景下，俯角可能无法到达最大值）。</param>
            if (panorama) {
                panorama.setPov({ heading: heading || this.CONSTANT.POV_HEADING, pitch: pitch || this.CONSTANT.POV_PITCH });
            }
        }
    };
    bUtils.tool = {
        CONSTANT: {
            RANDOM_NUMER: 100
        },
        addRandomInViewRange: function (number) {
            /// <summary>
            /// 在可视区域内添加随机标记
            /// 默认100个
            /// </summary>
            /// <param name="number">标记个数</param>
            if (window.bmap) {
                var bounds = window.bmap.getBounds();
                var sw = bounds.getSouthWest();
                var ne = bounds.getNorthEast();
                var lngSpan = Math.abs(sw.lng - ne.lng);
                var latSpan = Math.abs(ne.lat - sw.lat);
                var mkNumber = number || this.CONSTANT.RANDOM_NUMER;
                for (var i = 0; i < mkNumber ; i++) {
                    var marker = bUtils.marker.addWithTitle(sw.lng + lngSpan * (Math.random() * 0.7), ne.lat - latSpan * (Math.random() * 0.7), i);
                    marker.id = i;
                    bUtils.marker.setInfoWindow(marker, "上海啊上海啊", '', '122');
                    bUtils.marker.setLable(marker, i);
                    marker.disableDragging();
                    bUtils.addMarker(marker);
                }
            }
        }
    };
    bUtils.bounds = {
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
                var bounds = new BMap.Bounds();
                for (var i = 0; i < points.length; i++) {
                    var point = points[i];
                    bounds.extend(point);
                }
                var point = bounds.getCenter();
                window.bmap.setCenter(point);
            }
        },
        getExtendedBounds: function (bounds, gridSize) {
            /// <summary>
            /// 获取一个扩展的视图范围，把上下左右都扩大一样的像素值。
            /// </summary>
            /// <param name="bounds">BMap.Map的实例化对象</param>
            /// <param name="gridSize">要扩大的像素值</param>
            /// <returns type="">返回扩大后的视图范围</returns>

            gridSize = gridSize || this.CONSTANT.GRID_SIZE;
            bounds = this.cutBoundsInRange(bounds);
            var pixelNE = window.bmap.pointToPixel(bounds.getNorthEast());
            var pixelSW = window.bmap.pointToPixel(bounds.getSouthWest());
            pixelNE.x += gridSize;
            pixelNE.y -= gridSize;
            pixelSW.x -= gridSize;
            pixelSW.y += gridSize;
            var newNE = window.bmap.pixelToPoint(pixelNE);
            var newSW = window.bmap.pixelToPoint(pixelSW);
            return new BMap.Bounds(newSW, newNE);
        },
        getRealBounds: function (bounds, offset) {
            /// <summary>
            /// 得到实际的bound范围
            /// </summary>
            /// <param name="bounds">BMap.Map的实例化对象</param>
            /// <param name="offset">偏移量</param>
            /// <returns type="BMap.Bounds">实际的bound范围</returns>
            southWest = window.bmap.pointToPixel(bounds.getSouthWest());
            northEast = window.bmap.pointToPixel(bounds.getNorthEast());
            offset = offset || this.CONSTANT.OFF_SET;
            extendSW = {
                x: southWest.x - offset,
                y: southWest.y + offset
            },
            extendNE = {
                x: northEast.x + offset,
                y: northEast.y - offset
            },
            extendSwPoint = window.bmap.pixelToPoint(extendSW),
            extendNePoint = window.bmap.pixelToPoint(extendNE);
            return new BMap.Bounds(extendSwPoint, extendNePoint);
        },
        cutBoundsInRange: function (bounds) {
            /// <summary>
            /// 按照百度地图支持的世界范围对bounds进行边界处理
            /// </summary>
            /// <param name="bounds">BMap.Bounds</param>
            /// <returns type="BMap.Bounds">返回不越界的视图范围</returns>
            var maxX = this.getRange(bounds.getNorthEast().lng, -180, 180);
            var minX = this.getRange(bounds.getSouthWest().lng, -180, 180);
            var maxY = this.getRange(bounds.getNorthEast().lat, -74, 74);
            var minY = this.getRange(bounds.getSouthWest().lat, -74, 74);
            return new BMap.Bounds(new BMap.Point(minX, minY), new BMap.Point(maxX, maxY));
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
    bUtils.overlays = {
        find: function (properties, value) {
            /// <summary>
            /// 查找覆盖物
            /// </summary>
            /// <param name="properties">键</param>
            /// <param name="value">值</param>
            /// <returns type="">找到则返回覆盖物；若没找到则返回NULL</returns>
            var overlays = window.bmap.getOverlays();
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
            var overlays = window.bmap.getOverlays();
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
            var overlays = window.bmap.getOverlays();
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
                overlay.show();
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
                window.bmap.removeOverlay(overlay);
            }
        },
        removeAll: function (properties, value) {
            /// <summary>
            /// 查找所有符合条件的覆盖物并删除
            /// </summary>
            /// <param name="properties">键</param>
            /// <param name="value">值</param>
            var overlays = this.findAll(properties, value);
            for (var i = 0; i < overlays.length; i++) {
                var overlay = overlays[i];
                window.bmap.removeOverlay(overlay);
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
                overlay.hide();
            }
        },
        clearAll: function () {
            /// <summary>
            /// 清除地图上所有的覆盖物
            /// </summary>
            window.bmap.clearOverlays();
        }
    };
    bUtils.marker = {
        add: function (lon, lat) {
            /// <summary>
            /// 创建地图标记
            /// 说明：需要 window.bmap.addOverlay(marker)或MarkerManger进行管理
            /// </summary>
            /// <param name="lon">地理纬度</param>
            /// <param name="lat">地理经度</param>
            /// <returns type="">BMap.Marker</returns>
            if (window.bmap) {
                var marker = new BMap.Marker(new BMap.Point(lon, lat));
                return marker;
            }
        },
        addWithTitle: function (lon, lat, title) {
            /// <summary>
            ///创建标记并且设置toolTip文字
            ///说明：需要 window.bmap.addOverlay(marker)或MarkerManger进行管理
            /// </summary>
            /// <param name="lon">地理纬度</param>
            /// <param name="lat">地理经度</param>
            /// <param name="title">toolTip文字</param>
            /// <returns type="">BMap.Marker</returns>
            var marker = bUtils.marker.add(lon, lat);
            marker.setTitle(title);
            return marker;
        },
        addWithIcon: function (lon, lat, iconpath, size) {
            /// <summary>
            /// 创建标记并且定义图标(不带阴影)
            /// </summary>
            /// <param name="lon">地理纬度</param>
            /// <param name="lat">地理经度</param>
            /// <param name="iconpath">图标路径</param>
            /// <param name="size">BMap.Size</param>
            /// <returns type="">BMap.Marker</returns>
            var icon = new BMap.Icon(iconpath, size);
            var position = new BMap.Point(lon, lat);  // 创建点
            var marker = new BMap.Marker(position, { icon: icon });
            return marker;
        },
        setLable: function (marker, name) {
            /// <summary>
            /// 设置标记显示名称
            /// </summary>
            /// <param name="marker">标记</param>
            /// <param name="name">显示名称</param>
            if (marker) {
                var label = new BMap.Label(name, {
                    offset: new BMap.Size(20, -10)
                });
                marker.setLabel(label);
            }
        },
        setIcon: function (marker, iconpath, size) {
            /// <summary>
            /// 设置标记图标
            /// </summary>
            /// <param name="marker">标记</param>
            /// <param name="iconpath">图标路径</param>
            /// <param name="size">BMap.Size</param>
            if (marker) {
                var icon = new BMap.Icon(iconpath, size);
                marker.setIcon(icon);
            }
        },
        findInBounds: function (properties, value) {
            /// <summary>
            /// 查找可视范围内标记
            /// </summary>
            /// <param name="properties">键</param>
            /// <param name="value">值</param>
            /// <returns type="">若查找到则返回BMap.Marker；若查找不到则返回NULL</returns>
            var bounds = window.bmap.getBounds();
            var overlays = window.bmap.getOverlays();
            for (var i = 0; i < overlays.length; i++) {
                var overlay = overlays[i];
                if (bounds.containsPoint(overlay.getPosition())) {
                    if (overlay[properties] == value)
                        return overlay;
                }
            }
            return null;
        },
        findAllInBounds: function (properties, value) {
            /// <summary>
            /// 查找可视范围内符合条件标记的集合
            /// </summary>
            /// <param name="properties"></param>
            /// <param name="value"></param>
            /// <returns type=""></returns>
            var finded = new Array;
            var bounds = window.bmap.getBounds();
            var overlays = window.bmap.getOverlays();
            for (var i = 0; i < overlays.length; i++) {
                var overlay = overlays[i];
                if (bounds.containsPoint(overlay.getPosition())) {
                    if (overlay[properties] == value)
                        finded.push(overlay);
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
            if (marker) {
                var point = marker.getPosition();
                bUtils.focused(point, zoom);
            }
        },
        showInfoWindow: function (marker, title, message, htmlElement, width, height) {
            /// <summary>
            /// 显示InfoWindow
            /// </summary>
            /// <param name="marker">标记</param>
            /// <param name="title">信息窗标题文字</param>
            /// <param name="message">自定义部分的短信内容</param>
            /// <param name="htmlElement">htmlElement</param>
            /// <param name="width">宽度</param>
            /// <param name="height">高度</param>
            var point = marker.getPosition();
            bUtils.infoWindow.add(point, title, message, htmlElement, true, width, height);
        },
        setInfoWindow: function (marker, title, message, htmlElement, width, height) {
            /// <summary>
            /// 设置marker标记点击时候展现的InfoWindow
            /// </summary>
            /// <param name="marker">标记</param>
            /// <param name="title">信息窗标题文字</param>
            /// <param name="message">自定义部分的短信内容</param>
            /// <param name="htmlElement">htmlElement</param>
            /// <param name="width">宽度</param>
            /// <param name="height">高度</param>
            marker.addEventListener("click", function (m, o) {
                infoWindow = bUtils.infoWindow.create(title, message, htmlElement, width, height);
                marker.openInfoWindow(infoWindow);
            });
        },
        inBound: function (marker) {
            /// <summary>
            /// 判断标记是否在可是范围
            /// </summary>
            /// <param name="marker">标记</param>
            /// <returns type="bool">是否在可是范围内</returns>
            if (marker instanceof BMap.Marker) {
                var bounds = window.bmap.getBounds();
                return bounds.containsPoint(marker.getPosition());
            }
            return false;
        },
        addMenu: function (marker, menuItem) {
            /// <summary>
            /// 为MARKER标记创建右键菜单
            /// eg:
            ///var txtMenuItem = [
            ///         {
            ///             text: '删除坐标',
            ///            callback: function (e, ee, marker) {
            ///                window.bmap.removeOverlay(marker);
            ///            }
            ///        }
            /// </summary>
            /// <param name="marker">标记</param>
            /// <param name="menuItem">txtMenuItem</param>
            if (menuItem.length > 0) {
                var menu = new BMap.ContextMenu();
                for (var i = 0; i < menuItem.length; i++) {
                    menu.addItem(new BMap.MenuItem(menuItem[i].text, menuItem[i].callback, 100));
                }
                marker.addContextMenu(menu);
            }
        }
    };
    bUtils.infoWindow =
        {
            CONSTANT: {
                WIDTH: 250,
                HEIGHT: 80
            },
            create: function (title, message, htmlElement, width, height) {
                /// <summary>
                /// 创建infoWindow
                /// </summary>
                /// <param name="title">信息窗标题文字</param>
                /// <param name="message">自定义部分的短信内容</param>
                /// <param name="htmlElement">htmlElement</param>
                /// <param name="width">宽度</param>
                /// <param name="height">高度</param>
                /// <returns type="">BMap.InfoWindow</returns>
                var sendMessage = false;
                if (message)
                    sendMessage = true;
                var opts = {
                    width: width || this.CONSTANT.WIDTH, //信息窗宽度，单位像素
                    height: height || this.CONSTANT.HEIGHT, //信息窗高度，单位像素
                    title: title, // 信息窗标题文字，支持HTML内容
                    enableMessage: sendMessage, // 设置允许信息窗发送短息
                    message: message// 自定义部分的短信内容，可选项。完整的短信内容包括：自定义部分+位置链接，不设置时，显示默认短信内容。
                };
                infoWindow = new BMap.InfoWindow(htmlElement, opts); // 创建信息窗口对象
                return infoWindow;
            },
            add: function (point, title, message, htmlElement, focused, width, height) {
                /// <summary>
                /// 为point添加infoWindow对象
                /// </summary>
                /// <param name="point">point</param>
                /// <param name="title">信息窗标题文字</param>
                /// <param name="message">自定义部分的短信内容</param>
                /// <param name="htmlElement">htmlElement</param>
                /// <param name="focused">是否聚焦</param>
                /// <param name="width">宽度</param>
                /// <param name="height">高度</param>
                if (point) {
                    infoWindow = this.create(title, message, htmlElement, width, height);
                    window.bmap.openInfoWindow(infoWindow, point);
                    if (focused) {
                        bUtils.focused(point, window.bmap.getZoom());
                    }
                }
            }
        };
    window.bUtils = bUtils;
})();