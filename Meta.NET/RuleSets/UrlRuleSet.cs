using System;
using System.Collections.Generic;
using System.Text;
using Meta.NET.Extensions;

namespace Meta.NET.RuleSets
{
    public class UrlRuleSet : RuleSet
    {
        public UrlRuleSet()
        {
            Rules.Add(new Rule("meta[property=\"og:url\"]", element => element.GetAttribute("content")));
            Rules.Add(new Rule("link[rel=\"canonical\"]", element => element.GetAttribute("href")));

            DefaultValue = (context) => context.Url;

            Processor = (url, context) => url.MakeUrlAbsolute(context.Url);
        }
    }
}
