using System;
using System.Collections.Generic;
using System.Text;

namespace Meta.NET.RuleSets
{
    public class TypeRuleSet : RuleSet
    {
        public TypeRuleSet()
        {
            Rules.Add(new Rule("meta[property=\"og:type\"]", element => element.GetAttribute("content")));
        }
    }
}
