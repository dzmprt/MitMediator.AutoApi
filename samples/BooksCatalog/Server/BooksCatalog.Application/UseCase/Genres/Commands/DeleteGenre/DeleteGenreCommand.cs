using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Genres.Commands.DeleteGenre;

/// <summary>
/// Delete genre command.
/// </summary>
public class DeleteGenreCommand : KeyRequest<string>, IRequest;