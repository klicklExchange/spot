namespace Klickl.WebsocketConsle.Models
{
    public class DataOutput<T> : BaseOutput
    {
        public T Data { set; get; }
    }

    public class BaseOutput
    {
        public string Event { set; get; }

        public bool Result { set; get; }

        public string ErrorCode { set; get; }
    }
}
