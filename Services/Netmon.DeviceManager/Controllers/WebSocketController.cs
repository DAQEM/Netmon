using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using Netmon.DeviceManager.Jobs.Poll;

namespace Netmon.DeviceManager.Controllers;

public class WebSocketController(IPollDeviceJob pollDeviceJob) : ControllerBase
{
    [HttpGet("ws")]
    public async Task<IActionResult> Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            OnWebSocketOpened(webSocket);

            await HandleWebSocket(webSocket, HttpContext.RequestAborted);
        }
        else
        {
            return BadRequest("WebSocket requests only");
        }

        return Ok();
    }

    private async Task HandleWebSocket(WebSocket webSocket, CancellationToken cancellationToken)
    {
        byte[] buffer = new byte[1024 * 4];
        
        while (webSocket.State == WebSocketState.Open)
        {
            try
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(buffer, cancellationToken);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }
            }
            catch (WebSocketException)
            {
                break;
            }
        }
        
        if (webSocket.State != WebSocketState.Closed)
        {
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by the server", cancellationToken);
        }
    }

    private void OnWebSocketOpened(WebSocket webSocket)
    {
        pollDeviceJob.Subscribe(webSocket);
    }
}