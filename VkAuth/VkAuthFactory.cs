﻿using System;
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
                    throw new NotImplementedException();
                case VkAuthType.WindowsForm:
                case VkAuthType.IE:
                case VkAuthType.GekoFx:
                    return CreateInstance(type);

                default:
                    throw new NotSupportedException();
                        
            }
        }

        private static IVkAuth CreateInstance(VkAuthType uiType)
        {
            var path = Path.Combine(AssemblyDirectory, $@"VkAuth.{uiType}.dll");
            var dll = Assembly.LoadFile(path);

            IFactory factory = null;
            foreach (var type in dll.GetExportedTypes())
            {
                if (typeof(IFactory).IsAssignableFrom(type))
                {
                    factory = (IFactory)Activator.CreateInstance(type);
                    break;
                }
            }

            if(factory == null)
                throw new FileNotFoundException(path);

            return factory.Create();

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