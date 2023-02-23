namespace Service.Status.Monitor.Api;

public class Service
{
    private string _status;
    private string _displayName;
    private string _serviceName;
    private string _machineName;

    public string Status { get => _status; set => _status = value; }
    public string DisplayName { get => _displayName; set => _displayName = value; }
    public string ServiceName { get => _serviceName; set => _serviceName = value; }
    public string MachineName { get => _machineName; set => _machineName = value; }
}
