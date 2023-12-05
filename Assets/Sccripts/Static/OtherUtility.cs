using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

namespace GameUtil
{
    public static class OtherUtility
    {
        #region Property
        /// <summary>
        /// 十万
        /// </summary>
        private const int Hundred_Thousand = 100000;
        /// <summary>
        /// 亿
        /// </summary>
        private const int Hundred_Million = 100000000;

        #endregion

        #region 获得数值的个位、十位、百位数值
        /// <summary>
        /// 获取个位数值
        /// </summary>
        /// <param name="number"></param>
        public static int ReturnSplitUnit(int number)
        {
            return number % 10;
        }

        public static int ReturnSplitTen(int number)
        {
            return Mathf.FloorToInt(number * 0.1f) % 10;
        }

        public static int ReturnSplitHundred(int number)
        {
            return Mathf.FloorToInt(number * 0.01f) % 10;
        }

        public static int ReturnSplitThousand(int number)
        {
            return Mathf.FloorToInt(number * 0.001f) % 10;
        }

        public static void ReturnSplitToTen(int number, out int unit, out int ten)
        {
            unit = number % 10;
            //Mathf.FloorToInt 向下取整
            ten = Mathf.FloorToInt(number * 0.1f) % 10;
        }

        public static void ReturnSplitToHundred(int number, out int unit, out int ten, out int hundred)
        {
            unit = number % 10;
            ten = Mathf.FloorToInt(number * 0.1f) % 10;
            hundred = Mathf.FloorToInt(number * 0.01f) % 10;
        }

        public static void ReturnSplitToThousand(int number, out int unit, out int ten, out int hundred, out int thousand)
        {
            unit = number % 10;
            ten = Mathf.FloorToInt(number * 0.1f) % 10;
            hundred = Mathf.FloorToInt(number * 0.01f) % 10;
            thousand = Mathf.FloorToInt(number * 0.001f) % 10;
        }
        #endregion

        #region 数值转换成字符串
        /// <summary>
        /// 将大数值转换为带有对应单位的字符串
        /// </summary>
        /// <param name="number">数值，不一定为整型，且long / int = long / long</param>
        /// <returns></returns>
        public static string BigNumberToUnitString(double number)
        {
            if (number >= Hundred_Million)
            {
                return $"{GetPreciseDecimal((float)(number / Hundred_Million), 2)}亿";
            }
            else if (number >= Hundred_Thousand)
            {
                //后面所接文字，后续可修改为多语言
                return $"{GetPreciseDecimal((float)(number / Hundred_Thousand), 2) * 10}万";
            }
            else
            {
                return number.ToString();
            }
        }

        /// <summary>
        /// 对应数值保留几位小数,float精度大约为6-9位数字，double精度大约15-17位数字
        /// </summary>
        /// <param name="number">原始数值</param>
        /// <param name="decimalPlaces">保留小数位数</param>
        /// <returns></returns>
        public static float GetPreciseDecimal(float number, int decimalPlaces = 0)
        {
            if (decimalPlaces < 0)
                decimalPlaces = 0;

            int powerNumber = (int)Mathf.Pow(10, decimalPlaces);
            float tmeporary = number * powerNumber;
            return (float)Math.Round(tmeporary / powerNumber, decimalPlaces);
        }

        /// <summary>
        /// 对应数值保留几位小数,float精度大约为6-9位数字，double精度大约15-17位数字
        /// </summary>
        /// <param name="number">原始数值</param>
        /// <param name="decimalPlaces">保留小数位数</param>
        /// <returns></returns>
        public static double GetPreciseDecimal(double number, int decimalPlaces = 0)
        {
            if (decimalPlaces < 0)
                decimalPlaces = 0;

            int powerNumber = (int)Mathf.Pow(10, decimalPlaces);
            double tmeporary = number * powerNumber;
            //Math.Round，参数二将双精度浮点值舍入到指定数量的小数位数。
            return Math.Round(tmeporary / powerNumber, decimalPlaces);
        }

