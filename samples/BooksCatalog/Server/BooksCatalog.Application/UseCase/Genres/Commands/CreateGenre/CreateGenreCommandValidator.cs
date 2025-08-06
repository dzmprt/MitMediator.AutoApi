using BooksCatalog.Application.UseCase.Authors.Commands.CreateAuthor;
using BooksCatalog.Domain;
using FluentValidation;

namespace BooksCatalog.Application.UseCase.Genres.Commands.CreateGenre;

/// <summary>
/// Validator for <see cref="CreateGenreCommand"/>.
/// </summary>
internal sealed class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateGenreCommandValidator"/>.
    /// </summary>
    public CreateGenreCommandValidator()
    {
        RuleFor(author => author.GenreName).NotEmpty().MaximumLength(Genre.MaxGenreNameLength);
    }
}