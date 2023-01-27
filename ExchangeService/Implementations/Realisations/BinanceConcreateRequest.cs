using System.Text;
using System.Security.Cryptography;
using ExchangeService.Implementations.Abstractions;


namespace ExchangeService.Implementations.Realisations
{
    public class BinanceConcreateRequest : AbstractRequest
    {
        private string _publicKey { get; set; }
        private string _privateKey { get; set; }
        private string _link { get; set; }

        public BinanceConcreateRequest(string link,
            string publicKey = "",
            string privateKey = "")
        {
            _link = link;
            _publicKey = publicKey;
            _privateKey = privateKey;
        }

        public override HttpResponseMessage UnauthorizedRequest(string endpoint, string args = "")
        {
            using (HttpClient httpclient = new HttpClient(_handler))
            {
                httpclient.BaseAddress = new Uri(_link);
                httpclient.DefaultRequestHeaders.Add("X-MBX-APIKEY", _publicKey);
                httpclient.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var task = Task.Run(async () => await httpclient.GetAsync($"{endpoint}?{args}"));
                return task.Result;
            }
        }

        public override HttpResponseMessage AuthorizedRequest(string endpoint, string args = "")
        {
            using (HttpClient httpclient = new HttpClient(_handler))
            {
                httpclient.DefaultRequestHeaders.Add("X-MBX-APIKEY", _publicKey);
                httpclient.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string headers = httpclient.DefaultRequestHeaders.ToString();
                string timestamp = GetTimestamp();
                args += "&timestamp=" + timestamp;
                var signature = CreateSignature(args, _privateKey);
                httpclient.BaseAddress = new Uri(_link);
                var task = Task.Run(async () => await httpclient.GetAsync($"{endpoint}?{args}&signature={signature}"));
                return task.Result;
            }
        }


        public override HttpResponseMessage PostRequest(string endpoint, string args = "")
        {
            using (HttpClient httpclient = new HttpClient(_handler))
            {
                httpclient.DefaultRequestHeaders.Add("X-MBX-APIKEY", _publicKey);
                httpclient.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string headers = httpclient.DefaultRequestHeaders.ToString();
                string timestamp = GetTimestamp();
                args += "&timestamp=" + timestamp;
                var signature = CreateSignature(args, _privateKey);
                httpclient.BaseAddress = new Uri(_link);
                var response = Task.Run(async () =>
                await httpclient.PostAsync($"{endpoint}?{args}&signature={signature}", null)).Result;
                return response;
            }
        }

        public override HttpResponseMessage DeleteRequest(string endpoint, string args = "")
        {
            using (HttpClient httpclient = new HttpClient(_handler))
            {
                httpclient.DefaultRequestHeaders.Add("X-MBX-APIKEY", _publicKey);
                httpclient.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string headers = httpclient.DefaultRequestHeaders.ToString();
                string timestamp = GetTimestamp();
                args += "&timestamp=" + timestamp;
                var signature = CreateSignature(args, _privateKey);
                httpclient.BaseAddress = new Uri(_link);
                var response = Task.Run(async () =>
                await httpclient.DeleteAsync($"{endpoint}?{args}&signature={signature}")).Result;
                return response;
            }
        }

        private static string GetTimestamp()
        {
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            return milliseconds.ToString();
        }

        public string CreateSignature(string message, string secret)
        {
            Encoding SignatureEncoding = Encoding.UTF8;
            byte[] keyBytes = SignatureEncoding.GetBytes(secret);
            byte[] messageBytes = SignatureEncoding.GetBytes(message);
            HMACSHA256 hmacsha256 = new HMACSHA256(keyBytes);

            byte[] bytes = hmacsha256.ComputeHash(messageBytes);

            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}

