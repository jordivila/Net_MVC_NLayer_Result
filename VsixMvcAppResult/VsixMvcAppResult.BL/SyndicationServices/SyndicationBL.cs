﻿using System.ServiceModel.Syndication;
using Microsoft.Practices.Unity;
using VsixMvcAppResult.DAL.SyndicationServices;
using VsixMvcAppResult.Models.Syndication;
using VsixMvcAppResult.Models.Unity;

namespace VsixMvcAppResult.BL.SyndicationServices
{
    public class SyndicationBL : BaseBL, ISyndicationBL
    {
        private ISyndicationDAL _dal;
        public SyndicationBL()
        {
            _dal = DependencyFactory.Resolve<ISyndicationDAL>();
        }
        public override void Dispose()
        {
            base.Dispose();

            if (this._dal != null)
            {
                this._dal.Dispose();
            }
        }

        public SyndicationFeedFormatter GetInfoWithNoItems()
        {
            return this._dal.GetInfoWithNoItems();
        }

        public DataResultSyndicationItems GetByCategory(DataFilterSyndication filter)
        {
            return this._dal.GetByCategory(filter);
        }

        public DataResultSyndicationItems GetByTitle(DataFilterSyndication filter)
        {
            return this._dal.GetByTitle(filter);
        }

        public DataResultSyndicationItems GetLast(DataFilterSyndication filter)
        {
            return this._dal.GetLast(filter);
        }
    }
}
