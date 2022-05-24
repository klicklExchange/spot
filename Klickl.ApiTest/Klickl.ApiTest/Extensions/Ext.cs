using Newtonsoft.Json;

namespace Klickl.ApiTest.Extensions
{
    public static class Ext
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
