<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="storeTest.aspx.cs" Inherits="YanZhiwei.JavaScript.Utilities.store.js.storeTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>store.js Test</title>
    <script src="json.js" type="text/javascript"></script>
    <script src="store.js" type="text/javascript"></script>
    <script src="storeWithExpiration.js" type="text/javascript"></script>
    <script type="text/javascript">
        if (store.enabled) {
            store.set('person', { name: 'yanzhiwei', age: 100 });
            var _person = store.get('person');
            if (_person != null) {
                console.log(_person.name + ':' + _person.age);
            }
            storeWithExpiration.set('foo', 'bar', 1000)
            setTimeout(function () { console.log(storeWithExpiration.get('foo')) }, 500) // -> "bar"
            setTimeout(function () { console.log(storeWithExpiration.get('foo')) }, 1500) // -> null
        }

        console.log('store enable:' + store.enabled);
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
