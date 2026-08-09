using BooksCatalog.Domain;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBooksByFilter;

public class GetBooksResponse : ITotalCount
{
    public Book[] Items { get; init; }

    public int TotalCount { get; init; }
}