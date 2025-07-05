using Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace Application.UseCase.Authors.Commands.UpdateAuthor;

/// <summary>
/// Update author.
/// </summary>
[UpdateByKey(nameof(Authors), "v1", "Update author by id.")]
public struct UpdateAuthorCommand : IRequest<Author>, IKeyRequest<int>
{
    /// <summary>
    /// Author id.
    /// </summary>
    internal int AuthorId { get; private set; }
    
    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; init; }
    
    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; init; }

    public void SetKey(int key)
    {
        AuthorId = key;
    }
}