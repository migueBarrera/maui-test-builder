using TestMaui.Features.Configuration;
using TestMaui.Features.NetworkClient;

namespace TestMaui;

public class MainViewModel
{
    public TestNetworkService TestNetworkService { get; set; }

    public ConfigurationService ConfigurationService { get; set; }

    public MainViewModel(
        TestNetworkService testNetworkService, 
        ConfigurationService configurationService)
    {
        TestNetworkService = testNetworkService;
        ConfigurationService = configurationService;
    }
}
