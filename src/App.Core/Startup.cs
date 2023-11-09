using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Orleans.Configuration;

using StackExchange.Redis;

namespace App.Core;

public static class Startup
{
    public static void ConfigureCore(this IServiceCollection builder)
    {
        builder.AddOrleans(siloBuilder =>
            {
                siloBuilder.AddRedisGrainStorage("Redis",
                optionsBuilder => optionsBuilder.Configure(options =>
                    {
                        options.ConfigurationOptions = ConfigurationOptions.Parse("redis:6379,abortConnect=false");
                        options.ConfigurationOptions.DefaultDatabase = 0;
                    })
                );
                siloBuilder.UseRedisClustering(options =>
                    {
                        options.ConfigurationOptions = ConfigurationOptions.Parse("redis:6379,abortConnect=false");
                        options.ConfigurationOptions.DefaultDatabase = 0;
                    }
                );
                siloBuilder.Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "Dev";
                    options.ServiceId = "DevService";
                });

                siloBuilder.Configure<ClientMessagingOptions>(options => options.ResponseTimeout = TimeSpan.FromMilliseconds(120000));
                siloBuilder.Configure<SiloMessagingOptions>(options => options.ResponseTimeout = TimeSpan.FromMilliseconds(45000));
                siloBuilder.Configure<MessagingOptions>(options => options.ResponseTimeout = TimeSpan.FromMilliseconds(10000));
            });
    }
}
