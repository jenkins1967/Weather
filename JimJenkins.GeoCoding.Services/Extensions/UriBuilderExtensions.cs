using System;

namespace JimJenkins.GeoCoding.Services.Extensions
{
    public static class UriBuilderExtensions
    {
        public static void AddToQueryString(this UriBuilder builder, string query)
        {
            if (builder.Query.Length > 1)
            {
                builder.Query = string.Format("{0}&{1}", builder.Query.Substring(1), query);
            }
            else
            {
                builder.Query = query;
            }
        }
    }
}
