using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Extension.Interuptions;
using Extension.Records;
using Microsoft.Win32;

namespace Extension
{
    class TimeTracker
    {
        private UserKeyBoard keyBoard;
        private UserMouse mouse;
        protected internal DispatcherTimer timer;
        protected internal DispatcherTimer saveTime;
        private Record record;
        private RecordToFile recordToFile;
        private bool isActive = true;
        UserBoxCintrolOfTimeSpeed userBoxControl;
        public TimeTracker(UserBoxCintrolOfTimeSpeed userBox)
        {
            keyBoard = new UserKeyBoard();
            mouse = new UserMouse();
            mouse.MouseMoved += Mouse_MouseMoved;
            keyBoard.KeyBoardPressed += KeyBoard_KeyBoardPressed;
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
            recordToFile = new RecordToFile("testRecord.txt");
            record = recordToFile.CurentRecord;
            userBoxControl = userBox;
            StartSaveTimer();
            DisplayTimer();
            
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if(e.Mode == PowerModes.Suspend)
            {
                isActive = false;
            }
            else if(e.Mode == PowerModes.Resume)
            {
                isActive = true;
            }
        }

        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
                isActive = false;
            if (e.Reason == SessionSwitchReason.SessionLogoff)
                isActive = false;
            if (e.Reason == SessionSwitchReason.SessionLogon)
                isActive = true;
            if (e.Reason == SessionSwitchReason.SessionUnlock)
                isActive = true;
            if (e.Reason == SessionSwitchReason.RemoteConnect)
                isActive = true;
            if (e.Reason == SessionSwitchReason.RemoteDisconnect)
                isActive = false;
            if (e.Reason == SessionSwitchReason.ConsoleConnect)
                isActive = true;
            if (e.Reason == SessionSwitchReason.ConsoleDisconnect)
                isActive = false;
            
        }

        private void KeyBoard_KeyBoardPressed(object sender, EventArgs e)
        {
            if(isActive)
            {
                record.DateTime = DateTime.Now;
            }

        }

        private void Mouse_MouseMoved(object sender, EventArgs e)
        {
            if (isActive)
            {
                record.DateTime = DateTime.Now;
            }
        }

        private void StartSaveTimer()
        {
            saveTime = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1, 0, 0)
            };
            saveTime.Tick += SaveTime_Tick;
            saveTime.Start();
        }

        private void SaveTime_Tick(object sender, EventArgs e)
        {
            var date = record.BeginDateTime;
           record.TimeSpended = record.CurrentTimeSpended();


             if (RecordToFile.GetKeyIfExists(date))
            {
                
                RecordToFile.Records[date.Date] = record;
            }
            else
            {
                RecordToFile.Records.Add(date.Date, record);
            }
            recordToFile.WriteRecords();
        }

        public void DisplayTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            userBoxControl.valTime.Content = record.CurrentTimeSpended();

        }

    }
}
