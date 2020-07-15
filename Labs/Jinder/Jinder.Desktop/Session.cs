using System;

namespace Jinder.Desktop
{
    public static class Session
    {
        public static Guid Token
        {
            get; 
            set;
        }

        public static String HostUrl => "http://localhost:49616";
    }
}