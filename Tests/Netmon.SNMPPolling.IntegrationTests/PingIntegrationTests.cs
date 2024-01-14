using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Netmon.SNMPPolling.IntegrationTests;

public class PingIntegrationTests(WebApplicationFactory<SNMPPollingProgram> factory, ITestOutputHelper testOutputHelper)
    : IClassFixture<WebApplicationFactory<SNMPPollingProgram>>
{
    [Theory]
    [InlineData("/Ping", "{\"message\":\"Pong\"}")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url, string expectedJsonResponse)
    {
        // Arrange
        HttpClient client = factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.GetAsync(url);

        testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        Assert.Equal (expectedJsonResponse, await response.Content.ReadAsStringAsync());
        //i love yiouuu
    }
    //hey ik hou van jouuuuuuuuuu kusjessssssss 
}