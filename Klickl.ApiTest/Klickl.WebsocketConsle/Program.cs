using Klickl.WebsocketConsle.Models;
using Klickl.WebsocketConsle.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using WebSocketSharp;

namespace Klickl.WebsocketConsle
{
    class Program
    {
        private readonly static string URL = "wss://api.Klickl.com/websocket";
        private readonly static string ApiKey = "XXXXX";
        private readonly static string ApiSecretKey = "XXXXX";
        private static string Sign = string.Empty;
        static void Main(string[] args)
        {
            Dictionary<String, String> paras = new Dictionary<String, String>();
            paras.Add("apikey", ApiKey);
            Sign = MD5Util.buildMysignV1(paras, ApiSecretKey);
            LoginTest();
            Console.Read();
        }

        static void PingTest()
        {
            var @event = "ping";
            Console.WriteLine($"{nameof(PingTest)} Start");
            using (var ws = new WebSocket(URL))
            {
                if (URL.StartsWith("wss", StringComparison.OrdinalIgnoreCase))
                {
                    ws.SslConfiguration.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                ws.OnMessage += (sender, e) =>
                {
                    Console.WriteLine($"Result：{e.Data}");
                };

                BaseEvent baseEvent = new BaseEvent
                {
                    Event = @event
                };
                var data = JsonConvert.SerializeObject(baseEvent);
                ws.Connect();
                ws.Send(data);
                Console.Read();
            }
            Console.WriteLine($"{nameof(PingTest)} End");
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;// Always accept
        }
        static void AddChannelTest()
        {
            var @event = "addChannel";
            Console.WriteLine($"{nameof(AddChannelTest)} Start");
            using (var ws = new WebSocket(URL))
            {
                if (URL.StartsWith("wss", StringComparison.OrdinalIgnoreCase))
                {
                    ws.SslConfiguration.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                ws.OnMessage += (sender, e) =>
                {
                    Console.WriteLine(e.Data);
                    BaseOutput baseOutput = JsonConvert.DeserializeObject<BaseOutput>(e.Data);
                    if (baseOutput.Event == @event)
                    {
                        Console.WriteLine($"Result：{baseOutput.Result} Code:{baseOutput.ErrorCode}");
                    }
                };
                ChannelEvent baseEvent = new ChannelEvent
                {
                    Event = @event,
                    Channel = "idcm_sub_spot_BTC-VHKD_deals"
                };
                var data = JsonConvert.SerializeObject(baseEvent);
                ws.Connect();
                Login(ws);
                ws.Send(data);
                Console.ReadLine();
            }
            Console.WriteLine($"{nameof(AddChannelTest)}  End");
        }
        private static void Login(WebSocket ws)
        {
            LoginEvent baseEvent = new LoginEvent
            {
                Event = "login",
                parameters = new LoginInfo
                {
                    ApiKey = ApiKey,
                    Sign = Sign
                }
            };
            var data = JsonConvert.SerializeObject(baseEvent);//先登录
            ws.Send(data);
            Console.WriteLine("已登录，请输入a继续！");
            while (Console.ReadKey(false).Key != ConsoleKey.A) ;
        }
        static void LoginTest()
        {
            var @event = "login";
            Console.WriteLine($"{nameof(LoginTest)} Start");
            using (var ws = new WebSocket(URL))
            {
                if (URL.StartsWith("wss", StringComparison.OrdinalIgnoreCase))
                {
                    ws.SslConfiguration.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                ws.OnMessage += (sender, e) =>
                {
                    BaseOutput baseOutput = JsonConvert.DeserializeObject<BaseOutput>(e.Data);
                    if (baseOutput.Event == @event)
                    {
                        Console.WriteLine($"Result：{baseOutput.Result} Code:{baseOutput.ErrorCode}");
                    }
                };
                LoginEvent baseEvent = new LoginEvent
                {
                    Event = @event,
                    parameters = new LoginInfo
                    {
                        ApiKey = ApiKey,
                        Sign = Sign
                    }
                };
                var data = JsonConvert.SerializeObject(baseEvent);
                ws.Connect();
                ws.Send(data);
                Console.ReadLine();
            }
            Console.WriteLine($"{nameof(LoginTest)}  End");
        }

        static void SendOrderTest()
        {
            var @event = "sendorder";
            Console.WriteLine($"{nameof(SendOrderTest)} Start");
            using (var ws = new WebSocket(URL))
            {
                if (URL.StartsWith("wss", StringComparison.OrdinalIgnoreCase))
                {
                    ws.SslConfiguration.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                ws.OnMessage += (sender, e) =>
                {
                    DataOutput<string> baseOutput = JsonConvert.DeserializeObject<DataOutput<string>>(e.Data);
                    if (baseOutput.Event == @event)
                    {
                        var parameters = baseOutput.Data == null ? "" : JsonConvert.SerializeObject(baseOutput.Data);
                        Console.WriteLine($"Parameters:{parameters} Result：{baseOutput.Result} Code:{baseOutput.ErrorCode} ");
                    }
                };

                SendOrderEvent baseEvent = new SendOrderEvent
                {
                    Event = @event,
                    Parameters = new OrderInfo
                    {
                        ApiKey = ApiKey,
                        Sign = Sign,
                        Symbol = "BTC/VHKD",
                        Amount = 1,
                        IsMarket = false,
                        Price = (decimal)1,
                        Type = "buy"
                    }
                };
                var data = JsonConvert.SerializeObject(baseEvent);
                ws.Connect();
                Login(ws);
                Thread.Sleep(50);
                ws.Send(data);
                Console.ReadLine();
            }
            Console.WriteLine($"{nameof(SendOrderTest)} End");
        }

        static void CancelOrderTest()
        {
            var @event = "cancelorder";
            Console.WriteLine($"{nameof(CancelOrderTest)} Start");
            using (var ws = new WebSocket(URL))
            {
                if (URL.StartsWith("wss", StringComparison.OrdinalIgnoreCase))
                {
                    ws.SslConfiguration.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                ws.OnMessage += (sender, e) =>
                {
                    BaseOutput baseOutput = JsonConvert.DeserializeObject<BaseOutput>(e.Data);
                    if (baseOutput.Event == @event)
                    {
                        Console.WriteLine($"Result：{baseOutput.Result} Code:{baseOutput.ErrorCode} ");
                    }
                };

                CancelOrderEvent baseEvent = new CancelOrderEvent
                {
                    Event = @event,
                    Parameters = new CancelOrder
                    {
                        Side = TradeSide.BUY,
                        Sign = Sign,
                        Symbol = "BTC/VHKD",
                        ApiKey = ApiKey,
                        OrderID = "xIW9cbvgYk-D5T4TtrIVqQ"
                    }
                };
                var data = JsonConvert.SerializeObject(baseEvent);
                ws.Connect();
                Login(ws);
                ws.Send(data);
                Console.ReadLine();
            }
            Console.WriteLine($"{nameof(CancelOrderTest)}  End");
        }

        static void GetUserInfoTest()
        {
            Console.WriteLine($"{nameof(GetUserInfoTest)} Start");
            using (var ws = new WebSocket(URL))
            {
                if (URL.StartsWith("wss", StringComparison.OrdinalIgnoreCase))
                {
                    ws.SslConfiguration.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                ws.OnMessage += (sender, e) =>
                {
                    DataOutput<dynamic> output = JsonConvert.DeserializeObject<DataOutput<dynamic>>(e.Data);
                    Console.WriteLine(e.Data);
                    if (output.Event == "getuserinfo")
                    {
                        DataOutput<IList<GetUserInfoOutput>> baseOutput = JsonConvert.DeserializeObject<DataOutput<IList<GetUserInfoOutput>>>(e.Data);
                        var parameters = baseOutput.Data == null ? string.Empty : JsonConvert.SerializeObject(baseOutput.Data);
                        Console.WriteLine($"Parameters:{parameters} Result：{baseOutput.Result} Code:{baseOutput.ErrorCode} ");
                    }
                    if (output.Event == "login")
                    {
                        Console.WriteLine($"Result：{output.Result} Code:{output.ErrorCode}");
                    }
                };
                ws.Connect();
                Login(ws);//先登录
                Thread.Sleep(50);
                GetUserinfoEvent userinfoEvent = new GetUserinfoEvent//后获取用户信息
                {
                    Event = "getuserinfo",
                    Parameters = new Userinfo
                    {
                        Sign = Sign,
                        ApiKey = ApiKey
                    }
                };
                var userInfodata = JsonConvert.SerializeObject(userinfoEvent);
                ws.Send(userInfodata);
                Console.ReadLine();
            }
            Console.WriteLine($"{nameof(GetUserInfoTest)}  End");
        }

        static void GetOrderInfoTest()
        {
            var @event = "getorderinfo";
            Console.WriteLine($"{nameof(GetOrderInfoTest)} Start");
            using (var ws = new WebSocket(URL))
            {
                if (URL.StartsWith("wss", StringComparison.OrdinalIgnoreCase))
                {
                    ws.SslConfiguration.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                ws.OnMessage += (sender, e) =>
                {
                    var output = JsonConvert.DeserializeObject<DataOutput<dynamic>>(e.Data);
                    if (output.Event == @event)
                    {
                        DataOutput<IList<GetOrderInfoOutput>> baseOutput = JsonConvert.DeserializeObject<DataOutput<IList<GetOrderInfoOutput>>>(e.Data);
                        var parameters = baseOutput.Data == null ? string.Empty : JsonConvert.SerializeObject(baseOutput.Data);
                        Console.WriteLine($"Parameters:{parameters} Result：{baseOutput.Result} Code:{baseOutput.ErrorCode} ");
                    }
                    if (output.Event == "login")
                    {
                        Console.WriteLine($"Result：{output.Result} Code:{output.ErrorCode}");
                    }
                };

                GetOrderinfoEvent baseEvent = new GetOrderinfoEvent
                {
                    Event = @event,
                    Parameters = new Orderinfo
                    {
                        Symbol = "BTC/VHKD",
                        Sign = Sign,
                        ApiKey = ApiKey,
                        OrderId = "EvIJUCwEp0CClKGlM8MRKg"
                    }
                };
                var data = JsonConvert.SerializeObject(baseEvent);
                ws.Connect();
                Login(ws);
                Thread.Sleep(50);
                ws.Send(data);
                Console.ReadLine();
            }
            Console.WriteLine($"{nameof(GetOrderInfoTest)}  End");
        }
    }
}
