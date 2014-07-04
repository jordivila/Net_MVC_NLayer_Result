using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace VsixMvcAppResult.Models.Profile
{
    [ServiceContract]
    public interface IProfileProxy : IDisposable
    {
        [OperationContract]
        DataResultUserProfile Get();

        [OperationContract]
        DataResultUserProfile Update(UserProfileModel userProfile);
    }
}
