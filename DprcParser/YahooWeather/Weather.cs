using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Windows.Media.Imaging;

namespace ClassAirPort.Json
{

    public class Weather
    {

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        private string result = "";
        private JObject dataObject;
        private WeatherJson jsonWeather;
        private WeatherJson jsonForecast;
        private List<WeatherJson> _listWeatherForecast;

        public string City { get; set; }
        public string Url { get; set; }
        public string Error { get; set; }
        public int ErrorCode { get; set; }

        /// <summary>
        /// Get current weather by url from service 
        /// Donwload string JSON 
        /// Deserialize string to WeatherJson.cs class
        /// </summary>
        public Weather(string city)
        {
            City = city;
            Url = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%27" + city + "%27)&diagnostics=true&format=json";

            if (CheckInternetConnection().Equals(true))
            {
                using (WebClient wc = new WebClient())
                {
                    result = wc.DownloadString(Url);
                }
                dataObject = JObject.Parse(result);
                try
                {
                    jsonWeather = JsonConvert.DeserializeObject<WeatherJson>(dataObject["query"]["results"]["channel"]["item"]["condition"].ToString());
                }
                catch (System.InvalidOperationException) { Error = "The city is not found"; ErrorCode = 2; return; }          
            }
            else
            {
                Error = "Check internet connection!";
                ErrorCode = 1;
                return;
            }
        }

        /// <summary>
        /// Get forecast weather 
        /// </summary>
        /// <param name="city"></param>
        /// <param name="forecast"></param>
        public Weather(string city, bool forecast)
        {
            _listWeatherForecast = new List<WeatherJson>();
            if (forecast)
            {
                City = city;
                Url = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22" + city + "%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
                if (CheckInternetConnection().Equals(true))
                {
                    using (WebClient wc = new WebClient())
                    {
                        result = wc.DownloadString(Url);
                    }
                    dataObject = JObject.Parse(result);
                    try
                    {
                        dynamic jsArray = (JArray)JsonConvert.DeserializeObject(dataObject["query"]["results"]["channel"]["item"]["forecast"].ToString());
                        foreach (JObject item in jsArray)
                        {
                            jsonForecast = JsonConvert.DeserializeObject<WeatherJson>(item.ToString());
                            _listWeatherForecast.Add(new WeatherJson()
                            {
                                Day = jsonForecast.Day,
                                Image = GetImageWeather(jsonForecast),
                                Code = jsonForecast.Code,
                                Date = jsonForecast.Date,
                                TempLow = Math.Round((jsonForecast.TempLow - 32) / 1.8, 2),
                                TempHigh = Math.Round((jsonForecast.TempHigh - 32) / 1.8, 2),
                                TempAverage = "Average temp: " + Math.Round((((jsonForecast.TempHigh + jsonForecast.TempLow) / 2) - 32) / 1.8, 2),
                                Conditions =  jsonForecast.Conditions, // Conditions: 
                                City = this.City
                            });
                        }
                    }
                    catch (System.InvalidOperationException) { Error = "The city is not found"; ErrorCode = 2; return; }         
                }
                else
                {
                    Error = "Check internet connection!";
                    ErrorCode = 1;
                    return;
                }
            }
        }

        public void SetCity(string city)
        {
            Url = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%27" + city + "%27)&diagnostics=true&format=json";
        }

        /// <summary>
        /// Method check internet connection
        /// </summary>
        /// <returns>True: Have internet connection</returns>
        /// <returns>False: Not have internet connection</returns>
        public bool CheckInternetConnection()
        {
            int Desc;
            if (InternetGetConnectedState(out Desc, 0) == true)
                return true;
            else
            {
                Error = "Check internet connection!";
                return false;
            }
        }

        /// <summary>
        /// Return title of search weather
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
        {
            dynamic json = (JObject)JsonConvert.DeserializeObject(dataObject["query"]["results"]["channel"]["item"].ToString());
            return json.title;
        }

        /// <summary>
        /// Returns the current weather
        /// </summary>
        /// <returns>WeatherJson object</returns>
        public WeatherJson GetWeather()
        {
            return new WeatherJson()
            {
                Image = GetImageWeather(jsonWeather),
                Code = jsonWeather.Code,
                Date = jsonWeather.Date,
                Temp = "Temperature: " + Math.Round((Convert.ToDouble(jsonWeather.Temp) - 32) / 1.8, 2).ToString() + " *C",
                Conditions = "Condition: " + jsonWeather.Conditions,
                City = this.City
            };
        }

        /// <summary>
        /// Returns weather forecast
        /// </summary>
        /// <returns>List of WeatherJson</returns>
        public List<WeatherJson> GetWeatherForecast()
        {
            return _listWeatherForecast;
        }

        /// <summary>
        /// Get conditions from service 
        /// </summary>
        /// <returns></returns>
        public string GetConditions()
        {
            return jsonWeather.Conditions;
        }

        /// <summary>
        /// Get date from service
        /// </summary>
        /// <returns></returns>
        public string GetDate()
        {
            return jsonWeather.Date;
        }

        /// <summary>
        /// Get temperature from service
        /// </summary>
        /// <returns></returns>
        public string GetTemperature()
        {
            return Math.Round((Convert.ToDouble(jsonWeather.Temp) - 32) / 1.8, 2).ToString();
        }

        /// <summary>
        /// Get code from country that search weather
        /// </summary>
        /// <returns></returns>
        public string GetCode()
        {
            return jsonWeather.Code;
        }

        /// <summary>
        /// Returns the image depending on weather conditions
        /// </summary>
        /// <param name="_weather"></param>
        /// <returns></returns>
        private BitmapImage GetImageWeather(WeatherJson _weather)
        {
            switch (_weather.Conditions)
            {
                case "Clear":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Sunny.png"));      
                case "Light Rain":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/light_rain.png"));              
                case "Haze":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Haze.png"));
                case "Cloudy":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Cloudy.png"));              
                case "Snow":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Snow.png"));                
                case "Snow Flurries":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Snow.png"));                   
                case "Showers":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Drizzle.png"));
                case "AM Showers":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Drizzle.png")); 
                case "Drizzle":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Drizzle.png"));                   
                case "Freezing Drizzle":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Drizzle.png"));
                case "Thunderstorms":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Thunderstorms.png"));
                case "AM Thundershowers":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Thunderstorms.png"));
                case "Foggy":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Cloudy.png"));                  
                case "Mostly Cloudy":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Mostly Cloudy.png"));               
                case "Partly Cloudy":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Mostly Cloudy.png"));               
                case "Sunny":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Sunny.png"));
                case "Mostly Sunny":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Sunny.png"));
                case "Mostly Clear":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Cloudy.png"));    
                case "Fair":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Sunny.png"));                   
                case "Hot":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/Sunny.png"));                   
                case "Mixed rain and snow":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/rain_snow.png"));                 
                case "Windy":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/wind.png"));                    
                case "Blustery":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/wind.png"));
                case "Mixed rain and sleet":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Weather/wind.png"));
                case "Not available":
                    return null; 
                default:
                    return null;      
            }
        }

    }
}
