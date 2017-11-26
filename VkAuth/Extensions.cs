using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VkAuth.Enums;

namespace VkAuth
{

    public static class Extensions
    {
        public static long BuildScope(this HashSet<Scope> scopeSet)
        {
            return scopeSet.Aggregate(0, (current, scope) => current | (int) scope);
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