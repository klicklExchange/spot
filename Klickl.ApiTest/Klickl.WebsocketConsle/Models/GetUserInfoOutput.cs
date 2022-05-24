using Newtonsoft.Json;

namespace Klickl.WebsocketConsle.Models
{
    public class GetUserInfoOutput
    {
        /// <summary>
        /// 币种
        /// </summary>
        [JsonProperty("code")]
        public string Code { set; get; }
        /// <summary>
        /// 可用
        /// </summary>
        [JsonProperty("free")]
        public decimal Free { set; get; }
        /// <summary>
        /// 冻结
        /// </summary>
        [JsonProperty("freezed")]
        public decimal Freezed { set; get; }
    }
}
