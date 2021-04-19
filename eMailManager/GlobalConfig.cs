using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace eMailManager
{
    static class GlobalConfig
    {

        public static Settings GlobalSettings;

        public static Outlook.NameSpace MapiNamespace = Globals.ThisAddIn.Application.GetNamespace("MAPI");
        public static string DeletedItemsFolderName = "Deleted Items (eMail Manager)";
        public static Outlook.Folder DeletedItemsFolder;
        public static Outlook.Folder OlInbox = MapiNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox) as Outlook.Folder;
        public static Outlook.Folder OlDeletedItems = MapiNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderDeletedItems) as Outlook.Folder;
        public static Outlook.MailItem OlLastItem;

        public static int MaxFile = 255;

        private static Outlook.Folder _saveFolder;

        public static void ClearDeletedItems()
        {

        }

        public static string TrimLongFileName(ref string strValue)
        {
            string retval = strValue;
            if (strValue.Length >= MaxFile)
            {
                retval = strValue.Substring(0, MaxFile - 4) + GlobalSettings.MessageFileExt;
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
            retval = retval.Replace("<sent>", email.SentOn.Year.ToString() + "-" + modDateTime.ShortMonth3(email.SentOn.Month.ToString()) + "-" + modDateTime.ShortDay1(email.SentOn.Day.ToString()) + "_" + RemoveIllegalCharacters(email.SentOn.ToShortTimeString()));
            retval = retval.Replace("<sent_yy>", modDateTime.ShortYear2(email.SentOn.Year.ToString()));
            retval = retval.Replace("<sent_yyyy>", email.SentOn.Year.ToString());
            retval = retval.Replace("<sent_mm>", modDateTime.ShortMonth3(email.SentOn.Month.ToString()));
            retval = retval.Replace("<sent_dd>", modDateTime.ShortDay1(email.SentOn.Day.ToString()));
            retval = retval.Replace("<sent_hh-mm>", RemoveIllegalCharacters(email.SentOn.ToShortTimeString()));
            retval = retval.Replace("<sent_hh.mm>", RemoveIllegalCharacters(email.SentOn.ToShortTimeString()).Replace("-", "."));
            retval = retval.Replace("<sent_hh-mm12>", RemoveIllegalCharacters(email.SentOn.ToString("h:mm tt")));
            retval = retval.Replace("<sent_hh.mm12>", RemoveIllegalCharacters(email.SentOn.ToString("h:mm tt")).Replace("-", "."));
            retval = retval.Replace("<sent_hhmmss>", RemoveIllegalCharacters(email.SentOn.ToString("HHmmss"))); // em.SentOn.Hour & em.SentOn.Minute & em.SentOn.Second)

            // handle subject information
            string sSafeSubject = Strings.Trim(RemoveIllegalCharacters(NullSubject(sSubject)));
            retval = retval.Replace("<subject>", sSafeSubject);

            // does the filename filter contain a short subject variable?
            if (filenameFilter.Contains("<$["))
            {
                int iStart = filenameFilter.IndexOf("<$[");
                int iEnd = filenameFilter.IndexOf("]subject>");
                string sLength = filenameFilter.Substring(iStart + 3, 2);
                int iLength = Conversions.ToInteger(sLength);
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
                var recip = MapiNamespace.CreateRecipient(email.SenderEmailAddress);
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
            if (SentMail == true)
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

            retval = retval.Replace("<from_domain>", GetDomainFromEmail(ref email));
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

        public static string RemoveIllegalCharacters(string sStart)
        {
            string RemoveIllegalCharactersRet = default;
            // this function will remove these characters \ / : * ? < > | 
            // from sString and replace them a space
            string sLegalString = sStart;
            sLegalString = String.Replace(sLegalString, @"\", " ");
            sLegalString = String.Replace(sLegalString, "/", " ");
            sLegalString = String.Replace(sLegalString, ":", "-");
            sLegalString = String.Replace(sLegalString, "*", " ");
            sLegalString = String.Replace(sLegalString, "?", " ");
            sLegalString = String.Replace(sLegalString, "\"", " ");
            sLegalString = String.Replace(sLegalString, "<", " ");
            sLegalString = String.Replace(sLegalString, ">", " ");
            sLegalString = String.Replace(sLegalString, "|", " ");
            RemoveIllegalCharactersRet = sLegalString;
            return RemoveIllegalCharactersRet;
        }



    }
}
