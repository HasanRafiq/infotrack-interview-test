using InfoTrackWebScraper.Contracts.Responses;
using MediatR;

namespace InfoTrackWebScraper.Application.Behaviours.Queries.GetWebScraperResultQuery
{
    public class GetWebScraperResultQuery : IRequest<GetWebScraperResultResponse>
    {
        public string Url { get; set; }
        public string Keyword { get; set; }
    }
}
