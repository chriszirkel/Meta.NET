using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Meta.NET.RuleSets;
using Meta.NET.RuleSets.Default;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Meta.NET
{
    public class Parser
    {
        public List<RuleSet> RuleSets { get; }

        public Parser()
        {
            //TODO: statt Dictionary<string, RuleSet> einen eigenen Typ erstellen, e.g. RuleSetCollection
            // damit dann vorderfinierte RuleSets bauen, z.b. OpenGraphRuleSet, TwitterRuleSets, oder DefaultRuleSet...
            // ggfs. den RuleSetKey in die RuleSet wieder integrieren => in der RuleSetCollection dann intern ein Dict verwenden und nur Methoden zum hinzufügen von RuleSets anbieten,
            // zugriff über index oder direkte GetRuleSetByKey...

            RuleSets = new List<RuleSet>();
            RuleSets.Add(new DescriptionRuleSet());
            RuleSets.Add(new TitleRuleSet());
            RuleSets.Add(new TypeRuleSet());
            RuleSets.Add(new UrlRuleSet());
            RuleSets.Add(new ProviderRuleSet());
            RuleSets.Add(new KeywordsRuleSet());
            RuleSets.Add(new ImageRuleSet());
            RuleSets.Add(new IconRuleSet());
        }

        public Parser(List<RuleSet> ruleSets)
        {
            RuleSets = ruleSets;
        }

        public async Task<Dictionary<string, string>> ParseUrlAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var html = await httpClient.GetStringAsync(url);

                return await ParseHtmlAsync(html, url);
            }
        }

        public async Task<Dictionary<string, string>> ParseUrlAsync(string url, HttpMessageHandler handler)
        {
            using (var httpClient = new HttpClient(handler))
            {
                var html = await httpClient.GetStringAsync(url);

                return await ParseHtmlAsync(html, url);
            }
        }

        public async Task<Dictionary<string, string>> ParseHtmlAsync(string html, string url)
        {
            var resultSet = new Dictionary<string, string>();

            var parser = new HtmlParser();
            var document = await parser.ParseAsync(html);
            var context = new Context { Url = url };

            foreach (var ruleSet in RuleSets)
            {
                var result = BuildRuleSet(ruleSet, document, context);
                resultSet.Add(ruleSet.RuleSetKey, result);
            }

            return resultSet;
        }

        protected string BuildRuleSet(RuleSet ruleSet, IHtmlDocument document, IContext context)
        {
            int maxScore = 0;
            string maxValue = string.Empty;

            for (int i = 0; i < ruleSet.Rules.Count; i++)
            {
                var rule = ruleSet.Rules[i];
                var elements = document.QuerySelectorAll(rule.QuerySelector);

                foreach (var element in elements)
                {
                    var score = ruleSet.Rules.Count - i;

                    // scorers
                    if (ruleSet.Scorer != null)
                    {
                        int? newScore = ruleSet.Scorer(element, score);

                        if (newScore.HasValue)
                            score = newScore.Value;
                    }

                    if (score > maxScore)
                    {
                        maxScore = score;
                        maxValue = rule.ElementSelector(element);
                    }
                }
            }

            if (string.IsNullOrEmpty(maxValue))
            {
                // default value
                if (ruleSet.DefaultValue != null)
                {
                    maxValue = ruleSet.DefaultValue(context);
                }
            }

            if (!string.IsNullOrEmpty(maxValue))
            {
                // processors
                if (ruleSet.Processor != null)
                {
                    maxValue = ruleSet.Processor(maxValue, context);
                }

                maxValue = maxValue.Trim();
            }

            return maxValue;
        }
    }
}
