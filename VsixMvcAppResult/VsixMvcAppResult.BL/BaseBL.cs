using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using VsixMvcAppResult.Models.UserRequestModel;
using VsixMvcAppResult.Models.Unity;
using System.Transactions;

namespace VsixMvcAppResult.BL
{
    public abstract class BaseBL : IDisposable
    {
        public BaseBL()
        {

        }

        private IUserRequestModel _userRequest = null;

        internal IUserRequestModel UserRequest
        {
            get
            {
                if (_userRequest == null)
                {
                    _userRequest = DependencyFactory.Resolve<IUserRequestModel>();
                }

                return _userRequest;
            }
        }

        internal TransactionScope TransactionScopeCreate()
        {
            return new TransactionScope(TransactionScopeOption.Required,
                                            new TransactionOptions()
                                            {
                                                IsolationLevel = IsolationLevel.ReadCommitted
                                            });
        }

        public virtual void Dispose()
        {

        }
    }
}