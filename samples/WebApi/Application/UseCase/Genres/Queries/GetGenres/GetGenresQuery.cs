using Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace Application.UseCase.Genres.Queries.GetGenres;

/// <summary>
/// Get genres query.
/// </summary>
public struct GetGenresQuery : IRequest<Genre[]>
{
    
}