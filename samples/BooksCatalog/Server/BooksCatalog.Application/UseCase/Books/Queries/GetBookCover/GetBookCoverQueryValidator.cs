using FluentValidation;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBookCover;

/// <summary>
/// Validator for <see cref="GetBookCoverQuery"/>.
/// </summary>
public class GetBookCoverQueryValidator : AbstractValidator<GetBookCoverQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetBookCoverQueryValidator"/>.
    /// </summary>
    public GetBookCoverQueryValidator()
    {
        RuleFor(c => c.GetKey()).GreaterThan(0);
    }
}