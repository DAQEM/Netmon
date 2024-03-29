﻿using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Netmon.SNMPPolling.IntegrationTests;

public class SNMPIntegrationTests(WebApplicationFactory<SNMPPollingProgram> factory, ITestOutputHelper testOutputHelper)
    : IClassFixture<WebApplicationFactory<SNMPPollingProgram>>
{
    [Theory]
    [InlineData("/SNMP/GetBulkWalk", "1.3.6.1.2.1.1.4", "[{\"oid\":\"1.3.6.1.2.1.1.4.0\",\"value\":\"Root <root@snmpd.test>\"}]")]
    [InlineData("/SNMP/GetBulkWalk", "1.3.6.1.2.1.1.5", "[{\"oid\":\"1.3.6.1.2.1.1.5.0\",\"value\":\"snmpd-test\"}]")]
    [InlineData("/SNMP/GetBulkWalk", "1.3.6.1.2.1.1.6", "[{\"oid\":\"1.3.6.1.2.1.1.6.0\",\"value\":\"SNMPD Test\"}]")]
    public async Task Post_EndpointsReturnSuccessAndCorrectContentType(string url, string oid, string expectedJsonResponse)
    {
        // Arrange
        HttpClient client = factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync($"{url}?oid={oid}&timeoutMillis=1000", new
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
    [InlineData("/SNMP/GetBulkWalk", "1.3.6.1.2.1.1.4")]
    [InlineData("/SNMP/GetBulkWalk", "1.3.6.1.2.1.1.5")]
    [InlineData("/SNMP/GetBulkWalk", "1.3.6.1.2.1.1.6")]
    public async Task Post_EndpointsReturnNotFound(string url, string oid)
    {
        // Arrange
        HttpClient client = factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync($"{url}?oid={oid}&timeoutMillis=1000", new
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