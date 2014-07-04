using System;
using VsixMvcAppResult.Models.Enumerations;

namespace VsixMvcAppResult.Models.Configuration.ConnectionProviders
{
    public static class Info
    {
        public static string GetDatabaseName(ApplicationConfiguration.DatabaseNames connectionString)
        {
            string cnnProvider = connectionString.ToEnumMemberString();

            return cnnProvider;
        }
    }
}
