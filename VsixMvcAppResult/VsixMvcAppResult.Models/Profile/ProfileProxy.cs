﻿using VsixMvcAppResult.Models.ProxyProviders;
using System;
using System.ServiceModel;

namespace VsixMvcAppResult.Models.Profile
{
    public class ProfileProxy : ProviderBaseChannel<IProfileProxy>, IProfileProxy
    {
        public DataResultUserProfile Get()
        {
            return proxy.Get();
        }
        public DataResultUserProfile Update(UserProfileModel userProfile)
        {
            return this.proxy.Update(userProfile);
        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}