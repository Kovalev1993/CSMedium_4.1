using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSMedium_4._1
{
    class Program
    {
        static void Main(string[] args)
        {
            new ConsoleLogWriter().WriteError("Запись в консоль");
            new FileLogWriter().WriteError("Запись в файл");
            new DoubleLogWriter(new ConsoleLogWriter(), new FileLogWriter()).WriteError("Запись в консоль и файл");
            new SecureLogWritter(new ConsoleLogWriter(), true).WriteError("Безопасная запись в консоль");
            new SecureLogWritter(new FileLogWriter(), true).WriteError("Безопасная запись в файл");
            new SecureLogWritter(new DoubleLogWriter(new ConsoleLogWriter(), new FileLogWriter()), true).WriteError("Безопасная запись в консоль и файл");
        }
    }

    interface ILogger
    {
        void WriteError(string message);
    }
    
    class ConsoleLogWriter : ILogger
    {
        public void WriteError(string message)
        {
            Console.WriteLine(message);
        }
    }

    class FileLogWriter : ILogger
    {
        public void WriteError(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    class DoubleLogWriter : ILogger
    {
        private ILogger _log0;
        private ILogger _log1;

        public DoubleLogWriter(ILogger log0, ILogger log1)
        {
            _log0 = log0;
            _log1 = log1;
        }

        public virtual void WriteError(string message)
        {
            _log0.WriteError(message);
            _log1.WriteError(message);
        }
    }

    class SecureLogWritter : ILogger
    {
        private ILogger _log;
        private bool _testMode;

        public SecureLogWritter(ILogger log, bool testMode = false)
        {
            _log = log;
            _testMode = testMode;
        }

        public void WriteError(string message)
        {
            if((int)(DateTime.Now.DayOfWeek) % 2 == 0 || _testMode)
            {
                _log.WriteError(message);
            }
        }
    }
}
