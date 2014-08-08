using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Common.Extensions
{
    public static class UriExtensions
    {
        /// <summary>
        /// returns something like: http://www.google.com
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetRoot(this Uri uri)
        {
            var port = uri.Port == 80 ? string.Empty : ":" + uri.Port;
            return string.Format(uri.Scheme + "://" + uri.Host + port);
        }
    }
}
