using Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace Application.UseCase.Books.Commands.DeleteBook;

/// <summary>
/// Delete book command.
/// </summary>
public struct DeleteBookCommand : IRequest, IKeyRequest<int>
{
    /// <summary>
    /// Book id.
    /// </summary>
    internal int BookId { get; private set; }

    public void SetKey(int key)
    {
        BookId = key;
    }
}