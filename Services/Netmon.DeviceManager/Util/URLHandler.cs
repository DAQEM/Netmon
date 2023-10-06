namespace Netmon.DeviceManager.Util;

public static class URLHandler
{
    private static readonly string Domain = Environment.GetEnvironmentVariable("ENV") == "production" ? "host.docker.internal" : "localhost";
    
    public static string GetSNMPPollingURL(string path)
    {
        return $"http://{Domain}:5003/api/{path}";
    }
}