using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReminder.DataClasses
{
    /// <summary>
    /// Helper class for parse js strings in html document. Need to get segment_id for getting all information about car
    /// </summary>
    public class CarInfo
    {
        [JsonProperty("guididx")]
        public string GuidIdx {get;set;}
        [JsonProperty("cost_1001")]
        public string Cost1001 { get; set; }
        [JsonProperty("cost_1050")]
        public string Cost1050 { get; set; }
        [JsonProperty("cost_1040")]
        public string Cost1040 { get; set; }
        [JsonProperty("cost_1030")]
        public string Cost1030 { get; set; }
        [JsonProperty("cost_1020")]
        public string Cost1020 { get; set; }
        [JsonProperty("cost_1025")]
        public string Cost1025 { get; set; }
    }
}
