using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Authors.Commands.DeleteAuthor;

/// <summary>
/// Delete author command.
/// </summary>
public class DeleteAuthorCommand : KeyRequest<int>, IRequest;