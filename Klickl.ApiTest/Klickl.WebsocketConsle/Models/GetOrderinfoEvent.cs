﻿namespace Klickl.WebsocketConsle.Models
{
    public class GetOrderinfoEvent : BaseEvent
    {
        public Orderinfo Parameters { set; get; }
    }

    public class Orderinfo
    {
        public string ApiKey { set; get; }
        public string Sign { set; get; }
        public string Symbol { set; get; }
        public string OrderId { set; get; }
    }
}
