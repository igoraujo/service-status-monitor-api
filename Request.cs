namespace Service.Status.Monitor.Api;

public class Request
{
    private string serviceName;

    public string ServiceName { get => serviceName; set => serviceName = value; }
}
