using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTFSValidatorAndImportTool.Enums
{
    public enum Operation
    {
        ExtractInterfaceDev,
        ExtractGTFS,
        RunValidation,
        RunImport,
        CompareTheFilesAndSendEmailOnSuccessfulValidation
    }

    public enum FileOperation
    {
        CheckExistanceOfFilesWithPattern,
        CheckExistanceOfFileName
    }

    public enum CompletedOperation
    {
        None,
        FailedToRemoveExistingInterfaceDevExtractionPath,
        InterfaceDevExtractionSuccess,
        InterfaceDevExtractionFailed,
        GTFSExtractionSuccess,
        GTFSExtractionFailed,
        ValidationSuccess,
        ValidationFailed,
        FaildToCopyAudioFile
    }
}
