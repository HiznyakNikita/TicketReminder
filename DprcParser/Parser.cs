using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TicketReminder.DataClasses;

namespace DprcParser
{
    public class Parser
    {
        public static CookieContainer cookies = new CookieContainer();
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
        public static void Registration(string email, string password, string confirmPassword, string name, string surname, string phone, string captcha)
        {
            //NOT TESTED!!! //NOT NEED IN TODAYS VERSION(REGISTRATION BY WEB BROWSER NOW)
            string url = "http://dprc.gov.ua/register.php";
            string data = "email=" + email + "&password1=" + password + "&password2=" + confirmPassword + "&name1=" + name + "&name2=" + surname + "&phone=" + phone + "&captcha_value=" + captcha + "&do_submit=Подтвердить";
            PostHttp(url, data);
        }

        /// <summary>
        /// User authorization at dprc.gov.ua
        /// </summary>
        /// <param name="login"> user login at dprc.gov.ua</param>
        /// <param name="password">user password at dprc.gov.ua`</param>
        public static void Auth(string login, string password)
        {
            try
            {
                string url = "http://dprc.gov.ua/auth.php";
                string data = "USER_LOGIN=" + login + "&USER_PASSWORD=" + password + "&USER_REMEMBER=Y&do_login=%D0%92%D0%BE%D0%B9%D1%82%D0%B8&back_url=%252Finvoice.php";
                PostHttp(url, data);
            }
            catch(Exception)
            {
                throw new UnauthorizedAccessException();
            }
        }

        /// <summary>
        /// Gets id of point(city, or station) by name
        /// </summary>
        /// <param name="name">name of city, station, etc.</param>
        /// <returns>id of city, station, etc.</returns>
        public static string GetPointId(string name)
        {
            string getPage = GetHttp("https://dprc.gov.ua/awg/xml?class_name=IStations&method_name=search_station&var_0=3&var_1=2&var_2=0&var_3=16&var_4=" + name, Encoding.GetEncoding("windows-1251"));
            HtmlAgilityPack.HtmlDocument page = new HtmlAgilityPack.HtmlDocument();
            page.LoadHtml(getPage);
            string id = page.DocumentNode.Descendants("childs").First().ChildNodes[0].Attributes["v"].Value;
            return id;
        }

        /// <summary>
        /// Get trains numbers between two points
        /// </summary>
        /// <param name="fromPlace">Route from point</param>
        /// <param name="toPlace">Route finish point</param>
        /// <param name="date">Date of route</param>
        /// <returns>array with train numbers for this route</returns>
        public static string[] GetTrainsNumbers(string fromPlace, string toPlace, string date)
        {
            string fromPlaceId = GetPointId(fromPlace);
            string toPlaceId = GetPointId(toPlace);
            string getPage = GetHttp("https://dprc.gov.ua/show.php?transport_type=2&src=" + fromPlaceId + "&dst=" + toPlaceId + "&dt=" + date + "&ret_dt=2001-01-01&ps=ec_privat", Encoding.GetEncoding("utf-8"));
            HtmlAgilityPack.HtmlDocument Page = new HtmlAgilityPack.HtmlDocument();
            Page.LoadHtml(getPage);
            List<HtmlNode> tdNodes = Page.DocumentNode.Descendants("td").Where(d => d.Attributes.Contains("style") && d.Attributes["style"].Value.Contains("font-size: 14pt; vertical-align: top; margin-top: 0px; padding-top: 1px; padding-right: 0px;")).ToList();
            string[] trainNumbers = new string[tdNodes.Count];
            int i = 0;
            foreach (var item in tdNodes)
            {
                trainNumbers[i] = item.InnerText;
                i++;
            }
            return trainNumbers;
        }

        /// <summary>
        /// Helper method for getting name of stations, cities in runtime in UI
        /// </summary>
        /// <param name="namePart">substring of station(city) name</param>
        /// <returns>array of names that success with "namePart" filter</returns>
        public static string[] GetRunTimeNames(string namePart)
        {
            string getPage = GetHttp("https://dprc.gov.ua/awg/xml?class_name=IStations&method_name=search_station&var_0=3&var_1=2&var_2=0&var_3=16&var_4=" + namePart, Encoding.GetEncoding("windows-1251"));
            HtmlAgilityPack.HtmlDocument page = new HtmlAgilityPack.HtmlDocument();
            page.LoadHtml(getPage);
            List<HtmlNode> nodes = page.DocumentNode.Descendants("childs").ToList();
            string[] names = new string[nodes.Count];
            int i = 0;
            foreach (var node in nodes)
            {
                string currentName = node.ChildNodes[1].Attributes["v"].Value;
                if (currentName.Contains("&amp;quot;"))
                    currentName = currentName.Replace("&amp;quot;", "'");
                names[i] = currentName;
                i++;
            }
            return names;
        }

