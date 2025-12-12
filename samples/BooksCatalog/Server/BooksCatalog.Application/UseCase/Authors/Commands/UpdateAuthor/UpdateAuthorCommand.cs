using BooksCatalog.Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Authors.Commands.UpdateAuthor;

/// <summary>
/// Update author.
/// </summary>
public class UpdateAuthorCommand : KeyRequest<int>, IRequest<Author>
{
    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; init; }
    
    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; init; }
}