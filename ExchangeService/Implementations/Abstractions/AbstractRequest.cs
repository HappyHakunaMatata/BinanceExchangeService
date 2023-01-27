using System.Security.Cryptography;
using System.Text;


namespace ExchangeService.Implementations.Abstractions
{
    public abstract class AbstractRequest
    {
        protected SocketsHttpHandler _handler { get; set; }


        public AbstractRequest()
        {
            SocketsHttpHandler handler = new SocketsHttpHandler();
            handler.PooledConnectionLifetime = TimeSpan.FromSeconds(20);
            handler.PooledConnectionIdleTimeout = TimeSpan.FromSeconds(30);
            _handler = handler;
        }


        public abstract HttpResponseMessage UnauthorizedRequest(string endpoint = "", string args = "");
        public abstract HttpResponseMessage AuthorizedRequest(string endpoint = "", string args = "");
        public abstract HttpResponseMessage PostRequest(string endpoint = "", string args = "");
        public abstract HttpResponseMessage DeleteRequest(string endpoint = "", string args = "");
    }
}
