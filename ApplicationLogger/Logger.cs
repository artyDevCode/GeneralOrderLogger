using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogger
{
    public class Logger
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Logger));

        public static void CallLog(string loggingInfo)
        {
            log.Info(loggingInfo);
        }

        /// <summary>
        /// This initializes the logger instead of using the app.config file from a client, it sets it up here and reads the appsetting parameter from
        /// RemoveTRIMRevisions.dll.config. This replaces the <log4net> settings in an app.config from client 
        /// </summary>
        /// <param name="logFile"></param>
        public static void ConfigureFileAppender(string logFile)
        {
            var fileAppender = GetFileAppender(logFile);
            BasicConfigurator.Configure(fileAppender);
            ((Hierarchy)LogManager.GetRepository()).Root.Level = Level.Debug;
        }

        private static IAppender GetFileAppender(string logFile)
        {
            var layout = new PatternLayout("%date %-5level %logger - %message%newline");
            layout.ActivateOptions(); // According to the docs this must be called as soon as any properties have been changed.
            //Create the <apender> 
            var appender = new FileAppender
            {
                File = logFile,
                Encoding = Encoding.UTF8,
                Threshold = Level.Debug,
                Layout = layout
            };

            appender.ActivateOptions();

            return appender;
        }

        public static string getLoggerPath()
        {
            NameValueCollection config = null;

            try
            {
                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                map.ExeConfigFilename = System.Reflection.Assembly.GetExecutingAssembly().Location + ".config";

                if (!System.IO.File.Exists(map.ExeConfigFilename))
                {
                    System.IO.File.Create(map.ExeConfigFilename); //create the file if it does not exist so the user can find where its looking for the config
                    throw new System.IO.FileNotFoundException("File not found " + map.ExeConfigFilename + "  in GetConfiguration");
                }
                Configuration libConfig = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                AppSettingsSection section = (libConfig.GetSection("appSettings") as AppSettingsSection);
                if (section != null)
                {
                    //Reads the config file, in this case RemoveTRIMRevisions.dll.config located where the binaries for this assembly is
                    config = new NameValueCollection
                    {
                            { section.Settings["LoggerName"].Key, section.Settings["LoggerName"].Value }, //Logger name and path
                    };
                }
                return config.GetValues(0).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new FieldAccessException("path =- " + System.Reflection.Assembly.GetExecutingAssembly().Location + " in getLoggerPath");
            }

        }
    }
}
