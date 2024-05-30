using InfoTrackWebScraper.Contracts.Dtos;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace InfoTrackWebScraper.Services.WebScraper
{
    public static class WebScraper
    {
        public static async Task<OccurrenceInfo> GetKeywordOccurrencesFromUrlAsync(string url, string keyword)
        {
            var webpageXml = await GetWebpageXmlAsync(url);

            // Get node containing search results
            XmlNode searchResultsNode = webpageXml.SelectSingleNode("//*[@id=\"rso\"]");

            // Narrow search results to only elements containing relevant results (removing "People also ask" block and others from google search result)
            XmlNodeList results = searchResultsNode.SelectNodes("//*[@data-snc]");

            var keywordOcc = new List<string>();

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].InnerText.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    keywordOcc.Add($"{i + 1}");
                }
            }

            var occurrenceString = "";

            for (int i = 0; i < keywordOcc.Count; i++)
            {
                occurrenceString += keywordOcc[i];
                if (i != keywordOcc.Count - 1)
                {
                    occurrenceString += ", ";
                }
            }

            var occurrenceInfo = new OccurrenceInfo(occurrenceString, keywordOcc.Count);

            return occurrenceInfo;
        }

        private static async Task<XmlDocument> GetWebpageXmlAsync(string url)
        {
            try
            {
                using var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36");

                var webpage = await httpClient.GetByteArrayAsync(url);
                var webpageutf8 = Encoding.UTF8.GetString(webpage);

                var webpageBody = ExtractBodyContentAsync(webpageutf8);

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(webpageBody);

                return xmlDoc;
            } 
            catch (XmlException ex)
            {
                throw new XmlException($"html obtained from {nameof(url)} could not be parsed into xml", ex);
            }
        }

        private static string ExtractBodyContentAsync(string html)
        {
            string[] superfluousElements = { "script", "form", "noscript", "style", "svg" };
            string[] selfClosingElements = { "img", "br", "hr", "link", "col", "embed", "input", "link", "meta", "param", "source", "track", "wbr", "area", "base", "command", "keygen" };
            var htmlEntityCodes = new Dictionary<string, string>
            {
                { "&nbsp;", "&#160;" },
                { "&middot;", "&#183;" },
                { "&amp;", "&#38;" },
                { "&gt;", "&#62;" },
                { "&lt;", "&#60;" },
                { "&quot;", "&#34;" }
            };

            // Use a regular expression to extract content within the <body> tag
            Match match = Regex.Match(html, @"<body.*>([\s\S]*?)<\/body>", RegexOptions.IgnoreCase);
            var bodyContent = match.Success ? match.Groups[0].Value : string.Empty;

            // remove superfluous elements, self-closing elements and html characters not supported by xml
            for (int i = 0; i < superfluousElements.Length; i++)
            {
                bodyContent = Regex.Replace(bodyContent, $@"<{superfluousElements[i]}\b[^>]*>[\s\S]*?</{superfluousElements[i]}>", string.Empty, RegexOptions.IgnoreCase);
            }

            // remove non closing html elements (non xhtml compat) not compatible with xml
            for (int i = 0; i < selfClosingElements.Length; i++)
            {
                bodyContent = Regex.Replace(bodyContent, $@"<{selfClosingElements[i]}\b*[\s\S]*?>", string.Empty, RegexOptions.IgnoreCase);
            }

            // replace html entity codes with xhtml compatible ones
            foreach (KeyValuePair<string, string> entry in htmlEntityCodes)
            {
                bodyContent = Regex.Replace(bodyContent, $"{entry.Key}", $"{entry.Value}", RegexOptions.IgnoreCase);
            }

            return bodyContent;
        }
    }
}
