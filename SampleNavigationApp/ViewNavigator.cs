namespace SampleNavigationApp
{
    public class ViewNavigator
    {
        private readonly MainWindow _mainWindow;

        public ViewNavigator(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _mainWindow.MainContentControl.Content = new FirstView();
        }

        public void GoToFirstView()
        {
            _mainWindow.MainContentControl.Content = new FirstView();
        }

        public void GoToSecondView()
        {
            _mainWindow.MainContentControl.Content = new SecondView();
        }

    }
}
