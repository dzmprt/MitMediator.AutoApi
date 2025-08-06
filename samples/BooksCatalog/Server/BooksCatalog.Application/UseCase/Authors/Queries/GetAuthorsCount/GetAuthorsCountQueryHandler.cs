using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.UseCase.Authors.Queries.GetAuthorsByFilter;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Authors.Queries.GetAuthorsCount;

/// <summary>
/// Handler for <see cref="GetAuthorsQuery"/>.
/// </summary>
internal sealed class GetAuthorsCountQueryHandler : IRequestHandler<GetAuthorsCountQuery, int>
{
    private readonly IBaseProvider<Author> _authorProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAuthorsQueryHandler"/>.
    /// </summary>
    /// <param name="authorProvider">Author provider.</param>
    public GetAuthorsCountQueryHandler(IBaseProvider<Author> authorProvider)
    {
        _authorProvider = authorProvider;
    }
    
    /// <inheritdoc/>
    public ValueTask<int> HandleAsync(GetAuthorsCountQuery request, CancellationToken cancellationToken)
    {
        var freeText = request.FreeText?.Trim().ToUpperInvariant();
        return _authorProvider.CountAsync(
            freeText is null
                ? null
                : q => q.FirstName.Contains(freeText) || q.LastName.Contains(freeText),
            cancellationToken
        );
    }
}