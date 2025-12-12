using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Authors.Queries.GetAuthorsByFilter;

/// <summary>
/// Handler for <see cref="GetAuthorsQuery"/>.
/// </summary>
internal sealed class GetAuthorsQueryHandler(IBaseProvider<Author> authorProvider) : IRequestHandler<GetAuthorsQuery, Author[]>
{
    /// <inheritdoc/>
    public ValueTask<Author[]> HandleAsync(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var freeText = request.FreeText?.Trim().ToUpperInvariant();
        return authorProvider.SearchAsync(
            freeText is null
                ? null
                : q => q.FirstName.Contains(freeText) || q.LastName.Contains(freeText),
            o => o.AuthorId,
            request.Limit,
            request.Offset,
            cancellationToken
        );
    }
}