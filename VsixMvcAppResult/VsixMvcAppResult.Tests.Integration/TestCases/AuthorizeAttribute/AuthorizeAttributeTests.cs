﻿using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsixMvcAppResult.Models;
using VsixMvcAppResult.Models.Roles;
using VsixMvcAppResult.Models.Unity;
using VsixMvcAppResult.Models.UserRequestModel;
using VsixMvcAppResult.UI.Web.Areas.Error;
using VsixMvcAppResult.Models.Enumerations;
using VsixMvcAppResult.Tests.Integration.Common;
using VsixMvcAppResult.Tests.Integration.Common.Controllers;

namespace VsixMvcAppResult.Tests.Integration.TestCases.AuthorizeAttribute
{
    [TestClass]
    public class AuthorizeAttributeControllerTest : TestControllerBase<AdminOnlyControllerAreaRegistration>
    {
        private void User_Check_Access<TModel>(ControllerFake<AdminOnlyControllerController, TModel> controller,
                                        Func<ActionResult, ActionResult, bool> resultComparer,
                                        ActionResult actionResultExpected)
        {
            MemberInfo action = baseModel.GetInfoMethod(() => controller.Controller.Index());
            ControllerActionInvokerForTesting invoker = new ControllerActionInvokerForTesting();


            invoker.ActionResultExpected = actionResultExpected == null ?
                                                new RedirectResult(ErrorUrlHelper.UnAuthorized(controller.Controller.Url), false)
                                                : actionResultExpected;

            invoker.ActionResultComparer = resultComparer == null ? delegate(ActionResult result1, ActionResult result2)
                                                                    {
                                                                        return ((RedirectResult)result1).Url == ((RedirectResult)result2).Url;
                                                                    }
                                                                    : resultComparer;

            Assert.IsTrue(invoker.InvokeAction(controller.Controller.ControllerContext, action.Name));
        }

        [TestMethod]
        public void User_Unauthenticated_GetsRedirect()
        {
            this.User_Check_Access(new ControllerFake<AdminOnlyControllerController, object>(), null, null);
        }

        [TestMethod]
        public void User_Authenticated_WithNOAccess_GetsRedirected()
        {
            UserForTesting userTesting = new UserForTesting(Guid.NewGuid().ToString(), "123456");
            ControllerFake<AdminOnlyControllerController, object> controller = new ControllerFake<AdminOnlyControllerController, object>();
            controller.Controller.Request.Cookies.Add(new HttpCookie(UserRequestModel_Keys.WcfFormsAuthenticationCookieName, userTesting.AuthenticationToken));
            this.User_Check_Access(controller, null, null);
        }

        [TestMethod]
        public void User_Authenticated_WithAccess_GetsController()
        {
            UserForTesting userTesting = new UserForTesting(Guid.NewGuid().ToString(), "123456");
            ControllerFake<AdminOnlyControllerController, object> controller = new ControllerFake<AdminOnlyControllerController, object>();
            controller.Controller.Request.Cookies.Add(new HttpCookie(UserRequestModel_Keys.WcfFormsAuthenticationCookieName, userTesting.AuthenticationToken));
            Func<ActionResult, ActionResult, bool> resultComparer = delegate(ActionResult result1, ActionResult result2)
            {
                return (result1.GetType() == result2.GetType()) && (result2.GetType() == typeof(JsonResult));
            };

            IRoleManagerProxy providerRoles = DependencyFactory.Resolve<IRoleManagerProxy>();
            providerRoles.AddToRoles(userTesting.Email, new string[1] { SiteRoles.Administrator.ToString() });
            providerRoles.Dispose();


            this.User_Check_Access(controller, resultComparer, AdminOnlyControllerController.IndexResult);
        }
    }
}

