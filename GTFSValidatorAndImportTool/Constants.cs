using GTFSValidatorAndImportTool.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTFSValidatorAndImportTool
{
    public class Constants
    {
        public static readonly string[] AGENCIES = { "STL", "STSAG", "STSH", "STTR" };
        public const string AGENCY_CONTEXTE_PREFIX = "SAEIV";
        public const string RTE_FILE_EXTENSION_PATTERN = "*.rte";
        public const string INDEX_POUR_CAPSULE_AUDIO_PATTERN = "Index_pour_capsules_audio_*";
        public const string INTERFACEDEV_EXTRACTION_ROOT_PATH = @"C:\";
        public const string INTERFACEDEV_EXTRACTED_PATH = @"C:\interfacedev";
        public static string GTFS_EXTRACTION_PATH = @"C:\interfacedev\{0}\GTFS";
        public static string SUPERCLIENT_EXTRACTION_PATH = @"C:\interfacedev\{0}\SuperClient";
        public static string SIPEIMPORT_EXE_PATH = @"C:\interfacedev\{0}\Applications\ISR_SipeImport\sipeimport.exe";
        public const string VALIDATED_AUDIO_FILE_OUTPUT_PATH = @"C:\Validation\{0}";
        public const string SIPEIMPORT_DB_CONNECTION_STRING_NAME = "SipeImport.Properties.Settings.ISRDB_ConnectionString";
        public const string GMAIL_SMTP_SERVER_ADDRESS = @"smtp.gmail.com";
    }
}
