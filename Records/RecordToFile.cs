using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Extension.Records
{
    class RecordToFile
    {
        public static string FilePath { get; set; } 
        public static SortedDictionary<DateTime, Record> Records { get; set; } = new SortedDictionary<DateTime, Record>();

        public RecordToFile(string fileName)
        {
            FilePath = fileName;

           if (!File.Exists(fileName))
                File.CreateText(fileName);
        }

        public  static void AddNewRecord(Record record)
        {
            if (!GetKeyIfExists(record.DateTime.Date))
                Records.Add(record.DateTime.Date,record);
            
        }
        public  Record CurentRecord
        {
            get
            {
                var date = DateTime.Now;
                if (GetKeyIfExists(date))
                    return Records[date];
                else
                    return new Record(DateTime.Now,DateTime.Now.AddSeconds(1));
            }
        }

        public  static bool GetKeyIfExists(DateTime date)
        {
            foreach(var key in Records.Keys)
            {
                if (key.Date == date.Date)
                    return true;

            }
            return false;
        }

        public  static  SortedDictionary<DateTime, Record> GetRecords ()
        {
            if (FilePath != null)
            {
                using (var sr = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null && line != "")
                    {
                        string[] data = line.Split(Record.Separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        Record record = new Record(DateTime.Parse(data[0]), Convert.ToBoolean(data[1]), data[2]);
                        AddNewRecord(record);

                    }

                }
            }
            return Records;
        }
        

        public void WriteRecords()
        {
            GetRecords();
            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                foreach (var record in Records)
                {
                     
                    sw.WriteLine(record.Value);
                    }
            }
           
        }
    }

}
