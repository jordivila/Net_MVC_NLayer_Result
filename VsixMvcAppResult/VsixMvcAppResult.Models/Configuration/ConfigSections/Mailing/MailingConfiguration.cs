using System;
using System.Configuration;

namespace VsixMvcAppResult.Models.Configuration.ConfigSections.Mailing
{
    public interface IMailingConfiguration
    {
        string SupportTeamEmailAddress { get; }
        string SmtpHostName { get; }
        int SmtpHostPort { get; }
        bool SmtpHostUseSsl { get; }
        string SmtpHostPassword { get; }
        bool SmtpIsEnabled { get;  }
    }

    public class MailingConfiguration : IMailingConfiguration
    {
        public bool SmtpIsEnabled
        {
            get
            {
                return Convert.ToBoolean(ApplicationConfiguration.AppSettings["mail:settings:smtp:isEnabled"]);
            }
        }

        public string SupportTeamEmailAddress
        {
            get
            {
                return Configuration.ApplicationConfiguration.AppSettings["mail:settings:smtp:addressSupportTeam"];
            }
        }

        public string SmtpHostName
        {
            get
            {
                return Configuration.ApplicationConfiguration.AppSettings["mail:settings:smtp:hostName"];
            }
        }

        public int SmtpHostPort
        {
            get
            {
                return Convert.ToInt32(Configuration.ApplicationConfiguration.AppSettings["mail:settings:smtp:hostPort"]);
            }
        }

        public bool SmtpHostUseSsl
        {
            get
            {
                return Convert.ToBoolean(Configuration.ApplicationConfiguration.AppSettings["mail:settings:smtp:hostUseSsl"]);
            }
        }

        public string SmtpHostPassword
        {
            get
            {
                return Configuration.ApplicationConfiguration.AppSettings["mail:settings:smtp:hostPassword"];
            }
        }
    }
}

//using System.Configuration;

//namespace VsixMvcAppResult.Models.Configuration.ConfigSections.Mailing
//{
//    public interface IMailingConfiguration
//    {
//        string SupportTeamEmailAddress { get; set; }
//    }

//    internal class MailingSettingsConfigSection : ConfigurationSection
//    {
//        [ConfigurationProperty("mailAddresses")]
//        public MailAddressesElement MailAddresses
//        {
//            get
//            {
//                MailAddressesElement mailAddresses = (MailAddressesElement)base["mailAddresses"];
//                return mailAddresses;
//            }
//        }
//    }

//    internal class MailAddressesElement : ConfigurationElement
//    {
//        [ConfigurationProperty("supportTeamEmailAddress")]
//        public string SupportTeamEmailAddress
//        {
//            get
//            {
//                return (string)this["supportTeamEmailAddress"];
//            }
//            set
//            {
//                this["supportTeamEmailAddress"] = value;
//            }
//        }
//    }

//    public class MailingConfiguration : IMailingConfiguration
//    {
//        private static MailingSettingsConfigSection mailingConfigSection = (MailingSettingsConfigSection)System.Configuration.ConfigurationManager.GetSection("templateSettings/mailingSettings");

//        public string SupportTeamEmailAddress
//        {
//            get
//            {
//                return mailingConfigSection.MailAddresses.SupportTeamEmailAddress;
//            }
//            set
//            {

//            }
//        }
//    }
//}
