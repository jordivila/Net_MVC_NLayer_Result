using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VsixMvcAppResult.Resources.Helpers.GeneratedResxClasses;
using VsixMvcAppResult.UI.Web.Models;

namespace VsixMvcAppResult.UI.Web.Areas.Test.Models
{
    public class TestViewModel : baseViewModel
    {
        [Display(ResourceType = typeof(VsixMvcAppResult.Resources.UserAdministration.UserAdminTexts), Name = UserAdminTextsKeys.CreationDate)]
        public DateTime SomeDate { get; set; }

        [Display(Name = "Some Double Value")]
        public double SomeDouble { get; set; }

        [Display(Name = "Some Boolean Value")]
        public bool SomeBoolean { get; set; }

        [Display(Name = "Some Boolean Nullable")]
        public bool? SomeBooleanNullable { get; set; }

        [Display(Name = "Some String Array")]
        public IEnumerable<string> SomeStringArray { get; set; }

        [Display(Name = "Another String Array")]
        public IEnumerable<string> SomeStringArrayII { get; set; }

        [Display(Name = "Another String Array")]
        public IEnumerable<string> SomeStringArrayIII { get; set; }
    }
}