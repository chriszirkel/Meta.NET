using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Meta.NET.Extensions;

namespace Meta.NET.RuleSets
{
    public class IconRuleSet : RuleSet
    {
        public IconRuleSet()
        {
            Rules.Add(new Rule("link[rel=\"apple-touch-icon\"]", element => element.GetAttribute("href")));
            Rules.Add(new Rule("link[rel=\"apple-touch-icon-precomposed\"]", element => element.GetAttribute("href")));
            Rules.Add(new Rule("link[rel=\"icon\"]", element => element.GetAttribute("href")));
            Rules.Add(new Rule("link[rel=\"fluid-icon\"]", element => element.GetAttribute("href")));
            Rules.Add(new Rule("link[rel=\"shortcut icon\"]", element => element.GetAttribute("href")));
            Rules.Add(new Rule("link[rel=\"Shortcut Icon\"]", element => element.GetAttribute("href")));
            Rules.Add(new Rule("link[rel=\"mask-icon\"]", element => element.GetAttribute("href")));

            Scorer = (element, score) =>
            {
                var sizes = element.GetAttribute("sizes");

                if (!string.IsNullOrEmpty(sizes))
                {
                    var sizeMatches = Regex.Matches(sizes, @"/\d+/g").Cast<int>();

                    if (sizeMatches.Any())
                        return sizeMatches.Aggregate((a, b) => a * b);
                }

                return null;
            };

            DefaultValue = (context) => "favicon.ico";

            Processor = (iconUrl, context) => iconUrl.MakeUrlAbsolute(context.Url);
        }
    }
}
