using BooksCatalog.Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Books.Commands.UpdateBook;

/// <summary>
/// Update book command.
/// </summary>
public class UpdateBookCommand : IRequest<Book>, IKeyRequest<int>
{
    /// <summary>
    /// Book id.
    /// </summary>
    internal int BookId { get; private set; }
    
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

    public void SetKey(int key)
    {
        BookId = key;
    }
    
    public int GetKey() => BookId;
}