        /// <summary>
        /// getting all places information for route
        /// <param name="fromPlace">Route from point</param>
        /// <param name="toPlace">Route to point</param>
        /// <param name="date">Route date</param>
        /// <param name="trainNumber">Route train number</param>
        /// <param name="carType">Route carType</param>
        /// <param name="trainNumberIdInList">helper int for parsing train in html table</param>
        /// </summary>
        public static Train GetAllTrainInfo(string fromPlace, string toPlace, string date, string trainNumber, int trainNumberIdInList, bool isReserve, CarType reservePriority)
        {
            Train train = new Train()
            {
                Number = trainNumber,
                Route = fromPlace + "-" + toPlace,
                Date = date,
                Cars = new List<Car>()
            };
            bool isAlreadyReserve = false;
            string fromPlaceId = GetPointId(fromPlace);
            string toPlaceId = GetPointId(toPlace);
            string getPage = GetHttp("https://dprc.gov.ua/show.php?transport_type=2&src=" + fromPlaceId + "&dst=" + toPlaceId + "&dt=" + date + "&ret_dt=2001-01-01&ps=ec_privat", Encoding.GetEncoding("utf-8"));
            HtmlAgilityPack.HtmlDocument PageGeneral = new HtmlAgilityPack.HtmlDocument();
            PageGeneral.LoadHtml(getPage);
            HtmlAgilityPack.HtmlDocument TablePage = GetTablePageForTrain(trainNumberIdInList, PageGeneral);
            var nodeCarRows = TablePage.DocumentNode.Descendants("tr").Where(d => d.Attributes.Contains("id") && d.Attributes["id"].Value.Contains("wagon_")).ToList();
            foreach (var node in nodeCarRows)
            {
                int Number = Convert.ToInt32(node.ChildNodes[1].InnerText.Substring(6, node.ChildNodes[1].InnerText.Length - 1 - 5));
                CarType Type = ConvertCarType(node.ChildNodes[3].InnerText);
                int HighCoupeCount = Convert.ToInt32(node.ChildNodes[5].InnerText);
                int LowCoupeCount = Convert.ToInt32(node.ChildNodes[7].InnerText);
                int HighSideCount = Convert.ToInt32(node.ChildNodes[9].InnerText);
                int LowSideCount = Convert.ToInt32(node.ChildNodes[11].InnerText);
                double Price = Convert.ToDouble(node.ChildNodes[13].InnerText.Substring(0, node.ChildNodes[13].InnerText.IndexOf('&')).Replace('.', ','));
                Car car = new Car(Number, HighCoupeCount, LowCoupeCount, HighSideCount, LowSideCount, Price, Type);
                train.Cars.Add(car);
                if (isReserve && car.Type == reservePriority && !isAlreadyReserve)
                {
                    ReserveTicket(trainNumberIdInList, PageGeneral, Convert.ToInt32(node.Attributes["id"].Value.ToString().Substring(6, node.Attributes["id"].Value.ToString().Length - 6)));
                    isAlreadyReserve = true;
                }
            }
            if(isReserve && !isAlreadyReserve)
            {
                ReserveTicket(trainNumberIdInList, PageGeneral, Convert.ToInt32(nodeCarRows[0].Attributes["id"].ToString().Substring(5, nodeCarRows[0].Attributes["id"].Value.ToString().Length - 5)));
                isAlreadyReserve = true;
            }
            return train;
        }

        private static HtmlDocument GetTablePageForTrain(int trainNumberIdInList, HtmlAgilityPack.HtmlDocument PageGeneral)
        {
            CarInfo info = GetCarInfo(trainNumberIdInList, PageGeneral);
            string[] parseRes = SetCostTosPropertiesCarInfo(info);
            string cost = parseRes[0];
            string tos = parseRes[1];

            string tablePage = GetHttp("https://dprc.gov.ua/trip-info.php?segment_id=" + info.GuidIdx + "&row_id=" + trainNumberIdInList + "&tos_id=" + tos + "&cost=" + cost + "&ps=ec_privat&apn=&user_data=&apn_back_ref=", Encoding.GetEncoding("utf-8"));
            HtmlAgilityPack.HtmlDocument TablePage = new HtmlAgilityPack.HtmlDocument();
            TablePage.LoadHtml(tablePage);
            return TablePage;
        }

