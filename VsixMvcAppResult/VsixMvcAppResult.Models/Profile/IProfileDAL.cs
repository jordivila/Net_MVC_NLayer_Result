using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using VsixMvcAppResult.Models.UserRequestModel;

namespace VsixMvcAppResult.Models.Profile
{
    public interface IProfileDAL : IDisposable
    {
        DataResultUserProfile Create(string userName, IUserRequestModel userRequest);

        DataResultUserProfile Get(IUserRequestModel userRequest);

        DataResultUserProfile Update(UserProfileModel userProfile, IUserRequestModel userRequest);
    }
}