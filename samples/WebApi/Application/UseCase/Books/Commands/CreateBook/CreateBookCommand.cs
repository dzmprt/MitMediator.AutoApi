using Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace Application.UseCase.Books.Commands.CreateBook;

/// <summary>
/// Create book command.
/// </summary>
public class CreateBookCommand : IRequest<Book>
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