using Application.Abstractions.Infrastructure;
using Domain;
using MitMediator;

namespace Application.UseCase.Authors.Queries.GetAuthorsByFilter;

/// <summary>
/// Handler for <see cref="GetAuthorsQuery"/>.
/// </summary>
internal sealed class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, Author[]>
{
    private readonly IBaseProvider<Author> _authorProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAuthorsQueryHandler"/>.
    /// </summary>
    /// <param name="authorProvider">Author provider.</param>
    public GetAuthorsQueryHandler(IBaseProvider<Author> authorProvider)
    {
        _authorProvider = authorProvider;
    }
    
    /// <inheritdoc/>
    public ValueTask<Author[]> HandleAsync(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var freeText = request.FreeText?.Trim().ToUpperInvariant();
        return _authorProvider.SearchAsync(
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