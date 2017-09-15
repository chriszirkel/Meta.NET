using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meta.NET.RuleSets
{
    public class Rule
    {
        public delegate string ElementSelectorFunc(IElement element);

        public string QuerySelector { get; set; }

        public ElementSelectorFunc ElementSelector { get; set; }

        public Rule(string querySelector, ElementSelectorFunc elementSelector)
        {
            QuerySelector = querySelector;
            ElementSelector = elementSelector;
        }
    }
}
