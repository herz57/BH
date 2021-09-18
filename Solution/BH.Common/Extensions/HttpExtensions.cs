using System;
using System.Web;

namespace BH.Common.Extensions
{
    public static class HttpExtensions
    {
        public static Uri AddQuery(this Uri uri, string key, string value)
        {
            if (value == null)
                return uri;

            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);
            httpValueCollection.Remove(key);
            httpValueCollection.Add(key, value);

            var ub = new UriBuilder(uri)
            {
                Query = httpValueCollection.ToString()
            };

            return ub.Uri;
        }
    }
}
