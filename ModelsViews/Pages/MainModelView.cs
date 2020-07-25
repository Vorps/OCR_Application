using OCR.Models.Services;
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

namespace OCR.ModelsViews
{
    public partial class MainModelView : INotifyPropertyChanged
    {

        private BackgroundWorker worker;

        public Stack<Point> matrix;
        private Canvas canvas;
        private bool _stateValid;


        private String _inProcess;


        public String InProcess
        {
            get { return this._inProcess; }
            set { this._inProcess = value; this.OnPropertyChanged("InProcess"); }
        }
        private Window _master;
        public bool StateValid
        {
            get { return this._stateValid; }
            set { this._stateValid = value; this.Score = ""+this.Data.GetScore(); this.TrueCommand.OnActionChanged(); this.ProcessCommand.OnActionChanged(); this.FalseCommand.OnActionChanged(); }
        }

        private String _score;
        public String Score
        {
            get { return "Score : " + this._score + " %"; }
            set { this._score = value; this.OnPropertyChanged("Score"); }
        }


        private String _label;
        public String Label
        {
            get { return this._label; }
            set { this._label = value; this.OnPropertyChanged("Label"); }
        }

        public bool state { set; get; }

        public String _functionPredictor;
        public String FunctionPredictor
        {
            get { return this._functionPredictor; }
            set {
                if (this._functionPredictor == value) return;
                this.OnPropertyChanged("FunctionPredictor");
                if (Data.IsExistData(value))
                {
                    this.Data = Data.Open(value);
                    this.Score = "" + this.Data.GetScore();
                }
                /*if (Data.IsExistData(this._functionPredictor))
                {
                    this.Data.Save();
                }*/
                this._functionPredictor = value;
            }
        }


        public Data Data;
        public MainModelView(Canvas canvas, Window master)
        {
            if (Properties.Settings.Default.ModelPath.Length == 0) Properties.Settings.Default.ModelPath = Directory.GetCurrentDirectory();
            if (Properties.Settings.Default.AppPath.Length == 0) Properties.Settings.Default.AppPath = Directory.GetCurrentDirectory();
            
            this._master = master;
            this.TrashCommand = new Command(this.TrashAction, this.TrashActionCan);
            this.BackCommand = new Command(this.BackAction, this.BackActionCan);
            this.ProcessCommand = new Command(this.ProcessAction, this.ProcessActionCan);
            this.TrueCommand = new Command(this.TrueAction, this.TrueActionCan);
            this.FalseCommand = new Command(this.FalseAction, this.FalseActionCan);
            this.OpenMatriceCommand = new Command(this.OpenMatriceAction);
            this.ResetDataCommand = new Command(this.ResetDataAction);
            this.OpenSettingsCommand = new Command(this.OpenSettingsAction);

            this.matrix = new Stack<Point>();
            this.canvas = canvas;
            this.FunctionPredictor = Properties.Settings.Default.NameModel;
            this.Data = Data.Open(this.FunctionPredictor);
            this.state = false;
            this.StateValid = true;

            this.worker = new BackgroundWorker() { WorkerSupportsCancellation = true };
            this.worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            this.worker.DoWork += (sender, e) =>
            {
                try
                {
                    bool[,] image = new bool[28, 28];
                    foreach (Point point in this.matrix)
                    {
                        image[(int)point.Y * 2, (int)point.X * 2] = true;
                        image[(int)point.Y * 2 + 1, (int)point.X * 2] = true;
                        image[(int)point.Y * 2, (int)point.X * 2 + 1] = true;
                        image[(int)point.Y * 2 + 1, (int)point.X * 2 + 1] = true;
                    }

                    MLApp.MLApp matlab = new MLApp.MLApp();
                    matlab.Execute("cd " + Properties.Settings.Default.ModelPath);
                    matlab.PutWorkspaceData("X", "base", image);
                    matlab.Execute(@"label = " + this.FunctionPredictor + "(X)");
                    var label = matlab.GetVariable("label", "base");
                    e.Result = label;
                   
                }
                catch (Exception err)
                {
                    e.Result = err.Message;
                }
            };
            this.InProcess = "Hidden";
        }


        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Label = (String) e.Result;
            this.StateValid = false;
            this.TrashAction(null);
            this.Data.addTotal();
            this.InProcess = "Hidden";
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
