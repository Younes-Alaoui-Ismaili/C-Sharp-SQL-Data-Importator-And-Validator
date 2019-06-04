using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GTFSValidatorAndImportTool.Utilities
{
    public class EmailHelper
    {
        private static EmailHelper _Instance = null;
        private static readonly object padlock = new object();
        SmtpClient SmtpServer = null;
        MailMessage mail = null;
        /// <summary>
        /// please use the Instance property to get the current instance
        /// </summary>
        public EmailHelper()
        {
            SmtpServer = new SmtpClient(Constants.GMAIL_SMTP_SERVER_ADDRESS);
            mail = new MailMessage();
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(AppConfigReader.EmailAddressFrom, AppConfigReader.EmailAddressFromPassword);
            SmtpServer.EnableSsl = true;

        }

        /// <summary>
        /// Public accessor to get EmailHelper object
        /// </summary>
        public static EmailHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    //to avoid creation of multiple instance in a multithread environment
                    lock (padlock)
                    {
                        if (_Instance == null)
                            _Instance = new EmailHelper();
                    }
                }
                return _Instance;
            }
        }

        public void Send(string to, string attachmentPath)
        {
            try
            {
                mail.From = new MailAddress(AppConfigReader.EmailAddressFrom);
                mail.To.Add(to);
                mail.Subject = "SipeImport: Index Pour Capsule Audio File";
                mail.Body = "Please see the attachment includes the output file Index Pour Capsule Audio File.";

                Attachment attachment = new Attachment(attachmentPath);
                mail.Attachments.Add(attachment);
                SmtpServer.Send(mail);
            }
            catch (Exception ex) {
                //log here
            }
        }
    }
}
