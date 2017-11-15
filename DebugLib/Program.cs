using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkAuth;
//using VkAuth.Awesomium;
using VkAuth.Enums;
using VkAuth.GeckoFx;
//using VkAuth.IE;

namespace DebugLib
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new Factory();
            var vkauth = factory.Create();
            vkauth.OnResponse += OnResponse;
            vkauth.Login(CreateRequest());

            Console.ReadLine();
        }

        private static void OnResponse(Response obj)
        {
            throw new NotImplementedException();
        }

        private static Request CreateRequest()
        {
            return new Request()
            {
               
                ClientID = 1546546346, ///4159456,
                Display = DisplayPageType.Page,
                State = "1234",
                Scope = new HashSet<Scope>() { Scope.Friends }
            };
        }
    }
}
