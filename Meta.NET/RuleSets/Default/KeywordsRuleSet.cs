using System;
using System.Collections.Generic;
using System.Text;

namespace Meta.NET.RuleSets.Default
{
    public class KeywordsRuleSet : RuleSet
    {
        public KeywordsRuleSet() : this("keywords") { }

        public KeywordsRuleSet(string ruleSetKey) : base(ruleSetKey)
        {
            Rules.Add(new Rule("meta[name=\"keywords\"]", element => element.GetAttribute("content")));

            //TODO: processor: check keyword splitting
        }
    }
}
