namespace Extension
{
    partial class ChartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartLinear = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartLinear)).BeginInit();
            this.SuspendLayout();
            // 
            // chartLinear
            // 
            this.chartLinear.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated90;
            chartArea1.Name = "ChartArea";
            this.chartLinear.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartLinear.Legends.Add(legend1);
            this.chartLinear.Location = new System.Drawing.Point(76, 62);
            this.chartLinear.Name = "chartLinear";
            series1.ChartArea = "ChartArea";
            series1.Legend = "Legend1";
            series1.Name = "TimeSpended";
            this.chartLinear.Series.Add(series1);
            this.chartLinear.Size = new System.Drawing.Size(854, 472);
            this.chartLinear.TabIndex = 0;
            this.chartLinear.Text = "chartLinear";
            this.chartLinear.Click += new System.EventHandler(this.ChartLinear_Click);
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 571);
            this.Controls.Add(this.chartLinear);
            this.Name = "ChartForm";
            ((System.ComponentModel.ISupportInitialize)(this.chartLinear)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart chartLinear;
    }
}