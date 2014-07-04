using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using VsixMvcAppResult.DAL.TokenTemporaryPersistenceServices;
using VsixMvcAppResult.Models.TokenPersistence;
using VsixMvcAppResult.Models.Unity;
using System.Linq.Expressions;
using VsixMvcAppResult.Models.Common;

namespace VsixMvcAppResult.BL.TokenTemporaryPersistenceServices
{
    public class TokenTemporaryPersistenceBL<T> : BaseBL, ITokenTemporaryPersistenceBL<T>
    {
        private ITokenTemporaryPersistenceDAL<T> _dal;

        public TokenTemporaryPersistenceBL()
        {
            _dal = DependencyFactory.Resolve<ITokenTemporaryPersistenceDAL<T>>();
        }

        public override void Dispose()
        {
            base.Dispose();

            if (this._dal != null)
            {
                this._dal.Dispose();
            }
        }

        public TokenTemporaryPersistenceServiceItem<T> Insert(TokenTemporaryPersistenceServiceItem<T> entity)
        {
            return this._dal.Insert(entity);
        }

        public object Delete(TokenTemporaryPersistenceServiceItem<T> entity)
        {
            return this._dal.Delete(entity);
        }

        public TokenTemporaryPersistenceServiceItem<T> Update(TokenTemporaryPersistenceServiceItem<T> entity)
        {
            return this._dal.Update(entity);
        }

        public TokenTemporaryPersistenceServiceItem<T> GetById(object id)
        {
            return this._dal.GetById(id);
        }
    }
}
