using InfoTrackWebScraper.Contracts.Responses;
using InfoTrackWebScraper.Services.WebScraper;
using MediatR;
using System.Text.RegularExpressions;

namespace InfoTrackWebScraper.Application.Behaviours.Queries.GetWebScraperResultQuery
{
    public class GetWebScraperResultQueryHandler : IRequestHandler<GetWebScraperResultQuery, GetWebScraperResultResponse>
    {
        public async Task<GetWebScraperResultResponse> Handle(GetWebScraperResultQuery request, CancellationToken cancellationToken)
        {
            var googleSearchUrlCheck = Regex.Match(request.Url, @"^https://www\.google\.co\.uk/search", RegexOptions.IgnoreCase);
            if (!googleSearchUrlCheck.Success)
            {
                throw new ArgumentException("Url must be a valid google search url");
            }

            var webScraperResult = await WebScraper.GetKeywordOccurrencesFromUrlAsync(request.Url, request.Keyword);
            return new GetWebScraperResultResponse(webScraperResult);
        }
    }
}
