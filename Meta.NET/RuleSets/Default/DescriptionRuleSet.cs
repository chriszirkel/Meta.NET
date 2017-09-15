using System;
using System.Collections.Generic;
using System.Text;

namespace Meta.NET.RuleSets.Default
{
    public class DescriptionRuleSet : RuleSet
    {
        public DescriptionRuleSet() : this("description") { }

        public DescriptionRuleSet(string ruleSetKey) : base(ruleSetKey)
        {
            Rules.Add(new Rule("meta[property=\"og:description\"]", element => element.GetAttribute("content")));
            Rules.Add(new Rule("meta[name=\"description\"]", element => element.GetAttribute("content")));
        }
    }
}
