using BooksCatalog.Domain;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Books.Commands.CreateBook;

public class CreatedBookResponse //: IResourceKey
{
    /// <summary>
    /// Book id.
    /// </summary>
    public int BookId { get; private set; }
    
    /// <summary>
    /// Title.
    /// </summary>
    public string Title { get; private set; }
    
    /// <summary>
    /// Author.
    /// </summary>
    public Author Author { get; private set; }
    
    /// <summary>
    /// Genre.
    /// </summary>
    public Genre Genre { get; private set; }

    public CreatedBookResponse()
    {
        
    }
    
    public CreatedBookResponse(Book book)
    {
        BookId = book.BookId;
        Title = book.Title;
        Author = book.Author;
        Genre = book.Genre;
    }
    
    public string GetResourceKey() => BookId.ToString();
}