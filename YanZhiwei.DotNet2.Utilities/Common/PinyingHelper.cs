namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Collections;
    using System.Text;

    /// <summary>
    /// 汉字拼音帮助类
    /// </summary>
    public static class PinyingHelper
    {
        #region Fields

        /// <summary>
        /// 拼音对应数值存储
        /// </summary>
        /// 日期：2015-10-09 17:23
        /// 备注：
        private static Hashtable pinyinHash;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes the <see cref="PinyingHelper"/> class.
        /// </summary>
        /// 日期：2015-10-09 17:23
        /// 备注：
        static PinyingHelper()
        {
            pinyinHash = new Hashtable();
            pinyinHash.Add(-20319, "a");
            pinyinHash.Add(-20317, "ai");
            pinyinHash.Add(-20304, "an");
            pinyinHash.Add(-20295, "ang");
            pinyinHash.Add(-20292, "ao");
            pinyinHash.Add(-20283, "ba");
            pinyinHash.Add(-20265, "bai");
            pinyinHash.Add(-20257, "ban");
            pinyinHash.Add(-20242, "bang");
            pinyinHash.Add(-20230, "bao");
            pinyinHash.Add(-20051, "bei");
            pinyinHash.Add(-20036, "ben");
            pinyinHash.Add(-20032, "beng");
            pinyinHash.Add(-20026, "bi");
            pinyinHash.Add(-20002, "bian");
            pinyinHash.Add(-19990, "biao");
            pinyinHash.Add(-19986, "bie");
            pinyinHash.Add(-19982, "bin");
            pinyinHash.Add(-19976, "bing");
            pinyinHash.Add(-19805, "bo");
            pinyinHash.Add(-19784, "bu");
            pinyinHash.Add(-19775, "ca");
            pinyinHash.Add(-19774, "cai");
            pinyinHash.Add(-19763, "can");
            pinyinHash.Add(-19756, "cang");
            pinyinHash.Add(-19751, "cao");
            pinyinHash.Add(-19746, "ce");
            pinyinHash.Add(-19741, "ceng");
            pinyinHash.Add(-19739, "cha");
            pinyinHash.Add(-19728, "chai");
            pinyinHash.Add(-19725, "chan");
            pinyinHash.Add(-19715, "chang");
            pinyinHash.Add(-19540, "chao");
            pinyinHash.Add(-19531, "che");
            pinyinHash.Add(-19525, "chen");
            pinyinHash.Add(-19515, "cheng");
            pinyinHash.Add(-19500, "chi");
            pinyinHash.Add(-19484, "chong");
            pinyinHash.Add(-19479, "chou");
            pinyinHash.Add(-19467, "chu");
            pinyinHash.Add(-19289, "chuai");
            pinyinHash.Add(-19288, "chuan");
            pinyinHash.Add(-19281, "chuang");
            pinyinHash.Add(-19275, "chui");
            pinyinHash.Add(-19270, "chun");
            pinyinHash.Add(-19263, "chuo");
            pinyinHash.Add(-19261, "ci");
            pinyinHash.Add(-19249, "cong");
            pinyinHash.Add(-19243, "cou");
            pinyinHash.Add(-19242, "cu");
            pinyinHash.Add(-19238, "cuan");
            pinyinHash.Add(-19235, "cui");
            pinyinHash.Add(-19227, "cun");
            pinyinHash.Add(-19224, "cuo");
            pinyinHash.Add(-19218, "da");
            pinyinHash.Add(-19212, "dai");
            pinyinHash.Add(-19038, "dan");
            pinyinHash.Add(-19023, "dang");
            pinyinHash.Add(-19018, "dao");
            pinyinHash.Add(-19006, "de");
            pinyinHash.Add(-19003, "deng");
            pinyinHash.Add(-18996, "di");
            pinyinHash.Add(-18977, "dian");
            pinyinHash.Add(-18961, "diao");
            pinyinHash.Add(-18952, "die");
            pinyinHash.Add(-18783, "ding");
            pinyinHash.Add(-18774, "diu");
            pinyinHash.Add(-18773, "dong");
            pinyinHash.Add(-18763, "dou");
            pinyinHash.Add(-18756, "du");
            pinyinHash.Add(-18741, "duan");
            pinyinHash.Add(-18735, "dui");
            pinyinHash.Add(-18731, "dun");
            pinyinHash.Add(-18722, "duo");
            pinyinHash.Add(-18710, "e");
            pinyinHash.Add(-18697, "en");
            pinyinHash.Add(-18696, "er");
            pinyinHash.Add(-18526, "fa");
            pinyinHash.Add(-18518, "fan");
            pinyinHash.Add(-18501, "fang");
            pinyinHash.Add(-18490, "fei");
            pinyinHash.Add(-18478, "fen");
            pinyinHash.Add(-18463, "feng");
            pinyinHash.Add(-18448, "fo");
            pinyinHash.Add(-18447, "fou");
            pinyinHash.Add(-18446, "fu");
            pinyinHash.Add(-18239, "ga");
            pinyinHash.Add(-18237, "gai");
            pinyinHash.Add(-18231, "gan");
            pinyinHash.Add(-18220, "gang");
            pinyinHash.Add(-18211, "gao");
            pinyinHash.Add(-18201, "ge");
            pinyinHash.Add(-18184, "gei");
            pinyinHash.Add(-18183, "gen");
            pinyinHash.Add(-18181, "geng");
            pinyinHash.Add(-18012, "gong");
            pinyinHash.Add(-17997, "gou");
            pinyinHash.Add(-17988, "gu");
            pinyinHash.Add(-17970, "gua");
            pinyinHash.Add(-17964, "guai");
            pinyinHash.Add(-17961, "guan");
            pinyinHash.Add(-17950, "guang");
            pinyinHash.Add(-17947, "gui");
            pinyinHash.Add(-17931, "gun");
            pinyinHash.Add(-17928, "guo");
            pinyinHash.Add(-17922, "ha");
            pinyinHash.Add(-17759, "hai");
            pinyinHash.Add(-17752, "han");
            pinyinHash.Add(-17733, "hang");
            pinyinHash.Add(-17730, "hao");
            pinyinHash.Add(-17721, "he");
            pinyinHash.Add(-17703, "hei");
            pinyinHash.Add(-17701, "hen");
            pinyinHash.Add(-17697, "heng");
            pinyinHash.Add(-17692, "hong");
            pinyinHash.Add(-17683, "hou");
            pinyinHash.Add(-17676, "hu");
            pinyinHash.Add(-17496, "hua");
            pinyinHash.Add(-17487, "huai");
            pinyinHash.Add(-17482, "huan");
            pinyinHash.Add(-17468, "huang");
            pinyinHash.Add(-17454, "hui");
            pinyinHash.Add(-17433, "hun");
            pinyinHash.Add(-17427, "huo");
            pinyinHash.Add(-17417, "ji");
            pinyinHash.Add(-17202, "jia");
            pinyinHash.Add(-17185, "jian");
            pinyinHash.Add(-16983, "jiang");
            pinyinHash.Add(-16970, "jiao");
            pinyinHash.Add(-16942, "jie");
            pinyinHash.Add(-16915, "jin");
            pinyinHash.Add(-16733, "jing");
            pinyinHash.Add(-16708, "jiong");
            pinyinHash.Add(-16706, "jiu");
            pinyinHash.Add(-16689, "ju");
            pinyinHash.Add(-16664, "juan");
            pinyinHash.Add(-16657, "jue");
            pinyinHash.Add(-16647, "jun");
            pinyinHash.Add(-16474, "ka");
            pinyinHash.Add(-16470, "kai");
            pinyinHash.Add(-16465, "kan");
            pinyinHash.Add(-16459, "kang");
            pinyinHash.Add(-16452, "kao");
            pinyinHash.Add(-16448, "ke");
            pinyinHash.Add(-16433, "ken");
            pinyinHash.Add(-16429, "keng");
            pinyinHash.Add(-16427, "kong");
            pinyinHash.Add(-16423, "kou");
            pinyinHash.Add(-16419, "ku");
            pinyinHash.Add(-16412, "kua");
            pinyinHash.Add(-16407, "kuai");
            pinyinHash.Add(-16403, "kuan");
            pinyinHash.Add(-16401, "kuang");
            pinyinHash.Add(-16393, "kui");
            pinyinHash.Add(-16220, "kun");
            pinyinHash.Add(-16216, "kuo");
            pinyinHash.Add(-16212, "la");
            pinyinHash.Add(-16205, "lai");
            pinyinHash.Add(-16202, "lan");
            pinyinHash.Add(-16187, "lang");
            pinyinHash.Add(-16180, "lao");
            pinyinHash.Add(-16171, "le");
            pinyinHash.Add(-16169, "lei");
            pinyinHash.Add(-16158, "leng");
            pinyinHash.Add(-16155, "li");
            pinyinHash.Add(-15959, "lia");
            pinyinHash.Add(-15958, "lian");
            pinyinHash.Add(-15944, "liang");
            pinyinHash.Add(-15933, "liao");
            pinyinHash.Add(-15920, "lie");
            pinyinHash.Add(-15915, "lin");
            pinyinHash.Add(-15903, "ling");
            pinyinHash.Add(-15889, "liu");
            pinyinHash.Add(-15878, "long");
            pinyinHash.Add(-15707, "lou");
            pinyinHash.Add(-15701, "lu");
            pinyinHash.Add(-15681, "lv");
            pinyinHash.Add(-15667, "luan");
            pinyinHash.Add(-15661, "lue");
            pinyinHash.Add(-15659, "lun");
            pinyinHash.Add(-15652, "luo");
            pinyinHash.Add(-15640, "ma");
            pinyinHash.Add(-15631, "mai");
            pinyinHash.Add(-15625, "man");
            pinyinHash.Add(-15454, "mang");
            pinyinHash.Add(-15448, "mao");
            pinyinHash.Add(-15436, "me");
            pinyinHash.Add(-15435, "mei");
            pinyinHash.Add(-15419, "men");
            pinyinHash.Add(-15416, "meng");
            pinyinHash.Add(-15408, "mi");
            pinyinHash.Add(-15394, "mian");
            pinyinHash.Add(-15385, "miao");
            pinyinHash.Add(-15377, "mie");
            pinyinHash.Add(-15375, "min");
            pinyinHash.Add(-15369, "ming");
            pinyinHash.Add(-15363, "miu");
            pinyinHash.Add(-15362, "mo");
            pinyinHash.Add(-15183, "mou");
            pinyinHash.Add(-15180, "mu");
            pinyinHash.Add(-15165, "na");
            pinyinHash.Add(-15158, "nai");
            pinyinHash.Add(-15153, "nan");
            pinyinHash.Add(-15150, "nang");
            pinyinHash.Add(-15149, "nao");
            pinyinHash.Add(-15144, "ne");
            pinyinHash.Add(-15143, "nei");
            pinyinHash.Add(-15141, "nen");
            pinyinHash.Add(-15140, "neng");
            pinyinHash.Add(-15139, "ni");
            pinyinHash.Add(-15128, "nian");
            pinyinHash.Add(-15121, "niang");
            pinyinHash.Add(-15119, "niao");
            pinyinHash.Add(-15117, "nie");
            pinyinHash.Add(-15110, "nin");
            pinyinHash.Add(-15109, "ning");
            pinyinHash.Add(-14941, "niu");
            pinyinHash.Add(-14937, "nong");
            pinyinHash.Add(-14933, "nu");
            pinyinHash.Add(-14930, "nv");
            pinyinHash.Add(-14929, "nuan");
            pinyinHash.Add(-14928, "nue");
            pinyinHash.Add(-14926, "nuo");
            pinyinHash.Add(-14922, "o");
            pinyinHash.Add(-14921, "ou");
            pinyinHash.Add(-14914, "pa");
            pinyinHash.Add(-14908, "pai");
            pinyinHash.Add(-14902, "pan");
            pinyinHash.Add(-14894, "pang");
            pinyinHash.Add(-14889, "pao");
            pinyinHash.Add(-14882, "pei");
            pinyinHash.Add(-14873, "pen");
            pinyinHash.Add(-14871, "peng");
            pinyinHash.Add(-14857, "pi");
            pinyinHash.Add(-14678, "pian");
            pinyinHash.Add(-14674, "piao");
            pinyinHash.Add(-14670, "pie");
            pinyinHash.Add(-14668, "pin");
            pinyinHash.Add(-14663, "ping");
            pinyinHash.Add(-14654, "po");
            pinyinHash.Add(-14645, "pu");
            pinyinHash.Add(-14630, "qi");
            pinyinHash.Add(-14594, "qia");
            pinyinHash.Add(-14429, "qian");
            pinyinHash.Add(-14407, "qiang");
            pinyinHash.Add(-14399, "qiao");
            pinyinHash.Add(-14384, "qie");
            pinyinHash.Add(-14379, "qin");
            pinyinHash.Add(-14368, "qing");
            pinyinHash.Add(-14355, "qiong");
            pinyinHash.Add(-14353, "qiu");
            pinyinHash.Add(-14345, "qu");
            pinyinHash.Add(-14170, "quan");
            pinyinHash.Add(-14159, "que");
            pinyinHash.Add(-14151, "qun");
            pinyinHash.Add(-14149, "ran");
            pinyinHash.Add(-14145, "rang");
            pinyinHash.Add(-14140, "rao");
            pinyinHash.Add(-14137, "re");
            pinyinHash.Add(-14135, "ren");
            pinyinHash.Add(-14125, "reng");
            pinyinHash.Add(-14123, "ri");
            pinyinHash.Add(-14122, "rong");
            pinyinHash.Add(-14112, "rou");
            pinyinHash.Add(-14109, "ru");
            pinyinHash.Add(-14099, "ruan");
            pinyinHash.Add(-14097, "rui");
            pinyinHash.Add(-14094, "run");
            pinyinHash.Add(-14092, "ruo");
            pinyinHash.Add(-14090, "sa");
            pinyinHash.Add(-14087, "sai");
            pinyinHash.Add(-14083, "san");
            pinyinHash.Add(-13917, "sang");
            pinyinHash.Add(-13914, "sao");
            pinyinHash.Add(-13910, "se");
            pinyinHash.Add(-13907, "sen");
            pinyinHash.Add(-13906, "seng");
            pinyinHash.Add(-13905, "sha");
            pinyinHash.Add(-13896, "shai");
            pinyinHash.Add(-13894, "shan");
            pinyinHash.Add(-13878, "shang");
            pinyinHash.Add(-13870, "shao");
            pinyinHash.Add(-13859, "she");
            pinyinHash.Add(-13847, "shen");
            pinyinHash.Add(-13831, "sheng");
            pinyinHash.Add(-13658, "shi");
            pinyinHash.Add(-13611, "shou");
            pinyinHash.Add(-13601, "shu");
            pinyinHash.Add(-13406, "shua");
            pinyinHash.Add(-13404, "shuai");
            pinyinHash.Add(-13400, "shuan");
            pinyinHash.Add(-13398, "shuang");
            pinyinHash.Add(-13395, "shui");
            pinyinHash.Add(-13391, "shun");
            pinyinHash.Add(-13387, "shuo");
            pinyinHash.Add(-13383, "si");
            pinyinHash.Add(-13367, "song");
            pinyinHash.Add(-13359, "sou");
            pinyinHash.Add(-13356, "su");
            pinyinHash.Add(-13343, "suan");
            pinyinHash.Add(-13340, "sui");
            pinyinHash.Add(-13329, "sun");
            pinyinHash.Add(-13326, "suo");
            pinyinHash.Add(-13318, "ta");
            pinyinHash.Add(-13147, "tai");
            pinyinHash.Add(-13138, "tan");
            pinyinHash.Add(-13120, "tang");
            pinyinHash.Add(-13107, "tao");
            pinyinHash.Add(-13096, "te");
            pinyinHash.Add(-13095, "teng");
            pinyinHash.Add(-13091, "ti");
            pinyinHash.Add(-13076, "tian");
            pinyinHash.Add(-13068, "tiao");
            pinyinHash.Add(-13063, "tie");
            pinyinHash.Add(-13060, "ting");
            pinyinHash.Add(-12888, "tong");
            pinyinHash.Add(-12875, "tou");
            pinyinHash.Add(-12871, "tu");
            pinyinHash.Add(-12860, "tuan");
            pinyinHash.Add(-12858, "tui");
            pinyinHash.Add(-12852, "tun");
            pinyinHash.Add(-12849, "tuo");
            pinyinHash.Add(-12838, "wa");
            pinyinHash.Add(-12831, "wai");
            pinyinHash.Add(-12829, "wan");
            pinyinHash.Add(-12812, "wang");
            pinyinHash.Add(-12802, "wei");
            pinyinHash.Add(-12607, "wen");
            pinyinHash.Add(-12597, "weng");
            pinyinHash.Add(-12594, "wo");
            pinyinHash.Add(-12585, "wu");
            pinyinHash.Add(-12556, "xi");
            pinyinHash.Add(-12359, "xia");
            pinyinHash.Add(-12346, "xian");
            pinyinHash.Add(-12320, "xiang");
            pinyinHash.Add(-12300, "xiao");
            pinyinHash.Add(-12120, "xie");
            pinyinHash.Add(-12099, "xin");
            pinyinHash.Add(-12089, "xing");
            pinyinHash.Add(-12074, "xiong");
            pinyinHash.Add(-12067, "xiu");
            pinyinHash.Add(-12058, "xu");
            pinyinHash.Add(-12039, "xuan");
            pinyinHash.Add(-11867, "xue");
            pinyinHash.Add(-11861, "xun");
            pinyinHash.Add(-11847, "ya");
            pinyinHash.Add(-11831, "yan");
            pinyinHash.Add(-11798, "yang");
            pinyinHash.Add(-11781, "yao");
            pinyinHash.Add(-11604, "ye");
            pinyinHash.Add(-11589, "yi");
            pinyinHash.Add(-11536, "yin");
            pinyinHash.Add(-11358, "ying");
            pinyinHash.Add(-11340, "yo");
            pinyinHash.Add(-11339, "yong");
            pinyinHash.Add(-11324, "you");
            pinyinHash.Add(-11303, "yu");
            pinyinHash.Add(-11097, "yuan");
            pinyinHash.Add(-11077, "yue");
            pinyinHash.Add(-11067, "yun");
            pinyinHash.Add(-11055, "za");
            pinyinHash.Add(-11052, "zai");
            pinyinHash.Add(-11045, "zan");
            pinyinHash.Add(-11041, "zang");
            pinyinHash.Add(-11038, "zao");
            pinyinHash.Add(-11024, "ze");
            pinyinHash.Add(-11020, "zei");
            pinyinHash.Add(-11019, "zen");
            pinyinHash.Add(-11018, "zeng");
            pinyinHash.Add(-11014, "zha");
            pinyinHash.Add(-10838, "zhai");
            pinyinHash.Add(-10832, "zhan");
            pinyinHash.Add(-10815, "zhang");
            pinyinHash.Add(-10800, "zhao");
            pinyinHash.Add(-10790, "zhe");
            pinyinHash.Add(-10780, "zhen");
            pinyinHash.Add(-10764, "zheng");
            pinyinHash.Add(-10587, "zhi");
            pinyinHash.Add(-10544, "zhong");
            pinyinHash.Add(-10533, "zhou");
            pinyinHash.Add(-10519, "zhu");
            pinyinHash.Add(-10331, "zhua");
            pinyinHash.Add(-10329, "zhuai");
            pinyinHash.Add(-10328, "zhuan");
            pinyinHash.Add(-10322, "zhuang");
            pinyinHash.Add(-10315, "zhui");
            pinyinHash.Add(-10309, "zhun");
            pinyinHash.Add(-10307, "zhuo");
            pinyinHash.Add(-10296, "zi");
            pinyinHash.Add(-10281, "zong");
            pinyinHash.Add(-10274, "zou");
            pinyinHash.Add(-10270, "zu");
            pinyinHash.Add(-10262, "zuan");
            pinyinHash.Add(-10260, "zui");
            pinyinHash.Add(-10256, "zun");
            pinyinHash.Add(-10254, "zuo");
            pinyinHash.Add(-10247, "zz");
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 获得汉字的拼音，如果输入的是英文字符将原样输出，中文标点符号将被忽略
        /// </summary>
        /// <param name="chineseChars">汉字字符串</param>
        /// <returns>拼音</returns>
        public static string GetPinyin(string chineseChars)
        {
            byte[] _bytes = System.Text.Encoding.Default.GetBytes(chineseChars);
            int _value;
            StringBuilder _builder = new StringBuilder(chineseChars.Length * 4);
            for (int i = 0; i < _bytes.Length; i++)
            {
                _value = (int)_bytes[i];
                if (_value > 160)
                {
                    _value = _value * 256 + _bytes[++i] - 65536;
                    _builder.Append(GetPinyin(_value));
                }
                else
                {
                    _builder.Append((char)_value);
                }
            }

            return _builder.ToString();
        }

        /// <summary>
        /// 获得汉字拼音的简写，即每一个汉字的拼音的首字母组成的串，如果输入的是英文字符将原样输出，中文标点符号将被忽略
        /// </summary>
        /// <param name="chineseChars">汉字字符串</param>
        /// <returns>拼音简写</returns>
        public static string GetShortPinyin(string chineseChars)
        {
            byte[] _bytes = Encoding.Default.GetBytes(chineseChars);
            int _value;
            StringBuilder _builder = new StringBuilder(chineseChars.Length * 4);
            for (int i = 0; i < _bytes.Length; i++)
            {
                _value = (int)_bytes[i];
                if (_value > 160)
                {
                    _value = _value * 256 + _bytes[++i] - 65536;
                    string charPinyin = GetPinyin(_value);
                    if (!string.IsNullOrEmpty(charPinyin))
                    {
                        charPinyin = new string(charPinyin[0], 1);
                    }

                    _builder.Append(charPinyin);
                }
                else
                {
                    _builder.Append((char)_value);
                }
            }

            return _builder.ToString();
        }

        /// <summary>
        /// 获得汉字的拼音
        /// </summary>
        /// <param name="charValue">数值</param>
        /// <returns>拼音</returns>
        private static string GetPinyin(int charValue)
        {
            if (charValue < -20319 || charValue > -10247)
            {
                return string.Empty;
            }

            while (!pinyinHash.ContainsKey(charValue))
            {
                charValue--;
            }

            return (string)pinyinHash[charValue];
        }

        #endregion Methods
    }
}