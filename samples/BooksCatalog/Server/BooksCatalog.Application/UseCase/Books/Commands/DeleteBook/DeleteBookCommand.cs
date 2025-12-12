using BooksCatalog.Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Books.Commands.DeleteBook;

/// <summary>
/// Delete book command.
/// </summary>
public class DeleteBookCommand : KeyRequest<int>, IRequest;