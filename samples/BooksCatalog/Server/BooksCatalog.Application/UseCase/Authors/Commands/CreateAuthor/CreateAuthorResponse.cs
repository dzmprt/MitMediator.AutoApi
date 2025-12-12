using BooksCatalog.Domain;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Authors.Commands.CreateAuthor;

public class CreateAuthorResponse(Author author) : IResourceKey
{
    /// <summary>
    /// Author id.
    /// </summary>
    public int AuthorId { get; private set; } = author.AuthorId;

    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; private set; } = author.FirstName;

    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; private set; } = author.LastName;

    public string GetResourceKey() => AuthorId.ToString();
}