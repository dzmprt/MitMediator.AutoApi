using Application.Abstractions.Infrastructure;
using Application.UseCase.Authors.Queries.GetAuthorsByFilter;
using Domain;
using MitMediator;

namespace Application.UseCase.Authors.Queries.GetAuthorsCount;

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