using H3_Symmetric_encryption.Controllers;
using Microsoft.Extensions.DependencyInjection;
using H3_Symmetric_encryption.Interfaces.Controllers;

namespace H3_Symmetric_encryption
{
	internal static class Program
	{
		internal static void Main()
		{
			IServiceProvider serviceProvider = ConfigureServices();

			using (IServiceScope scope = serviceProvider.CreateScope())
			{
				IMainController mainController = scope.ServiceProvider.GetRequiredService<IMainController>();
				mainController.HandleMainMenuDisplay();
			}
		}

		private static ServiceProvider ConfigureServices()
		{
			ServiceCollection serviceCollection = new ServiceCollection();

			serviceCollection.AddSingleton<IMainController, MainController>();

			ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
			return serviceProvider;
		}
	}
}
