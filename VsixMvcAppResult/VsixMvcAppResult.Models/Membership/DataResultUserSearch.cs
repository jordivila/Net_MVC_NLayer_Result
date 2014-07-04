using System.Runtime.Serialization;
using VsixMvcAppResult.Models.Common;

namespace VsixMvcAppResult.Models.Membership
{
    [DataContract]
    public class DataResultUserSearch : baseDataResult<DataResultUserList>, IDataResultModel<DataResultUserList>
    {
    }
}
