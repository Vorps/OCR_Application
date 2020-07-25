using OCR.ModelsViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OCR.View
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {


        private MainModelView mainModelView;
        public MainView()
        {
            InitializeComponent();
            this.mainModelView = new MainModelView(this.FormGenerator, this);
            this.DataContext = this.mainModelView;
        }


        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point position = e.GetPosition(this.FormGenerator);
                Point point = new Point((int)position.X / 20, (int)position.Y / 20);
                if (point.X >= 0 && point.X < 14 && point.Y >= 0 && point.Y < 14 && !this.mainModelView.matrix.Contains(point))
                {
                    this.mainModelView.matrix.Push(point);
                    this.mainModelView.BackCommand.CanExecute(true);
                    Rectangle rect = new Rectangle();
                    rect.Stroke = new SolidColorBrush(Colors.Black);
                    rect.Fill = new SolidColorBrush(Colors.Black);
                    rect.Width = 20;
                    rect.Height = 20;
                    Canvas.SetLeft(rect, point.X*20);
                    Canvas.SetTop(rect, point.Y * 20);
                    this.FormGenerator.Children.Add(rect);
                }
                if (!this.mainModelView.state)
                {
                    this.mainModelView.state = true;
                    this.mainModelView.TrashCommand.OnActionChanged();
                    this.mainModelView.BackCommand.OnActionChanged();
                    this.mainModelView.ProcessCommand.OnActionChanged();
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.mainModelView.Data.Save();
            Properties.Settings.Default.NameModel = this.mainModelView.FunctionPredictor;
            Properties.Settings.Default.Save();
        }
    }
}
