using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meta.NET.RuleSets
{
    public class RuleSet
    {
        public List<Rule> Rules { get; } = new List<Rule>();

        public delegate string DefaultValueFunc(IContext context);

        public DefaultValueFunc DefaultValue { get; set; }

        public delegate int? ScorerFunc(IElement element, int score);

        public ScorerFunc Scorer { get; set; }

        public delegate string ProcessorFunc(string value, IContext context);

        public ProcessorFunc Processor { get; set; }

        public RuleSet()
        {
        }
    }
}
