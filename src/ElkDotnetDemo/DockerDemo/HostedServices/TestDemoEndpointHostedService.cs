using static System.Threading.Tasks.Task;

namespace DockerDemo.HostedServices;

public class TestDemoEndpointHostedService : BackgroundService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<TestDemoEndpointHostedService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public TestDemoEndpointHostedService(ILogger<TestDemoEndpointHostedService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("TEST DEMO ENDPOINT HOSTED SERVICE STARTS NOW!");

        var httpClient = _httpClientFactory.CreateClient("GitHub");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("BEGIN DEMO ENDPOINT REQUEST!");

            try
            {
                var randomQuery = GetRandomWord();

                var httpResponseMessage = await httpClient.GetAsync(
                    $"https://localhost:49153/api/Demo/log-stuff?query={randomQuery}", stoppingToken);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    _logger.LogInformation("IT WAS A SUCCESS");
                }
                else
                {
                    _logger.LogError("IT FAILED");
                }

                _logger.LogInformation("BEGIN DEMO ENDPOINT REQUEST!");

            }
            catch(HttpRequestException ex)
            {
                _logger.LogCritical($"{nameof(HttpRequestException)}: {ex.Message}");
            }

            await Delay(3000, stoppingToken);
        }

        throw new NotImplementedException();
    }

    private static string GetRandomWord()
    {
        return Summaries[Random.Shared.Next(Summaries.Length)];
    }
}