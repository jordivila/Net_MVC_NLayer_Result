using System;

namespace VsixMvcAppResult.Models.Membership
{
    public interface IMembershipBL : IMembershipProxy
    {
        bool ValidatePasswordStrength(string password);
    }
}
