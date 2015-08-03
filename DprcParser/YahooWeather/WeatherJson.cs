using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;

namespace ClassAirPort.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WeatherJson
    {
        public BitmapImage Image { get; set; }

        public string City { get; set; }

        /// <summary>
        /// Properties for current weather
        /// </summary>

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("temp")]
        public string Temp { get; set; }

        [JsonProperty("text")]
        public string Conditions { get; set; }

        /// <summary>
        /// Properties for weather forecast
        /// </summary>
        [JsonProperty("day")]
        public string Day { get; set; }

        [JsonProperty("low")]
        public double TempLow { get; set; }

        [JsonProperty("high")]
        public double TempHigh { get; set; }

        public string TempAverage { get; set; }
    }
}
