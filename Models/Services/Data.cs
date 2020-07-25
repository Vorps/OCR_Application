using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace OCR.Models.Services
{
    [Serializable]
    public class Data
    {

        private bool _state;
        public String Name;

        public List<String> Labels;
        public int Total;
        public int[,] ConfusMatrice;
        public int Score;


        public Data(List<String> labels, String name)
        {
            this.Name = name;
            this.Labels = labels;
            this.Total = 0;
            this.ConfusMatrice = new int[labels.Count, labels.Count];
            this.Score = 0;
        }


        public void addTotal()
        {
            this.Total += 1;
            this._state = true;
        }

        public void AddTrue(String label)
        {
            int index = Labels.LastIndexOf(label);
            ConfusMatrice[index, index] += 1;
            this.Score += 1;
            this._state = true;
        }


        public double GetScore()
        {
            return Math.Round((this.Total != 0 ? ((double)this.Score) / this.Total : 0)*100);
        }


        public void AddFalse(String falseLabel, String trueLabel)
        {
            int indexFalse = Labels.LastIndexOf(falseLabel);
            int indexTrue = Labels.LastIndexOf(trueLabel);
            ConfusMatrice[indexTrue, indexFalse] += 1;
            this._state = true;
        }

        public static Data Open(String name)
        {
            Data data;
            if (Data.IsExistData(name))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(Properties.Settings.Default.AppPath +@"\"+ name+".ocr", FileMode.Open, FileAccess.Read);
                data = (Data)formatter.Deserialize(stream);
            }
            else
            {
                data = new Data(new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, name);
            }
            return data;
        }

        public void Reset()
        {
            this.Score = 0;
            this.Total = 0;
            Array.Clear(this.ConfusMatrice, 0,Labels.Count* Labels.Count);
        }
        public void Save()
        {
            if (Directory.Exists(Properties.Settings.Default.AppPath + @"\") && this._state)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(Properties.Settings.Default.AppPath + @"\" + this.Name + ".ocr", FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, this);
                stream.Close();
            }
        }

        public static bool IsExistData(String name)
        {
            return File.Exists(Properties.Settings.Default.AppPath + @"\" + name+".ocr");
        }
    }
}
