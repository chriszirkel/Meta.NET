using System;
using System.Collections.Generic;
using System.Text;
using Meta.NET.Extensions;

namespace Meta.NET.RuleSets.Default
{
    public class UrlRuleSet : RuleSet
    {
        public UrlRuleSet() : this("url") { }

        public UrlRuleSet(string ruleSetKey) : base(ruleSetKey)
        {
            Rules.Add(new Rule("meta[property=\"og:url\"]", element => element.GetAttribute("content")));
            Rules.Add(new Rule("link[rel=\"canonical\"]", element => element.GetAttribute("href")));

            DefaultValue = (context) => context.Url;

            Processor = (url, context) => Url.MakeUrlAbsolute(context.Url, url);
        }
    }
}
