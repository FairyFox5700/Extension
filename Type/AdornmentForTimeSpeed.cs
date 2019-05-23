using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.TextManager.Interop;

namespace Extension
{
    /// <summary>
    /// AdornmentForTimeSpeed places red boxes behind all the "a"s in the editor window
    /// </summary>
    internal sealed class AdornmentForTimeSpeed
    {
        /// <summary>
        /// The layer of the adornment.
        /// </summary>
        /// Timer timer;
      // private static DispatcherTimer timer;
        private int Speed { get; set; }
        private int Max{ get; set; }
        private readonly IAdornmentLayer layer;
        private static  DateTime startTime;
        UserBoxCintrolOfTimeSpeed userBoxControl;
        private int maxCountOfCharacters;
        private static TimeTracker TimeTracker;
        private static bool IsStarted = true;
       

        /// <summary>
        /// Text view where the adornment is created.
        /// </summary>
         private readonly IWpfTextView view;
        /// <param name="view">Text view to create the adornment for</param>
        public AdornmentForTimeSpeed(IWpfTextView view)
        {
            
            this.view = view;
            userBoxControl = new UserBoxCintrolOfTimeSpeed();
            maxCountOfCharacters = 0;
            startTime = DateTime.UtcNow;
            this.layer = view.GetAdornmentLayer("AdornmentForTimeSpeed");
            view.ViewportHeightChanged += delegate { OnLayoutChanged(); };
            view.ViewportWidthChanged += delegate { OnLayoutChanged(); };
            TimeTracker = new TimeTracker(userBoxControl);
           
        }

        public static void buttonStart_Click(object sender, EventArgs e)
        {
            if (TimeTracker.timer != null)
            {
                IsStarted = true;
                TimeTracker.timer.IsEnabled = true;
                TimeTracker.saveTime.IsEnabled = true;
            }
        }

        public static void buttonStop_Click(object sender, EventArgs e)
        {
            IsStarted = false;
            TimeTracker.timer.IsEnabled = false;
            TimeTracker.saveTime.IsEnabled = false;
        }


     

        /// <summary>
        /// Handles whenever the text displayed in the view changes by adding the adornment to any reformatted lines
        /// </summary>
        /// <remarks><para>This event is raised whenever the rendered text displayed in the <see cref="ITextView"/> changes.</para>
        /// <para>It is raised whenever the view does a layout (which happens when DisplayTextLineContainingBufferPosition is called or in response to text or classification changes).</para>
        /// <para>It is also raised whenever the view scrolls horizontally or when its size changes.</para>
        /// </remarks>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        internal void OnLayoutChanged()
        {
            this.layer.RemoveAdornment(userBoxControl);
            Canvas.SetLeft(userBoxControl, view.ViewportRight -80);
            Canvas.SetTop(userBoxControl, view.ViewportTop+15);

            this.layer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, userBoxControl, null);
        }
        internal void OnLayoutChanged2(ChartData chartData)
        {
            this.layer.RemoveAdornment(chartData);
            Canvas.SetLeft(chartData, view.ViewportRight - 80);
            Canvas.SetTop(chartData, view.ViewportTop + 15);

            this.layer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, chartData, null);
        }

            public void ListenToTextSpeed(int typedText)
            {
            if (IsStarted)
            {
                int max = 1000;
                double level = 0;
                DateTime currentTime = DateTime.UtcNow;
                var interval = currentTime.Subtract(startTime).TotalMinutes;
                int speed = (int)(typedText / interval);
                userBoxControl.valTypedText.Content = speed;

                if (speed > maxCountOfCharacters)
                {
                    maxCountOfCharacters = speed;
                    userBoxControl.maxCountOfTypedCharacters.Content = "Max:" + maxCountOfCharacters.ToString();
                }
                Speed = speed;
                Max = maxCountOfCharacters;
                var percent = (speed * 100 / Max);
                double fl = Convert.ToDouble(percent * Max / 100);
                var value = fl;
                userBoxControl.pbStatus.Maximum = Max;
                userBoxControl.pbStatus.Value = value;
            }
        }

        private void ButtonOpenChart_Click(object sender, RoutedEventArgs e)
        {
            var userControls = new ChartData();
            OnLayoutChanged2(userControls);
        }
    }
    }
