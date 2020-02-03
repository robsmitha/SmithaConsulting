using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.Tests
{
    /// <summary>
    /// A fake http client handler to allow easy mocking of responses
    /// </summary>
    public class FakeHttpClientHandler : HttpClientHandler
    {
        private readonly HttpResponseMessage responseMessage;
        public FakeHttpClientHandler(HttpResponseMessage responseMessage)
        {
            this.responseMessage = responseMessage;
        }

        public HttpRequestMessage Request { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.Request = request;
            return Task.FromResult(this.responseMessage);
        }
    }
}