        //getting information about cars
        private static CarInfo GetCarInfo(int trainNumberIdInList, HtmlAgilityPack.HtmlDocument PageGeneral)
        {
            //current train node
            int nodeGuid1 = PageGeneral.DocumentNode.InnerHtml.IndexOf("window.trips_['" + trainNumberIdInList + "']");
            //next train node. For getting id of html segment in document
            int nodeGuid2 = PageGeneral.DocumentNode.InnerHtml.IndexOf("window.trips_['" + (trainNumberIdInList + 1).ToString() + "']");
            // parse next html code(need if train node is last in html document)
            int functionStringIndex = 0;
            CarInfo info = new CarInfo();
            if (nodeGuid2 == -1)
            {
                functionStringIndex = PageGeneral.DocumentNode.InnerHtml.IndexOf("function stip(a,b,c) {");
                string doc = PageGeneral.DocumentNode.InnerHtml.Substring(nodeGuid1 + 18 + trainNumberIdInList.ToString().Length, functionStringIndex - (nodeGuid1 + 18 + trainNumberIdInList.ToString().Length + 2));
                info = JsonConvert.DeserializeObject<CarInfo>(doc);
            }
            //getting info about html segment (need for form request string)
            if (nodeGuid2 != -1)
            {
                string doc = PageGeneral.DocumentNode.InnerHtml.Substring(nodeGuid1 + 18 + trainNumberIdInList.ToString().Length, nodeGuid2 - (nodeGuid1 + 18 + trainNumberIdInList.ToString().Length + 2));
                info = JsonConvert.DeserializeObject<CarInfo>(doc);
            }
            return info;
        }

        /// <summary>
        /// Reserving ticket by train number
        /// </summary>
        /// <param name="trainNumberIdInList">train number</param>
        /// <param name="PageGeneral">general page html code with all trains by route</param>
        /// <param name="carId">car id for current train</param>
        public static void ReserveTicket(int trainNumberIdInList, HtmlAgilityPack.HtmlDocument PageGeneral, int carId)
        {
            CarInfo info = GetCarInfo(trainNumberIdInList, PageGeneral);
            string[] parseRes = SetCostTosPropertiesCarInfo(info);
            string cost = parseRes[0];
            string tos = parseRes[1];

            string tablePage = GetHttp("https://dprc.gov.ua/car_map.php?segment_id=" + info.GuidIdx + "&car_id=" + carId, Encoding.GetEncoding("utf-8"));
            HtmlAgilityPack.HtmlDocument TablePage = new HtmlAgilityPack.HtmlDocument();
            TablePage.LoadHtml(tablePage);
            var placeNode = TablePage.DocumentNode.Descendants("td").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("free_seat")).First();
            ReserveTicketAction(placeNode.InnerText, tos, info.GuidIdx);
        }

        //method for reserve ticket by seat number
        private static void ReserveTicketAction(string seatNumber, string tosNumber, string segmentId)
        {
            string reservePage = GetHttp("https://dprc.gov.ua/invoice.php?bedding_flag=on&ticket_0=1000&seat_0=" + seatNumber + "&priv_0=full&date_0=01.01.1600&surname_0=UZ&name_0=HELPER&i_aggreed=1&segment_id=" + segmentId + "&tos_id=" + tosNumber + "&transport_type=2&src_place=&dst_place=&pay_type=ec_privat&car_id=8&apn=&user_data=&apn_back_ref=&lock_type=bought&reserve_places=do", Encoding.GetEncoding("windows-1251"));
            HtmlAgilityPack.HtmlDocument ReserveTicketPage = new HtmlAgilityPack.HtmlDocument();
            ReserveTicketPage.LoadHtml(reservePage);
            var node = ReserveTicketPage.DocumentNode.Descendants("input").Where(d => d.Attributes.Contains("onclick") && d.Attributes["onclick"].Value.Contains("javascript:window.location.href='cancel.php?invoice=")).First();
            currentInvoiceNumber = node.Attributes["onclick"].Value.Substring(52, node.Attributes["onclick"].Value.Length - 12 - 52);
        }

        /// <summary>
        /// Method for canceling buying ticket. Need to buy cancel reservation ticket and buying it by user from a UZ site
        /// </summary>
        public static void CancelReserveTicket()
        {
            string url = "https://dprc.gov.ua/invoice.php";
            string data = "cancel_invoice=" + currentInvoiceNumber;
            PostHttp(url, data);
        }

        private static CarType ConvertCarType(string type)
        {
            switch (type)
            {
                case "Люкс":
                    return CarType.Luxe;
                case "Купе фірм.":
                    return CarType.CoupeFirm;
                case "Купе":
                    return CarType.Coupe;
                case "Плацкарт фірм.":
                    return CarType.ReservedSeatFirm;
                case "Плацкарт":
                    return CarType.ReservedSeat;
                case "Сидя":
                    return CarType.Seat;
                default:
                    return CarType.Luxe;
            }
        }

        private static string[] SetCostTosPropertiesCarInfo(CarInfo info)
        {
            string cost = "";
            string tos = "";
            if (info.Cost1001 != null)
            {
                cost = info.Cost1001;
                tos = "1001";
            }
            else if (info.Cost1050 != null)
            {
                cost = info.Cost1050;
                tos = "1050";
            }
            else if (info.Cost1040 != null)
            {
                cost = info.Cost1040;
                tos = "1040";
            }
            else if (info.Cost1030 != null)
            {
                cost = info.Cost1030;
                tos = "1030";
            }
            else if (info.Cost1025 != null)
            {
                cost = info.Cost1025;
                tos = "1025";
            }
            else if (info.Cost1020 != null)
            {
                cost = info.Cost1020;
                tos = "1020";
            }
            return new string[] { cost, tos };
        }
    }
}

