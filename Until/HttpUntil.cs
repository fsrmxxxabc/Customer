using System;
using System.IO;
using System.Net;
using System.Text;

namespace Customer.until
{
    class HttpUntil
    {
        private const String urlClient = "http://10.7.9.186:8080/api/";

        private static HttpWebRequest http;

        private static String result;

        /**
         * get restful
         * path api 接口除却固定的地址外的可变虚拟目录
         * token 令牌
         */
        public static String GetClient(String path, String token)
        {
            //以Get方式调用
            http = WebRequest.Create(new Uri(urlClient + path)) as HttpWebRequest;
            http.Headers.Add("Authorization", token);
            using HttpWebResponse response = http.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            result = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return result;
        }


        /**
         * get restful
         * path api 接口除却固定的地址外的可变虚拟目录
         */
        public static String GetClient(String path)
        {
            //以Get方式调用
            http = WebRequest.Create(new Uri(urlClient + path)) as HttpWebRequest;
            using HttpWebResponse response = http.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            result = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return result;
        }


        /**
         * post restful
         * token 令牌
         * data 需要提交的参数
         * path api 接口除却固定的地址外的可变虚拟目录
         */
        public static String PostClient(String path, StringBuilder data, String token)
        {
            //以Post方式调用

            Uri address = new Uri(urlClient + path);
            http = WebRequest.Create(address) as HttpWebRequest;
            http.Method = "POST";
            http.ContentType = "application/x-www-form-urlencoded";
            http.Headers.Add("token", token);

            /**String id = "789";
            String name = "test";
            StringBuilder data = new StringBuilder();
            //调用HttpUtility需要在.net 4.0框架下，并且添加System.web.dll引用，请自行谷歌
            data.Append("id=" + System.Web.HttpUtility.UrlEncode(id));
            data.Append("&name=" + System.Web.HttpUtility.UrlEncode(name));*/
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());
            http.ContentLength = byteData.Length;
            using (Stream postStream = http.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
                postStream.Close();
            }

            using HttpWebResponse response = http.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            //Console.WriteLine(reader.ReadToEnd());
            result = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return result;
        }


        /**
         * post restful
         * data 需要提交的参数
         * path api 接口除却固定的地址外的可变虚拟目录
         */
        public static String PostClient(String path, StringBuilder data)
        {
            //以Post方式调用

            Uri address = new Uri(urlClient + path);
            http = WebRequest.Create(address) as HttpWebRequest;
            http.Method = "POST";
            http.ContentType = "application/x-www-form-urlencoded";
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());
            http.ContentLength = byteData.Length;
            using (Stream postStream = http.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
                postStream.Close();
            }

            using HttpWebResponse response = http.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            result = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return result;
        }
    }
}
