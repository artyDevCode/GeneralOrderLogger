using System;
using System.Linq;
using System.Configuration;
using System.Collections.Specialized;

namespace ApplicationLogger
{
    public static class AppConfig
    {

        /// <summary>
        /// CreateConnectionString handles the connection to a SQL database (ILD). It returns the DB Connection back to the calling class
        /// AppConfig
        /// </summary>
        /// <returns></returns>
        public static System.Data.Common.DbConnection CreateConnectionString() //string datasource, string initialCatalog, string userId, string password)
        {
            NameValueCollection config = null;
            try
            {
                //Reads the config file, in this case RemoveTRIMRevisions.dll.config located where the binaries for this assembly is
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

                    //Get the app settings parameters
                    config = new NameValueCollection
                        {
                                { section.Settings["ApplicationLogger"].Key, section.Settings["ApplicationLogger"].Value },
                                { section.Settings["Provider"].Key, section.Settings["Provider"].Value }
                        };

                }

                //Pass the appsettings parameters to the DB provider and connection string
                var conn = System.Data.Common.DbProviderFactories.GetFactory(config.GetValues(1).FirstOrDefault()).CreateConnection();
                conn.ConnectionString = config.GetValues(0).FirstOrDefault();

                return conn;
            }
            catch (Exception e)
            {
                throw new FieldAccessException("Location =- " + System.Reflection.Assembly.GetExecutingAssembly().Location + " in GetConfiguration");
            }

        }

    }
}
