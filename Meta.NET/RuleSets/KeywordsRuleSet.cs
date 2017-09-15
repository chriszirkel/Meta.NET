using System;
using System.Collections.Generic;
using System.Text;

namespace Meta.NET.RuleSets
{
    public class KeywordsRuleSet : RuleSet
    {
        public KeywordsRuleSet()
        {
            Rules.Add(new Rule("meta[name=\"keywords\"]", element => element.GetAttribute("content")));

            //TODO: processor: check keyword splitting
        }
    }
}
