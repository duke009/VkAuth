using System;
using System.Collections.Generic;
using VkAuth;
//using VkAuth.Awesomium;
using VkAuth.Enums;
//using VkAuth.GeckoFx;
using VkAuth.IE;

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
               
                ClientID =4159456 , ///1546546346,
                Display = DisplayPageType.Page,
                State = "1234",
                Scope = new HashSet<Scope>() { Scope.Friends }
            };
        }
    }
}
