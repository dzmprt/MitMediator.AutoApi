using Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace Application.UseCase.Authors.Commands.CreateAuthor;

/// <summary>
/// Create author command.
/// </summary>
public struct CreateAuthorCommand : IRequest<Author>
{
    /// <summary>
    /// New author first name.
    /// </summary>
    public string FirstName { get; init; }
    
    /// <summary>
    /// New author last name.
    /// </summary>
    public string LastName { get; init; }
}