using System.Net.WebSockets;

namespace Netmon.DeviceManager.Jobs.Poll;

public interface IPollDeviceJob : IJob
{
    void Subscribe(WebSocket webSocket);
}