        /// <summary>
        /// 数字0-9转换为中文数字
        /// </summary>
        /// <param name="num"></param>
        /// <returns>中文大写数值</returns>
        public static string SingleDigitsNumberToChinese(int num = 0)
        {
            num = Mathf.Clamp(num, 0, 9);
            //切分出字符数组
            string[] unitAllString = "零,一,二,三,四,五,六,七,八,九".Split(',');
            return unitAllString[num];
        }

        /// <summary>
        /// 数字转换为中文，可以适用任何3位及其以下数值
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string NumberToCNString(int number)
        {
            string numStr = number.ToString();
            string resultStr = "";
            int strLength = numStr.Length;
            //切分出字符数组
            string[] unitAllString = GetSpliteStringFormat("零,一,二,三,四,五,六,七,八,九");
            string units = "", tens = "", hundreds = "";
            string tenStr = "十";
            string hundredStr = "百";

            for (int i = 1; i <= strLength; i++)
            {
                //Substring第一个参数为从第几个字符索引位置开始截取， 参数二为截取的长度
                int sNum = Convert.ToInt32(numStr.Substring(i - 1, 1));
                string cnStr = unitAllString[sNum];
                if (i == 1)
                {
                    units = cnStr;
                    resultStr = cnStr;
                }
                else if (i == 2)
                {
                    tens = cnStr;
                    //判断十位是否是0
                    if (tens == unitAllString[0])
                    {
                        if (units == unitAllString[1])
                            resultStr = tenStr;
                        else
                            //例如二十，三十
                            resultStr = units + tenStr;
                    }
                    else
                    {
                        if (units == unitAllString[1])
                            resultStr = tenStr + tens;
                        else
                            resultStr = units + tenStr + tens;
                    }
                }
                else if (i == 3)
                {
                    hundreds = cnStr;
                    //判断百位是否是0
                    if (hundreds == unitAllString[0])
                    {
                        if (tens.Equals(unitAllString[0]))
                            resultStr = units + hundredStr;
                        else
                            //例如一百一，二百一
                            resultStr = units + hundredStr + tens;
                    }
                    else if (tens == unitAllString[0])
                        resultStr = units + hundredStr + tens + hundreds;
                    else
                        resultStr = units + hundredStr + tens + tenStr + hundreds;
                }
            }
            return resultStr;
        }

        /// <summary>
        /// 数值转成英文序号格式（小于100的数值）
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string NumberToSequence(long num = 1)
        {
            string temporaryStr = "";
            if (num > 100)
                return temporaryStr;
            if (num % 10 == 1)
                temporaryStr = "ST";
            else if (num % 10 == 2)
                temporaryStr = "ND";
            else if (num % 10 == 3)
                temporaryStr = "RD";
            else
                temporaryStr = "TH";
            return num.ToString() + temporaryStr;
        }
        #endregion

