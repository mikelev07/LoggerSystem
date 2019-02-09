using System;
using System.Collections;

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
             //textLogger.Description = "Text_log";
             searchLogger.Description = "Search_log";

             logStore[0] = textLogger;
             logStore[1] = searchLogger;
             logStore[2] = new FileLogger { Description = "PUT /Home/Index" };
             logStore[3] = new TextLogger { Description = "GET /Home/GetText" };

             Console.WriteLine();

             foreach (Logger log in logStore)
             {
                Console.WriteLine(log?.Description ?? FileLogger.Default.Description);
             }

            Console.WriteLine();

            Account<int> acc1 = new Account<int> { Id = 1, Title = "New_account1" };
            Account<int> acc2 = new Account<int> { Id = 2, Title = "New_account2" };

            UniversalLogger<Logger, string> transaction1 = new UniversalLogger<Logger, string>
            {
                FromAccount = textLogger,
                ToAccount = searchLogger,
                Code = "123123123"
            };

            Console.WriteLine(logStore[11]);
            Console.ReadKey();
        }
    }

    public class UniversalLogger<T,C> where T : Logger
    {
        public T FromAccount { get; set; }
        public T ToAccount { get; set; }   
        public C Code { get; set; }        
    }

    public class Account<T>
    {
        public static T session;
        public T Id { get; set; }
        public string Title { get; set; }
    }

    public class TransactionLog { }

    public class LogStore
    {
        public readonly Logger[] loggers = new Logger[10];

        public Logger this[int index]
        {
            get
            {
                if (index > loggers.Length)
                    throw new IndexOutOfRangeException();
                return loggers[index];
            }
            set
            {
                loggers[index] = value;
            }
        }

        public Logger this[string description]
        {
            get
            {
                Logger logger = null;
                foreach (var log in loggers)
                {
                    if (log?.Description == description)
                    {
                        logger = log;
                        break;
                    }
                }
                return logger;
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < loggers.Length; i++)
            {
                yield return loggers[i];
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
        public static FileLogger Default { get; } = new FileLogger() { Description = "default log" };

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
