using System;
using System.ComponentModel;

namespace VsixMvcAppResult.Models.DataAnnotationsAttributes
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string resourceName;

        public LocalizedDisplayNameAttribute(Type resourceType)
            : base()
        {
            this.resourceName = resourceType.Name; ;
        }

        public override string DisplayName
        {
            get
            {
                return this.resourceName;
            }
        }
    }

}