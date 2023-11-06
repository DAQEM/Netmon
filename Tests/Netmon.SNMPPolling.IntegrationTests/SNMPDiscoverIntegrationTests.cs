using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Netmon.SNMPPolling.IntegrationTests;

public class SNMPDiscoverIntegrationTests : IClassFixture<WebApplicationFactory<SNMPPollingProgram>>
{
    private readonly WebApplicationFactory<SNMPPollingProgram> _factory;

    public SNMPDiscoverIntegrationTests(WebApplicationFactory<SNMPPollingProgram> factory)
    {
        _factory = factory;
    }
    
    [Theory]
    [InlineData("/Discover/Details")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync(url, new
        {
            version = "V2",
            ipAddress = "127.0.0.1",
            port = 161,
            community = "public"
        });

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
    }
}