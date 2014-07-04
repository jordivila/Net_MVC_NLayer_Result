using System;
using System.Web.Mvc;
using VsixMvcAppResult.Models.Membership;
using VsixMvcAppResult.Models.UserRequestModel;
using VsixMvcAppResult.UI.Web.Areas.UserAccount.Controllers;

namespace VsixMvcAppResult.UI.Web.Common.AspNetApplicationServices
{
    public class MembershipUpdateLastActivityActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (filterContext.Controller.GetType() == typeof(UserAccountBarController))
                {
                    if (MvcApplication.UserRequest.UserIsLoggedIn)
                    {
                        MembershipUserWrapper user = MvcApplication.UserRequest.UserMembership_GetAndUpdateActivity;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

            base.OnActionExecuting(filterContext);
        }
    }
}