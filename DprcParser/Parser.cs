using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DprcParser
{
    public class Parser
    {
        static CookieContainer cookies = new CookieContainer();
        static string currentInvoiceNumber = "";

        //GET request method
        private static string GetHttp(string url, Encoding encoding)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var reqGET = (HttpWebRequest)WebRequest.Create(url);
            reqGET.CookieContainer = cookies;
            reqGET.Method = "GET";

            System.Net.HttpWebResponse resp = (HttpWebResponse)reqGET.GetResponse();
            SetCookie(resp);
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream, encoding);
            string html = sr.ReadToEnd();
            return html;
        }

        
        private static void SetCookie(System.Net.HttpWebResponse resp)
        {
            // add cookies from set-cookies container
            string[] cookieVal = null;
            if (resp.Headers["Set-Cookie"] != null)
                cookieVal = resp.Headers["Set-Cookie"].Split(new char[] { ',' });
            if (cookieVal != null)
            {
                foreach (string cook in cookieVal)
                {
                    string[] cookie1 = cook.Split(new char[] { ';' });
                    if (cookie1.Length < 2)
                        continue;
                    cookies.Add(new Cookie(cookie1[0].Split(new char[] { '=' })[0], cookie1[0].Split(new char[] { '=' })[1],
                    cookie1[1].Split(new char[] { '=' })[1], cookie1.Length > 2 ? cookie1[2].Split(new char[] { '=' })[1] : "localhost"));
                }
            }
            //add response cookies
            cookies.Add(resp.Cookies);
        }

        //POST request method
        private static string PostHttp(string url, string data)
        {
            ServicePointManager.Expect100Continue = false;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookies;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            using (var requestStream = request.GetRequestStream())
            using (var writer = new StreamWriter(requestStream))
                writer.Write(data);
            using (var responseStream = request.GetResponse().GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                var response = (HttpWebResponse)request.GetResponse();
                var result = reader.ReadToEnd();
                SetCookie(response);
                return result;
            }
        }

        /// <summary>
        /// Reqistartion at dprc.gov.ua method
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user password</param>
        /// <param name="confirmPassword">confirm password value</param>
        /// <param name="name">username</param>
        /// <param name="surname">user surname</param>
        /// <param name="phone">user mobile phone</param>
        /// <param name="captcha">captcha value</param>
        public static void Registration(string email,string password, string confirmPassword, string name, string surname, string phone, string captcha)
        {
            string url = "http://dprc.gov.ua";
            string data = "email=" + email + "&password1=" +password + "&password2=" + confirmPassword + "&name1=" + name + "&name2=" + surname + "&phone=" + phone + "&captcha_value=" + captcha + "&do_submit=%D0%9F%D0%BE%D0%B4%D1%82%D0%B2%D0%B5%D1%80%D0%B4%D0%B8%D1%82%D1%8C";
            PostHttp(url, data);
        }
    }
}
