using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Klickl.ApiTest.HttpTools
{
    public class HttpHelp
    {
        private HttpClient httpClient;
        public HttpHelp()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="strPostdata"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public T HttpPost<T>(string url, string apikey, string input, string sign, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "post";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            request.Headers["X-IDCM-APIKEY"] = apikey;
            request.Headers["X-IDCM-SIGNATURE"] = sign;
            var buffer = encoding.GetBytes(input);
            request.ContentLength = buffer.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(buffer, 0, buffer.Length);
            }

            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback =
                        new RemoteCertificateValidationCallback(CheckValidationResult);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            if (responseStream == null) return default(T);
            using (var reader = new StreamReader(responseStream, encoding))
            {
                var result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        public async Task<T> PostAsync<T>(string url, string apikey, string input, string sign)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrWhiteSpace(apikey)) throw new ArgumentNullException(nameof(apikey));
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));
            if (string.IsNullOrWhiteSpace(sign)) throw new ArgumentNullException(nameof(sign));

            var byteArray = Encoding.UTF8.GetBytes(input);
            using (HttpContent httpContent = new StreamContent(new MemoryStream(byteArray)))
            {
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                httpContent.Headers.Add("X-IDCM-APIKEY", apikey);
                httpContent.Headers.Add("X-IDCM-SIGNATURE", sign);
                httpClient.CancelPendingRequests();
                var response = await httpClient.PostAsync(url, httpContent);
                var ret = await this.CheckResponseOk<T>(response);

                return ret;
            }
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
        /// <summary>
        /// Check the crypto client response is ok.
        /// </summary>
        /// <typeparam name="T">
        /// The type.
        /// </typeparam>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <exception cref="BitcoinClientException">
        /// The exception message.
        /// </exception>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<T> CheckResponseOk<T>(HttpResponseMessage response)
        {
            try
            {
                using (var jsonStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var jsonStreamReader = new StreamReader(jsonStream))
                    {
                        var result = await jsonStreamReader.ReadToEndAsync();
                        var ret = JsonConvert.DeserializeObject<T>(result);

                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            throw new Exception(result);
                        }

                        return ret;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Failed parsing the result, StatusCode={0}, row message={1}", response.StatusCode, response.Content.ReadAsStringAsync().Result), ex);
            }
        }
    }
}
