namespace Klickl.WebsocketConsle.Models
{
    public class LoginEvent : BaseEvent
    {
        public LoginInfo parameters { set; get; }
    }

    public class LoginInfo
    {
        public string ApiKey { set; get; }

        public string Sign { set; get; }
    }
}
