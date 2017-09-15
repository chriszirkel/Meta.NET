using System;
using System.Collections.Generic;
using System.Text;
using Meta.NET.Extensions;

namespace Meta.NET.RuleSets.Default
{
    public class ImageRuleSet : RuleSet
    {
        public ImageRuleSet() : this("image") { }

        public ImageRuleSet(string ruleSetKey) : base(ruleSetKey)
        {
            Rules.Add(new Rule("meta[property=\"og:image:secure_url\"]", element => element.GetAttribute("content")));
            Rules.Add(new Rule("meta[property=\"og:image:url\"]", element => element.GetAttribute("content")));
            Rules.Add(new Rule("meta[property=\"og:image\"]", element => element.GetAttribute("content")));
            Rules.Add(new Rule("meta[name=\"twitter:image\"]", element => element.GetAttribute("content")));
            Rules.Add(new Rule("meta[property=\"twitter:image\"]", element => element.GetAttribute("content")));
            Rules.Add(new Rule("meta[name=\"thumbnail\"]", element => element.GetAttribute("content")));

            Processor = (imageUrl, context) => Url.MakeUrlAbsolute(context.Url, imageUrl);
        }
    }
}
