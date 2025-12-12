using BooksCatalog.Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Books.Commands.UpdateBook;

/// <summary>
/// Update book command.
/// </summary>
public class UpdateBookCommand : KeyRequest<int>, IRequest<Book>
{
    /// <summary>
    /// Title.
    /// </summary>
    public string Title { get; init; }
    
    /// <summary>
    /// Author id.
    /// </summary>
    public int AuthorId { get; init; }
    
    /// <summary>
    /// Genre.
    /// </summary>
    public string GenreName { get; init; }
}