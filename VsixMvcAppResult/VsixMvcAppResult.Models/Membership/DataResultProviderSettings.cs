using System.Runtime.Serialization;
using VsixMvcAppResult.Models.Common;

namespace VsixMvcAppResult.Models.Membership
{
    [DataContract]
    public class DataResultProviderSettings : baseDataResult<MembershipProviderSettings>, IDataResultModel<MembershipProviderSettings>
    {
    }
}
