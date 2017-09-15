using System;
using System.Collections.Generic;
using System.Text;

namespace Meta.NET.RuleSets
{
    public class TitleRuleSet : RuleSet
    {
        public TitleRuleSet()
        {
            Rules.Add(new Rule("meta[property=\"og:title\"]", element => element.GetAttribute("content")));
            Rules.Add(new Rule("meta[name=\"twitter:title\"]", element => element.GetAttribute("content")));
            Rules.Add(new Rule("meta[property=\"twitter:title\"]", element => element.GetAttribute("content")));
            Rules.Add(new Rule("meta[name=\"hdl\"]", element => element.GetAttribute("content")));
            Rules.Add(new Rule("title", element => element.TextContent));
        }
    }
}
