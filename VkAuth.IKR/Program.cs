using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkAuth.Enums;

namespace VkAuth.IKR
{
    class Program
    {
        static void Main(string[] args)
        {
            var vkauth = VkAuthFactory.Create(VkAuthType.Awesomium);
            vkauth.OnResponse += OnResponse;
            vkauth.Login(CreateRequest());

            Console.ReadLine();
        }

        private static Request CreateRequest()
        {
            return new Request()
            {
                ClientID = 1, ///4159456,
                Display = DisplayPageType.Page,
                State = "1234",
                Scope = new HashSet<Scope>() { Scope.Friends }
            };
        }

        private static void OnResponse(Response response)
        {

        }
    }
}
