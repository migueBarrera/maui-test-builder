using Microsoft.Extensions.Logging;

namespace TestMaui.Features.NetworkClient;

public class TestNetworkService
{
    private readonly HttpClient httpClient;
    private readonly ILogger<TestNetworkService> logger;

    public TestNetworkService(
        ILogger<TestNetworkService> logger,
        IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient("testConfig");
        this.logger = logger;
    }
}
