using System.ServiceModel.Syndication;
using VsixMvcAppResult.Models.Syndication;
using VsixMvcAppResult.UI.Web.Models;

namespace VsixMvcAppResult.UI.Web.Areas.Blog.Models
{
    //[NonValidateModelOnHttpGet]
    public class BlogModel : baseViewModel
    {
        public string FeedId { get; set; }
        public SyndicationFeedFormatter FeedFormatter { get; set; }

        public DataFilterSyndication FeedFilter { get; set; }
        public DataResultSyndicationItems FeedItems { get; set; }
    }
}