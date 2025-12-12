using BooksCatalog.Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Genres.Queries.GetGenres;

/// <summary>
/// Get genres query.
/// </summary>
public struct GetGenresQuery : IRequest<Genre[]>;