using BooksCatalog.Domain;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Authors.Commands.CreateAuthor;

public class CreateAuthorResponse : IResourceKey
{
    /// <summary>
    /// Author id.
    /// </summary>
    public int AuthorId { get; private set; }
    
    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; private set; }
    
    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; private set; }

    public CreateAuthorResponse(Author author)
    {
        AuthorId = author.AuthorId;
        FirstName = author.FirstName;
        LastName = author.LastName;
    }

    public string GetResourceKey() => AuthorId.ToString();
}