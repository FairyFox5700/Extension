using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension.Records
{

    class Record
    {
        private DateTime endDateTime;
        public DateTime DateTime
        {
            get => endDateTime;
            set
            {
                endDateTime = value;
            }
        }

        public string CurrentTimeSpended()
        {
            var longity = endDateTime.ToUniversalTime() - BeginDateTime.ToUniversalTime();
            return longity.ToString(@"hh\:mm\:ss");
        }

        public void ResetDataTime()
        {
            if (BeginDateTime.Date != DateTime.Date)
                BeginDateTime = endDateTime;
        }
        public bool IsTimeTrackerOn { get; set; }
        public string TimeSpended {get;set;} 
        public static string Separator { get; } = ";";


        public DateTime BeginDateTime { get; set; }

        public Record(DateTime beginDate , DateTime endData)
        {
            BeginDateTime = beginDate;
            DateTime = endData;
            IsTimeTrackerOn = true;
            TimeSpended = CurrentTimeSpended();
        }
        

        public Record(DateTime dateTime , bool IsTimeTrackerOn, string currentTime):this(dateTime,IsTimeTrackerOn)
        {
            TimeSpended = currentTime;
            IsTimeTrackerOn = true;
        }
        

        public Record(DateTime dateTime, bool IsTimeTrackerOn) 
        {
            DateTime = dateTime;
            this.IsTimeTrackerOn = IsTimeTrackerOn;
            TimeSpended = CurrentTimeSpended();
            
        }

        public override string ToString()

        {
            return string.Join(Separator,  DateTime.ToLocalTime().ToString(), IsTimeTrackerOn,TimeSpended);
        }


    }
}
