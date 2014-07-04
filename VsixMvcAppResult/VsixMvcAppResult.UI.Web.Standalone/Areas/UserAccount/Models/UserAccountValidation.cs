using System.Web.Security;

namespace VsixMvcAppResult.UI.Web.Areas.UserAccount.Models
{
    public static class AccountValidation
    {
        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for a full list of status codes
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return VsixMvcAppResult.Resources.UserAdministration.UserAdminTexts.DuplicateUserName;

                case MembershipCreateStatus.DuplicateEmail:
                    return VsixMvcAppResult.Resources.UserAdministration.UserAdminTexts.DuplicateEmail;

                case MembershipCreateStatus.InvalidPassword:
                    return VsixMvcAppResult.Resources.UserAdministration.UserAdminTexts.InvalidPassword;

                case MembershipCreateStatus.InvalidEmail:
                    return VsixMvcAppResult.Resources.UserAdministration.UserAdminTexts.InvalidEmail;

                case MembershipCreateStatus.InvalidAnswer:
                    return VsixMvcAppResult.Resources.UserAdministration.UserAdminTexts.InvalidAnswer;

                case MembershipCreateStatus.InvalidQuestion:
                    return VsixMvcAppResult.Resources.UserAdministration.UserAdminTexts.InvalidQuestion;

                case MembershipCreateStatus.InvalidUserName:
                    return VsixMvcAppResult.Resources.UserAdministration.UserAdminTexts.InvalidUserName;

                case MembershipCreateStatus.ProviderError:
                    return VsixMvcAppResult.Resources.General.GeneralTexts.UnexpectedError;

                case MembershipCreateStatus.UserRejected:
                    return VsixMvcAppResult.Resources.General.GeneralTexts.UnexpectedError;

                default:
                    return VsixMvcAppResult.Resources.General.GeneralTexts.UnexpectedError;
            }
        }
    }
}