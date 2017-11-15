using System;

namespace VkAuth
{
    public interface IBrowser
    {
        void Navigate(Uri uri);
        void BrowserOnNavigated(Action<Uri> callback);
    }
}