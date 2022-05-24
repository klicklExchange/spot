namespace Klickl.WebsocketConsle.Models
{
    public class GetUserinfoEvent : BaseEvent
    {
        public Userinfo Parameters { set; get; }
    }

    public class Userinfo
    {
        public string ApiKey { set; get; }
        public string Sign { set; get; }
    }
}
