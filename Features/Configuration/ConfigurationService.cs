using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TestMaui.Features.Configuration;

public class ConfigurationService
{
    private readonly IConfiguration configuration;
    private readonly ILogger<ConfigurationService> logger;

    public ConfigurationService(
        IConfiguration configuration, 
        ILogger<ConfigurationService> logger)
    {
        this.configuration = configuration;
        this.logger = logger;
    }

    public Settings GetConfigurationSetting()
    {
        logger.LogInformation("Get settings from configuration");
        return configuration.GetRequiredSection("Settings").Get<Settings>();
    }
}
