using FluentValidation;

namespace InfoTrackWebScraper.Application.Behaviours.Queries.GetWebScraperResultQuery
{
    public class GetWebScraperResultQueryValidator : AbstractValidator<GetWebScraperResultQuery>
    {
        public GetWebScraperResultQueryValidator()
        {
            RuleFor(x => x.Url)
            .NotEmpty()
            .WithMessage("Url cannot be empty");

            RuleFor(x => x.Keyword)
            .NotEmpty()
            .WithMessage("Keyword cannot be empty");
        }
    }
}
