using System;
using Coding.Exercise;

var log = new NullLog();
var account = new Account(log);
account.SomeOperation();

namespace Coding.Exercise
{
    public interface ILog
    {
        // maximum # of elements in the log
        int RecordLimit { get; }
    
        // number of elements already in the log
        int RecordCount { get; set; }

        // expected to increment RecordCount
        void LogInfo(string message);
    }

    public class Account
    {
        private ILog log;

        public Account(ILog log)
        {
            this.log = log;
        }

        public void SomeOperation()
        {
            int c = log.RecordCount;
            log.LogInfo("Performing an operation");
            if (c+1 != log.RecordCount)
                throw new Exception();
            if (log.RecordCount >= log.RecordLimit)
                throw new Exception();

            Console.WriteLine("Ура!");
        }
    }

    public class NullLog : ILog
    {
        private static int _recordCount = 0;
        
        public int RecordLimit
        {
            get
            {
                return 2 * _recordCount + 1;
            }
        }

        public int RecordCount
        {
            get
            {
                return _recordCount++;
            }
            set
            {
                _recordCount = value;
            }
        }
        public void LogInfo(string message)
        {
            
        }
    }
}