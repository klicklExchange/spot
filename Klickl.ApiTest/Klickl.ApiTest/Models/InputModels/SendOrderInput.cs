using System.ComponentModel;

namespace Klickl.ApiTest.Models.InputModels
{
    /// <summary>
    /// 订单类型 0、Market   1、Limit,   2、Stop,   3、TrailingStop,   4、FillOrKill,   5、ExchangeMarket,  6、       ExchangeLimit  7、ExchangeStop   8、ExchangeTrailingStop,   9、ExchangeFillOrKill
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// Market
        /// </summary>
        [Description("市场价")]
        Market = 0,
        /// <summary>
        /// Limit
        /// </summary>
        [Description("限价")]
        Limit = 1,
        /// <summary>
        /// Stop
        /// </summary>
        [Description("Stop")]
        Stop = 2,
        /// <summary>
        /// TrailingStop
        /// </summary>
        [Description("TrailingStop")]
        TrailingStop = 3,
        /// <summary>
        /// FillOrKill
        /// </summary>
        [Description("FillOrKill")]
        FillOrKill = 4,
        /// <summary>
        /// ExchangeMarket
        /// </summary>
        [Description("ExchangeMarket")]
        ExchangeMarket = 5,
        /// <summary>
        /// ExchangeLimit
        /// </summary>
        [Description("ExchangeLimit")]
        ExchangeLimit = 6,
        /// <summary>
        /// ExchangeStop
        /// </summary>
        [Description("ExchangeStop")]
        ExchangeStop = 7,
        /// <summary>
        /// ExchangeTrailingStop
        /// </summary>
        [Description("ExchangeTrailingStop")]
        ExchangeTrailingStop = 8,
        /// <summary>
        /// ExchangeFillOrKill
        /// </summary>
        [Description("ExchangeFillOrKill")]
        ExchangeFillOrKill = 9
    }
    /// <summary>
    /// 交易方向
    /// </summary>
    public enum TradeSide
    {
        /// <summary>
        /// 买入
        /// </summary>
        [Description("买入")]
        BUY,
        /// <summary>
        /// 卖出
        /// </summary>
        [Description("卖出")]
        SELL
    }
    public class SendOrderInput : InputDto
    {
        string symbol;
        /// <summary>
        /// 交易对
        /// </summary>
        public string Symbol
        {
            set
            {
                symbol = value;
            }
            get
            {
                return symbol.Replace("-", "/");
            }
        }
        /// <summary>
        /// 交易数量
        /// </summary>
        public decimal Size { set; get; }
        /// <summary>
        /// 下单价格
        /// </summary>
        public decimal Price { set; get; }
        /// <summary>
        /// 买卖类型
        /// </summary>
        public TradeSide Side { set; get; }
        /// <summary>
        /// 单类型
        /// </summary>
        public OrderType Type { set; get; }
    }
}
