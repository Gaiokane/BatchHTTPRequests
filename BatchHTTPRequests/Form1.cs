using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace BatchHTTPRequests
{
    public partial class Form1 : Form
    {
        /*
         * 1.定时执行（执行频率）
         * 2.执行次数
         * 3.支持变量替换等操作
         * 4.自定义请求
         * 5.
         * 6.批量执行（执行间隔时间）
         */

        string[] sqlQuerys;
        static string noMatch = "";

        Regex rgGetDateTimeAll = new Regex("{{time(d|h|m|s)(\\+|\\-)\\d*:\\d{4}-(0?[1-9]|1[0-2])-((0?[1-9])|((1|2)[0-9])|30|31) (((0|1)[0-9])|(2[0-3])):((0|1|2|3|4|5)[0-9]):((0|1|2|3|4|5)[0-9])}}");//{{time(d|h|m|s)(+|-)7:2020-03-29 20:00:00}}取整块 日、小时、分钟、秒
        Regex rgGetDateTimeDiff = new Regex("(d|h|m|s)(\\+|\\-)\\d*");//{{timed+-7:2020-03-29 20:00:00}}取(d|h|m|s)(+|-)数字
        Regex rgGetDateTime = new Regex("\\d{4}-(0?[1-9]|1[0-2])-((0?[1-9])|((1|2)[0-9])|30|31) (((0|1)[0-9])|(2[0-3])):((0|1|2|3|4|5)[0-9]):((0|1|2|3|4|5)[0-9])");//{{timed+-7:2020-03-29 20:00:00}}取时间

        Regex rgGetDateAll = new Regex("{{date(\\+|\\-)\\d*:\\d{4}-(0?[1-9]|1[0-2])-((0[1-9])|((1|2)[0-9])|30|31)}}");//{{date(+|-)7:2020-03-29}}取整块 日
        Regex rgGetDateDiff = new Regex("(\\+|\\-)\\d*");//{{date+-7:2020-03-29 20:00:00}}取(+|-)数字
        Regex rgGetDate = new Regex("\\d{4}-(0?[1-9]|1[0-2])-((0[1-9])|((1|2)[0-9])|30|31)");//{{timed+-7:2020-03-29}}取时间

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //PUT 环境质量初始数据
            putTemplate();

            //POST 污染源初始数据
            //postTemplate();


            textBox1.Select();
            textBox1.Focus();

            //MessageBox.Show(DateTimeToTstamp(Convert.ToDateTime("2021-07-19 20:09:28")).ToString());
            //MessageBox.Show(TstampToDateTime("1626696568").ToString("yyyy-MM-dd HH:mm:ss"));
            //MessageBox.Show(ConvertJsonString("{\"aaa\":\"123\"}"));

            //RichTextBox增加右键菜单
            RichTextBoxMenu richTextBoxMenu_richTextBox1 = new RichTextBoxMenu(richTextBox1);
            RichTextBoxMenu richTextBoxMenu_richTextBox2 = new RichTextBoxMenu(richTextBox2);

        }

        //PUT 环境质量初始数据
        private void putTemplate()
        {
            #region 环境质量初始数据
            richTextBox1.Text = "{\"t0\":\"1627660800000000000\",\"t1\":\"1627660800000000000\",\"name\":\"point_audit_1\"}";
            richTextBox1.Text = "{\"t0\":\"{{timed+1:2021-07-08 00:00:00}}\",\"t1\":\"{{timed+1:2021-07-08 00:00:00}}\",\"name\":\"point_audit_1\",\"matcher-sn\":\"sn =~ /^(210)$/\",\"sn\":\"210\"}";
            richTextBox1.Text = "{\"t0\":\"{{timed+1:2020-06-01 00:00:00}}\",\"t1\":\"{{timed+1:2021-07-08 00:00:00}}\",\"name\":\"point_audit_1\"}";

            textBox2.Text = "http://192.168.30.73:9002/admin/task";
            textBox2.Text = "http://192.168.90.244:9002/admin/task";

            textBox1.Text = "407";
            #endregion
        }

        //POST 污染源初始数据
        private void postTemplate()
        {
            #region 污染源初始数据
            richTextBox1.Text = "{\"portIds\":\"1\",\"portType\":1,\"rule\":true,\"portGroup\":50,\"timeGroup\":10,\"beginTime\":\"{{date+1:2021-04-14}}\",\"endTime\":\"{{date+1:2021-04-14}}\",\"task\":\"gen\"}";

            textBox2.Text = "http://192.168.30.77:8082/v1/audit";

            textBox1.Text = "30";
            #endregion
        }

        private void btn_PUT_template_Click(object sender, EventArgs e)
        {
            //PUT 环境质量初始数据
            putTemplate();
        }

        private void btn_POST_template_Click(object sender, EventArgs e)
        {
            //POST 污染源初始数据
            postTemplate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string toJson = ConvertJsonString(richTextBox1.Text);

            if (JsonSplit.IsJson(toJson))//传入的json串
            {
                MessageBox.Show("json格式合法");
            }
            else
            {
                MessageBox.Show("json格式不合法");
            }

            MessageBox.Show(toJson);
        }

        private void btn_DELETE_Click(object sender, EventArgs e)
        {
            MessageBox.Show("empty");
        }

        private void btn_POST_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("empty");

            /*WebClient wc = new WebClient();
            string strUrlPara = "{\"t0\":\"1627660800000000000\",\"t1\":\"1627660800000000000\",\"name\":\"point_audit_1\"}";
            strUrlPara = HttpUtility.UrlEncode(strUrlPara);
            byte[] data = new ASCIIEncoding().GetBytes(strUrlPara);
            byte[] responseArray = wc.UploadData("http://192.168.30.73:9002/admin/task", data);
            var response = Encoding.UTF8.GetString(responseArray);
            //return response;
            MessageBox.Show(response);*/



            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("执行次数不能为空！");
                textBox1.Focus();
            }
            else
            {
                int times = Convert.ToInt32(textBox1.Text.Trim());
                sqlQuerys = new string[times];
                for (int i = 0; i < times; i++)
                {
                    sqlQuerys[i] = richTextBox1.Text.Trim();
                }

                DateTime dt = DateTime.Now;
                string str = "";
                string type = "";
                string symbol = "";
                int length = 0;

                #region 判断是否有匹配{{date(+|-)7:date}}
                //判断是否有匹配{{date(+|-)7:date}}
                if (rgGetDateAll.IsMatch(sqlQuerys[0]))
                {
                    Match matchDateAll;
                    Match matchDateDiff;
                    Match matchDate;
                    matchDateAll = rgGetDateAll.Match(sqlQuerys[0]);//{{date(+|-)7:2020-03-29}}取整块 日
                    matchDateDiff = rgGetDateDiff.Match(matchDateAll.Groups[0].Value);//{{date+-7:2020-03-29}}取(+|-)数字
                    matchDate = rgGetDate.Match(matchDateAll.Groups[0].Value);//{{date+-7:2020-03-29}}取时间
                    dt = Convert.ToDateTime(matchDate.Groups[0].Value);
                    str = matchDateDiff.Groups[0].Value;//取(d|h|m|s)(+|-)数字
                    symbol = str.Substring(0, 1);//(+|-)
                    length = Convert.ToInt32(str.Substring(1, str.Length - 1));//数字


                    //MessageBox.Show("true");
                    getNewDate(sqlQuerys);
                }
                else
                {
                    //MessageBox.Show("没有匹配项{{date(+|-)7:date}}");
                    noMatch += "没有匹配项{{date(+|-)7:date}}\n";
                }
                #endregion

                JavaScriptSerializer s = new JavaScriptSerializer();

                richTextBox2.Text = "";

                for (int i = 0; i < times; i++)
                {
                    Dictionary<string, object> JsonData = (Dictionary<string, object>)s.DeserializeObject(ConvertJsonString(sqlQuerys[i]));
                    //Dictionary<string, string> JsonData = (Dictionary<string, string>)JsonConvert.DeserializeObject(ConvertJsonString(richTextBox1.Text));
                    //Dictionary<string, string> JsonData = richTextBox1.Text.Trim().Split(',').ToDictionary(s => s.Split(':')[0].Replace("\"", ""), s => s.Split(':')[1].Replace("\"", ""));
                    //MessageBox.Show(HttpPut("http://192.168.30.73:9002/admin/task", obj));

                    try
                    {
                        //string result = HttpPut("http://192.168.30.73:9002/admin/task", JsonData);
                        string result = HttpPost(textBox2.Text.Trim(), JsonData);
                        if (result != "{\"msg\":\"ok\"}")
                        {
                            //MessageBox.Show(dt.ToString("yyyy-MM-dd HH:mm:ss") + "_" + type + symbol + i + "：" + result);
                            richTextBox2.Text += "\r\n" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "_" + type + symbol + i + "：" + result;
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        richTextBox2.Text += "\r\n" + ex.Message;
                    }

                    //MessageBox.Show(dt.ToString("yyyy-MM-dd HH:mm:ss") + "_" + type + symbol + i + "：" + HttpPut("http://192.168.30.73:9002/admin/task", JsonData));
                }
                MessageBox.Show("执行结束");
            }

        }

        private void btn_PUT_Click(object sender, EventArgs e)
        {
            var obj = new Dictionary<string, object>()
            {
                {"t0", "1627660800000000000"},
                { "t1" , "1627660800000000000"},
                { "name" , "point_audit_1"}
            };


            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("执行次数不能为空！");
                textBox1.Focus();
            }
            else
            {
                int times = Convert.ToInt32(textBox1.Text.Trim());
                sqlQuerys = new string[times];
                for (int i = 0; i < times; i++)
                {
                    sqlQuerys[i] = richTextBox1.Text.Trim();
                }

                DateTime dt = DateTime.Now;
                string str = "";
                string type = "";
                string symbol = "";
                int length = 0;

                #region 判断是否有匹配{{time(d|h|m|s)(+|-)7:datetime}}
                //判断是否有匹配{{time(d|h|m|s)(+|-)7:datetime}}
                if (rgGetDateTimeAll.IsMatch(sqlQuerys[0]))
                {
                    Match matchDateTimeAll;
                    Match matchDateTimeDiff;
                    Match matchDateTime;
                    matchDateTimeAll = rgGetDateTimeAll.Match(sqlQuerys[0]);//{{time(d|h|m|s)(+|-)7:2020-03-29 20:00:00}}取整块 日、小时、分钟、秒
                    matchDateTimeDiff = rgGetDateTimeDiff.Match(matchDateTimeAll.Groups[0].Value);//{{timed+-7:2020-03-29 20:00:00}}取(d|h|m|s)(+|-)数字
                    matchDateTime = rgGetDateTime.Match(matchDateTimeAll.Groups[0].Value);//{{timed+-7:2020-03-29 20:00:00}}取时间
                    dt = Convert.ToDateTime(matchDateTime.Groups[0].Value);
                    str = matchDateTimeDiff.Groups[0].Value;//取(d|h|m|s)(+|-)数字
                    type = str.Substring(0, 1);//(d|h|m|s)
                    symbol = str.Substring(1, 1);//(+|-)
                    length = Convert.ToInt32(str.Substring(2, str.Length - 2));//数字


                    //MessageBox.Show("true");
                    getNewDateTime(sqlQuerys);
                }
                else
                {
                    //MessageBox.Show("没有匹配项{{time(d|h|m|s)(+|-)7:datetime}}");
                    noMatch += "没有匹配项{{time(d|h|m|s)(+|-)7:datetime}}\n";
                }
                #endregion

                JavaScriptSerializer s = new JavaScriptSerializer();

                richTextBox2.Text = "";

                for (int i = 0; i < times; i++)
                {
                    Dictionary<string, object> JsonData = (Dictionary<string, object>)s.DeserializeObject(ConvertJsonString(sqlQuerys[i]));
                    //Dictionary<string, string> JsonData = (Dictionary<string, string>)JsonConvert.DeserializeObject(ConvertJsonString(richTextBox1.Text));
                    //Dictionary<string, string> JsonData = richTextBox1.Text.Trim().Split(',').ToDictionary(s => s.Split(':')[0].Replace("\"", ""), s => s.Split(':')[1].Replace("\"", ""));
                    //MessageBox.Show(HttpPut("http://192.168.30.73:9002/admin/task", obj));

                    try
                    {
                        //string result = HttpPut("http://192.168.30.73:9002/admin/task", JsonData);
                        string result = HttpPut(textBox2.Text.Trim(), JsonData);
                        if (result != "{\"msg\":\"success\"}")
                        {
                            //MessageBox.Show(dt.ToString("yyyy-MM-dd HH:mm:ss") + "_" + type + symbol + i + "：" + result);
                            richTextBox2.Text += "\r\n" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "_" + type + symbol + i + "：" + result;
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        richTextBox2.Text += "\r\n" + ex.Message;
                    }

                    //MessageBox.Show(dt.ToString("yyyy-MM-dd HH:mm:ss") + "_" + type + symbol + i + "：" + HttpPut("http://192.168.30.73:9002/admin/task", JsonData));
                }
                MessageBox.Show("执行结束");
            }

        }

        private string ConvertJsonString(string str)

        {
            try
            {
                //格式化json字符串
                JsonSerializer serializer = new JsonSerializer();
                TextReader tr = new StringReader(str);
                JsonTextReader jtr = new JsonTextReader(tr);
                object obj = serializer.Deserialize(jtr);
                if (obj != null)
                {
                    StringWriter textWriter = new StringWriter();
                    JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                    {
                        Formatting = Formatting.Indented,
                        Indentation = 4,
                        IndentChar = ' '
                    };
                    serializer.Serialize(jsonWriter, obj);
                    return textWriter.ToString();
                }
                else
                {
                    return str;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //url为请求的网址，param参数为需要查询的条件（服务端接收的参数，没有则为null）
        //返回该次请求的响应
        public string HttpGet(string url, Dictionary<String, String> param)
        {
            if (param != null) //有参数的情况下，拼接url
            {
                url = url + "?";
                foreach (var item in param)
                {
                    url = url + item.Key + "=" + item.Value + "&";
                }
                url = url.Substring(0, url.Length - 1);
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;//创建请求
            request.Method = "GET"; //请求方法为GET
            HttpWebResponse res; //定义返回的response
            try
            {
                res = (HttpWebResponse)request.GetResponse(); //此处发送了请求并获得响应
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
            }
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            string content = sr.ReadToEnd(); //响应转化为String字符串
            return content;
        }

        public static string HttpPut(string url, Dictionary<String, object> param)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest; //创建请求
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            //request.AllowReadStreamBuffering = true;
            request.MaximumResponseHeadersLength = 1024;
            request.Method = "Put"; //请求方式为post
            request.AllowAutoRedirect = true;
            request.MaximumResponseHeadersLength = 1024;
            request.ContentType = "application/json";
            JObject json = new JObject();
            if (param.Count != 0) //将参数添加到json对象中
            {
                foreach (var item in param)
                {
                    json.Add(item.Key, item.Value.ToString());
                }
            }
            string jsonstring = json.ToString();//获得参数的json字符串
            byte[] jsonbyte = Encoding.UTF8.GetBytes(jsonstring);
            Stream postStream = request.GetRequestStream();
            postStream.Write(jsonbyte, 0, jsonbyte.Length);
            postStream.Close();
            //发送请求并获取相应回应数据       
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
            }
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            string content = sr.ReadToEnd(); //获得响应字符串
            return content;
        }

        public static string HttpPost(string url, Dictionary<String, object> param)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest; //创建请求
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            //request.AllowReadStreamBuffering = true;
            request.MaximumResponseHeadersLength = 1024;
            request.Method = "POST"; //请求方式为post
            request.AllowAutoRedirect = true;
            request.MaximumResponseHeadersLength = 1024;
            request.ContentType = "application/json";
            JObject json = new JObject();
            if (param.Count != 0) //将参数添加到json对象中
            {
                foreach (var item in param)
                {
                    //MessageBox.Show(item.Value.GetType().ToString());
                    var itemValue = item.Value;
                    if (itemValue.GetType() == typeof(int))
                    {
                        json.Add(item.Key, Convert.ToInt32(itemValue));
                    }
                    if (itemValue.GetType() == typeof(Boolean))
                    {
                        json.Add(item.Key, Convert.ToBoolean(itemValue));
                    }
                    if (itemValue.GetType() == typeof(string))
                    {
                        json.Add(item.Key, Convert.ToString(itemValue));
                    }
                    //json.Add(item.Key, item.Value.ToString());
                }
            }
            string jsonstring = json.ToString();//获得参数的json字符串
            byte[] jsonbyte = Encoding.UTF8.GetBytes(jsonstring);
            Stream postStream = request.GetRequestStream();
            postStream.Write(jsonbyte, 0, jsonbyte.Length);
            postStream.Close();
            //发送请求并获取相应回应数据       
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
            }
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            string content = sr.ReadToEnd(); //获得响应字符串
            return content;
        }

        private void btn_GET_Click(object sender, EventArgs e)
        {
            MessageBox.Show(HttpGet(textBox2.Text.Trim(), null));
            //MessageBox.Show(HTTPHelper.Get("https://www.baidu.com/s?ie=utf-8&mod=1&isbd=1&isid=BB39D62DFB618099&ie=utf-8&f=8&rsv_bp=1&tn=baidu&wd=RestClient&oq=Rest%2526lt%253Blient&rsv_pq=b9fbf0f70005442f&rsv_t=c488k%2BX1FLp26DkB2hECfqAkSnV4FLK9E7FdQKb3VoI%2Bwrw66RSWTKo9%2BSY&rqlang=cn&rsv_enter=0&rsv_dl=tb&rsv_btype=t&bs=RestClient&rsv_sid=undefined&_ss=1&clist=&hsug=&f4s=1&csor=0&_cr1=26925"));
        }

        // 时间戳转为C#格式时间
        private DateTime TstampToDateTime(string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dateTimeStart.Add(toNow);
        }

        // DateTime时间格式转换为Unix时间戳格式
        private long DateTimeToTstamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            long lTime = long.Parse((int)(time - startTime).TotalSeconds + "000000000");
            return lTime;
            //return (int)(time - startTime).TotalSeconds;
        }

        #region 将time(d|h|m|s)(+|-)7:2020-03-29 20:00:00}}指定时间并递增
        /// <summary>
        /// 将time(d|h|m|s)(+|-)7:2020-03-29 20:00:00}}指定时间并递增
        /// </summary>
        /// <param name="sourceSQL">原始SQL数组</param>
        /// <returns>替换完的数组</returns>
        private string[] getNewDateTime(string[] sourceSQL)
        {
            //{{timed+777:2020-04-04 11:47:07}}

            Match matchDateTimeAll;
            Match matchDateTimeDiff;
            Match matchDateTime;

            while (rgGetDateTimeAll.Match(sourceSQL[0]).Success == true)
            {
                for (int i = 0; i < sourceSQL.Length; i++)
                {
                    matchDateTimeAll = rgGetDateTimeAll.Match(sourceSQL[i]);//{{time(d|h|m|s)(+|-)7:2020-03-29 20:00:00}}取整块 日、小时、分钟、秒
                    //matchDateTimeDiff = rgGetDateTimeDiff.Match(sourceSQL[i]);//{{timed+-7:2020-03-29 20:00:00}}取(d|h|m|s)(+|-)数字
                    matchDateTimeDiff = rgGetDateTimeDiff.Match(matchDateTimeAll.Groups[0].Value);//{{timed+-7:2020-03-29 20:00:00}}取(d|h|m|s)(+|-)数字
                    //matchDateTime = rgGetDateTime.Match(sourceSQL[i]);//{{timed+-7:2020-03-29 20:00:00}}取时间
                    matchDateTime = rgGetDateTime.Match(matchDateTimeAll.Groups[0].Value);//{{timed+-7:2020-03-29 20:00:00}}取时间
                    //DateTime dt = new DateTime();
                    DateTime dt = Convert.ToDateTime(matchDateTime.Groups[0].Value);
                    string str = matchDateTimeDiff.Groups[0].Value;//取(d|h|m|s)(+|-)数字
                    string type = str.Substring(0, 1);//(d|h|m|s)
                    string symbol = str.Substring(1, 1);//(+|-)
                    int length = Convert.ToInt32(str.Substring(2, str.Length - 2));//数字

                    //MessageBox.Show(type+"\n"+symbol+"\n"+length.ToString());
                    //MessageBox.Show(dt.AddHours(7).ToString("yyyy-MM-dd HH:mm:ss"));

                    if (type == "d")//日+/-
                    {
                        if (symbol == "+")//+
                        {
                            //用这条，替换的时候 如果有相同匹配对象，会全部替换成同一个值
                            //sourceSQL[i] = sourceSQL[i].Replace(matchDateTimeAll.Groups[0].Value, dt.AddDays(length * i).ToString("yyyy-MM-dd HH:mm:ss"));//日+
                            //用这条，仅替换第一个匹配对象
                            sourceSQL[i] = rgGetDateTimeAll.Replace(sourceSQL[i], DateTimeToTstamp(Convert.ToDateTime(dt.AddDays(length * i).ToString("yyyy-MM-dd HH:mm:ss"))).ToString(), 1);
                        }
                        if (symbol == "-")//-
                        {
                            //用这条，替换的时候 如果有相同匹配对象，会全部替换成同一个值
                            //sourceSQL[i] = sourceSQL[i].Replace(matchDateTimeAll.Groups[0].Value, dt.AddDays(-(length * i)).ToString("yyyy-MM-dd HH:mm:ss"));//日-
                            //用这条，仅替换第一个匹配对象
                            sourceSQL[i] = rgGetDateTimeAll.Replace(sourceSQL[i], DateTimeToTstamp(Convert.ToDateTime(dt.AddDays(-(length * i)).ToString("yyyy-MM-dd HH:mm:ss"))).ToString(), 1);
                        }
                    }
                    if (type == "h")//小时+/-
                    {
                        if (symbol == "+")//+
                        {
                            //用这条，替换的时候 如果有相同匹配对象，会全部替换成同一个值
                            //sourceSQL[i] = sourceSQL[i].Replace(matchDateTimeAll.Groups[0].Value, dt.AddHours(length * i).ToString("yyyy-MM-dd HH:mm:ss"));//小时+
                            //用这条，仅替换第一个匹配对象
                            sourceSQL[i] = rgGetDateTimeAll.Replace(sourceSQL[i], DateTimeToTstamp(Convert.ToDateTime(dt.AddHours(length * i).ToString("yyyy-MM-dd HH:mm:ss"))).ToString(), 1);
                        }
                        if (symbol == "-")//-
                        {
                            //用这条，替换的时候 如果有相同匹配对象，会全部替换成同一个值
                            //sourceSQL[i] = sourceSQL[i].Replace(matchDateTimeAll.Groups[0].Value, dt.AddHours(-(length * i)).ToString("yyyy-MM-dd HH:mm:ss"));//小时-
                            //用这条，仅替换第一个匹配对象
                            sourceSQL[i] = rgGetDateTimeAll.Replace(sourceSQL[i], DateTimeToTstamp(Convert.ToDateTime(dt.AddHours(-(length * i)).ToString("yyyy-MM-dd HH:mm:ss"))).ToString(), 1);
                        }
                    }
                    if (type == "m")//分钟+/-
                    {
                        if (symbol == "+")//+
                        {
                            //用这条，替换的时候 如果有相同匹配对象，会全部替换成同一个值
                            //sourceSQL[i] = sourceSQL[i].Replace(matchDateTimeAll.Groups[0].Value, dt.AddMinutes(length * i).ToString("yyyy-MM-dd HH:mm:ss"));//分钟+
                            //用这条，仅替换第一个匹配对象
                            sourceSQL[i] = rgGetDateTimeAll.Replace(sourceSQL[i], DateTimeToTstamp(Convert.ToDateTime(dt.AddMinutes(length * i).ToString("yyyy-MM-dd HH:mm:ss"))).ToString(), 1);
                        }
                        if (symbol == "-")//-
                        {
                            //用这条，替换的时候 如果有相同匹配对象，会全部替换成同一个值
                            //sourceSQL[i] = sourceSQL[i].Replace(matchDateTimeAll.Groups[0].Value, dt.AddMinutes(-(length * i)).ToString("yyyy-MM-dd HH:mm:ss"));//分钟-
                            //用这条，仅替换第一个匹配对象
                            sourceSQL[i] = rgGetDateTimeAll.Replace(sourceSQL[i], DateTimeToTstamp(Convert.ToDateTime(dt.AddMinutes(-(length * i)).ToString("yyyy-MM-dd HH:mm:ss"))).ToString(), 1);
                        }
                    }
                    if (type == "s")//秒+/-
                    {
                        if (symbol == "+")//+
                        {
                            //用这条，替换的时候 如果有相同匹配对象，会全部替换成同一个值
                            //sourceSQL[i] = sourceSQL[i].Replace(matchDateTimeAll.Groups[0].Value, dt.AddSeconds(length * i).ToString("yyyy-MM-dd HH:mm:ss"));//秒+
                            //用这条，仅替换第一个匹配对象
                            sourceSQL[i] = rgGetDateTimeAll.Replace(sourceSQL[i], DateTimeToTstamp(Convert.ToDateTime(dt.AddSeconds(length * i).ToString("yyyy-MM-dd HH:mm:ss"))).ToString(), 1);
                        }
                        if (symbol == "-")//-
                        {
                            //用这条，替换的时候 如果有相同匹配对象，会全部替换成同一个值
                            //sourceSQL[i] = sourceSQL[i].Replace(matchDateTimeAll.Groups[0].Value, dt.AddSeconds(-(length * i)).ToString("yyyy-MM-dd HH:mm:ss"));//秒-
                            //用这条，仅替换第一个匹配对象
                            sourceSQL[i] = rgGetDateTimeAll.Replace(sourceSQL[i], DateTimeToTstamp(Convert.ToDateTime(dt.AddSeconds(-(length * i)).ToString("yyyy-MM-dd HH:mm:ss"))).ToString(), 1);
                        }
                    }
                }
            }

            return sourceSQL;
        }
        #endregion

        #region 将date(+|-)7:2020-03-29}}指定时间并递增
        /// <summary>
        /// 将date(+|-)7:2020-03-29}}指定时间并递增
        /// </summary>
        /// <param name="sourceSQL">原始SQL数组</param>
        /// <returns>替换完的数组</returns>
        private string[] getNewDate(string[] sourceSQL)
        {
            //{{date+777:2020-04-04}}

            Match matchDateAll;
            Match matchDateDiff;
            Match matchDate;

            while (rgGetDateAll.Match(sourceSQL[0]).Success == true)
            {
                for (int i = 0; i < sourceSQL.Length; i++)
                {
                    matchDateAll = rgGetDateAll.Match(sourceSQL[i]);//{{date(+|-)7:2020-03-29}}取整块 日
                    matchDateDiff = rgGetDateDiff.Match(matchDateAll.Groups[0].Value);//{{date+-7:2020-03-29}}取(+|-)数字
                    matchDate = rgGetDate.Match(matchDateAll.Groups[0].Value);//{{date+-7:2020-03-29}}取时间
                    DateTime dt = Convert.ToDateTime(matchDate.Groups[0].Value);
                    string str = matchDateDiff.Groups[0].Value;//取(+|-)数字
                    string symbol = str.Substring(0, 1);//(+|-)
                    int length = Convert.ToInt32(str.Substring(1, str.Length - 1));//数字

                    //MessageBox.Show(type+"\n"+symbol+"\n"+length.ToString());
                    //MessageBox.Show(dt.AddHours(7).ToString("yyyy-MM-dd HH:mm:ss"));

                    if (symbol == "+")//+
                    {
                        //用这条，替换的时候 如果有相同匹配对象，会全部替换成同一个值
                        //sourceSQL[i] = sourceSQL[i].Replace(matchDateTimeAll.Groups[0].Value, dt.AddDays(length * i).ToString("yyyy-MM-dd HH:mm:ss"));//日+
                        //用这条，仅替换第一个匹配对象
                        sourceSQL[i] = rgGetDateAll.Replace(sourceSQL[i], Convert.ToDateTime(dt.AddDays(length * i).ToString("yyyy-MM-dd HH:mm:ss")).ToString("yyyy-MM-dd"), 1);
                    }
                    if (symbol == "-")//-
                    {
                        //用这条，替换的时候 如果有相同匹配对象，会全部替换成同一个值
                        //sourceSQL[i] = sourceSQL[i].Replace(matchDateTimeAll.Groups[0].Value, dt.AddDays(-(length * i)).ToString("yyyy-MM-dd HH:mm:ss"));//日-
                        //用这条，仅替换第一个匹配对象
                        sourceSQL[i] = rgGetDateAll.Replace(sourceSQL[i], Convert.ToDateTime(dt.AddDays(-(length * i)).ToString("yyyy-MM-dd HH:mm:ss")).ToString("yyyy-MM-dd"), 1);
                    }
                }
            }

            return sourceSQL;
        }
        #endregion
    }
}