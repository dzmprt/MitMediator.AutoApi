using BooksCatalog.Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBook;

/// <summary>
/// Get book query.
/// </summary>
public class GetBookQuery : KeyRequest<int>, IRequest<Book>;