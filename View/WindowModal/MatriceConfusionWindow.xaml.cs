using OCR.ModelsViews;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour MatriceConfusionWindow.xaml
    /// </summary>
    public partial class MatriceConfusionWindow : Window
    {

        public Command CloseWindowsCommand { get; set; }
        public MatriceConfusionWindow(List<String> labels, int[,] matrice)
        {
            InitializeComponent();
            this.DataContext = this;
            int dxy = (400-10) / (labels.Count+1);

            for (int x = 0; x < labels.Count; x++)
            {
                TextBlock label = new TextBlock { Text = labels[x] + "" };
                label.Foreground = Brushes.AliceBlue;
                label.FontSize = 15;
                Canvas.SetLeft(label, 30+x * dxy);
                Canvas.SetTop(label, 0);
                this.Matrice.Children.Add(label);
            }

            for (int x = 0; x < labels.Count; x++)
            {
                TextBlock label = new TextBlock { Text = labels[x] + "" };
                label.Foreground = Brushes.AliceBlue;
                label.FontSize = 15;
                Canvas.SetTop(label, 30 + x * dxy);
                Canvas.SetLeft(label, 0);
                this.Matrice.Children.Add(label);
            }
            for (int x = 0; x < labels.Count; x++)
            {
                for (int y = 0; y < labels.Count; y++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Stroke = Brushes.LightSteelBlue;
                    if (matrice[x, y] != 0)
                    {
                        if (x != y)
                            rectangle.Fill = Brushes.LightPink;
                        else
                            rectangle.Fill = Brushes.LightGreen;
                    }
                    else
                        rectangle.Fill = Brushes.White;

                    rectangle.Width = dxy;
                    rectangle.Height = dxy;
                    Canvas.SetLeft(rectangle, 20+x * dxy);
                    Canvas.SetTop(rectangle, 20 + y * dxy);


                    this.Matrice.Children.Add(rectangle);
                    if (matrice[x, y] != 0)
                    {
                        TextBlock value = new TextBlock { Text = matrice[x, y] + "" };
                        value.Foreground = Brushes.Black;
                        value.FontSize = 12;
                        Canvas.SetLeft(value, 35 + x * dxy);
                        Canvas.SetTop(value, 30 + y * dxy);
                        this.Matrice.Children.Add(value);
                    }
   
                 

                }
            }
          
            this.CloseWindowsCommand = new Command(this.CloseWindowsAction);
        }

        public void CloseWindowsAction(object parameter)
        {
            this.DialogResult = true;
        }
    }
}
