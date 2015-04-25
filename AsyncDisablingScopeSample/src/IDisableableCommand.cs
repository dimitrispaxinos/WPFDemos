using System.Windows.Input;

namespace AsyncDisablingScopeSample
{
    public interface IDisableableCommand : ICommand, IDisableable
    {

    }
}
