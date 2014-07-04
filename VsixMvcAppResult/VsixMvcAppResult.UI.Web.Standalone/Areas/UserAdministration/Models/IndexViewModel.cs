using System.Collections.Generic;
using VsixMvcAppResult.Models.Membership;
using VsixMvcAppResult.UI.Web.Models;
using VsixMvcAppResult.UI.Web.Common.Mvc.Attributes;


namespace VsixMvcAppResult.UI.Web.Areas.UserAdministration.Models
{
    public enum Actions : int
    {
        Approve,
        UnApprove,
        Delete,
        Update,
        Search,
        //Paginate,
        //PageSizeChange,
        //Sort,
        ViewDetail,
        UnLock
    }

    [NonValidateModelOnHttpGet]
    public class IndexViewModel : baseViewModel
    {
        public Actions Action { get; set; }
        public DataFilterUserList Filter { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public DataResultUserList UserListResult { get; set; }
    }
}