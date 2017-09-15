using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Meta.NET.Extensions;

namespace Meta.NET
{
    public static class Url
    {
        public static string MakeUrlAbsolute(string baseUrl, string relativeUrl)
        {
            var relativeParsed = new Uri(relativeUrl, UriKind.RelativeOrAbsolute);

            if (!relativeParsed.IsAbsoluteUri)
            {
                var absoluteUrl = new Uri(new Uri(baseUrl, UriKind.Absolute), relativeParsed);
                return absoluteUrl.ToString();
            }

            return relativeParsed.ToString();
        }

        public static string GetUrlProvider(string absoluteUrl)
        {
            var absoluteParsed = new Uri(absoluteUrl, UriKind.Absolute);
            var host = absoluteParsed.Host;

            var provider = string.Join
                (" ", Regex
                    .Replace(host, "www[a-zA-Z0-9]*\\.", "")
                    .Replace(".co.", ".")
                    .Split('.')
                    .Slice(0, -1)
                );

            return provider;
        }
    }
}
