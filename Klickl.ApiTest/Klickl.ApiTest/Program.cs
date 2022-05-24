using Klickl.ApiTest.Extensions;
using Klickl.ApiTest.HttpTools;
using Klickl.ApiTest.Models;
using Klickl.ApiTest.Models.InputModels;
using Klickl.ApiTest.Models.OutputModels;
using Klickl.ApiTest.Signs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Klickl.ApiTest
{
    class Program
    {
        private readonly static string host = "https://api.klickl.com";
        private readonly static string ApiKey = "XXXXX";
        private readonly static string SecretKey = "XXXXXX";
        private readonly static string OrderID = "XXXX";
        static void Main(string[] args)
        {
            Console.WriteLine("测试开始");
            do
            {
                GetTickerTest().Wait();
                Console.Read();
                Console.WriteLine("继续...");
            } while (true);
        }

        #region 币币行情测试用例
        /// <summary>
        /// 获取行情测试用例
        /// </summary>
        [Fact]
        static async Task GetTickerTest()
        {
            try
            {
                HttpHelp httpHelp = new HttpHelp();
                GetTickerInput inputDto = new GetTickerInput
                {
                    Symbol = "BTC/VUSD"
                };
                string url = host + "/api/v1/getticker";
                string input = JsonConvert.SerializeObject(inputDto);
                var response = httpHelp.HttpPost<OperateMessage<PositionOutput>>(url,
                    ApiKey,
                    input,
                    ApiSign.CreateSign(SecretKey, input)
                    );

                Console.WriteLine($"code:{response.code}");
                Console.WriteLine($"result:{response.result}");
                Console.WriteLine($"data:{response.ToJson()}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetTickerTest)}出错：{ex.Message}");
            }
        }
        /// <summary>
        /// 获取市场深度测试用例
        /// </summary>
        /// <returns></returns>
        [Fact]
        static async Task GetDepthTest()
        {
            try
            {
                HttpHelp httpHelp = new HttpHelp();
                GetDepthInput inputDto = new GetDepthInput
                {
                    Symbol = "BTC/VHKD"
                };
                string url = host + "/api/v1/getdepth";
                string input = JsonConvert.SerializeObject(inputDto);
                var response = httpHelp.HttpPost<OperateMessage<GetDepthOutput>>(url,
                    ApiKey,
                    input,
                    ApiSign.CreateSign(SecretKey, input)
                    );

                Console.WriteLine($"code:{response.code}");
                Console.WriteLine($"result:{response.result}");
                Console.WriteLine($"data:{response.ToJson()}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetDepthTest)}出错：{ex.Message}");
            }
        }
        /// <summary>
        /// 获取交易信息(500条)测试用例
        /// </summary>
        /// <returns></returns>
        [Fact]
        static async Task GetTradesTest()
        {
            try
            {
                HttpHelp httpHelp = new HttpHelp();
                GetTradesInput inputDto = new GetTradesInput
                {
                    Symbol = "BTC/VHKD",
                    Since = 1528359729
                };
                string url = host + "/api/v1/gettrades";
                string input = JsonConvert.SerializeObject(inputDto);
                var response = httpHelp.HttpPost<OperateMessage<IEnumerable<GetTradesOutput>>>(url,
                    ApiKey,
                    input,
                    ApiSign.CreateSign(SecretKey, input)
                    );

                Console.WriteLine($"code:{response.code}");
                Console.WriteLine($"result:{response.result}");
                Console.WriteLine($"data:{response.data.ToJson()}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetTradesTest)}出错：{ex.Message}");
            }
        }
        /// <summary>
        /// 获取K线数据测试用例
        /// </summary>
        /// <returns></returns>
        [Fact]
        static async Task GetKLineTest()
        {
            try
            {
                HttpHelp httpHelp = new HttpHelp();
                GetKLineInput inputDto = new GetKLineInput
                {
                    Symbol = "BTC/VHKD",
                    Since = 1528359729,
                    Size = 100,
                    Type = "5min"
                };
                string url = host + "/api/v1/getkline";
                string input = JsonConvert.SerializeObject(inputDto);
                var response = httpHelp.HttpPost<OperateMessage<IEnumerable<GetKLineOutput>>>(url,
                    ApiKey,
                    input,
                    ApiSign.CreateSign(SecretKey, input)
                    );

                Console.WriteLine($"code:{response.code}");
                Console.WriteLine($"result:{response.result}");
                Console.WriteLine($"data:{response.data.ToJson()}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetTradesTest)}出错：{ex.Message}");
            }
        }
        #endregion

        #region 币币交易测试用例
        /// <summary>
        /// 获取用户信息测试用例
        /// </summary>
        /// <returns></returns>
        [Fact]
        static async Task GetUserInfoTest()
        {
            try
            {
                HttpHelp httpHelp = new HttpHelp();
                string url = host + "/api/v1/getuserinfo";
                string input = "1";
                var response = httpHelp.HttpPost<OperateMessage<List<GetUserInfoOutput>>>(url,
                    ApiKey,
                    input,
                    ApiSign.CreateSign(SecretKey, input)
                    );

                Console.WriteLine($"code:{response.code}");
                Console.WriteLine($"result:{response.result}");
                Console.WriteLine($"data:{response.data.ToJson()}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetTickerTest)}出错：{ex.Message}");
            }
        }
        /// <summary>
        /// 下单交易测试用例
        /// </summary>
        /// <returns></returns>
        [Fact]
        static async Task TradeTest()
        {
            try
            {
                HttpHelp httpHelp = new HttpHelp();
                string url = host + "/api/v1/trade";
                SendOrderInput inputDto = new SendOrderInput
                {
                    Symbol = "BTC/VHKD",
                    Size = 1,
                    Side = TradeSide.BUY,
                    Price = (decimal)1,
                    Type = OrderType.Limit
                };
                string input = JsonConvert.SerializeObject(inputDto);
                var response = httpHelp.HttpPost<OperateMessage<TradeOutput>>(url,
                    ApiKey,
                    input,
                    ApiSign.CreateSign(SecretKey, input)
                    );

                Console.WriteLine($"code:{response.code}");
                Console.WriteLine($"result:{response.result}");
                Console.WriteLine($"data:{response.data.ToJson()}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetTickerTest)}出错：{ex.Message}");
            }
        }
        /// <summary>
        /// 获取用户的订单信息测试用例
        /// </summary>
        /// <returns></returns>
        [Fact]
        static async Task GetOrderInfoTest()
        {
            try
            {
                HttpHelp httpHelp = new HttpHelp();
                string url = host + "/api/v1/getorderinfo";
                GetOrderInfoInput inputDto = new GetOrderInfoInput
                {
                    Symbol = "BTC/VHKD",
                    OrderID = OrderID
                };
                string input = JsonConvert.SerializeObject(inputDto);
                var response = httpHelp.HttpPost<OperateMessage<List<GetOrderInfoOutput>>>(url,
                    ApiKey,
                    input,
                    ApiSign.CreateSign(SecretKey, input)
                    );

                Console.WriteLine($"code:{response.code}");
                Console.WriteLine($"result:{response.result}");
                Console.WriteLine($"data:{response.data.ToJson()}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetTickerTest)}出错：{ex.Message}");
            }
        }
        /// <summary>
        ///  获取历史订单信息，只返回最近两天的信息测试用例
        /// </summary>
        /// <returns></returns>
        [Fact]
        static async Task GetHistoryOrderTest()
        {
            try
            {
                HttpHelp httpHelp = new HttpHelp();
                string url = host + "/api/v1/gethistoryorder";
                GetHistoryOrderInput inputDto = new GetHistoryOrderInput
                {
                    Symbol = "BTC/VHKD",
                    PageIndex = 1,
                    PageSize = 10,
                    Status = OrderStatus.Cancel
                };
                string input = JsonConvert.SerializeObject(inputDto);
                var response = httpHelp.HttpPost<OperateMessage<List<GetOrderInfoOutput>>>(url,
                    ApiKey,
                    input,
                    ApiSign.CreateSign(SecretKey, input)
                    );

                Console.WriteLine($"code:{response.code}");
                Console.WriteLine($"result:{response.result}");
                Console.WriteLine($"data:{response.data.ToJson()}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetTickerTest)}出错：{ex.Message}");
            }
        }
        /// <summary>
        /// 提币测试
        /// </summary>
        /// <returns></returns>
        [Fact]
        static async Task WithdrawTest()
        {
            try
            {
                HttpHelp httpHelp = new HttpHelp();
                string url = host + "/api/v1/withdraw";
                WithdrawInput inputDto = new WithdrawInput
                {
                    Symbol = "VHKD",
                    Address = "VBssvPs5ayEzYMfBxVN3Ux2uGzgBBVBR2S",
                    Amount = 1
                };
                string input = JsonConvert.SerializeObject(inputDto);
                var response = httpHelp.HttpPost<OperateMessage<string>>(url,
                    ApiKey,
                    input,
                    ApiSign.CreateSign(SecretKey, input)
                    );

                Console.WriteLine($"code:{response.code}");
                Console.WriteLine($"result:{response.result}");
                Console.WriteLine($"data:{response.data.ToString()}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetTickerTest)}出错：{ex.Message}");
            }
        }
        /// <summary>
        /// 取消提币测试用例
        /// </summary>
        /// <returns></returns>
        [Fact]
        static async Task Cancel_WithdrawTest()
        {
            try
            {
                HttpHelp httpHelp = new HttpHelp();
                string url = host + "/api/v1/cancel_withdraw";
                CancelWithdrawInput inputDto = new CancelWithdrawInput
                {
                    Symbol = "VHKD",
                    WithdrawID = "XXXXXX"
                };
                string input = JsonConvert.SerializeObject(inputDto);
                var response = httpHelp.HttpPost<OperateMessage<bool>>(url,
                    ApiKey,
                    input,
                    ApiSign.CreateSign(SecretKey, input)
                    );

                Console.WriteLine($"code:{response.code}");
                Console.WriteLine($"result:{response.result}");
                Console.WriteLine($"data:{response.data.ToString()}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetTickerTest)}出错：{ex.Message}");
            }
        }
        /// <summary>
        /// 查询提币用例
        /// </summary>
        /// <returns></returns>
        [Fact]
        static async Task GetWithdrawInfoTest()
        {
            try
            {
                HttpHelp httpHelp = new HttpHelp();
                string url = host + "/api/v1/getwithdrawinfo";
                WithdrawinfoInput inputDto = new WithdrawinfoInput
                {
                    Symbol = "VHKD",
                    WithdrawID = "VBssvPs5ayEzYMfBxVN3Ux2uGzgBBVBR2S"
                };
                string input = JsonConvert.SerializeObject(inputDto);
                var response = httpHelp.HttpPost<OperateMessage<GetWithdrawInfoOutput>>(url,
                    ApiKey,
                    input,
                    ApiSign.CreateSign(SecretKey, input)
                    );

                Console.WriteLine($"code:{response.code}");
                Console.WriteLine($"result:{response.result}");
                Console.WriteLine($"data:{response.data.ToJson()}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetWithdrawInfoTest)}出错：{ex.Message}");
            }
        }


        #endregion
    }
}
