using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OCR.ModelsViews
{
    public class Command : ICommand
    {
        public delegate void Action(object parameter);
        public delegate bool CanAction(object parameter);

        private Action action;
        private CanAction canAction;

        public Command(Action action) : this(action, (parameter) => { return true; })
        {

        }

        public Command(Action action, CanAction canAction)
        {
            this.action = action;
            this.canAction = canAction;
        }

        public bool CanExecute(object parameter)
        {
            return this.canAction(parameter);
        }

        public void Execute(object parameter)
        {
            this.action(parameter);
        }

        public void OnActionChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
