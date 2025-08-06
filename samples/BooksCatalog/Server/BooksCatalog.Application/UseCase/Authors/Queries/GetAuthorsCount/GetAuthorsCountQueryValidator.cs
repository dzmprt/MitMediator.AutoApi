using BooksCatalog.Domain;
using FluentValidation;

namespace BooksCatalog.Application.UseCase.Authors.Queries.GetAuthorsCount;

/// <summary>
/// Validator for <see cref="GetAuthorsCountQuery"/>.
/// </summary>
internal sealed class GGetAuthorsCountQueryValidator : AbstractValidator<GetAuthorsCountQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GGetAuthorsCountQueryValidator"/>.
    /// </summary>
    public GGetAuthorsCountQueryValidator()
    {
        RuleFor(q => q.FreeText).MaximumLength(Author.MaxNameLength);
    }
}