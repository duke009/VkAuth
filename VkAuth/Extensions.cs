using System.Collections.Generic;
using System.Linq;

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