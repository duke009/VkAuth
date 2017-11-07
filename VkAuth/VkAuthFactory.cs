using System;
using System.IO;
using System.Reflection;
using System.Threading;
using VkAuth.Enums;

namespace VkAuth
{
    public static class VkAuthFactory
    {
        public static IVkAuth Create(VkAuthType type)
        {
            switch (type)
            {
                case VkAuthType.Awesomium:
                case VkAuthType.WebForms:
                case VkAuthType.IE:
                    return CreateInstance(type);

                default:
                    throw new NotSupportedException();
                        
            }
        }

        private static IVkAuth CreateInstance(VkAuthType uiType)
        {
            var path = Path.Combine(AssemblyDirectory, $@"VkAuth.{uiType}.dll");
            var dll = Assembly.LoadFile(path);

            var t = new Thread(MyThreadStartMethod);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            foreach (var type in dll.GetExportedTypes())
            {
                if (typeof(IVkAuth).IsAssignableFrom(type))
                {
                    return (IVkAuth)Activator.CreateInstance(type);
                }
            }

            throw new FileNotFoundException(path);
        }

        private static void MyThreadStartMethod()
        {
            throw new NotImplementedException();
        }

        // wtf lol https://stackoverflow.com/questions/52797/how-do-i-get-the-path-of-the-assembly-the-code-is-in
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}