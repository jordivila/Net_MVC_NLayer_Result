using System.Configuration;

namespace VsixMvcAppResult.Models.Configuration.ConfigSections.DomainInfo
{
    public interface IDomainInfoConfiguration
    {
        string DomainName { get; }
        string SecurityProtocol { get; }
    }

    public class DomainInfoConfiguration : IDomainInfoConfiguration
    {
        public string DomainName
        {
            get
            {
                return Configuration.ApplicationConfiguration.AppSettings["domain:settings:name"];
            }
        }

        public string SecurityProtocol
        {
            get
            {
                return Configuration.ApplicationConfiguration.AppSettings["domain:settings:securityProtocol"];
            }
        }
    }
}
