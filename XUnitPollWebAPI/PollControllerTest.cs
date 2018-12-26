using System;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using PollWebAPI;
using PollWebAPI.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;

namespace XUnitPollWebAPI
{
    public class PollControllerTest
    {
        private HttpClient Client { get; set; }
        private TestServer _server;

        public PollControllerTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = _server.CreateClient();
        }

        [Fact]
        public async Task Post_poll()
        {
            PollOptions[] options = new PollOptions[3];
            var options_1 = new PollOptions() { option_description = "option 1" };
            var options_2 = new PollOptions() { option_description = "option 2" };
            var options_3 = new PollOptions() { option_description = "option 3" };

            PollRequest poll = new PollRequest() { poll_description = "poll test 1", options = new List<string> { "option 1", "options 2", "options 3" } };
            var response = await Client.PostAsJsonAsync("/poll", poll);

            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_vote()
        {
            var response = await Client.PostAsJsonAsync("/poll/1/vote", 1);
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public async Task poll_Get_Returns_NotFound()
        {
            var response = await Client.GetAsync("/poll/999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task poll_Get_Stats_Returns_Notfound()
        {
            var response = await Client.GetAsync("/poll/999/stats");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task poll_Get_Returns_ok()
        {
            var response = await Client.GetAsync("/poll/1");
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task poll_Get_Stats_Returns_ok()
        {
            var response = await Client.GetAsync("/poll/1/stats");
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
        }



    }
}
