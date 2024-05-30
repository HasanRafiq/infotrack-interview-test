using InfoTrackWebScraper.Application.Behaviours.Queries.GetWebScraperResultQuery;
using InfoTrackWebScraper.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackWebScraper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebScraperController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WebScraperController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetWebScraperResultResponse>> GetWebScraperResult([FromQuery] string url, [FromQuery] string keyword)
        {
            // Example - "https://www.google.co.uk/search?num=100&q=land+registry+search", "www.infotrack.co.uk"

            var query = new GetWebScraperResultQuery
            {
                Url = url,
                Keyword = keyword
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
