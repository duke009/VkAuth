using System;
using System.IO;
using System.Reflection;
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
                    return CreateInstance(type);
                case VkAuthType.WebForms:
                    return CreateInstance(type);
                case VkAuthType.IE:
                    return CreateInstance(type);

                default:
                    throw new NotSupportedException();
                        
            }
        }

        private static IVkAuth CreateInstance(VkAuthType uiType)
        {
            var filename = $@"VkAuth.{uiType}.dll";
            var dll = Assembly.LoadFile(filename);

            foreach (var type in dll.GetExportedTypes())
            {
                if (typeof(IVkAuth).IsAssignableFrom(type))
                {
                    return (IVkAuth)Activator.CreateInstance(type);
                }
            }

            throw new FileNotFoundException(filename);
        }
    }
}