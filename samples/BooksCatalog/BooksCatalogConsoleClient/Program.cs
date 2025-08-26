// See https://aka.ms/new-console-template for more information

using BooksCatalog.Application.UseCase.Books.Commands.CreateBook;
using BooksCatalog.Application.UseCase.Books.Commands.DeleteBook;
using BooksCatalog.Application.UseCase.Books.Commands.UpdateBook;
using BooksCatalog.Application.UseCase.Books.Queries.GetBook;
using BooksCatalog.Application.UseCase.Books.Queries.GetBooksByFilter;
using BooksCatalog.Domain;
using Microsoft.Extensions.DependencyInjection;
using MitMediator;
using MitMediator.AutoApi.HttpMediator;

var baseApiUrl = "api";
var httpClientName = "baseHttpClient";
var serviceCollection = new ServiceCollection();
serviceCollection.AddHttpClient(httpClientName, client => { client.BaseAddress = new Uri("https://localhost:7127/"); });
serviceCollection.AddScoped(typeof(IHttpHeaderInjector<,>), typeof(AuthorizationHeaderInjection<,>));
serviceCollection.AddScoped<IClientMediator, HttpMediator>(c => new HttpMediator(c, baseApiUrl, httpClientName));

var provider = serviceCollection.BuildServiceProvider();
var httpMediator = provider.GetRequiredService<IClientMediator>();

while (true)
{
    Console.WriteLine("Commands:");
    Console.WriteLine("1. Show all books");
    Console.WriteLine("2. Show book by id");
    Console.WriteLine("3. Delete book by id");
    Console.WriteLine("4. Change book title by id");
    Console.WriteLine("5. Add bew book");
    Console.WriteLine("0. Exit");
    Console.Write("> ");
    var input = Console.ReadLine();
    if (int.TryParse(input, out int command))
    {
        switch (command)
        {
            case 1:
                await ShowAllBooks();
                break;
            case 2:
                await ShowBookById();
                break;
            case 3:
                await DeleteBookById();
                break;
            case 4:
               await ChangeBookTitleById();
                break;
            case 5:
                await CreateNewBook();
                break;
            case 0:
                System.Environment.Exit(0);
                break;
        }
    }
}

async ValueTask ShowAllBooks()
{
    var booksResponse = await httpMediator.SendAsync<GetBooksQuery, GetBooksResponse>(new GetBooksQuery(), CancellationToken.None);
    Console.WriteLine($"Books total count: {booksResponse.GetTotalCount()}");
    Console.WriteLine("Books:");
    foreach (var book in booksResponse.Items)
    {
        Console.WriteLine($"{book.BookId}: {book.Title}");
    }
    Console.WriteLine();
}

async ValueTask ShowBookById()
{
    Console.Write("Book id:>");
    var idStr = Console.ReadLine();
    if (int.TryParse(idStr, out var bookId))
    {
        var command = new GetBookQuery();
        command.SetKey(bookId);
        var book = await httpMediator.SendAsync<GetBookQuery, Book>(command, CancellationToken.None);
        Console.WriteLine($"id:{book.BookId}, title: {book.Title}");
        Console.WriteLine();
    }
}

async ValueTask DeleteBookById()
{
    Console.Write("Book id:>");
    var idStr = Console.ReadLine();
    if (int.TryParse(idStr, out var bookId))
    {
        var command = new DeleteBookCommand();
        command.SetKey(bookId);
        await httpMediator.SendAsync(command, CancellationToken.None);
        Console.WriteLine("Deleted");
        Console.WriteLine();
    }
}

async ValueTask ChangeBookTitleById()
{
    Console.Write("Book id:>");
    var idStr = Console.ReadLine();
    if (int.TryParse(idStr, out var bookId))
    {
        var getBookCommand = new GetBookQuery();
        getBookCommand.SetKey(bookId);
        var book = await httpMediator.SendAsync<GetBookQuery, Book>(getBookCommand, CancellationToken.None);
        Console.Write("New title:>");
        var newTitle = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(newTitle))
        {
            Console.WriteLine("Invalid title.");
            return;
        }
        var updatedBookCommand = new UpdateBookCommand()
        {
            Title = newTitle,
            AuthorId = book.Author.AuthorId,
            GenreName = book.Genre.GenreName,
        };
        updatedBookCommand.SetKey(bookId);
        book = await httpMediator.SendAsync<UpdateBookCommand, Book>(updatedBookCommand, CancellationToken.None);
        Console.WriteLine("New book info:");
        Console.WriteLine($"id:{book.BookId}, title: {book.Title}");
        Console.WriteLine();
    }
}

async ValueTask CreateNewBook()
{
    Console.Write("New book title:>");
    var newTitle = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(newTitle))
    {
        Console.WriteLine("Invalid title.");
        return;
    }
    var createBookCommand = new CreateBookCommand()
    {
        Title = newTitle,
        AuthorId = 1,
        GenreName = "Mystery / Thriller"
    };
    var book = await httpMediator.SendAsync<CreateBookCommand, CreatedBookResponse>(createBookCommand, CancellationToken.None);
    Console.WriteLine("New book info:");
    Console.WriteLine($"id:{book.BookId}, title: {book.Title}");
    Console.WriteLine();
}

public class AuthorizationHeaderInjection<TRequest, TResponse> : IHttpHeaderInjector<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public ValueTask<(string, string)?> GetHeadersAsync(CancellationToken cancellationToken)
    {
        var result = ("Authorization", "Bearer 123");
        return ValueTask.FromResult<(string, string)?>(result);
    }
}