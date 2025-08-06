using BooksCatalog.Application.UseCase.Authors.Queries.GetAuthorsByFilter;
using BooksCatalog.Domain;
using FluentValidation;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBooksByFilter;

/// <summary>
/// Validator for <see cref="GetBooksQuery"/>
/// </summary>
internal sealed class GetBooksQueryValidator : AbstractValidator<GetBooksQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetBooksQueryValidator"/>.
    /// </summary>
    public GetBooksQueryValidator()
    {
        RuleFor(q => q.Limit).GreaterThanOrEqualTo(0).When(q => q.Limit.HasValue);
        RuleFor(q => q.Offset).GreaterThanOrEqualTo(0).When(q => q.Offset.HasValue);
        RuleFor(q => q.FreeText).MaximumLength(1000);
    }
}