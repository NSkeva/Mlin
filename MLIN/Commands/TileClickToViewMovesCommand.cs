using System;
using System.Windows.Input;
using ReactiveUI.Xaml;

namespace MLIN.Commands
{
    public class TileClickToViewMovesCommand : IReactiveCommand
    {
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
        }

        public event EventHandler CanExecuteChanged;
        public IDisposable Subscribe(IObserver<object> observer)
        {
            throw new NotImplementedException();
        }

        public IObservable<Exception> ThrownExceptions { get; private set; }
        public IObservable<bool> CanExecuteObservable { get; private set; }
    }
}