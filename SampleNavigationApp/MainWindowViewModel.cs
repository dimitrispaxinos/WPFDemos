using System.Windows.Input;

namespace SampleNavigationApp
{
   public class MainWindowViewModel
    {
       private readonly ViewNavigator _viewNavigator;
       public ICommand GoToSecondViewCommand { get; private set; }
       public ICommand GoToFirstViewCommand { get; private set; }

       public MainWindowViewModel(ViewNavigator viewNavigator)
       {
           _viewNavigator = viewNavigator;
           GoToFirstViewCommand = new RelayCommand(GoToFirstViewCommandHandler);
           GoToSecondViewCommand = new RelayCommand(GoToSecondViewCommandHandler);
       }

       private void GoToSecondViewCommandHandler(object notUsed)
       {
           _viewNavigator.GoToSecondView();
       }

       private void GoToFirstViewCommandHandler(object notUsed)
       {
           _viewNavigator.GoToFirstView();
       }
    }
}
