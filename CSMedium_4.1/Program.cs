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
            new DoubleLogWriter().WriteError("Запись в консоль и файл");
            new SecureConsoleLogWritter(true).WriteError("Безопасная запись в консоль");
            new SecureFileWriter(true).WriteError("Безопасная запись в файл");
            new SecureDoubleWriter(true).WriteError("Безопасная запись в консоль и файл");
        }
    }
    
    class ConsoleLogWriter
    {
        public virtual void WriteError(string message)
        {
            Console.WriteLine(message);
        }
    }

    class FileLogWriter
    {
        public virtual void WriteError(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    class DoubleLogWriter
    {
        public virtual void WriteError(string message)
        {
            new ConsoleLogWriter().WriteError(message);
            new ConsoleLogWriter().WriteError(message);
        }
    }

    class SecureConsoleLogWritter : ConsoleLogWriter
    {
        private bool _testMode;

        public SecureConsoleLogWritter(bool testMode = false)
        {
            _testMode = testMode;
        }

        public override void WriteError(string message)
        {
            if((int)(DateTime.Now.DayOfWeek) % 2 == 0 || _testMode)
            {
                new ConsoleLogWriter().WriteError(message);
            }
        }
    }

    class SecureFileWriter : FileLogWriter
    {
        private bool _testMode;

        public SecureFileWriter(bool testMode = false)
        {
            _testMode = testMode;
        }

        public override void WriteError(string message)
        {
            if((int)(DateTime.Now.DayOfWeek) % 2 == 0 || _testMode)
            {
                new FileLogWriter().WriteError(message);
            }
        }
    }

    class SecureDoubleWriter : ConsoleLogWriter
    {
        private bool _testMode;

        public SecureDoubleWriter(bool testMode = false)
        {
            _testMode = testMode;
        }

        public override void WriteError(string message)
        {
            if((int)(DateTime.Now.DayOfWeek) % 2 == 0 || _testMode)
            {
                new DoubleLogWriter().WriteError(message);
            }
        }
    }
}
