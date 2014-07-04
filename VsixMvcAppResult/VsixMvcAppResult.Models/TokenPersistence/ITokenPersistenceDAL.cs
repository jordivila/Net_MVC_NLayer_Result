using VsixMvcAppResult.Models.Common;
using VsixMvcAppResult.Models.RepositoryPattern;

namespace VsixMvcAppResult.Models.TokenPersistence
{
    public interface ITokenTemporaryPersistenceBL<T> : IRepository<TokenTemporaryPersistenceServiceItem<T>>
    {

    }

    public interface ITokenTemporaryPersistenceDAL<T> : IRepository<TokenTemporaryPersistenceServiceItem<T>>
    {

    }
}
