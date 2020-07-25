using OCR.ModelsViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OCR.View.WindowModal
{
    /// <summary>
    /// Logique d'interaction pour SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window, INotifyPropertyChanged
    {
        public Command ValiderCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command DirectoryCommand { get; set; }
        
        private String _directoryModel;
        public String DirectoryModel
        {
            get { return this._directoryModel; }
            set { this._directoryModel = value; this.OnPropertyChanged("DirectoryModel"); }
        }


        private String _directoryApp;
        public String DirectoryApp
        {
            get { return this._directoryApp; }
            set { this._directoryApp = value; this.OnPropertyChanged("DirectoryApp"); }
        }



        public SettingsWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.ValiderCommand = new Command(this.ValiderAction);
            this.CancelCommand = new Command(this.CancelAction);
            this.DirectoryCommand = new Command(this.DirectoryAction);
            this.DirectoryModel = Properties.Settings.Default.ModelPath;
            this.DirectoryApp = Properties.Settings.Default.AppPath;
        }

        public void DirectoryAction(object parameter)
        {
            CommonDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String path = (dialog as FolderBrowserDialog).SelectedPath;
                switch (parameter)
                {
                    case "1":
                        this.DirectoryModel = path;
                        break;
                    case "2":
                        this.DirectoryApp = path;
                        break;
                    default:
                        break;
                }
            }
        }
        public void ValiderAction(object parameter)
        {
            Properties.Settings.Default.ModelPath = this.DirectoryModel;
            Properties.Settings.Default.AppPath = this.DirectoryApp;
            Properties.Settings.Default.Save();
            this.DialogResult = true;
        }

        public void CancelAction(object parameter)
        {
            this.DialogResult = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
