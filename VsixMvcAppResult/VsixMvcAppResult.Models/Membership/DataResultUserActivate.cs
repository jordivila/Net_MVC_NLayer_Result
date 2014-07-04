using System;
using System.Runtime.Serialization;
using VsixMvcAppResult.Models.Common;

namespace VsixMvcAppResult.Models.Membership
{
    public interface IAccountActivationModel
    {
        Guid ActivateUserToken { get; set; }
        MembershipUserWrapper User { get; set; }
    }

    public class AccountActivationModel : IAccountActivationModel
    {
        [DataMember]
        public Guid ActivateUserToken { get; set; }

        [DataMember]
        public MembershipUserWrapper User { get; set; }
    }

    [DataContract]
    public class DataResultUserActivate : baseDataResult<AccountActivationModel>, IDataResultModel<AccountActivationModel>
    {

    }
}
