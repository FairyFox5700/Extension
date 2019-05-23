using Extension.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Extension
{
    public partial class ChartForm : Form
    {
       private string FilePath { get; set; }
        
        public ChartForm()
        {

            InitializeComponent();
            FilePath = "testRecord.txt";
            RecordToFile.FilePath = FilePath;
            chartLinear.ChartAreas["ChartArea"].AxisY.Title = "Time in seconds";
            chartLinear.ChartAreas["ChartArea"].AxisX.Title = "Day";
            chartLinear.ChartAreas["ChartArea"].AxisX.TitleAlignment = StringAlignment.Center;
            chartLinear.ChartAreas["ChartArea"].AxisY.TitleAlignment = StringAlignment.Center;
            chartLinear.ChartAreas["ChartArea"].AxisX.TextOrientation = TextOrientation.Horizontal;
            chartLinear.ChartAreas["ChartArea"].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chartLinear.Titles.Add("Time spended in IDE");
            chartLinear.Legends["Legend1"].Enabled = false;

        }
        public void button1_Click(object sender, EventArgs e)
        {
            
            chartLinear.Series["TimeSpended"].Points.Clear();
            addSeries();
            this.ShowDialog();
        }

        public void addSeries()
        {
            var data = GetDataFromRecord();
            foreach (var pair in data)
            {
                DataPoint dataPoint = new DataPoint();
                dataPoint.SetValueXY(pair.Key.ToString("yyyy-MM-dd"), pair.Value.timeInSeconds);
                chartLinear.Series["TimeSpended"].Points.Add(dataPoint);
                chartLinear.Series["TimeSpended"].Color = Color.Violet;
                chartLinear.AlignDataPointsByAxisLabel();
                chartLinear.ChartAreas["ChartArea"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                LabelStyle style = new LabelStyle();
                style.Angle = -90;
                 chartLinear.ChartAreas["ChartArea"].AxisX.LabelStyle = style;
                chartLinear.ChartAreas["ChartArea"].AxisX.IsLabelAutoFit = false;
                chartLinear.ChartAreas["ChartArea"].AxisX.LabelStyle = style;
            }
            chartLinear.Update();
        }
        struct ValueAxis
        {
           public double timeInSeconds { get; set; }
           public DateTime timeSpended { get; set; }
           public ValueAxis(double time, DateTime date)
            {
                timeInSeconds = time;
                timeSpended = date;
            }
        }
        private static Dictionary<DateTime,ValueAxis> GetDataFromRecord()
        {
            var data = new Dictionary<DateTime, ValueAxis>();
            var text = RecordToFile.GetRecords();
            if (text != null)
            {
                foreach (var keyValue in text)
                {
                    var key = keyValue.Key;
                    var value = ToDateTimeFormat(keyValue.Value.TimeSpended);
                    var timeValue = DateTime.Parse(keyValue.Value.TimeSpended);
                    data[key] = new ValueAxis( value,timeValue) ;
                }
            }
            return data;

        }
        private static double ToDateTimeFormat(string dataInString)
        {
            //TotalMinutes
            var startTime = DateTime.Parse(dataInString);
            var second = startTime.Second;
            var minutes = startTime.Minute;
            var hours = startTime.Hour;
            var timestamp = second + minutes * 60 + hours * 3600;

            return timestamp;
        }

        private void ChartLinear_Click(object sender, EventArgs e)
        {

        }
    }
}
