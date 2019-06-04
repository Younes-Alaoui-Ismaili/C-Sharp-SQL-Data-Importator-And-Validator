using GTFSValidatorAndImportTool.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace GTFSValidatorAndImportTool.Utilities
{
    public class DirectoryHelper
    {
        private static DirectoryHelper _Instance = null;
        private static readonly object padlock = new object();
        /// <summary>
        /// please use the Instance property to get the current instance
        /// </summary>
        public DirectoryHelper()
        {
        }

        /// <summary>
        /// Public accessor to get DirectoryHelper object
        /// </summary>
        public static DirectoryHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    //to avoid creation of multiple instance in a multithread environment
                    lock (padlock)
                    {
                        if (_Instance == null)
                            _Instance = new DirectoryHelper();
                    }
                }
                return _Instance;
            }
        }

        public string GetCommonLocation()
        {
            string location = string.Empty;
            try
            {
                location = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            }
            catch (Exception ex)
            {
            }
            return location;
        }

        public static string GetTemporaryWorkSpace(string projectName)
        {
            string temporaryWorkSpace = Path.Combine(Path.GetTempPath(), projectName);
            if (Directory.Exists(temporaryWorkSpace))
                Directory.Delete(temporaryWorkSpace, true);
            Directory.CreateDirectory(temporaryWorkSpace);

            return temporaryWorkSpace;
        }

        public void CreateLogDirectory()
        {
            string logDirectory = Path.Combine(GetCommonLocation(), @"TS\log");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
                GrantAccess(logDirectory);
            }
        }

        public static void FileCopy(string sourceFilePath, string destFilePath) {

            if (File.Exists(sourceFilePath))
            {
                string destinationRootDir = Path.GetDirectoryName(destFilePath);
                if (!Directory.Exists(destinationRootDir))
                    Directory.CreateDirectory(destinationRootDir);

                File.Copy(sourceFilePath, destFilePath, true);
            }

        } 
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs = false)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public static bool CleanDirectory(string path)
        {
            System.IO.DirectoryInfo directory = new DirectoryInfo(path);
            if (directory.Exists)
            {
                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }

                return true;
            }

            //foreach (DirectoryInfo dir in di.GetDirectories())
            //{
            //    dir.Delete(true);
            //}

            return false;
        }

        public static bool RemoveTemporaryWorkSpaceDir(string[] pathsNeedToClean)
        {
            bool isRemoved = true;
            foreach (string path in pathsNeedToClean)
            {
                try
                {
                    if (Directory.Exists(path))
                        Directory.Delete(path, true);
                }
                catch (IOException ioex)
                {
                    isRemoved = false;
                }
                catch (Exception ex)
                {
                    isRemoved = false;
                }
            }

            return isRemoved;
        }

        private static Dictionary<FileOperation, string[]> GetFilesInformationToBeProcessed(string agencyName)
        {
            Dictionary<FileOperation, string[]> filesNeedToExistInSuperClientDirectory = new Dictionary<FileOperation, string[]>
            {
                { FileOperation.CheckExistanceOfFileName, new string[] {

                    "buslines.lin",
                    "stopList.txt",
                    "stopNames.lst",
                    string.Format("stopcodes_{0}_{1}.lst", Constants.AGENCY_CONTEXTE_PREFIX, agencyName),
                    string.Format("RouteList_EN_{0}_{1}.lst", Constants.AGENCY_CONTEXTE_PREFIX, agencyName ),
                    string.Format("RouteList_FR_{0}_{1}.lst", Constants.AGENCY_CONTEXTE_PREFIX, agencyName )

                } },
                { FileOperation.CheckExistanceOfFilesWithPattern, new string[] {
                    Constants.RTE_FILE_EXTENSION_PATTERN,
                    Constants.INDEX_POUR_CAPSULE_AUDIO_PATTERN
                } }
            };

            return filesNeedToExistInSuperClientDirectory;
        }

        public static string GetIndexPourCapsulesAudioFileLocation(string rootPath, string agencyName) {

            bool isSucceed = true;
            string Index_Pour_Capsule_Audio_Pattern_File_Path = string.Empty;
            if (Directory.Exists(rootPath))
            {
                Dictionary<FileOperation, string[]> filesInformation = GetFilesInformationToBeProcessed(agencyName);

                foreach (var fileInfo in filesInformation) {

                    switch (fileInfo.Key)
                    {
                        case FileOperation.CheckExistanceOfFileName:
                            foreach (string value in fileInfo.Value)
                            {
                                if (!File.Exists(Path.Combine(rootPath, value)))
                                {
                                    isSucceed = false;
                                    break;
                                }
                            }
                            break;
                        case FileOperation.CheckExistanceOfFilesWithPattern:
                            foreach (string value in fileInfo.Value)
                            {
                                String[] allfiles = Directory.GetFiles(rootPath, value, SearchOption.AllDirectories);

                                if (allfiles.Length <= 0)
                                {
                                    isSucceed = false;
                                    break;
                                }
                                else if (value == Constants.INDEX_POUR_CAPSULE_AUDIO_PATTERN)
                                    Index_Pour_Capsule_Audio_Pattern_File_Path = allfiles[0];
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            else {
                //log here
            }

            return Index_Pour_Capsule_Audio_Pattern_File_Path;
        }

        public bool GrantAccess(string fullPath)
        {
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(fullPath);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();
                dSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl,
                                                                 InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                                                                 PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                dInfo.SetAccessControl(dSecurity);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
