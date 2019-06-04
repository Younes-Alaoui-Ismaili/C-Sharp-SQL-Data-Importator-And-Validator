//using GTFSValidatorAndImportTool.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Xml;

namespace GTFSValidatorAndImportTool.Utilities
{
    public class UtilHelper
    {
        public static string GetFormattedWitAdminExportImportArguments(string fileName)
        {
            return GetFormattedWitAdminExportImportArguments(string.Empty, fileName);
        }

        public static string GetFormattedWitAdminExportImportArguments(string projectName, string fileName)
        {
            return GetFormattedWitAdminExportImportArguments(projectName, string.Empty, fileName);
        }

        public static string GetFormattedWitAdminExportImportArguments(string projectName, string typeName = "", string fileName = "")
        {

            return string.Format(@"{0} {1} {2}", !string.IsNullOrEmpty(projectName) ? @"/p:" + projectName : string.Empty, !string.IsNullOrEmpty(typeName) ? string.Format(@"/n:""{0}""", typeName) : string.Empty, !string.IsNullOrEmpty(fileName) ? string.Format(@"/f:""{0}""", fileName) : string.Empty);
        }

        public static string RemoveNode(string nodeName, string attributeName, string attributeValue, string xmlContent, string templatePath)
        {
            XmlDocument sourceDoc = new XmlDocument();
            sourceDoc.LoadXml(xmlContent);
            //sourceDoc.SelectSingleNode(string.Format("//FIELD[@name='Team']",));
            XmlNode nodeFound = sourceDoc.SelectSingleNode(string.Format("//{0}[@{1}='{2}']", nodeName, attributeName, attributeValue));
            if (nodeFound != null)
            {
                nodeFound.InnerText = string.Empty;
                //sourceDoc.Save(templatePath);
            }
            return sourceDoc.OuterXml;
            //return true;
        }

        #region Commented out
        //public static string GetTemporaryStandardWitsPath(string standardWitsRootDir)
        //{

        //    string tempStandardDir = Path.Combine(Path.GetTempPath(), Constants.STM);
        //    //Directory.CreateDirectory(tempDir);
        //    DirectoryCopy(standardWitsRootDir, tempStandardDir);
        //    return tempStandardDir;
        //}


        //public static string GetTemporaryWITPath(string rootPath, string witTypeName)
        //{
        //    return Path.Combine(rootPath, witTypeName + Constants.XML_EXTENSION);
        //}
        #endregion

    }

    public class Parser
    {
        #region commneted out
        //public static CommandLineWitArgument MapCommandLineArguments(string[] args, out Stack<string> commandsHasMissingValue)
        //{
        //    CommandLineWitArgument parsedArgs = null;
        //    commandsHasMissingValue = CommandHelper.MapHelpRequests(args);
        //    if (commandsHasMissingValue == null || commandsHasMissingValue.Count <= 0)
        //    {
        //        parsedArgs = new CommandLineWitArgument();
        //        parsedArgs.Mode = args[0].ToLower();
        //        parsedArgs.Environment = args[1].ToLower();
        //        parsedArgs.CollectionName = args[2].ToLower();
        //        parsedArgs.ProjectName = GetProjectName(args[3].ToLower());

        //        for (int index = 4; index < args.Length; index++)
        //        {
        //            switch (args[index].ToLower())
        //            {
        //                case Constants.DEPLOY_SPECIFIC_TEMPLATES:
        //                    int valueIndex = index + 1;
        //                    if (valueIndex <= args.Length)
        //                        parsedArgs.WorkItemsNeedToProcess = ConvertFileName(Array.ConvertAll(args[valueIndex].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim()));
        //                    else commandsHasMissingValue.Push(Constants.DEPLOY_SPECIFIC_TEMPLATES);
        //                    break;
        //                case Constants.TEAMFIELD:
        //                    valueIndex = index + 1;
        //                    if (valueIndex <= args.Length)
        //                        parsedArgs.TeamField = args[valueIndex];
        //                    else commandsHasMissingValue.Push(Constants.TEAMFIELD);
        //                    break;
        //                case Constants.VERSION:
        //                    valueIndex = index + 1;
        //                    int tfsVersion = 0;
        //                    if (valueIndex <= args.Length)
        //                    {
        //                        int.TryParse(args[valueIndex], out tfsVersion);
        //                        parsedArgs.Version = tfsVersion;
        //                    }
        //                    else commandsHasMissingValue.Push(Constants.VERSION);
        //                    break;
        //                case Constants.GUI:
        //                    parsedArgs.IsGUIRequested = true;
        //                    break;
        //                case Constants.VERBOSE:
        //                    parsedArgs.IsVerbose = true;
        //                    break;
        //                case Constants.STM_ROOT:
        //                    valueIndex = index + 1;
        //                    if (valueIndex <= args.Length)
        //                        parsedArgs.StmRoot = args[valueIndex];
        //                    else commandsHasMissingValue.Push(Constants.STM_ROOT);
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }

