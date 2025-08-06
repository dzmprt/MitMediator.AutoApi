using FluentValidation;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBooksCount;

/// <summary>
/// Validator for <see cref="GetBooksCountQuery"/>
/// </summary>
internal sealed class GetBooksCountQueryValidator : AbstractValidator<GetBooksCountQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetBooksCountQueryValidator"/>.
    /// </summary>
    public GetBooksCountQueryValidator()
    {
        RuleFor(q => q.FreeText).MaximumLength(1000);
    }
}