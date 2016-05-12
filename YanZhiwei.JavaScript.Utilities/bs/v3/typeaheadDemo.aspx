<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="typeaheadDemo.aspx.cs" Inherits="YanZhiwei.JavaScript.Utilities.bs.v3.typeaheadDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="typeahead/typeahead.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="js/bootstrap.js" type="text/javascript"></script>
    <script src="typeahead/typeahead.bundle.js" type="text/javascript"></script>
    <script src="../../Scripts/handlebars-v3.0.3.js" type="text/javascript"></script>
    <script type="text/javascript">

        var country_list = ["Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Anguilla", "Antigua &amp; Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia &amp; Herzegovina", "Botswana", "Brazil", "British Virgin Islands", "Brunei", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Cape Verde", "Cayman Islands", "Chad", "Chile", "China", "Colombia", "Congo", "Cook Islands", "Costa Rica", "Cote D Ivoire", "Croatia", "Cruise Ship", "Cuba", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Estonia", "Ethiopia", "Falkland Islands", "Faroe Islands", "Fiji", "Finland", "France", "French Polynesia", "French West Indies", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guam", "Guatemala", "Guernsey", "Guinea", "Guinea Bissau", "Guyana", "Haiti", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Isle of Man", "Israel", "Italy", "Jamaica", "Japan", "Jersey", "Jordan", "Kazakhstan", "Kenya", "Kuwait", "Kyrgyz Republic", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Mauritania", "Mauritius", "Mexico", "Moldova", "Monaco", "Mongolia", "Montenegro", "Montserrat", "Morocco", "Mozambique", "Namibia", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Norway", "Oman", "Pakistan", "Palestine", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russia", "Rwanda", "Saint Pierre &amp; Miquelon", "Samoa", "San Marino", "Satellite", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "South Africa", "South Korea", "Spain", "Sri Lanka", "St Kitts &amp; Nevis", "St Lucia", "St Vincent", "St. Lucia", "Sudan", "Suriname", "Swaziland", "Sweden", "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Timor L'Este", "Togo", "Tonga", "Trinidad &amp; Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks &amp; Caicos", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "Uruguay", "Uzbekistan", "Venezuela", "Vietnam", "Virgin Islands (US)", "Yemen", "Zambia", "Zimbabwe"];
        $(document).ready(function () {

            //localtypeahead();
            remotetypeahead();
            /*
    'typeahead:initialized',
    'typeahead:initialized:err',
    'typeahead:selected',
    'typeahead:autocompleted',
    'typeahead:cursorchanged',
    'typeahead:opened',
    'typeahead:closed'
* */
            //$('.twitter-typeahead').on([
            //    'typeahead:selected'
            //].join(' '), function (obj, datum) {
            //    debugger;
            //    console.log(this.value);
            //});
        });
        function remotetypeahead() {

            var movies = new Bloodhound({
                datumTokenizer: function (datum) {
                    return Bloodhound.tokenizers.whitespace(datum.value);
                },
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                remote: {
                    url: '../../BackHandler/BaseHandler.ashx?query=%QUERY',
                    filter: function (movies) {
                        return $.map(movies, function (item) {
                            return {
                                name: item.Name,
                                age: item.Age,
                                birth: item.Birth
                            };
                        });
                    }
                }
            });

            movies.initialize();

            $('.twitter-typeahead').typeahead({
                hint: true,
                highlight: true,
                minLength: 1
            }, {
                displayKey: 'name',
                source: movies.ttAdapter(),
                templates: {
                    suggestion: Handlebars.compile("<p style='padding:6px'><b>Name:{{name}}</b> -Age:{{age}}- Birth date:{{birth}} </p>"),
                    footer: Handlebars.compile("<b>Searched for '{{query}}'</b>"),
                    empty: ['<div class="noitems">', '未搜索到.', '</div>'].join('\n')
                }
            }).on('typeahead:selected', function (obj, datum) {
                debugger;
                console.log(obj);
                console.log(datum);
            });

        }
        function localtypeahead() {
            /// <summary>
            /// 本地数据
            /// </summary>

            var _bloodhound = new Bloodhound({
                limit: 10,
                datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                local: $.map(country_list, function (item) {
                    return { value: item };
                })
            });

            _bloodhound.initialize();
            $('.twitter-typeahead').typeahead({
                hint: true,
                highlight: true,
                minLength: 1
            },
            {
                name: 'value',
                displayKey: 'value',
                source: _bloodhound.ttAdapter(),
                templates: {
                    empty: [
                        '<div class="noitems">',
                        '未搜索到.',
                        '</div>'
                    ].join('\n')
                }
            });

        }
    </script>
</head>
<body>
    <form>
        <div class="container">
            <input class="twitter-typeahead form-control" type="text" placeholder="States of USA" />
        </div>
    </form>
</body>
</html>
