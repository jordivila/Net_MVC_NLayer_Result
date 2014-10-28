using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VsixMvcAppResult.UI.Web.Common.Mvc.Attributes;
using VsixMvcAppResult.UI.Web.Models;

namespace VsixMvcAppResult.UI.Web.Areas.Home.Models
{

    [NonValidateModelOnHttpGet]
    public class ThemeSelectModel : baseViewModel
    {
        public MenuModel ThemesMenu { get; set; }
    }

    [NonValidateModelOnHttpGet]
    public class CultureSelectModel : baseViewModel
    {
        public MenuModel CulturesMenu { get; set; }
    }

}