using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace GTFSValidatorAndImportTool.Utilities
{
    public class AppConfigReader
    {
        public static string EmailAddressFrom
        {
            get { return ReadMyConfig("EmailAddressFrom"); }
        }

        public static string EmailAddressFromPassword
        {
            get { return ReadMyConfig("EmailAddressFromPassword"); }
        }

        public static string SQLServerAddress
        {
            get { return ReadMyConfig("SQLServerAddress"); }
        }

        public static string SQLUserId
        {
            get { return ReadMyConfig("SQLUserId"); }
        }

        public static string SQLPassword
        {
            get { return ReadMyConfig("SQLPassword"); }
        }

        public static string StmRoot
        {
            get { return ReadMyConfig("StmRoot"); }
        }

        /// <summary>
        /// This method decides detailed log enabled or not
        /// </summary>
        /// <returns></returns>
        public static bool DetailedLogEnabled()
        {
            try
            {
                string DetailedLog = ReadMyConfig("DetailedLog");
                if (DetailedLog.ToUpper().Equals("ENABLE"))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //GTFSValidatorAndImportLog.Debug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType + "::" + System.Reflection.MethodBase.GetCurrentMethod().ToString() + ":" + ex.Message);
            }

            return false;
        }
        /// <summary>
        /// Read application cofig value using the key
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        internal static string ReadMyConfig(string strKey)
        {
            try
            {
                return ConfigurationSettings.AppSettings[strKey];
            }
            catch (Exception ex)
            { 

            }

            return string.Empty;
        }

    }
}
