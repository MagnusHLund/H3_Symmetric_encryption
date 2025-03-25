using H3_Symmetric_encryption.Data;
using Microsoft.EntityFrameworkCore;
using H3_Symmetric_encryption.Controllers;
using H3_Symmetric_encryption.Repositories;
using Microsoft.Extensions.DependencyInjection;
using H3_Symmetric_encryption.Interfaces.Controllers;
using H3_Symmetric_encryption.Interfaces.Repositories;

namespace H3_Symmetric_encryption
{
	internal static class Program
	{
		internal static void Main()
		{
			IServiceProvider serviceProvider = ConfigureServices();

			using (IServiceScope scope = serviceProvider.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<AlgorithmPerformanceDbContext>();
				dbContext.Database.Migrate(); 

				IMainController mainController = scope.ServiceProvider.GetRequiredService<IMainController>();
				mainController.HandleMainMenu();
			}
		}

		private static ServiceProvider ConfigureServices()
		{
			ServiceCollection serviceCollection = new ServiceCollection();

			serviceCollection.AddSingleton<IMainController, MainController>();
			serviceCollection.AddSingleton<IHexadecimalController, HexadecimalController>();
			serviceCollection.AddSingleton<ITestResultsController, TestResultsController>();
			serviceCollection.AddSingleton<IAesEncryptionController, AesEncryptionController>();
			serviceCollection.AddSingleton<IDesEncryptionController, DesEncryptionController>();
			serviceCollection.AddSingleton<IPerformanceTestController, PerformanceTestController>();
			serviceCollection.AddSingleton<IRijndaelEncryptionController, RijndaelEncryptionController>();

			serviceCollection.AddScoped<IFileController, FileController>();

			serviceCollection.AddSingleton<IAlgorithmPerformanceRepository, AlgorithmPerformanceRepository>();

			serviceCollection.AddDbContext<AlgorithmPerformanceDbContext>(options =>
				options.UseSqlite("Data Source=AlgorithmPerformance.db")
			);

			ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
			return serviceProvider;
		}
	}
}
