using Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace Application.UseCase.Genres.Queries.GetGenres;

/// <summary>
/// Get genres query.
/// </summary>
[Get(nameof(Genres), "v1", "Get genres.")]
public struct GetGenresQuery : IRequest<Genre[]>
{
    
}