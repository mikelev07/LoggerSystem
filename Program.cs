using System;

namespace LoggerSystem
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            Logger textLogger = new TextLogger();
            textLogger.InvokeLog();

            Console.WriteLine();

            Logger searchLogger = new SearchLogger();
            searchLogger.InvokeLog();

            Console.WriteLine();

            textLogger = FileLogger.Default;
            textLogger.InvokeLog();

            LogStore logStore = new LogStore();
            logStore[0] = textLogger;
            logStore[1] = searchLogger;


            Console.WriteLine(logStore[0].Title);

            Console.ReadKey();
        }
    }


    public class LogStore
    {
        public readonly Logger[] loggers = new Logger[10];

        public Logger this[int index]
        {
            get
            {
                return loggers[index];
            }
            set
            {
                loggers[index] = value;
            }
        }

    }


    /// <summary>
    /// Logger.
    /// </summary>
    public abstract class Logger
    {
        #region Const
        public const string LT = "new log";
        #endregion

        #region Constructors
        public Logger() { }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        #endregion

        #region Functions
        protected abstract void CreateLog();
        public abstract void RemoveLog();
        public abstract string CheckStatus();
        public abstract int GetLogIndex();
        #endregion

        public void InvokeLog()
        {
            Title = this.CheckStatus();
            this.CreateLog();
            Display(Title);
        }

        void Display(string title)
        {
            Console.WriteLine(title);
        }

    }

    /// <summary>
    /// File logger.
    /// </summary>
    class FileLogger : Logger
    {
        public FileLogger() { }

        /// <summary>
        /// The default instance.
        /// </summary>
        private static readonly FileLogger _default = new FileLogger();
        public static FileLogger Default { get { return _default; } }

        #region Functions
        protected bool IsCreate()
        {
            return true;
        }

        protected override void CreateLog()
        {
            Console.WriteLine("File log creator");
        }

        public override string CheckStatus()
        {
            return "File log create...";
        }

        public override void RemoveLog()
        {
            throw new NotImplementedException();
        }

        public override int GetLogIndex()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    class SearchLogger : Logger
    {
        public SearchLogger(string title, string description) : base()
        {
            Title = title;
            Description = description;
        }

        public SearchLogger() { }

        #region Functions
        protected bool IsCreate()
        {
            return true;
        }

        protected override void CreateLog()
        {
            Console.WriteLine("Search log creator");
        }

        public override string CheckStatus()
        {
            return "Search log create...";
        }

        public override void RemoveLog()
        {
            throw new NotImplementedException();
        }

        public override int GetLogIndex()
        {
            throw new NotImplementedException();
        }
        #endregion

    }

    /// <summary>
    /// Text logger.
    /// </summary>
    class TextLogger : Logger
    {
        public TextLogger(string title, string description) : base()
        {
            Title = title;
            Description = description;
        }

        public TextLogger() { }

        #region Functions
        protected bool IsCreate()
        {
            return true;
        }

        protected override void CreateLog()
        {
            Console.WriteLine("Text log creator");
        }

        public override string CheckStatus()
        {
            return "Text log create...";
        }

        public override void RemoveLog()
        {
            throw new NotImplementedException();
        }

        public override int GetLogIndex()
        {
            throw new NotImplementedException();
        }
        #endregion

    }


}
