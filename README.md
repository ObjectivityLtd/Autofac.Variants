# Bot.Plugins

Library for Bot Framework that makes it easy to introduce multi-tenant plugins by defining resources and customer-specific logic.

# Note

This project is still a work in progress, all contributions from your site will be very desirable!

## Installation:

You can install the package using the nuget:
Install-Package Objectivity.Bot.Plugins

## Usage

### Project structure

To use the library properly you need to structurize your project correctly. It’s necessary to define the project for each tenant using the similar pattern, eg:

* `MyBot.Plugins.Company1`
* `MyBot.Plugins.CompanyN`

If you want to define a logic that should be tenant-specific, it is recommended to define the common project, containing the interfaces that can be implemented for specific tenants:

* MyBot.Plugins.Common

### Implement plugins

At first you need to define the interface that should be resolved using plugins, which inherits from the IPluginType interface. Example:

```cs
using Objectivity.Bot.Plugins;

public interface ITenantDialog : IPluginType {
}
```

Each plugin can introduce its own implementation of the interface. It is essential to include the export decorator. Example:

```cs
[Serializable]
[Export(typeof(ITenantDialog))]
[]
public class CompanyNTenantDialog : ITenantDialog {
}
```

Note: Export annotation is available after referencing System.ComponentModel.Composition assembly.

### Register & use the plugins

To use the plugins you need at first to register them in your DI module. You will need to deliver the implementation of ITenancySettings interface:

```cs
namespace MyBot.Settings
{
    using System.Configuration;
    using Objectivity.Bot.Plugins.Settings;

    public class TenancySettings : ITenancySettings
    {
        public string TenantName => ConfigurationManager.AppSettings["TenantName"];

        public string PluginAssemblyNamePrefix => "MyBot.Plugins";
    }
}

```

Then you can use the helper method available in `Objectivity.Bot.Plugins.DI` namespace:

```cs
using Autofac;
using Objectivity.Bot.Plugins.DI;

public class LuisDialogsModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		base.Load(builder);
		
		// register your types here

		PluginsRegistrator.RegisterPlugins(builder, new TenancySettings());
	}
}
```

Finally, you can use the generic IPluginTypeProvider, which is getting registered to your container. The provider can be injected to your code, example:

```cs
public class DialogManager
{
	public DialogManager(IPluginTypeProvider<ITenantDialog> pluginTypeProvider)
	{
	}
}
```

The IPluginTypeProvider comes with 2 methods:

* GetDefaultPluginType - delivers the instance of the plugin implementation based on current tenant name
* GePluginTypeFor - delivers the instance of the plugin implementation for tenant name specified in parameter.

### Plugin resources

The library provides also the resources feature. You can define the Resources folder in some of your plugin projects with RESX resources, that can be used globally in your bot, example:

* `MyBot.Plugins.Common.Resources` -> TenantMessages.resx
* `MyBot.Plugins.Company1.Resources` -> TenantMessages.resx

The resource file needs to be marked as embedded resource. Also, for each assembly, which delivers the resources you need to create expoeted Resource Provider class, example:

```cs
public class Company1ResourceProvider : BaseAssemblyResourceProvider
{
	public override string TenantName => "Company1";
}
```

Finally, you can use the IResourcesManager, which is getting registered to your container. The provider can be injected to your code, example:

```cs
public class ResourceConsumer
{
	public ResourceConsumer(IResourcesManager resourcesManager)
	{
		var text1 = resourcesManager.GetString("Text1_Key", "TenantMessages");
	}
}
```

The resources manager returns the resource string from embedded resource included in the assembly matched with currently selected tenant.