using System.Collections.Generic;
using System.Linq;
using VkAuth.Enums;

namespace VkAuth
{

    public static class Extensions
    {
        public static long BuildScope(this HashSet<Scope> scope)
        {
            return scope.Aggregate(0, (current, scope1) => current & (int) scope1);
        }
    }
}