        #region 正则表达式检测相关
        #region 数值相关
        /// <summary>
        /// 是否是正整数（不包含0）
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsPositiveInteger(string inputStr)
        {
            //这里是我们使用的匹配规则，使用@（逐字字符串标识符）将原义解释为字符串
            Regex reg = new Regex(@"^\+?[1-9]\d*$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是负整数（不包含0）
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsNegateInteger(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex(@"^-[1-9]\d*$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是整数
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsInteger(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex(@"^(?:0|(?:-?[1-9]\d*))$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputStr">例如1.01</param>
        /// <returns></returns>
        public static bool IsFloat(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex(@"^(-?[1-9]\d*\.\d+|-?0\.\d*[1-9]\d*|0\.0+)$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是浮点数（严格）
        /// </summary>
        /// <param name="inputStr">例如1.01</param>
        /// <returns></returns>
        public static bool IsStrictlyFloat(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex(@"^(-?[1-9]\d*\.\d+|-?0\.\d*[1-9]\d*|0\.0+)$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否只包含数字
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsOnlyNumber(string inputStr)
        {
            Regex reg = new Regex(@"^\d+$");
            return reg.IsMatch(inputStr);
        }
        #endregion

        #region 计算机网络相关
        /// <summary>
        /// 是否是电子邮箱
        /// </summary>
        /// <param name="inputStr">例如10086@qq.com</param>
        /// <returns></returns>
        public static bool IsEmail(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex(@"^[A-Za-z0-9\u4e00-\u9fa5]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是计算机域名（非网址, 不包含协议）
        /// </summary>
        /// <param name="inputStr">例如www.baidu.com</param>
        /// <returns></returns>
        public static bool IsInternetDomainName(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex(@"^([0-9a-zA-Z-]{1,}\.)+([a-zA-Z]{2,})$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 必须是带端口号的网址(或ip)
        /// </summary>
        /// <param name="inputStr">例如https://www.qq.com:8080</param>
        /// <returns></returns>
        public static bool IsIpAddressHavePort(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex(@"^((ht|f)tps?:\/\/)?[\w-]+(\.[\w-]+)+:\d{1,5}\/?$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是统一资源标识符
        /// </summary>
        /// <param name="inputStr">例如www.qq.com</param>
        /// <returns></returns>
        public static bool IsURL(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex(@"^(((ht|f)tps?):\/\/)?([^!@#$%^&*?.\s-]([^!@#$%^&*?.\s]{0,63}[^!@#$%^&*?.\s])?\.)+[a-z]{2,6}\/?");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是子网掩码（不包含0.0.0.0）
        /// </summary>
        /// <param name="inputStr">例如255.255.255.0</param>
        /// <returns></returns>
        public static bool IsSubNetMask(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex(@"^(254|252|248|240|224|192|128)\.0\.0\.0|255\.(254|252|248|240|224|192|128|0)\.0\.0|255\.255\.(254|252|248|240|224|192|128|0)\.0|255\.255\.255\.(255|254|252|248|240|224|192|128|0)$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是迅雷链接
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsThunderURL(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex(@"^thunderx?:\/\/[a-zA-Z\d]+=$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是磁力链接
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsMagneticLinkURL(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex(@"^magnet:\?xt=urn:btih:[0-9a-fA-F]{40,}.*$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是IPV4端口
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsIPV4Port(string inputStr)
        {
            Regex reg = new Regex(@"^((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.){3}(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])(?::(?:[0-9]|[1-9][0-9]{1,3}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5]))?$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是Mac地址
        /// </summary>
        /// <param name="inputStr">例如：38:f9:d3:4b:f5:51或00-0C-29-CA-E4-66</param>
        /// <returns></returns>
        public static bool IsMacAddress(string inputStr)
        {
            Regex reg = new Regex(@"^((([a-f0-9]{2}:){5})|(([a-f0-9]{2}-){5}))[a-f0-9]{2}$");
            return reg.IsMatch(inputStr);
        }
        #endregion

        #region 输入检测、密码检测相关
        /// <summary>
        /// 是否是md5格式（32位）
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool CheckMD5Format(string inputStr)
        {
            Regex reg = new Regex(@"^[a-fA-F0-9]{32}$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// QQ号格式是否正确
        /// </summary>
        /// <param name="inputStr">例如1210420078</param>
        /// <returns></returns>
        public static bool CheckQQFormat(string inputStr)
        {
            Regex reg = new Regex(@"^[1-9][0-9]{4,10}$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 账号是否合法（字母开头，允许5-16字节，允许字母数字下划线组合）
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool CheckAccountIsLegitimate(string inputStr)
        {
            Regex reg = new Regex(@"^[a-zA-Z]\w{4,15}$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是数字和字母组成
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsNumberAndLetterStruct(string inputStr)
        {
            Regex reg = new Regex("^[A-Za-z0-9]+$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是小写英文字母组成
        /// </summary>
        /// <param name="inputStr">例如russel</param>
        /// <returns></returns>
        public static bool IsLowercaseLetterStruct(string inputStr)
        {
            Regex reg = new Regex("^[a-z]+$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否由大写英文字母组成
        /// </summary>
        /// <param name="inputStr">例如ABC</param>
        /// <returns></returns>
        public static bool IsUppercaseLetterStruct(string inputStr)
        {
            Regex reg = new Regex("^[A-Z]+$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 密码强度校验，最少6位，包括至少1个大写字母，1个小写字母，1个数字，1个特殊字符
        /// </summary>
        /// <param name="inputStr">例如Zc@bilibili66666</param>
        /// <returns></returns>
        public static bool CheckPasswordStrength(string inputStr)
        {
            Regex reg = new Regex(@"^\S*(?=\S{6,})(?=\S*\d)(?=\S*[A-Z])(?=\S*[a-z])(?=\S*[!@#$%^&*? ])\S*$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否由中文和数字组成
        /// </summary>
        /// <param name="inputStr">例如：你好6啊</param>
        /// <returns></returns>
        public static bool IsChineseAndNumberStruct(string inputStr)
        {
            Regex reg = new Regex(@"^((?:[\u3400-\u4DB5\u4E00-\u9FEA\uFA0E\uFA0F\uFA11\uFA13\uFA14\uFA1F\uFA21\uFA23\uFA24\uFA27-\uFA29]|[\uD840-\uD868\uD86A-\uD86C\uD86F-\uD872\uD874-\uD879][\uDC00-\uDFFF]|\uD869[\uDC00-\uDED6\uDF00-\uDFFF]|\uD86D[\uDC00-\uDF34\uDF40-\uDFFF]|\uD86E[\uDC00-\uDC1D\uDC20-\uDFFF]|\uD873[\uDC00-\uDEA1\uDEB0-\uDFFF]|\uD87A[\uDC00-\uDFE0])|(\d))+$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否不包含字母
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsWithoutAlphabet(string inputStr)
        {
            Regex reg = new Regex("^[^A-Za-z]*$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 大写字母，小写字母，数字，特殊符号 `@#$%^&*`~()-+=` 中任意3项密码
        /// </summary>
        /// <param name="inputStr">例如：a1@,A1@</param>
        /// <returns></returns>
        public static bool CheckPasswordHaveHowManyType(string inputStr)
        {
            Regex reg = new Regex(@"^(?![a-zA-Z]+$)(?![A-Z0-9]+$)(?![A-Z\W_!@#$%^&*`~()-+=]+$)(?![a-z0-9]+$)(?![a-z\W_!@#$%^&*`~()-+=]+$)(?![0-9\W_!@#$%^&*`~()-+=]+$)[a-zA-Z0-9\W_!@#$%^&*`~()-+=]");
            return reg.IsMatch(inputStr);
        }
        #endregion

        #region 身份证检测、国家及地区检测
        /// <summary>
        /// 是否是中国的省份
        /// </summary>
        /// <param name="inputStr">例如浙江、台湾</param>
        /// <returns></returns>
        public static bool IsChineseProvinces(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex("^浙江|上海|北京|天津|重庆|黑龙江|吉林|辽宁|内蒙古|河北|新疆|甘肃|青海|陕西|宁夏|河南|山东|山西|安徽|湖北|湖南|江苏|四川|贵州|云南|广西|西藏|江西|广东|福建|台湾|海南|香港|澳门$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是身份证号（1代，15位数字）
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsIdentityCard_1(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex(@"^[1-9]\d{7}(?:0\d|10|11|12)(?:0[1-9]|[1-2][\d]|30|31)\d{3}$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是身份证号（2代，18位数字，最后一位是校验位,可能为数字或字符X）
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsIdentityCard_2(string inputStr)
        {
            Regex reg = new Regex(@"^[1-9]\d{5}(?:18|19|20)\d{2}(?:0[1-9]|10|11|12)(?:0[1-9]|[1-2]\d|30|31)\d{3}[\dXx]$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是香港身份证
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsHongkongIdentityCard(string inputStr)
        {
            Regex reg = new Regex(@"^[a-zA-Z]\d{6}\([\dA]\)$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是澳门身份证
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsMacauIdentityCard(string inputStr)
        {
            Regex reg = new Regex(@"^[1|5|7]\d{6}\(\d\)$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是台湾身份证
        /// </summary>
        /// <param name="inputStr">例如：U193683453</param>
        /// <returns></returns>
        public static bool IsTaiwanIdentityCard(string inputStr)
        {
            Regex reg = new Regex(@"^[a-zA-Z][0-9]{9}$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是中国的邮政编码
        /// </summary>
        /// <param name="inputStr">例如100101</param>
        /// <returns></returns>
        public static bool IsChinesePostalCode(string inputStr)
        {
            Regex reg = new Regex(@"^(0[1-7]|1[0-356]|2[0-7]|3[0-6]|4[0-7]|5[1-7]|6[1-7]|7[0-5]|8[013-6])\d{4}$");
            return reg.IsMatch(inputStr);
        }
        #endregion

        #region 字符串拆分与替换
        /// <summary>
        /// 按照,#+|，将字符串进行拆分
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string[] GetSpliteStringFormat(string inputStr)
        {
            Regex reg = new Regex("[,#+|]");
            return reg.Split(inputStr);
        }

        /// <summary>
        /// 用特定格式替换字符串中的，所有空格、换行符、新行、tab字符
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="replaceStr">要替换成的字符</param>
        /// <returns></returns>
        public static string ReplaceSpaceByFormat(string inputStr, string replaceStr = "")
        {
            Regex reg = new Regex(@"\s");
            return reg.Replace(inputStr, replaceStr);
        }

        /// <summary>
        /// 仅以指定格式替换左右两端的空格，string里的 Trim也可删除左右两端空格
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="replaceStr">要替换成的字符</param>
        /// <returns></returns>
        public static string ReplaceTrimSpaceByFormat(string inputStr, string replaceStr = "")
        {
            Regex reg = new Regex(@"(^\s*)|(\s*$)");
            return reg.Replace(inputStr, replaceStr);
        }

        /// <summary>
        /// 仅以指定格式替换左端的空格，string里的 TrimStart也可删除左端空格
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="replaceStr">要替换成的字符</param>
        /// <returns></returns>
        public static string ReplaceTrimStartByFormat(string inputStr, string replaceStr = "")
        {
            Regex reg = new Regex(@"(^\s*)");
            return reg.Replace(inputStr, replaceStr);
        }

        /// <summary>
        /// 仅以指定格式替换右端的空格，string里的 TrimEnd也可删除右端空格
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="replaceStr">要替换成的字符</param>
        /// <returns></returns>
        public static string ReplaceTrimEndByFormat(string inputStr, string replaceStr = "")
        {
            Regex reg = new Regex(@"(\s*$)");
            return reg.Replace(inputStr, replaceStr);
        }
        #endregion

        #region 其他
        /// <summary>
        /// 版本号(version)格式必须为X.Y.Z
        /// </summary>
        /// <param name="inputStr">例如16.3.10</param>
        /// <returns></returns>
        public static bool CheckVersionForamt(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex(@"^\d+(?:\.\d+){2}$");
            return reg.IsMatch(inputStr);
        }

        /// <summary>
        /// 是否是16进制颜色格式
        /// </summary>
        /// <param name="inputStr">例如#f00,#000,#fe9de8</param>
        /// <returns></returns>
        public static bool IsBas16ColorFormat(string inputStr)
        {
            //这里是我们使用的匹配规则
            Regex reg = new Regex(@"^#?([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$");
            return reg.IsMatch(inputStr);
        }
        #endregion

        #endregion
    }
}