using H3_Symmetric_encryption.Views;
using H3_Symmetric_encryption.Interfaces.Controllers;

namespace H3_Symmetric_encryption.Controllers
{
    public class MainController : IMainController
    {
        private readonly IPerformanceTestController _performanceTestController;
        private readonly ITestResultsController _testResultsController;


        public MainController(
            IPerformanceTestController performanceTestController,
            ITestResultsController testResultsController
        )
        {
            _performanceTestController = performanceTestController;
            _testResultsController = testResultsController;
        }

        public void HandleMainMenu()
        {
            string input = MainMenuView.SelectMainMenuOption();

            switch (input)
            {
                case "1":
                    _performanceTestController.HandleTestEncryptionPerformanceMenu();
                    break;
                case "2":
                    _testResultsController.HandleViewTestResultsMenu();
                    break;
                default:
                    throw new InvalidOperationException("Input is out of range");
            }
        }
    }
}