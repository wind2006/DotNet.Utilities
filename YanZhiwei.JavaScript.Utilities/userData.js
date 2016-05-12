var userData = {
    //高版本IE需要兼容性视图才可；
    // 定义userData对象
    o: null,
    // 设置文件过期时间
    defExps: 365,
    init: function () {
        /// <summary>
        /// 初始化userdate对象
        /// </summary>
        if (!userData.o) {
            try {
                userData.o = document.createElement('INPUT');
                userData.o.type = "hidden";
                userData.o.addBehavior("#default#userData");
                document.body.appendChild(userData.o);
            } catch (e) {
                return false;
            }
        };
        return true;
    },

    save: function (f, c, e) {
        /// <summary>
        /// 保存文件到userData文件夹中
        /// </summary>
        /// <param name="f">文件名</param>
        /// <param name="c">文件内容</param>
        /// <param name="e">过期时间</param>
        if (userData.init()) {
            var o = userData.o;
            // 保持对象的一致
            o.load(f);
            // 将传入的内容当作属性存储
            if (c) o.setAttribute("code", c);
            // 设置文件过期时间
            var d = new Date(),
                e = (arguments.length == 3) ? e : userData.defExps;
            d.setDate(d.getDate() + e);
            o.expires = d.toUTCString();
            // 存储为制定的文件名
            o.save(f);
        }
    },
    load: function (f) {
        /// <summary>
        /// 从uerdata文件夹中读取指定文件，并以字符串形式返回
        /// </summary>
        /// <param name="f">文件名</param>
        if (userData.init()) {
            var o = userData.o;
            // 读取文件
            o.load(f);
            // 返回文件内容
            return o.getAttribute("code");
        }
    },
    exist: function (f) {
        /// <summary>
        /// 检查userData文件是否存在
        /// </summary>
        /// <param name="f">文件名</param>
        return userData.load(f) != null;
    },
    remove: function (f) {
        /// <summary>
        /// 删除userData文件夹中的指定文件
        /// </summary>
        /// <param name="f">文件名</param>
        userData.save(f, false, -userData.defExps);
    }
};