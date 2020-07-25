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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OCR.View.WindowModal
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class MessageBoxLabel : Window
    {

        public Command ValiderCommand { get; set; }
        public Command CancelCommand { get; set; }

        private String _label;
        public String Label
        {
            get { return this._label; }
            set { this._label = value; if (this._label == value) return;}
        }


        private CollectionView _labels;

        public CollectionView Labels
        {
            get { return this._labels; }
        }
        public MessageBoxLabel(String label)
        {
            InitializeComponent();
            this.DataContext = this;
            this.ValiderCommand = new Command(this.ValiderAction);
            this.CancelCommand = new Command(this.CancelAction);
            this.Label = "1";
            this._labels = new CollectionView((new String[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }).Where(x => !x.Equals(label)).ToList());
        }

        public void ValiderAction(object parameter)
        {
            this.DialogResult = true;
        }

        public void CancelAction(object parameter)
        {
            this.DialogResult = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
