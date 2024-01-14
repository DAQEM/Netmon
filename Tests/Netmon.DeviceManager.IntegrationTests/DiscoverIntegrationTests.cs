using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Netmon.DeviceManager.IntegrationTests;

public class DiscoverIntegrationTests : IClassFixture<WebApplicationFactory<DeviceManagerProgram>>
{
    private const string IpAddress = "1.1.1.1";
    
    private readonly WebApplicationFactory<DeviceManagerProgram> _factory;
    private readonly string _token;

    public DiscoverIntegrationTests(WebApplicationFactory<DeviceManagerProgram> factory)
    {
        _factory = factory;
        const string email = "test@test.com";
        const string password = "P@ssw0rd!";

        //Using new HttpClient() instead of factory.CreateClient() because the factory client cannot access the AccountService
        HttpClient client = new();
        try
        {
            HttpResponseMessage registerResponse =
                client.PostAsJsonAsync("http://localhost:5001/register", new { email, password }).Result;
            HttpResponseMessage loginResponse =
                client.PostAsJsonAsync("http://localhost:5001/login", new { email, password }).Result;
            loginResponse.EnsureSuccessStatusCode();
            JsonElement jsonElement = loginResponse.Content.ReadFromJsonAsync<JsonElement>().Result;
            _token = jsonElement.GetProperty("accessToken").GetString() ?? string.Empty;
            Assert.NotEmpty(_token);
        }
        finally
        {
            client.Dispose();
        }
    }

    [Fact]
    [Order(1)]
    public async Task Post_CreateNewDevice()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync("/Device", new
        {
            ipAddress = IpAddress,
            connection = new
            {
                port = 161,
                community = "public",
                version = 2
            }
        });
        
        // Assert
        Assert.True(response.StatusCode is HttpStatusCode.Created or HttpStatusCode.Conflict);
    }

    [Fact]
    [Order(2)]
    public async Task GetDevices()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

        // Act
        HttpResponseMessage response = await client.GetAsync("/Device");
        response.EnsureSuccessStatusCode();
        JsonElement jsonElement = await response.Content.ReadFromJsonAsync<JsonElement>();

        // Assert
        Assert.Equal(JsonValueKind.Array, jsonElement.ValueKind);
        Assert.True(jsonElement.GetArrayLength() > 0);
        Assert.Contains(jsonElement.EnumerateArray(), x => x.GetProperty("ipAddress").GetString() == IpAddress);
    }
    
    [Fact]
    [Order(2)]
    public async Task GetDeviceById()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
        Guid id = await GetDeviceId(client);

        // Act
        HttpResponseMessage response = await client.GetAsync($"/Device/{id}");
        JsonElement jsonElement = await response.Content.ReadFromJsonAsync<JsonElement>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(JsonValueKind.Object, jsonElement.ValueKind);
        Assert.Equal(IpAddress, jsonElement.GetProperty("ipAddress").GetString());
    }
    
    [Fact]
    [Order(3)]
    public async Task UpdateDeviceCommunity()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
        Guid id = await GetDeviceId(client);

        // Act
        HttpResponseMessage response = await client.PutAsJsonAsync($"/Device/{id}", new
        {
            ipAddress = IpAddress,
            connection = new
            {
                port = 161,
                community = "private",
                version = 2
            }
        });
        
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.True(response.StatusCode is HttpStatusCode.OK);
    }
    
    [Fact]
    [Order(4)]
    public async Task GetDeviceConnection()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

        // Act
        HttpResponseMessage response = await client.GetAsync("/Device/Connection");
        JsonElement jsonElement = await response.Content.ReadFromJsonAsync<JsonElement>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(JsonValueKind.Array, jsonElement.ValueKind);
        Assert.True(jsonElement.GetArrayLength() > 0);
        Assert.Contains(jsonElement.EnumerateArray(), x => x.GetProperty("community").GetString() == "private");
    }

    [Fact]
    [Order(4)]
    public async Task GetDeviceDiskStatistics()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
        Guid id = await GetDeviceId(client);

        // Act

        HttpResponseMessage response = await client.GetAsync($"/Device/{id}/Statistics/Disk");
        JsonElement jsonElement = await response.Content.ReadFromJsonAsync<JsonElement>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(JsonValueKind.Object, jsonElement.ValueKind);
        Assert.Equal(JsonValueKind.Array, jsonElement.GetProperty("disks").ValueKind);
    }
    
    [Fact]
    [Order(4)]
    public async Task GetDeviceInterfaceStatistics()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
        Guid id = await GetDeviceId(client);

        // Act

        HttpResponseMessage response = await client.GetAsync($"/Device/{id}/Statistics/interface/inout");
        JsonElement jsonElement = await response.Content.ReadFromJsonAsync<JsonElement>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(JsonValueKind.Object, jsonElement.ValueKind);
        Assert.Equal(JsonValueKind.Array, jsonElement.GetProperty("interfaces").ValueKind);
    }
    
    [Fact]
    [Order(5)]
    public async Task DeleteDevice()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
        Guid id = await GetDeviceId(client);
        
        // Act
        HttpResponseMessage response = await client.DeleteAsync($"/Device/{id}");
        
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.True(response.StatusCode is HttpStatusCode.OK);
    }

    private static async Task<Guid> GetDeviceId(HttpClient client)
    {
        HttpResponseMessage response = await client.GetAsync("/Device");
        response.EnsureSuccessStatusCode();
        JsonElement jsonElement = await response.Content.ReadFromJsonAsync<JsonElement>();
        return jsonElement.EnumerateArray().First(x => x.GetProperty("ipAddress").GetString() == IpAddress).GetProperty("id").GetGuid();
    }
}