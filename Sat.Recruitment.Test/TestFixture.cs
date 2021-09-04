using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.IO;
using System.Net.Http;

namespace Sat.Recruitment.Test
{
    public class TestFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly TestServer Server;
        private readonly HttpClient _client;


        public TestFixture()
        {
            var builder = new WebHostBuilder()                
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<TStartup>();

            Server = new TestServer(builder);
            _client = new HttpClient();
        }


        public void Dispose()
        {
            _client.Dispose();
            Server.Dispose();
        }
    }
}
