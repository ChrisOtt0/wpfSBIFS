using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wpfSBIFS.Tools
{
    public class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public Action<object> actionToInvoke;
        public Func<Task> actionToInvokeAsync { get; set; }

        public bool CanExecute(object? parameter)
        {
            return actionToInvoke != null || actionToInvokeAsync != null;
        }

        public void Execute(object? parameter)
        {
            if (CanExecute(parameter))
            {
                if (actionToInvoke != null)
                    actionToInvoke(parameter);
                else
                    actionToInvokeAsync();
            }
        }

        public Command(Action<object> action)
        {
            this.actionToInvoke = action;
        }

        public Command(Func<Task> action)
        {
            this.actionToInvokeAsync = action;
        }
    }
}
