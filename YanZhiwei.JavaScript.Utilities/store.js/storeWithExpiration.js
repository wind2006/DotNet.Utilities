/// <reference path="store.js" />
var storeWithExpiration = {
    set: function (key, val, exp) {
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="exp">过期时间，毫秒</param>
        store.set(key, { val: val, exp: exp, time: new Date().getTime() })
    },
    get: function (key) {
        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="key">键</param>
        var _info = store.get(key)
        if (!_info) { return null }
        if (new Date().getTime() - _info.time > _info.exp) {
            return null
        }
        return _info.val
    }
}