﻿using System.Reflection;
using TestWare.Core;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;
using TestWare.Engines.Appium.WinAppDriver;

namespace TestWare.Samples.WinAppDriver.Desktop;

internal class LifeCycle : AutomationLifeCycleBase
{
    protected override IEnumerable<Assembly> GetTestWareComponentAssemblies()
    {
        IEnumerable<Assembly> assemblies = new[]
        {
            typeof(Hook).Assembly
        };

        return assemblies;
    }

    protected override IEnumerable<IEngineManager> GetTestWareEngines()
    {
        IEnumerable<IEngineManager> engines = new[]
        {
            new WinAppDriverManager()
        };

        return engines;
    }

    protected override TestConfiguration GetConfiguration()
    {
        return ConfigurationManager.ReadConfigurationFile("TestConfiguration.Desktop.json");
    }
}
