using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jinder.Desktop.Commands
{
    public class BaseCommand : ICommand
    {
        private readonly Action<Object> _action;

        public BaseCommand(Action<Object> action)
        {
            _action = action;
        }

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter)
        {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
    public class BaseCommand<T> : ICommand
    {
        private readonly Action<T> _action;

        public BaseCommand(Action<T> action)
        {
            _action = action;
        }

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter)
        {
            _action((T)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
