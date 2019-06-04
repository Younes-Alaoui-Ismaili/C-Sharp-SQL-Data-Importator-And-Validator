using GTFSValidatorAndImportTool.Entities;
using GTFSValidatorAndImportTool.Enums;
using GTFSValidatorAndImportTool.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTFSValidatorAndImportTool
{
    public partial class Form1 : Form
    {
        #region private declearation section
        CompletedOperation currentCompletedOperation = CompletedOperation.None;
        BackgroundWorker bgWorker = null;
        bool isInterfaceDevExtracted = false;
        bool isGTFSExtracted = false;
        string outputAudioFilePath = string.Empty;
        #endregion

        #region private functions
        private void ShowMessage()
        {
            switch (currentCompletedOperation)
            {
                case CompletedOperation.FailedToRemoveExistingInterfaceDevExtractionPath:
                    MessageBox.Show("Failed to remove old InterfaceDev extraction Root Path." + Environment.NewLine + "Please Close all open file explorer from InterfaceDev and try again.");
                    break;
                case CompletedOperation.InterfaceDevExtractionFailed:
                    MessageBox.Show("InterfaceDev Extraction Failed. Please try again.");
                    break;
                case CompletedOperation.GTFSExtractionFailed:
                    MessageBox.Show("GTFS Extraction Failed. Please try again.");
                    break;
                case CompletedOperation.ValidationFailed:
                    MessageBox.Show("Validation Failed. Please try again.");
                    break;
                case CompletedOperation.ValidationSuccess:
                    MessageBox.Show("Validation Successful. Please find the validated audio file at " + outputAudioFilePath);
                    break;
                default:
                    break;

            }

            currentCompletedOperation = CompletedOperation.None;
            outputAudioFilePath = string.Empty;
        }

        private void UpdateSQLDBInfoInUI()
        {
            txtSQLServerAddress.Text = AppConfigReader.SQLServerAddress;
            txtSQLUserId.Text = AppConfigReader.SQLUserId;
            txtSQLPassword.Text = AppConfigReader.SQLPassword;
        }

        private void UpdateDBConnectionString(string exePath, SQLServerInfo sqlServerInfo) {

            Configuration sipeImporterConfiguration = ConfigurationManager.OpenExeConfiguration(exePath);
            sipeImporterConfiguration.ConnectionStrings.ConnectionStrings[Constants.SIPEIMPORT_DB_CONNECTION_STRING_NAME].ConnectionString = string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3}", sqlServerInfo.ServerAddress, sqlServerInfo.DatabaseName, sqlServerInfo.UserId, sqlServerInfo.Password);
            sipeImporterConfiguration.Save(ConfigurationSaveMode.Modified, true);

        }


        #endregion

        #region Constructor
        public Form1()
        {
            InitializeComponent();
            InitializeAgencies();

            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
            //DisableAll();
            //bgWorker.RunWorkerAsync(new object[] { Operation.LoadCollection });
        }
        #endregion

        #region Background Worker Operations
        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Current operation was cancelled.");
                EnableAll();
            }
            else if (e.Error != null)
            {
                MessageBox.Show("There was an error running the current operation. The thread aborted.");
                EnableAll();
            }
            else
            {
                object[] parameters = e.Result as object[];

                switch ((Operation)parameters[0])
                {
                    case Operation.ExtractInterfaceDev:
                        EnableAll();
                        ShowMessage();
                        break;
                    case Operation.ExtractGTFS:
                        EnableAll();
                        ShowMessage();
                        UpdateSQLDBInfoInUI();
                        break;
                    case Operation.RunValidation:
                        EnableAll();
                        ShowMessage();
                        break;
                    default: break;
                }
            }
        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            operationProgress.Value = e.ProgressPercentage;
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            bgWorker.ReportProgress(0);

            object[] parameters = e.Argument as object[];

            //Check if there is a request to cancel the process
            if (bgWorker.CancellationPending)
            {
                e.Cancel = true;
                bgWorker.ReportProgress(0);
                return;
            }

            switch ((Operation)parameters[0])
            {
                case Operation.ExtractInterfaceDev:
                    if (parameters.Length == 2)
                    {
                        bgWorker.ReportProgress(25);

                        if (DirectoryHelper.RemoveTemporaryWorkSpaceDir(new string[] { Constants.INTERFACEDEV_EXTRACTED_PATH }))
                        {
                            bgWorker.ReportProgress(50);
                            try
                            {
                                ZipFile.ExtractToDirectory(parameters[1].ToString(), Constants.INTERFACEDEV_EXTRACTION_ROOT_PATH);
                                isInterfaceDevExtracted = true;
                            }
                            catch (Exception ex)
                            {
                                currentCompletedOperation = CompletedOperation.InterfaceDevExtractionFailed;
                            }
                        }
                        else currentCompletedOperation = CompletedOperation.FailedToRemoveExistingInterfaceDevExtractionPath;

                        bgWorker.ReportProgress(75);
                        e.Result = e.Argument;
                        bgWorker.ReportProgress(100);
                    }
                    break;
                case Operation.ExtractGTFS:
                    if (parameters.Length == 3)
                    {
                        bgWorker.ReportProgress(25);
                        string SuperClientPath = string.Format(Constants.SUPERCLIENT_EXTRACTION_PATH, parameters[2].ToString());
                        string GTFSExtractionPath = string.Format(Constants.GTFS_EXTRACTION_PATH, parameters[2].ToString());
                        if (DirectoryHelper.CleanDirectory(SuperClientPath) && DirectoryHelper.CleanDirectory(GTFSExtractionPath))
                        {
                            bgWorker.ReportProgress(50);
                            try
                            {
                                ZipFile.ExtractToDirectory(parameters[1].ToString(), GTFSExtractionPath);
                                isGTFSExtracted = true;
                            }
                            catch (Exception ex) {

                                currentCompletedOperation = CompletedOperation.GTFSExtractionFailed;
                            }
                            bgWorker.ReportProgress(75);
                        }

                        e.Result = e.Argument;
                        bgWorker.ReportProgress(100);
                    }
                    break;
                case Operation.RunValidation:
                    if (parameters.Length == 3)
                    {
                        bgWorker.ReportProgress(20);
                        Agency agency = (Agency)parameters[1];
                        SQLServerInfo sQLServerInfo = (SQLServerInfo)parameters[2];
                        string sipeImporteExePath = string.Format(Constants.SIPEIMPORT_EXE_PATH, agency.Name);

                        UpdateDBConnectionString(sipeImporteExePath, sQLServerInfo);

                        ProcessHelper sipeImportProcess = new ProcessHelper(sipeImporteExePath);
                        bgWorker.ReportProgress(40);
                        //sipeImportProcess.RunProcess();
                        if (sipeImportProcess.RunProcess())
                        {
                            bgWorker.ReportProgress(60);
                            string SuperClientPath = string.Format(Constants.SUPERCLIENT_EXTRACTION_PATH, agency.Name);
                            string Index_Pour_Calsule_Audio_File_Path = DirectoryHelper.GetIndexPourCapsulesAudioFileLocation(SuperClientPath, agency.Name);
                            bgWorker.ReportProgress(80);
                            if (!string.IsNullOrEmpty(Index_Pour_Calsule_Audio_File_Path))
                            {
                                try
                                {
                                    string validatedAudioFileOutputRootPath = string.Format(Constants.VALIDATED_AUDIO_FILE_OUTPUT_PATH, agency.Name);
                                    string validatedAudioFileOutputPath = Path.Combine(validatedAudioFileOutputRootPath, Path.GetFileName(Index_Pour_Calsule_Audio_File_Path));
                                    DirectoryHelper.FileCopy(Index_Pour_Calsule_Audio_File_Path, validatedAudioFileOutputPath);
                                    //EmailHelper.Instance.Send(agency.EmailAddress, Index_Pour_Calsule_Audio_File_Path);
                                    bgWorker.ReportProgress(90);
                                    currentCompletedOperation = CompletedOperation.ValidationSuccess;
                                    outputAudioFilePath = validatedAudioFileOutputPath;
                                }
                                catch (Exception ex)
                                {
                                    currentCompletedOperation = CompletedOperation.FaildToCopyAudioFile;
                                }
                            }
                            else
                            {
                                currentCompletedOperation = CompletedOperation.ValidationFailed;
                            }
                        }
                        else
                        {
                            currentCompletedOperation = CompletedOperation.ValidationFailed;
                        }

                        e.Result = e.Argument;
                        bgWorker.ReportProgress(100);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Control Operations
        private void InitializeAgencies()
        {
            comboBoxAgency.Items.AddRange(Constants.AGENCIES);
            //comboBoxAgency.DataSource = Constants.AGENCIES;
        }

        private void EnableAll()
        {
            btnLoadAndExtractInterfaceDevzip.Enabled = true;
            comboBoxAgency.Enabled = true;
            btnLoadAndExtractGTFS.Enabled = true;
            //txtAgencyEmail.Enabled = true;
            txtSQLServerAddress.Enabled = true;
            txtSQLUserId.Enabled = true;
            txtSQLPassword.Enabled = true;
            btnRunValidationAndImport.Enabled = true;

            if (isInterfaceDevExtracted)
                btnLoadAndExtractInterfaceDevzip.Enabled = false;
            if (isGTFSExtracted)
                btnLoadAndExtractGTFS.Enabled = false;
        }

        private void DisableAll()
        {

            btnLoadAndExtractInterfaceDevzip.Enabled = false;
            comboBoxAgency.Enabled = false;
            btnLoadAndExtractGTFS.Enabled = false;
            //txtAgencyEmail.Enabled = false;
            txtSQLServerAddress.Enabled = false;
            txtSQLUserId.Enabled = false;
            txtSQLPassword.Enabled = false;
            btnRunValidationAndImport.Enabled = false;
        }

        #endregion

        #region Control Events
        private void btnLoadAndExtractInterfaceDevzip_Click(object sender, EventArgs e)
        {
            openFileDialogInterfaceDevzip.Filter = "Zip File|*.zip";
            DialogResult result = openFileDialogInterfaceDevzip.ShowDialog();
            if (result == DialogResult.OK)
            {
                string sourceInterfaceDevZipFilePath = openFileDialogInterfaceDevzip.FileName;
                try
                {
                    DisableAll();
                    object[] parameters = new object[] { Operation.ExtractInterfaceDev, sourceInterfaceDevZipFilePath };
                    bgWorker.RunWorkerAsync(parameters);
                }
                catch (IOException ioex)
                {
                    MessageBox.Show(ioex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnLoadAndExtractGRFS_Click(object sender, EventArgs e)
        {
            if (comboBoxAgency.SelectedIndex >= 0)
            {
                openFileDialogGTFSzip.Filter = "Zip File|*.zip";
                DialogResult result = openFileDialogGTFSzip.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string sourceGTFSDevZipFilePath = openFileDialogGTFSzip.FileName;
                    try
                    {
                        DisableAll();
                        object[] parameters = new object[] { Operation.ExtractGTFS, sourceGTFSDevZipFilePath, comboBoxAgency.Text.Trim() };
                        bgWorker.RunWorkerAsync(parameters);
                    }
                    catch (IOException ioex)
                    {
                        MessageBox.Show(ioex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else MessageBox.Show("Please select an Agency first for the GTFS.");
        }

        private void btnRunValidationAndImport_Click(object sender, EventArgs e)
        {
            if (isInterfaceDevExtracted && isGTFSExtracted && !string.IsNullOrEmpty(comboBoxAgency.Text.Trim()) && !string.IsNullOrEmpty(txtSQLServerAddress.Text.Trim()) && !string.IsNullOrEmpty(txtSQLUserId.Text.Trim()) && !string.IsNullOrEmpty(txtSQLPassword.Text.Trim()))
            {
                DisableAll();
                Agency agency = new Agency();
                agency.Name = comboBoxAgency.Text.Trim();
                agency.ContextName = Constants.AGENCY_CONTEXTE_PREFIX + "_" + comboBoxAgency.Text.Trim();
                //agency.EmailAddress = txtAgencyEmail.Text.Trim();

                SQLServerInfo sQLServerInfo = new SQLServerInfo();
                sQLServerInfo.ServerAddress = txtSQLServerAddress.Text.Trim();
                sQLServerInfo.DatabaseName = agency.ContextName;
                sQLServerInfo.UserId = txtSQLUserId.Text.Trim();
                sQLServerInfo.Password = txtSQLPassword.Text.Trim();

                object[] parameters = new object[] { Operation.RunValidation, agency, sQLServerInfo };
                bgWorker.RunWorkerAsync(parameters);
                // run the sipeimporter.exe process and wait it to finish
                // after finishing run
                //IsFilesExist
            }
            else MessageBox.Show("Required fields are missing! Please fill all the fields in the form before proceed.");
        }
        #endregion

    }
}
