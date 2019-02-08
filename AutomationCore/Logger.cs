using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;


namespace AutomationCore
{
    public class Logger
    {

        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Setup()
        {
            Hierarchy hierarchy = (Hierarchy) LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout
            {
                ConversionPattern = "%date{dd-MMM-yyyy HH:mm:ss} %level %message%newline"
            };
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender
            {
                AppendToFile = true,
                File = @"..\..\LoggFiles\\" +
                       DateTime.Now.ToString("dd-MMM-yyyy") /*+ "\\" + DateTime.Now.ToString("hh.mm.ss") */ + ".txt",
                Layout = patternLayout,
                MaxSizeRollBackups = 5,
                MaximumFileSize = "1GB",
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true
            };
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }

        public static void LogInfo(string message)
        {
            logger.Info(message);
            Console.WriteLine(message);
        }

        public static void LogError(string message, Exception e)
        {
            logger.Error(message, e);
        }
    }
}
