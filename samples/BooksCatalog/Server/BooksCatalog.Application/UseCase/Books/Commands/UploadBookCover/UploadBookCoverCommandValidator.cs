using FluentValidation;

namespace BooksCatalog.Application.UseCase.Books.Commands.UploadBookCover;

/// <summary>
/// Validator for <see cref="UploadBookCoverCommand"/>
/// </summary>
public class UpdateBookCommandValidator : AbstractValidator<UploadBookCoverCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBookCommandValidator"/>.
    /// </summary>
    public UpdateBookCommandValidator()
    {
        RuleFor(c => c.BookId).GreaterThan(0);
    }
}