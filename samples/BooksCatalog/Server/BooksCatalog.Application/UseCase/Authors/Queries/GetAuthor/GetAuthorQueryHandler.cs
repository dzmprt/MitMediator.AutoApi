using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.Exceptions;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Authors.Queries.GetAuthor;

/// <summary>
/// Handler for <see cref="GetAuthorQuery"/>.
/// </summary>
internal sealed class GetAuthorQueryHandler(IBaseProvider<Author> authorProvider) : IRequestHandler<GetAuthorQuery, Author>
{
    /// <inheritdoc/>
    public async ValueTask<Author> HandleAsync(GetAuthorQuery query, CancellationToken cancellationToken)
    {
        var author = await authorProvider.FirstOrDefaultAsync(q => q.AuthorId == query.GetKey(), cancellationToken);
        if (author is null)
        {
            throw new NotFoundException();
        }

        return author;
    }
}