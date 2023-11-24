using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Netmon.SNMPPolling.IntegrationTests;

public class PollIntegrationTests : IClassFixture<WebApplicationFactory<SNMPPollingProgram>>
{
    private readonly WebApplicationFactory<SNMPPollingProgram> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    public PollIntegrationTests(WebApplicationFactory<SNMPPollingProgram> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData("/Poll/Device", "{\"ipAddress\":\"127.0.0.1\",\"name\":\"snmpd-test\",\"location\":\"SNMPD Test\",\"contact\":\"Root <root@snmpd.test>\",\"deviceConnection\":{\"port\":161,\"community\":\"public\",\"authPassword\":\"\",\"privacyPassword\":\"\",\"authProtocol\":\"SHA256\",\"privacyProtocol\":\"AES\",\"contextName\":\"\",\"snmpVersion\":1},\"disks\":[],\"cpus\":[],\"memory\":[],\"interfaces\":[]}")]
    public async Task Post_EndpointsReturnSuccessAndCorrectContentType(string url, string expectedJsonResponse)
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

        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        Assert.Equal(expectedJsonResponse, await response.Content.ReadAsStringAsync());
    }
    
    [Theory]
    [InlineData("/Poll/Device")]
    public async Task Post_EndpointsReturnNotFound(string url)
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync(url, new
        {
            version = "V2",
            ipAddress = "1.1.1.1",
            port = 161,
            community = "public"
        });

        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}