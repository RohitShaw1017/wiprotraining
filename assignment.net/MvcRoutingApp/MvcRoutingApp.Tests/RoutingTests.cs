using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing; 
using Xunit;
using MvcRoutingApp; // to see Program


namespace MvcRoutingApp.Tests
{
    public class RoutingTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public RoutingTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/Products/Electronics/101", "Category: Electronics, Product ID: 101")]
        [InlineData("/Users/john/Orders", "Showing orders for user: john")]
        public async Task Route_ReturnsExpectedContent(string url, string expected)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(expected, content);
        }

        [Fact]
        public async Task Dashboard_Admin_ReturnsAdminView()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/Dashboard/admin");
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("Admin", content);
        }

        [Fact]
        public async Task GuidRoute_ValidGuid_Works()
        {
            var client = _factory.CreateClient();
            var guid = "3f2a0b5f-7b3d-4c3c-93d0-18d49d56c8a2";

            var response = await client.GetAsync($"/Products/Guid/{guid}");
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(guid, content);
        }
    }
}
