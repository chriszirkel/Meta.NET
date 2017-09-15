using System;
using System.Collections.Generic;
using System.Text;
using Meta.NET.Extensions;

namespace Meta.NET.RuleSets
{
    public class ProviderRuleSet : RuleSet
    {
        public ProviderRuleSet()
        {
            Rules.Add(new Rule("meta[property=\"og:site_name\"]", element => element.GetAttribute("content")));

            DefaultValue = (context) => context.Url.GetUrlProvider();
        }
    }
}
