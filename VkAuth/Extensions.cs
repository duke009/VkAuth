using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VkAuth.Enums;

namespace VkAuth
{

    public static class Extensions
    {
        public static long BuildScope(this HashSet<Scope> scope)
        {
            return scope.Aggregate(0, (current, scope1) => current & (int) scope1);
        }

        public static Uri AddQuery(this Uri uri, string name, string value)
        {
            var parsed = HttpUtility.ParseQueryString(uri.Query);

            parsed.Remove(name);
            parsed.Add(name, value);

            return new UriBuilder(uri) {Query = parsed.ToString()}.Uri;
        }
    }
}