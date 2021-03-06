﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsixMvcAppResult.Models.Common;
using VsixMvcAppResult.Models.Enumerations;
using VsixMvcAppResult.Models.Membership;
using VsixMvcAppResult.Models.Roles;
using VsixMvcAppResult.Models.Unity;
using VsixMvcAppResult.Resources.UserAdministration;
using VsixMvcAppResult.Tests.Common;
using VsixMvcAppResult.Tests.Integration.Common;

namespace VsixMvcAppResult.Tests.Integration.TestProxies
{
    [TestClass]
    public class RoleServiceAdminTests : TestIntegrationBase
    {
        private static IMembershipProxy _memberShipeServices;
        private static IRoleManagerProxy _rolesServices;

        private static List<MembershipUserWrapper> _listUsers = new List<MembershipUserWrapper>();
        private static string _tempRoleName = "SomeRoleName";

        public RoleServiceAdminTests()
        {

        }

        static Func<bool?, string[], bool?, MembershipUserWrapper> createUserTemp = delegate(bool? approved, string[] isInRoleName, bool? locked)
        {
            Thread.Sleep(1);
            Guid userGuid = Guid.NewGuid();
            string userName = userGuid.ToString();
            string userEmail = string.Format("{0}@gmail.com", userName.Replace("-", string.Empty));
            string userPwd = "1*dk**_=lsdk/()078909";

            _memberShipeServices.CreateUser(userName, userPwd, userEmail, string.Empty, string.Empty, "/useraccountt/activate/");
            MembershipUserWrapper userTemp = _memberShipeServices.GetUserByName(userName, false).Data;
            return userTemp;
        };




        [TestInitialize()]
        public void MyTestInitialize()
        {
            _memberShipeServices = DependencyFactory.Resolve<IMembershipProxy>();
            _rolesServices = DependencyFactory.Resolve<IRoleManagerProxy>();



            if (_listUsers.Count == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    _listUsers.Add(createUserTemp(false, new string[0], false));
                }
            }

        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            TestIntegrationBase.SetHttpContext();

            foreach (MembershipUserWrapper item in _listUsers)
            {
                _memberShipeServices.DeleteUser(item.UserName, true);
            }

            if (_rolesServices.RoleExists(_tempRoleName).Data)
            {
                _rolesServices.Delete(_tempRoleName);
            }

            _memberShipeServices.Dispose();
            _rolesServices.Dispose();

        }


        [TestMethod]
        public void RoleServiceAdminTests_ShouldPass()
        {
            this.Create();
            this.AddToRoles();
            this.FindUserNamesByRole();
            this.FindAll();
            this.IsInRole();
            this.RemoveFromRole();
            this.Delete();
            this.FindByUserName();
        }

        public void Create()
        {
            string existingRole = SiteRoles.Guest.ToString();
            DataResultBoolean resultFail = _rolesServices.Create(existingRole);
            Assert.AreEqual(resultFail.Data, false);
            Assert.AreEqual(resultFail.Message, string.Format(UserAdminTexts.RoleAlreadyExists, existingRole));

            DataResultBoolean resultSuccess = _rolesServices.Create(_tempRoleName);
            Assert.AreEqual(resultSuccess.Data, true);
        }
        public void AddToRoles()
        {
            // Test adding role to a user already in
            DataResultBoolean resultFail = _rolesServices.AddToRoles(_listUsers[0].UserName, new string[1] { SiteRoles.Guest.ToString() });
            //Assert.AreEqual(resultFail.Data, false);
            //Assert.AreEqual(resultFail.Message, string.Format(UserAdminTexts.UserIsAlreadyInRole, _listUsers[0], SiteRoles.Guest.ToString()));
            Assert.AreEqual(resultFail.Data, true);

            //Test success
            foreach (var item in _listUsers)
            {
                DataResultBoolean result = _rolesServices.AddToRoles(item.UserName, new string[1] { _tempRoleName });
                Assert.AreEqual(result.Data, true);
                Assert.AreEqual(_rolesServices.IsInRole(item.UserName, _tempRoleName).Data, true);
            }
        }
        public void FindUserNamesByRole()
        {
            DataResultStringArray result = _rolesServices.FindUserNamesByRole(_tempRoleName);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Data.Count() == _listUsers.Count, true);
        }
        public void FindAll()
        {
            // Test roles contain at least roles existing in SiteRoles enum
            DataResultStringArray result = _rolesServices.FindAll();
            Array siteRolesValues = Enum.GetValues(typeof(SiteRoles));
            foreach (var item in siteRolesValues)
            {
                Assert.AreEqual(result.Data.Contains(((SiteRoles)item).ToString()), true);
            }
        }
        public void IsInRole()
        {
            DataResultBoolean resultFail = _rolesServices.IsInRole(_listUsers[0].UserName, SiteRoles.Administrator.ToString());
            Assert.AreEqual(resultFail.Data, false);

            DataResultBoolean result = _rolesServices.IsInRole(_listUsers[0].UserName, SiteRoles.Guest.ToString());
            Assert.AreEqual(result.Data, true);
        }
        public void RemoveFromRole()
        {
            DataResultBoolean resultFail = _rolesServices.RemoveFromRoles(_listUsers[0].UserName, new string[1] { "someUnexistingRoleName" });
            //Assert.AreEqual(resultFail.Data, false);
            //Assert.AreEqual(resultFail.Message, UserAdminTexts.RoleUnexists);
            Assert.AreEqual(resultFail.Data, true); //--> non existing roles won't be checked

            foreach (var item in _listUsers)
            {
                DataResultBoolean resultFailII = _rolesServices.RemoveFromRoles(item.UserName, new string[1] { _tempRoleName });
                Assert.AreEqual(resultFailII.Data, true);
                Assert.AreEqual(_rolesServices.IsInRole(item.UserName, _tempRoleName).Data, false);
            }
        }
        public void Delete()
        {
            // Test deleting unexisting role 
            DataResultBoolean resultFail = _rolesServices.Delete("someunexistingname_kjfhdskf");
            Assert.AreEqual(resultFail.Data, false);
            Assert.AreEqual(resultFail.Message, UserAdminTexts.RoleUnexists);

            //Test Role with pending users 
            DataResultBoolean resultFailII = _rolesServices.Delete(SiteRoles.Guest.ToString());
            Assert.AreEqual(resultFailII.Data, false);
            Assert.AreEqual(resultFailII.Message, string.Format(UserAdminTexts.RoleHasPendingUsersError, _tempRoleName));


            DataResultBoolean resultSuccess = _rolesServices.Delete(_tempRoleName);
            Assert.AreEqual(resultSuccess.Data, true);
        }
        public void FindByUserName()
        {
            DataResultStringArray result = _rolesServices.FindByUserName(_listUsers[0].UserName);
            Assert.AreEqual(result.Data.Count() == 1, true);
            Assert.AreEqual(result.Data.Contains(SiteRoles.Guest.ToString()), true);
        }
    }
}
