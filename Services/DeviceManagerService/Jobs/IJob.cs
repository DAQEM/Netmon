namespace DeviceManagerService.Jobs;

public interface IJob
{
    Task Execute();
}