using BooksCatalog.Domain;
using FluentValidation;

namespace BooksCatalog.Application.UseCase.Authors.Queries.GetAuthorsByFilter;

/// <summary>
/// Validator for <see cref="GetAuthorsQuery"/>.
/// </summary>
internal sealed class GetAuthorsQueryValidator : AbstractValidator<GetAuthorsQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAuthorsQueryValidator"/>.
    /// </summary>
    public GetAuthorsQueryValidator()
    {
        RuleFor(q => q.Limit).GreaterThanOrEqualTo(0).When(q => q.Limit.HasValue);
        RuleFor(q => q.Offset).GreaterThanOrEqualTo(0).When(q => q.Offset.HasValue);
        RuleFor(q => q.FreeText).MaximumLength(Author.MaxNameLength);
    }
}