using LS_Mod_Synchronizer;
using Newtonsoft.Json;
using LS_Mod_Synchronizer.Model;
using LS_Mod_Synchronizer.Fetcher;
using LS_Mod_Synchronizer.Extensions;
using LS_Mod_Synchronizer.Logic;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using LS_Mod_Synchronizer.Web;

namespace LS_Mod_Synchronizer;

public class Program
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    
    public static void Main(string[] args)
    {
        var serviceProvider = BuildServiceProvider();

        var app = serviceProvider.GetRequiredService<Synchronizer>();
        app.Run();
    }

    private static ServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddScoped<Synchronizer>();
        services.AddScoped<IOnlineFetcher, OnlineFetcher>();
        services.AddScoped<ILocalFetcher, LocalFetcher>();
        services.AddScoped<IModDownloader, ModDownloader>();
        services.AddScoped<IHtmlWebLoader, HtmlWebLoaderProxy>();
        services.AddScoped<ModComparer>();

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
}