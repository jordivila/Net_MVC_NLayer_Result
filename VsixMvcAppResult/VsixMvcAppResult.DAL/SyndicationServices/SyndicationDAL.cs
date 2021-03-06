﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.ServiceModel.Syndication;
using System.Xml;
using VsixMvcAppResult.Models.ExtensionMethods;
using VsixMvcAppResult.Models.Logging;
using VsixMvcAppResult.Models.Syndication;

namespace VsixMvcAppResult.DAL.SyndicationServices
{
    public class SyndicationDAL : BaseDAL, ISyndicationDAL
    {
        private string GetBlogRssFeedPath
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace(@"file:\", string.Empty), "SyndicationServices\\blogrss.xml");
            }
        }

        private SyndicationFeedFormatter GetAll()
        {
            FileStream fs = null;
            XmlReader xr = null;
            SyndicationFeed feed = null;

            try
            {
                    fs = System.IO.File.OpenRead(this.GetBlogRssFeedPath);
                    xr = XmlTextReader.Create((Stream)fs);
                    feed = SyndicationFeed.Load(xr);
                    return new Atom10FeedFormatter(feed);
            }
            catch (Exception ex)
            {
                LoggingHelper.Write(ex);

                throw;
            }
            finally
            {
                if (fs != null) { fs.Close(); fs.Dispose(); }
                if (xr != null) { xr.Close(); }
            }
        }

        private SyndicationItemFormatter GetFormatter(SyndicationItem feedItem)
        {
            return (SyndicationItemFormatter)new Atom10ItemFormatter(feedItem);
        }

        private DataResultSyndicationItems GetDataResult(IEnumerable<SyndicationItem> items, DataFilterSyndication filter)
        {
            return new DataResultSyndicationItems()
            {
                Data = items.Skip(Convert.ToInt32(filter.Page * filter.PageSize))
                            .Take(filter.PageSize)
                            .Select(p => { return this.GetFormatter(p); })
                            .ToList(),
                PageSize = filter.PageSize,
                Page = filter.Page,
                SortBy = filter.SortBy,
                SortAscending = filter.SortAscending,
                TotalRows = items.Count()
            };
        }

        public DataResultSyndicationItems GetLast(DataFilterSyndication filter)
        {
            SyndicationFeedFormatter feed = this.GetAll();
            return this.GetDataResult(feed.Feed.Items, filter);
        }

        public DataResultSyndicationItems GetByTitle(DataFilterSyndication filter)
        {
            SyndicationFeedFormatter feed = this.GetAll();

            List<SyndicationItem> items = feed.Feed.Items
                                            .Where(x => x.Links.Where(p => p.Uri.ToString().Contains(filter.Uri)).Count() > 0)
                                            .ToList();

            return this.GetDataResult(items, filter);
        }

        public DataResultSyndicationItems GetByCategory(DataFilterSyndication filter)
        {
            SyndicationFeedFormatter feed = this.GetAll();
            SyndicationCategoryComparer categoryComparer = new SyndicationCategoryComparer();
            SyndicationCategory categoryToFind = new SyndicationCategory(filter.CategoryName);
            List<SyndicationItem> items = feed.Feed.Items
                                            .Where(p => p.Categories.Contains(categoryToFind, categoryComparer))
                                            .ToList();
            return this.GetDataResult(items, filter);
        }

        public SyndicationFeedFormatter GetInfoWithNoItems()
        {
            SyndicationFeedFormatter feedFormatter = this.GetAll();
            feedFormatter.Feed.Categories.AddRange(feedFormatter.CategoriesGetFromItems());
            return feedFormatter.RemoveContent();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
