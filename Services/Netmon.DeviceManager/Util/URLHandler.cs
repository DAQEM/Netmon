namespace Netmon.DeviceManager.Util;

public static class UrlHandler
{
    private static readonly string Domain = Environment.GetEnvironmentVariable("ENV") == "production" ? "netmon-snmp-polling-service:80" : "localhost:5003";
    
    public static string GetSNMPPollingURL(string path)
    {
        return $"http://{Domain}/api/{path}";
    }
}