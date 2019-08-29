using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace Registrar.FunctionalTests.Setup
{
    static class TestServerExtensions
    {
        public static HttpClient CreateIdempotentClient(this TestServer server)
        {
            var client = server.CreateClient();

            client.DefaultRequestHeaders.Add("x-requestid", Guid.NewGuid().ToString());

            return client;
        }
    }
}
