using System.ServiceProcess;
using Microsoft.AspNetCore.Mvc;

namespace Service.Status.Monitor.Api.Controllers;

[ApiController]
[Route("service")]
public class MonitorServiceController : ControllerBase
{
    private readonly ILogger<MonitorServiceController> _logger;
    private ServiceController _serviceController = new ServiceController();

    public MonitorServiceController(ILogger<MonitorServiceController> logger)
    {
        _logger = logger;
    }

    [HttpGet("all")]
    public IActionResult Get()
    {
        var services = ServiceController.GetServices();

        var response = new List<Service>();

        Service service;

        foreach (var widowsService in services)
        {
            service = MakeService(widowsService);

            response.Add(service);
        }

        return Ok(response);
    }

    [HttpGet]
    public IActionResult Get(string name)
    {
        _serviceController = new ServiceController(name);

        var response = MakeService(_serviceController);

        return Ok(response);
    }

    [HttpPost("start")]
    public IActionResult Post([FromBody] Request request)
    {
        _serviceController = new ServiceController(request.ServiceName);

        if (IsStopped)
        {
            _serviceController.Start();
        }

        _serviceController.Refresh();

        var response = MakeService(_serviceController);

        return Ok(response);

    }

    [HttpPost("stop")]
    public IActionResult Stop([FromBody] Request request)
    {
        _serviceController = new ServiceController(request.ServiceName);

        if (IsRunnig)
        {
            _serviceController.Stop();
        }

        _serviceController.Refresh();

        var response = MakeService(_serviceController);

        return Ok(response);

    }

    private Service MakeService(ServiceController serviceController) =>
      new Service
      {
          Status = serviceController.Status.ToString(),
          DisplayName = serviceController.DisplayName,
          ServiceName = serviceController.ServiceName,
          MachineName = serviceController.MachineName
      };

    private bool IsRunnig => _serviceController.Status.Equals(ServiceControllerStatus.Running) ||
               _serviceController.Status.Equals(ServiceControllerStatus.StartPending);

    private bool IsStopped => _serviceController.Status.Equals(ServiceControllerStatus.Stopped) ||
                _serviceController.Status.Equals(ServiceControllerStatus.StopPending);

}