        //    return parsedArgs;
        //}

        //private static string [] ConvertFileName(string [] fileNames)
        //{
        //    if (fileNames == null || fileNames.Length == 0)
        //        return null;

        //        string[] rs = new string[fileNames.Length];
        //        int i = 0;
        //        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

        //        foreach (string f in fileNames)
        //        {

        //            if (f.ToLower() == "processconfig")
        //            {
        //                rs[i++] = Constants.PROCESSCONFIG;
        //            }
        //            else if (f.ToLower() == "categoryconfig")
        //            {
        //                rs[i++] = Constants.CATEGORYCONFIG;
        //            }
        //            else
        //            {
        //                rs[i++] = myTI.ToTitleCase(f.ToLower()).Replace("Fixedin", "FixedIn").Trim();
        //            }

        //        }

        //            return rs;
        //}

        //public static string GetProjectName(string project)
        //{
        //    string result = null;
        //    TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

        //    switch (project)
        //    {
        //        case "3eclientsproject":
        //            result = "3EClientsProject";
        //            break;
        //        case "3efeaturesproject":
        //            result = "3EFeaturesProject";
        //            break;
        //        case "3emobileproject":
        //            result = "3EMobileProject";
        //            break;
        //        case "3eproject":
        //            result = "3EProject";
        //            break;
        //        case "qaautomationproject":
        //            result = "QAAutomationProject";
        //            break;
        //        case "businessintelligenceproject":
        //            result = "BusinessIntelligenceProject";
        //            break;
        //        case "enterprisebiproject":
        //            result = "EnterpriseBIProject";
        //            break;
        //        case "devopsproject":
        //            result = "DevOpsProject";
        //            break;
        //        case "financeproject":
        //            result = "FinanceProject";
        //            break;
        //        case "loadproject":
        //            result = "LoadProject";
        //            break;
        //        case "microservicesproject":
        //            result = "MicroServicesProject";
        //            break;
        //        case "seproject":
        //            result = "SEProject";
        //            break;
        //        case "testautomationproject":
        //            result = "TestAutomationProject";
        //            break;
        //        case "tfs":
        //            result = "TFS";
        //            break;
        //        case "tfsdashboard":
        //            result = "TFSDashboard";
        //            break;
        //        case "uxproject":
        //            result = "UXProject";
        //            break;
        //        case "builds2.0":
        //            result = "Builds2.0";
        //            break;
        //        case "elitearchitect":
        //            result = "EliteArchitect";
        //            break;
        //        case "eliteservices":
        //            result = "EliteServices";
        //            break;
        //        case "consultancy":
        //            result = "Consultancy";
        //            break;
        //        case "docketing":
        //            result = "Docketing";
        //            break;
        //        case "elite template test":
        //            result = "Elite Template Test";
        //            break;
        //        case "designgalleryproject":
        //            result = "DesignGalleryProject";
        //            break;
        //        case "documentstudioproject":
        //            result = "DocumentStudioProject";
        //            break;
        //        case "dts solutions":
        //            result = "DTS Solutions";
        //            break;
        //        case "dtstestproject":
        //            result = "DTSTestProject";
        //            break;
        //        case "eproforma":
        //            result = "eProforma";
        //            break;
        //        case "paperlessproformaproject":
        //            result = "PaperlessProformaProject";
        //            break;
        //        case "ebh2.0project":
        //            result = "EBH2.0Project";
        //            break;
        //        case "ebhproject":
        //            result = "EBHProject";
        //            break;
        //        case "bdp_sandboxproject":
        //            result = "BDP_SandBoxProject";
        //            break;
        //        case "bdpremier":
        //            result = "BDPremier";
        //            break;
        //        case "bds classic":
        //            result = "BDS Classic";
        //            break;
        //        case "cnservices":
        //            result = "CNServices";
        //            break;
        //        case "documents":
        //            result = "Documents";
        //            break;
        //        case "engage":
        //            result = "Engage";
        //            break;
        //        case "ecopproject":
        //            result = "ECOPProject";
        //            break;
        //        case "enterpriseproject":
        //            result = "EnterpriseProject";
        //            break;
        //        case "esimagingproject":
        //            result = "ESImagingProject";
        //            break;
        //        case "webviewproject":
        //            result = "WebViewProject";
        //            break;
        //        case "envisionproject":
        //            result = "EnvisionProject";
        //            break;
        //        case "financialreportingproject":
        //            result = "FinancialReportingProject";
        //            break;
        //        case "dotnetnuke":
        //            result = "DotNetNuke";
        //            break;
        //        case "flocase":
        //            result = "FloCase";
        //            break;
        //        case "floSuite":
        //            result = "FloSuite";
        //            break;
        //        case "flosuitelegal":
        //            result = "FloSuiteLegal";
        //            break;
        //        case "imageconnectproject":
        //            result = "ImageConnectProject";
        //            break;
        //        case "appchat":
        //            result = "AppChat";
        //            break;
        //        case "appleapps":
        //            result = "AppleApps";
        //            break;
        //        case "davenport release (tfs1)":
        //            result = "Davenport Release (TFS1)";
        //            break;
        //        case "framework":
        //            result = "Framework";
        //            break;
        //        case "goodwood":
        //            result = "Goodwood";
        //            break;
        //        case "mattercentre":
        //            result = "MatterCentre";
        //            break;
        //        case "mattersphere":
        //            result = "MatterSphere";
        //            break;
        //        case "melbourne":
        //            result = "Melbourne";
        //            break;
        //        case "product team":
        //            result = "Product Team";
        //            break;
        //        case "prototypes":
        //            result = "Prototypes";
        //            break;
        //        case "silverstone":
        //            result = "Silverstone";
        //            break;
        //        case "spa":
        //            result = "spa";
        //            break;
        //        case "test scrum":
        //            result = "test scrum";
        //            break;
        //        case "vssimport":
        //            result = "vssimport";
        //            break;
        //        case "dev ops mm":
        //            result = "Dev Ops MM";
        //            break;
        //        case "trial transfer test":
        //            result = "Trial Transfer Test";
        //            break;
        //        case "workflow":
        //            result = "WorkFlow";
        //            break;
        //        case "prolaw":
        //            result = "ProLaw";
        //            break;
        //        case "prolawtre":
        //            result = "ProLawTRE";
        //            break;
        //        case "eifproject":
        //            result = "EIFProject";
        //            break;
        //        case "eliteworkflow":
        //            result = "EliteWorkflow";
        //            break;
        //        case "quantum":
        //            result = "Quantum";
        //            break;
        //        case "isd2013project":
        //            result = "ISD2013Project";
        //            break;
        //        case "it tools":
        //            result = "IT Tools";
        //            break;
        //        case "qa":
        //            result = "QA";
        //            break;
        //        case "tracker":
        //            result = "Tracker";
        //            break;
        //        case "trackerproservices":
        //            result = "TrackerProServices";
        //            break;
        //        case "wlcrbackconversionproject":
        //            result = "WLCRBackConversionProject";
        //            break;
        //        default:
        //            result = myTI.ToTitleCase(project);
        //            break;
        //    }

        //    return result;
        //}

        //public static string GetWorkItemTemplatesPertialPath(string partialWorkItemTemplatesPath, int version)
        //{
        //    string partialWorkItemTemplatesPathWithVersion = string.Empty;
        //    switch (version)
        //    {
        //        case 2015:
        //            partialWorkItemTemplatesPathWithVersion = partialWorkItemTemplatesPath;
        //            break;
        //        case 2017:
        //        case 2018:
        //            partialWorkItemTemplatesPathWithVersion = string.Format(@"{0}/{1}", partialWorkItemTemplatesPath, Constants.TFS2018);
        //            break;
        //        default:
        //            partialWorkItemTemplatesPathWithVersion = partialWorkItemTemplatesPath;
        //            break;
        //    }
        //    return partialWorkItemTemplatesPathWithVersion;
        //}
        #endregion
    }
}
