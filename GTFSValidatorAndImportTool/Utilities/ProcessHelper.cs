using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTFSValidatorAndImportTool.Utilities
{
    public class ProcessHelper
    {
        string executablePath = string.Empty;
        
        public ProcessHelper(string executablePath)
        {
            this.executablePath = executablePath;
        }
        /// <summary>
        /// witadmin exportwitd /collection:CollectionURL /p:Project /n:TypeName /f:FileName
        /// </summary>
        /// <param name="operationCommand"></param>
        /// <param name="collectionUrl"></param>
        /// <param name="formattedArguments"></param>
        /// <returns></returns>
        public bool RunProcess(string operationCommand = "", string formattedArguments = "")
        {
            bool isPerformed = false;
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = executablePath;

                    //witAdminProcess.StartInfo.Arguments = string.Format(@"{0} /collection:{1} {2}", operationCommand, collectionUrl, formattedArguments);
                    //Console.WriteLine(witAdminProcess.StartInfo.Arguments);
                    process.StartInfo.UseShellExecute = false;
                    //process.StartInfo.RedirectStandardOutput = true;
                    process.Start();
                    //Console.WriteLine(process.StandardOutput.ReadToEnd());
                    process.WaitForExit();
                    isPerformed = process.ExitCode == 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return isPerformed;
        }
    }
}
