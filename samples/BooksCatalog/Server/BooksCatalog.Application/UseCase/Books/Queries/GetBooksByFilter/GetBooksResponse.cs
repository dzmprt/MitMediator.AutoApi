using BooksCatalog.Domain;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBooksByFilter;

public class GetBooksResponse : ITotalCount
{
    public Book[] Items { get; init; }

    private int _totalCount;
    public int GetTotalCount() => _totalCount;

    public void SetTotalCount(int totalCount)
    {
        _totalCount = totalCount;
    }
}