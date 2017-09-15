using System;
using System.Collections.Generic;
using System.Text;
using Meta.NET.Extensions;

namespace Meta.NET.RuleSets.Default
{
    public class ProviderRuleSet : RuleSet
    {
        public ProviderRuleSet() : this("provider") { }

        public ProviderRuleSet(string ruleSetKey) : base(ruleSetKey)
        {
            Rules.Add(new Rule("meta[property=\"og:site_name\"]", element => element.GetAttribute("content")));
            Rules.Add(new Rule("meta[name=\"application-name\"]", element => element.GetAttribute("content")));

            DefaultValue = (context) => Url.GetUrlProvider(context.Url); 
        }
    }
}
