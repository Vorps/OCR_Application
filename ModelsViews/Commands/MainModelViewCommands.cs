using OCR.View.WindowModal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace OCR.ModelsViews
{
    public partial class MainModelView
    {
        #region Properties Commands
        public Command BackCommand { get; set; }
        public Command TrashCommand { get; set; }
        public Command ProcessCommand { get; set; }
        public Command TrueCommand { get; set; }
        public Command FalseCommand { get; set; }
        public Command OpenMatriceCommand { get; set; }
        public Command ResetDataCommand { get; set; }

        public Command OpenSettingsCommand { get; set; }

        #endregion

        #region Action Commands
        public void TrashAction(object parameter)
        {
            this.canvas.Children.Clear();
            this.matrix.Clear();
            this.state = false;
            this.TrashCommand.OnActionChanged();
            this.BackCommand.OnActionChanged();
            this.ProcessCommand.OnActionChanged();
        }
        public void BackAction(object parameter)
        {
            this.canvas.Children.RemoveAt(this.canvas.Children.Count-1);
            this.matrix.Pop();
            if (this.canvas.Children.Count == 0)
            {
                this.state = false;
                this.TrashCommand.OnActionChanged();
                this.BackCommand.OnActionChanged();
                this.ProcessCommand.OnActionChanged();
            }
        }

        public void OpenSettingsAction(object parameter)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Owner = this._master;
            settingsWindow.ShowDialog();
        }

        public void ProcessAction(object parameter)
        {
            if (!this.worker.IsBusy)
                this.InProcess = "Visible";
                this.worker.RunWorkerAsync();
        }

        public void ResetDataAction(object parameter)
        {
            this.Data.Reset();
            this.Score = "0";
        }
            public void OpenMatriceAction(object parameter)
        {
            MatriceConfusionWindow matriceConfusionWindow = new MatriceConfusionWindow(this.Data.Labels, this.Data.ConfusMatrice);
            matriceConfusionWindow.Owner = this._master;
            matriceConfusionWindow.ShowDialog();
        }
        public void TrueAction(object parameter)
        {
            this.Data.AddTrue(this.Label);
            this.StateValid = true;
        }

        public void FalseAction(object parameter)
        {
          
            MessageBoxLabel messageBoxLabel = new MessageBoxLabel(this.Label);
            messageBoxLabel.Owner = this._master;
            messageBoxLabel.ShowDialog();
            if (messageBoxLabel.DialogResult == true)
            {
                this.Data.AddFalse(this.Label, messageBoxLabel.Label);
                this.StateValid = true;
            }

        }
        #endregion

        #region Action Can Commands
            public bool TrashActionCan(object parameter)
        {
            return this.state;
        }
        public bool BackActionCan(object parameter)
        {
            return this.state;
        }
        public bool ProcessActionCan(object parameter)
        {
            return this.state && this.StateValid;
        }

        public bool TrueActionCan(object parameter)
        {
            return !this.StateValid;
        }
        public bool FalseActionCan(object parameter)
        {
            return !this.StateValid;
        }
        #endregion
    }
}
