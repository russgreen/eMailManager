using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace eMailManager
{
    public static class CommonMethods
    {
        public static void ClearDeletedItems()
        {
            // first we get the inbox and the parent folder
            Outlook.Folder parentFolder;
            parentFolder = (Outlook.Folder)GlobalConfig.OlInbox.Parent;

            // try to get the new deleted items folder
            GlobalConfig.DeletedItemsFolder = (Outlook.Folder)parentFolder.Folders[GlobalConfig.DeletedItemsFolderName];

            var msgCount = GlobalConfig.DeletedItemsFolder.Items.Count;

            if(msgCount > 0)
            {
                var clearitems = GlobalConfig.GlobalSettings.AlwaysClearDeletedItems;

                if (GlobalConfig.GlobalSettings.AlwaysClearDeletedItems == false)
                {
                    if(MessageBox.Show(GlobalConfig.localResourceManager.GetString("Message002"), "Empty Folder", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        clearitems = true;
                    }
                }

                if (clearitems == true)
                {
                    foreach (var item in GlobalConfig.DeletedItemsFolder.Items)
                    {
                        try
                        {
                            Outlook.MailItem em = (Outlook.MailItem)item;
                            em.Save();
                            em.Delete();
                        }
                        catch
                        {
                            //just skip over things that can't be deleted
                        }

                    }
                }
            }
        }

        public static string TrimLongFileName(ref string strValue)
        {
            string retval = strValue;
            if (strValue.Length >= GlobalConfig.MaxFile)
            {
                retval = strValue.Substring(0, GlobalConfig.MaxFile - 4) + GlobalConfig.GlobalSettings.MessageFileExt;
            }

            return retval;
        }

        public static string ParseFilename(string filenameFilter, Outlook.MailItem email, bool sentMail, string reference = null, bool pvt = false)

        {
            string retval;
            // <sent> - date and time the message was sent in reverse format for chronological saving e.g.  yyyy-mm-dd_hh-mm 
            // <sent_yy> - year the message was sent
            // <sent_yyyy> - year the message was sent
            // <sent_mm> - month the message was sent
            // <sent_dd> - day in the month message was sent
            // <sent_hh-mm> - time the message was sent
            // <sent_hh.mm> - time the message was sent
            // <sent_hh-mm12> - time the message was sent in 12 hour format
            // <sent_hh.mm12> - time the message was sent in 12 hour format
            // <sent_hhmmss> - time the messages was sent in 24 hour hour HHMMSS
            // <from> - who the message was sent by (display name)
            // <from_email> - who the message was sent by (real email address). If the email address is an Exchange email address (sender on same local Exchange server) then the display name is used instead.
            // <from_domain> - domain name of sender.
            // <subject> - subject line of the message 
            // <$[XX]subject> - subject line of the message where nn is the length of the subject line to use in the filename.  nn must be 2 digits, e.g. 09, 20
            // <ref>
            // <type> - direction of the message incoming = in or outgoing = out - NOTE: email manager only detects outgoing messages if they are filed as they are sent.
            // <TYPE> - as above but in uppercase 
            // <type_sr>
            // <pvt> - private or open

            string sSubject = string.Empty;
            if (email.Subject is object)
                sSubject = email.Subject.ToString();
            retval = filenameFilter;

            // handle dates
            retval = retval.Replace("<sent>", email.SentOn.Year.ToString() + "-" + ShortMonth(email.SentOn.Month.ToString()) + "-" + ShortDay(email.SentOn.Day.ToString()) + "_" + RemoveIllegalCharacters(email.SentOn.ToShortTimeString()));
            retval = retval.Replace("<sent_yy>", ShortYear(email.SentOn.Year.ToString()));
            retval = retval.Replace("<sent_yyyy>", email.SentOn.Year.ToString());
            retval = retval.Replace("<sent_mm>", ShortMonth(email.SentOn.Month.ToString()));
            retval = retval.Replace("<sent_dd>", ShortDay(email.SentOn.Day.ToString()));
            retval = retval.Replace("<sent_hh-mm>", RemoveIllegalCharacters(email.SentOn.ToShortTimeString()));
            retval = retval.Replace("<sent_hh.mm>", RemoveIllegalCharacters(email.SentOn.ToShortTimeString()).Replace("-", "."));
            retval = retval.Replace("<sent_hh-mm12>", RemoveIllegalCharacters(email.SentOn.ToString("h:mm tt")));
            retval = retval.Replace("<sent_hh.mm12>", RemoveIllegalCharacters(email.SentOn.ToString("h:mm tt")).Replace("-", "."));
            retval = retval.Replace("<sent_hhmmss>", RemoveIllegalCharacters(email.SentOn.ToString("HHmmss"))); // em.SentOn.Hour & em.SentOn.Minute & em.SentOn.Second)

            // handle subject information
            string sSafeSubject = RemoveIllegalCharacters(NullSubject(sSubject)).Trim();
            retval = retval.Replace("<subject>", sSafeSubject);

            // does the filename filter contain a short subject variable?
            if (filenameFilter.Contains("<$["))
            {
                int iStart = filenameFilter.IndexOf("<$[");
                int iEnd = filenameFilter.IndexOf("]subject>");
                string sLength = filenameFilter.Substring(iStart + 3, 2);
                int iLength = int.Parse(sLength);
                int iSubjectLength = sSafeSubject.Length;
                if (iLength > iSubjectLength)
                    iLength = iSubjectLength;
                string sShortSubject = sSafeSubject.Substring(0, iLength);
                retval = retval.Replace("<$[" + sLength + "]subject>", sShortSubject);
            }

            // handle sender information
            retval = retval.Replace("<from>", email.SenderName);
            if (email.SenderEmailType == "EX")
            {
                // we must have an exchange server address so use SenderName
                var recip = GlobalConfig.MapiNamespace.CreateRecipient(email.SenderEmailAddress);
                var exUser = recip.AddressEntry.GetExchangeUser();
                try
                {
                    retval = retval.Replace("<from_email>", exUser.PrimarySmtpAddress);
                }
                catch (Exception ex)
                {
                    retval = retval.Replace("<from_email>", recip.Name);
                }
            }
            else
            {
                retval = retval.Replace("<from_email>", email.SenderEmailAddress);
            }


            // handle not automatic fields
            retval = retval.Replace("<ref>", reference);

            // handle direction of email
            string Type, TypeSR;
            if (sentMail == true)
            {
                Type = "out";
                TypeSR = "S";
            }
            // check if account email matches mail item sender
            else if ((Globals.ThisAddIn.Application.Session.CurrentUser.Name ?? "") == (email.SenderName ?? ""))
            {
                // we match so this must be a sent item
                Type = "out";
                TypeSR = "S";
            }
            else
            {
                Type = "in";
                TypeSR = "R";
            }

            retval = retval.Replace("<from_domain>", GetDomainFromEmail(email));
            retval = retval.Replace("<type>", Type);
            retval = retval.Replace("<TYPE>", Type.ToUpper());
            retval = retval.Replace("<type_sr>", TypeSR);
            if (pvt == true)
            {
                retval = retval.Replace("<pvt>", "Private");
            }
            else
            {
                retval = retval.Replace("<pvt>", "Open");
            }

            return RemoveIllegalCharacters(retval);
        }

        public static string ShortDay(string day)
        {
            string shortDay = default;
            switch (day ?? "")
            {
                case "1":
                    shortDay = "01";
                    break;

                case "2":
                    shortDay = "02";
                    break;

                case "3":
                    shortDay = "03";
                    break;

                case "4":
                    shortDay = "04";
                    break;

                case "5":
                    shortDay = "05";
                    break;

                case "6":
                    shortDay = "06";
                    break;

                case "7":
                    shortDay = "07";
                    break;

                case "8":
                    shortDay = "08";
                    break;

                case "9":
                    shortDay = "09";
                    break;

                default:
                    shortDay = day;
                    break;
            }

            return shortDay;
        }

        public static string ShortMonth(string month)
        {
            string shortMonth = default;
            // set new month values
            switch (month ?? "")
            {
                case "1":
                    shortMonth = "01";
                    break;

                case "2":
                    shortMonth = "02";
                    break;

                case "3":
                    shortMonth = "03";
                    break;

                case "4":
                    shortMonth = "04";
                    break;

                case "5":
                    shortMonth = "05";
                    break;

                case "6":
                    shortMonth = "06";
                    break;

                case "7":
                    shortMonth = "07";
                    break;

                case "8":
                    shortMonth = "08";
                    break;

                case "9":
                    shortMonth = "09";
                    break;

                default:
                    shortMonth = month;
                    break;
            }

            return shortMonth;
        }

        public static string ShortYear(string year)
        {
            return year.Substring(2, 2);
        }
        private static string NullSubject(string subject)
        {
            string retval;
            if (subject.Trim().Length == 0)
            {
                retval = "RE";
            }
            else
            {
                retval = subject.Trim();
            }

            return retval;
        }

        private static string GetDomainFromEmail(Outlook.MailItem email)
        {
            var retval = string.Empty;
            var smtp = string.Empty;

            if (email.SenderEmailType == "EX")
            {
                // we must have an exchange server address so use SenderName
                var recip = GlobalConfig.MapiNamespace.CreateRecipient(email.SenderEmailAddress);
                var exUser = recip.AddressEntry.GetExchangeUser();
                smtp = exUser.PrimarySmtpAddress;
            }
            else
            {
                smtp = email.SenderEmailAddress;
            }

            // extract domain name from senders email address
            retval = smtp.Substring(smtp.IndexOf("@") + 1);
            return retval;
        }

        public static string RemoveIllegalCharacters(string originalString)
        {
            // this function will remove these characters \ / : * ? < > | 
            // from sString and replace them a space
            string legalString;
            if (ReferenceEquals(originalString, DBNull.Value) || originalString is null || ReferenceEquals(originalString, string.Empty))
            {
                legalString = " ";
            }
            else
            {
                legalString = originalString;
            }


            legalString = legalString.Replace(@"\", " ");
            legalString = legalString.Replace("/", " ");
            legalString = legalString.Replace(":", " ");
            legalString = legalString.Replace("*", " ");
            legalString = legalString.Replace("?", " ");
            legalString = legalString.Replace("\"", " ");
            legalString = legalString.Replace("<", " ");
            legalString = legalString.Replace(">", " ");
            legalString = legalString.Replace("|", " ");

            return legalString;
        }
    }
}
