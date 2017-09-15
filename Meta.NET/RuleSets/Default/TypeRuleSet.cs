using System;
using System.Collections.Generic;
using System.Text;

namespace Meta.NET.RuleSets.Default
{
    public class TypeRuleSet : RuleSet
    {
        public TypeRuleSet() : this("type") { }

        public TypeRuleSet(string ruleSetKey) : base(ruleSetKey)
        {
            Rules.Add(new Rule("meta[property=\"og:type\"]", element => element.GetAttribute("content")));
        }
    }
}
