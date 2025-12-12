using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.UseCase.Authors.Queries.GetAuthorsByFilter;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Authors.Queries.GetAuthorsCount;

/// <summary>
/// Handler for <see cref="GetAuthorsQuery"/>.
/// </summary>
internal sealed class GetAuthorsCountQueryHandler(IBaseProvider<Author> authorProvider) : IRequestHandler<GetAuthorsCountQuery, int>
{
    /// <inheritdoc/>
    public ValueTask<int> HandleAsync(GetAuthorsCountQuery request, CancellationToken cancellationToken)
    {
        var freeText = request.FreeText?.Trim().ToUpperInvariant();
        return authorProvider.CountAsync(
            freeText is null
                ? null
                : q => q.FirstName.Contains(freeText) || q.LastName.Contains(freeText),
            cancellationToken
        );
    }
}