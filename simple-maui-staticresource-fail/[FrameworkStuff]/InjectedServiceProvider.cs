namespace Nau.Simple.Maui.Staticresource.Fail;

/// <summary>
/// Provides a means to persist and access the configured Dependency Injection service provider for resolving a concrete implementation of a registered dependency.
/// </summary>
/// <remarks>
/// The platform specific instances of the application (Android and iOS) register the various services to be provided from the Dependency Injection container
/// as part of bootstrapping on initial start up. While in general the DI container will automatically resolve these dependencies via constructor injection,
/// there are scenarios where one needs to explicitly access the DI container and resolve a registered service. A static provider was the recommended approach.
/// </remarks>
public static class InjectedServiceProvider
{
	#region Initialize Method

	/// <summary>
	/// Initializes this provider with the registered Dependency Injection service provider.
	/// </summary>
	/// <param name="serviceProvider">The instance of the service provider configured with all the Dependency Injection registrations.</param>
	public static void Initialize(IServiceProvider serviceProvider)
	{
		Services = serviceProvider;
	}

	#endregion Initialize Method

	#region Public Methods

	/// <summary>
	/// Resolves and returns the implementation of the requested service from the Dependency Injection container.
	/// </summary>
	/// <typeparam name="T">The type of the service to resolve.</typeparam>
	/// <returns>The concrete instance of the requested type resolved from the DI container.</returns>
	public static T GetService<T>()
	{
		return (T)Services.GetService(typeof(T));
	}

	#endregion Public Methods

	#region Private Properties

	/// <summary>
	/// Gets or sets the instance of the Dependency Injection Service Provider.
	/// </summary>
	private static IServiceProvider Services { get; set; }

	#endregion Private Properties
}
