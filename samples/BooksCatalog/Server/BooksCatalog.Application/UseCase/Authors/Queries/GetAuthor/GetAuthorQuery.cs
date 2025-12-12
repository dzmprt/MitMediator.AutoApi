using BooksCatalog.Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Authors.Queries.GetAuthor;

/// <summary>
/// Get author query.
/// </summary>
public class GetAuthorQuery : KeyRequest<int>, IRequest<Author>;