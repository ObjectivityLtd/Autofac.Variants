# Autofac.Variants

Autofac enhancement that makes it easy to introduce multi-tenant variants by defining resources and customer-specific logic.

# Note

This project is still a work in progress, all contributions from your site will be very desirable!

## Installation:

You can install the package using the nuget:
Install-Package Autofac.Variants

## Usage

### Project structure

To use the library properly you need to structurize your project correctly. At first, you need to define common project, which will include default functionalities and resources:

* `MyApp`

Then you can define tenant-specific projects, implementing tenant logic and keeping its resources. Tenant-specific project's name must start with the suffix taken from the common project and tenant identifier. eg:

* `MyApp.FirstVariant`
* `MyApp.SecondVariant`

The project name part that stands after common project suffix (eg. `FirstVariant`, `SecondVariant`) is the tenant name.

Note: you can also define the shared/core project, which will be referenced in each variant project. That's recommended solution to deliver interfaces and shared objects across all projects.

### Implement variants

At first you need to define the interface that should be resolved as variant, which inherits from IVariant interface in shared project. Example:

```cs
using Autofac.Variants;

public interface IEmployeeService : IVariant {
}
```

Each variant implementation can introduce its own implementation of the interface. It is essential to include the export decorator. Example:

```cs
[Serializable]
[Export(typeof(IVariant))]
[]
public class FirstVariantEmployeeService : IEmployeeService {
}
```

Note: Export annotation is available after referencing System.ComponentModel.Composition assembly.
Note: Each class implementing variant interface must has unique name.

### Register & use the variants

To use the variant you need at first to register them in your DI module. You will need to deliver the implementation of ISettings interface:

```cs
namespace MyApp
{
    using System.Configuration;
    using Autofac.Variants.Settings;

    public class VariantSettings : ISettings
    {
        public string VariantId => ConfigurationManager.AppSettings["VariantId"];

        public string DefaultVariantAssemblyName => "MyApp";
    }
}

```

Then you can use the helper method available in `Autofac.Variants.DI` namespace in your Autofac DI module to register variants:

```cs
using Autofac;
using Autofac.Variants.DI;

public class MyAppAutofacModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		base.Load(builder);
		
		// register your types here

		VariantsRegistrator.RegisterVariants(builder, new VariantSettings());
	}
}
```

Finally, you can use generic IVariantResolver interface to resolve apropriate variant. The provider can be injected to your code, example:

```cs
public class ServicesProvider
{
	private readonly IVariantResolver<IEmployeeService> employeeServiceResolver;

	public ServicesProvider(IVariantResolver<IEmployeeService> employeeServiceResolver)
	{
		this.employeeServiceResolver = employeeServiceResolver;
	}
	
	public IEmployeeService GetEmployeeService()
	{
		return this.employeeServiceResolver.Resolve();
	}
}
```

The IVariantResolver has parameterless `Resolve` method, which delivers the implementation for variant using the following convention:

* If current variant project has type implementing requested interface - resolve it
* Else if common variant project has type implementing requested interface - resolve it
* Else - throw DefaultVariantInterfaceNotFoundException

### Variant resources

The library provides also the resources feature. To use it, you should create resources files (*.resx) sharing the same name in your common tenant project and in your tenant-specific projects, eg:

* `MyApp` -> Resources/VariantMessages.resx
* `MyApp.FirstVariant` -> Resources/VariantMessages.resx

The resource file needs to be marked as embedded resource. However, it doesn't need to generate designer class.

Now you can use the IResourcesManager, which is getting registered to your container. The provider can be injected to your code, example:

```cs
public class ResourceConsumer
{
	public ResourceConsumer(IResourcesManager resourcesManager)
	{
		var text1 = resourcesManager.GetString("Text1_Key", "VariantMessages");
	}
}
```

The IResourcesManager has GetString method, which delivers the resource string from embedded resources included in the variants assemblies using the following convention:

* If current variant project has embedded resource matching by name and it contains searched key - return it
* Else if common variant project has embedded resource matching by name and it contains searched key - return it
* Else - throw ResourceKeyNotFoundException