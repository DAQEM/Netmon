using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Netmon.SNMPPolling.IntegrationTests;

public class DiscoverIntegrationTests(
    WebApplicationFactory<SNMPPollingProgram> factory,
    ITestOutputHelper testOutputHelper)
    : IClassFixture<WebApplicationFactory<SNMPPollingProgram>>
{
    [Theory]
    [InlineData("/Discover/Details", "{\"ipAddress\":\"127.0.0.1\",\"port\":161,\"community\":\"public\",\"name\":\"snmpd-test\",\"location\":\"SNMPD Test\",\"contact\":\"Root <root@snmpd.test>\"}")]
    [InlineData("/Discover/Device", "{\"ipAddress\":\"127.0.0.1\",\"name\":\"snmpd-test\",\"location\":\"SNMPD Test\",\"contact\":\"Root <root@snmpd.test>\",\"deviceConnection\":{\"port\":161,\"community\":\"public\",\"authPassword\":\"\",\"privacyPassword\":\"\",\"authProtocol\":\"SHA256\",\"privacyProtocol\":\"AES\",\"contextName\":\"\",\"snmpVersion\":1},\"disks\":[],\"cpus\":[],\"memory\":[],\"interfaces\":[]}")]
    [InlineData("/Discover/Disks", "[]")]
    [InlineData("/Discover/Memory", "[]")]
    [InlineData("/Discover/Cpus", "[]")]
    [InlineData("/Discover/Interfaces", "[]")]
    public async Task Post_EndpointsReturnSuccessAndCorrectContentType(string url, string expectedJsonResponse)
    {
        // Arrange
        HttpClient client = factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync(url, new
        {
            version = "V2",
            ipAddress = "127.0.0.1",
            port = 161,
            community = "public"
        });

        testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        Assert.Equal(expectedJsonResponse, await response.Content.ReadAsStringAsync());
    }
    
    [Theory]
    [InlineData("/Discover/Details")]
    [InlineData("/Discover/Device")]
    [InlineData("/Discover/Disks")]
    [InlineData("/Discover/Memory")]
    [InlineData("/Discover/Cpus")]
    [InlineData("/Discover/Interfaces")]
    public async Task Post_EndpointsReturnNotFound(string url)
    {
        // Arrange
        HttpClient client = factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync(url, new
        {
            version = "V2",
            ipAddress = "1.1.1.1",
            port = 161,
            community = "public"
        });

        testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}