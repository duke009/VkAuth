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
            var vkauth = VkAuthFactory.Create(VkAuthType.IE);
            vkauth.OnResponse += OnResponse;
            vkauth.Login(new Request());
        }

        private static void OnResponse(Response response)
        {

        }
    }
}
