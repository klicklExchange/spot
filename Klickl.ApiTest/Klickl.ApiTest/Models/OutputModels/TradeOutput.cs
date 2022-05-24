using Newtonsoft.Json;

namespace Klickl.ApiTest.Models.OutputModels
{
    public class TradeOutput
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [JsonProperty("orderid")]
        public string OrderID { set; get; }
    }
}
