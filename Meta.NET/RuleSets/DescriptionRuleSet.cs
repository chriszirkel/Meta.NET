using System;
using System.Collections.Generic;
using System.Text;

namespace Meta.NET.RuleSets
{
    public class DescriptionRuleSet : RuleSet
    {
        public DescriptionRuleSet()
        {
            Rules.Add(new Rule("meta[property=\"og:description\"]", element => element.GetAttribute("content")));
            Rules.Add(new Rule("meta[name=\"description\"]", element => element.GetAttribute("content")));
        }
    }
}
