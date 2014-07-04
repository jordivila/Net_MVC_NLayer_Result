using System.Runtime.Serialization;
using VsixMvcAppResult.Models.Common;

namespace VsixMvcAppResult.Models.Membership
{
    [DataContract]
    public class DataResultUserList : baseDataPagedResult<MembershipUserWrapper>, IDataResultPaginatedModel<MembershipUserWrapper>
    {

    }
}
