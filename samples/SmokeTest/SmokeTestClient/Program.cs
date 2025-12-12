// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.HttpMediator;
using SmokeTest.Application.UseCase.Files.Commands.ImportFileBytes;
using SmokeTest.Application.UseCase.Files.Commands.ImportFileBytesWithName;
using SmokeTest.Application.UseCase.Files.Commands.ImportFileStream;
using SmokeTest.Application.UseCase.Files.Commands.ImportFileStreamWithName;
using SmokeTest.Application.UseCase.Files.Queries.GetFile;
using SmokeTest.Application.UseCase.Files.Queries.GetFilePng;
using SmokeTest.Application.UseCase.Files.Queries.GetFileTxt;
using SmokeTest.Application.UseCase.Files.Queries.GetFileWithCustomName;
using SmokeTest.Application.UseCase.Test.Commands.Create;
using SmokeTest.Application.UseCase.Test.Commands.CreateBy2Keys;
using SmokeTest.Application.UseCase.Test.Commands.CreateBy6Keys;
using SmokeTest.Application.UseCase.Test.Commands.CreateByKey;
using SmokeTest.Application.UseCase.Test.Commands.Delete;
using SmokeTest.Application.UseCase.Test.Commands.DeleteByKey;
using SmokeTest.Application.UseCase.Test.Commands.DeleteByKey2;
using SmokeTest.Application.UseCase.Test.Commands.DeleteByKey7;
using SmokeTest.Application.UseCase.Test.Commands.Post;
using SmokeTest.Application.UseCase.Test.Commands.PostBy2Keys;
using SmokeTest.Application.UseCase.Test.Commands.PostBy7Keys;
using SmokeTest.Application.UseCase.Test.Commands.PostByKey;
using SmokeTest.Application.UseCase.Test.Commands.Update;
using SmokeTest.Application.UseCase.Test.Commands.UpdateBy2Keys;
using SmokeTest.Application.UseCase.Test.Commands.UpdateBy7Keys;
using SmokeTest.Application.UseCase.Test.Commands.UpdateByKey;
using SmokeTest.Application.UseCase.Test.Queries.Get;
using SmokeTest.Application.UseCase.Test.Queries.GetByKey;
using SmokeTest.Application.UseCase.Test.Queries.GetByKey2;
using SmokeTest.Application.UseCase.Test.Queries.GetByKey7;
using SmokeTest.Application.UseCase.Test.Queries.GetByKey7WithDifferentTypes;
using SmokeTest.Application.UseCase.Test.Queries.GetByKeyWithCustomPath;
using SmokeTest.Application.UseCase.Test.Queries.GetEmpty;
using SmokeTest.Application.UseCase.Test.Queries.GetList;
using SmokeTest.Application.UseCase.Test.Queries.GetWithQueryParams;
using SmokeTest.Application.UseCase.Test.Queries.GetWithSuffix;
using SmokeTestClient;
using MemoryStream = System.IO.MemoryStream;

var baseApiUrl = "api";
var httpClientName = "baseHttpClient";
var serviceCollection = new ServiceCollection();
serviceCollection.AddHttpClient(httpClientName, client => { client.BaseAddress = new Uri("http://localhost:5035/"); });
serviceCollection.AddScoped(typeof(IHttpHeaderInjector<,>), typeof(AuthorizationHeaderInjection<,>));
serviceCollection.AddScoped<IClientMediator, HttpMediator>(c => new HttpMediator(c, baseApiUrl, httpClientName));

var provider = serviceCollection.BuildServiceProvider();
var httpMediator = provider.GetRequiredService<IClientMediator>();

Console.WriteLine(await httpMediator.SendAsync<GetEmptyTestQuery, string>(new GetEmptyTestQuery { TestData = "test" },
    CancellationToken.None));


Console.WriteLine(await httpMediator.SendAsync<SmokeTest.Application.UseCase.Test.Queries.GetV2.GetTestQuery, string>(
    new SmokeTest.Application.UseCase.Test.Queries.GetV2.GetTestQuery { TestData = "test v2" },
    CancellationToken.None));

Console.WriteLine(
    await httpMediator.SendAsync<GetTestQuery, string>(new GetTestQuery { TestData = "test" }, CancellationToken.None));
var getTestByKeyQuery = new GetTestByKeyQuery
{
    Key = 123
};
Console.WriteLine(await httpMediator.SendAsync<GetTestByKeyQuery, string>(getTestByKeyQuery, CancellationToken.None));
var getTestByKey2Query = new GetTestByKey2Query
{
    Key1 = 1,
    Key2 = 2
};
Console.WriteLine(await httpMediator.SendAsync<GetTestByKey2Query, string>(getTestByKey2Query, CancellationToken.None));
var getTestByKey7Query = new GetTestByKey7Query
{
    Key1 = 1,
    Key2 = 2,
    Key3 = 3,
    Key4 = 4,
    Key5 = 5,
    Key6 = 6,
    Key7 = 7
};
Console.WriteLine(await httpMediator.SendAsync<GetTestByKey7Query, string>(getTestByKey7Query, CancellationToken.None));
Console.WriteLine(
    await httpMediator.SendAsync<GetTestQuery, string>(new GetTestQuery() { TestData = "test" },
        CancellationToken.None));
var getTestWithSuffixQuery = new GetTestWithSuffixQuery
{
    TestData = "test"
};
Console.WriteLine(
    await httpMediator.SendAsync<GetTestWithSuffixQuery, string>(getTestWithSuffixQuery, CancellationToken.None));

var getTestByKeyWithCustomPathQuery = new GetTestByKeyWithCustomPathQuery
{
    Key = 123
};
Console.WriteLine(
    await httpMediator.SendAsync<GetTestByKeyWithCustomPathQuery, string>(getTestByKeyWithCustomPathQuery,
        CancellationToken.None));

Console.WriteLine(
    (await httpMediator.SendAsync<CreateTestCommand, CreateTestResponse>(new CreateTestCommand { TestData = "test" },
        CancellationToken.None)).Value);
var createTestByKeyCommand = new CreateTestByKeyCommand
{
    Key = 123
};
Console.WriteLine(
    (await httpMediator.SendAsync<CreateTestByKeyCommand, CreateTestByKeyResponse>(createTestByKeyCommand,
        CancellationToken.None)).Value);
var createTestBy2KeysCommand = new CreateTestBy2KeysCommand
{
    TestData = "TestData",
    Key1 = 1,
    Key2 = 2
};
Console.WriteLine(
    (await httpMediator.SendAsync<CreateTestBy2KeysCommand, CreateTestBy2KeysResponse>(createTestBy2KeysCommand,
        CancellationToken.None)).Value);
var createTestBy6KeysCommand = new CreateTestBy6KeysCommand
{
    TestData = "test",
    Key1 = 1,
    Key2 = 2,
    Key3 = 3,
    Key4 = 4,
    Key5 = 5,
    Key6 = 6
};
Console.WriteLine(
    (await httpMediator.SendAsync<CreateTestBy6KeysCommand, CreateTestBy6KeysResponse>(createTestBy6KeysCommand,
        CancellationToken.None)).Value);

Console.WriteLine(await httpMediator.SendAsync<PostTestCommand, string>(new PostTestCommand { TestData = "test" },
    CancellationToken.None));
var postTestByKeyCommand = new PostTestByKeyCommand
{
    TestData = "TestData",
    Key = 1
};
Console.WriteLine(
    await httpMediator.SendAsync<PostTestByKeyCommand, string>(postTestByKeyCommand, CancellationToken.None));
var postTestBy2KeysCommand = new PostTestBy2KeysCommand
{
    TestData = "TestData",
    Key1 = 1,
    Key2 = 2
};
Console.WriteLine(
    await httpMediator.SendAsync<PostTestBy2KeysCommand, string>(postTestBy2KeysCommand, CancellationToken.None));
var postTestBy7KeysCommand = new PostTestBy7KeysCommand
{
    TestData = "test",
    Key1 = 1,
    Key2 = 2,
    Key3 = 3,
    Key4 = 4,
    Key5 = 5,
    Key6 = 6,
    Key7 = 7
};
Console.WriteLine(
    await httpMediator.SendAsync<PostTestBy7KeysCommand, string>(postTestBy7KeysCommand, CancellationToken.None));

Console.WriteLine(await httpMediator.SendAsync<UpdateTestCommand, string>(new UpdateTestCommand { TestData = "test" },
    CancellationToken.None));
var updateTestByKeyCommand = new UpdateTestByKeyCommand
{
    TestData = "TestData",
    Key = 1
};
Console.WriteLine(
    await httpMediator.SendAsync<UpdateTestByKeyCommand, string>(updateTestByKeyCommand, CancellationToken.None));
var updateTestBy2KeysCommand = new UpdateTestBy2KeysCommand
{
    Key1 = 1,
    TestData = "TestData",
    Key2 = 2
};
Console.WriteLine(
    await httpMediator.SendAsync<UpdateTestBy2KeysCommand, string>(updateTestBy2KeysCommand, CancellationToken.None));
var updateTestBy7KeysCommand = new UpdateTestBy7KeysCommand
{
    TestData = "test",
    Key1 = 1,
    Key2 = 2,
    Key3 = 3,
    Key4 = 4,
    Key5 = 5,
    Key6 = 6,
    Key7 = 7
};
Console.WriteLine(
    await httpMediator.SendAsync<UpdateTestBy7KeysCommand, string>(updateTestBy7KeysCommand, CancellationToken.None));

Console.WriteLine(await httpMediator.SendAsync(new DeleteTestCommand { TestData = "test" }, CancellationToken.None));
var deleteTestByKeyCommand = new DeleteTestByKeyCommand
{
    TestData = "test",
    Key = 1
};
Console.WriteLine(await httpMediator.SendAsync(deleteTestByKeyCommand, CancellationToken.None));
var deleteTestBy2KeysCommand = new DeleteTestBy2KeysCommand
{
    TestData = "test",
    Key1 = 1,
    Key2 = 2
};
Console.WriteLine(await httpMediator.SendAsync(deleteTestBy2KeysCommand, CancellationToken.None));
var deleteTestBy7KeysCommand = new DeleteTestBy7KeysCommand
{
    TestData = "test",
    Key1 = 1,
    Key2 = 2,
    Key3 = 3,
    Key4 = 4,
    Key5 = 5,
    Key6 = 6,
    Key7 = 7
};
Console.WriteLine(await httpMediator.SendAsync(deleteTestBy7KeysCommand, CancellationToken.None));

var fileResponse = await httpMediator.SendAsync<GetFileQuery, byte[]>(
    new GetFileQuery { Base64String = Convert.ToBase64String("test"u8.ToArray()) }, CancellationToken.None);
Console.WriteLine(
    $"GetFileQuery: file size {fileResponse.Length}, ToBase64String {Convert.ToBase64String(fileResponse)}, UTF8:{Encoding.UTF8.GetString(fileResponse)}");
var getFileTxtResponse = await httpMediator.SendAsync<GetFileTxtQuery, byte[]>(
    new GetFileTxtQuery { Base64String = Convert.ToBase64String("test"u8.ToArray()) }, CancellationToken.None);
Console.WriteLine(
    $"GetFileTxtQuery: file size {getFileTxtResponse.Length}, ToBase64String {Convert.ToBase64String(getFileTxtResponse)}, UTF8:{Encoding.UTF8.GetString(getFileTxtResponse)}");
var filePngResponse =
    await httpMediator.SendAsync<GetFilePngQuery, byte[]>(new GetFilePngQuery(), CancellationToken.None);
Console.WriteLine($"GetFilePng: file size {filePngResponse.Length}");
var fileWithCustomNameQuery =
    await httpMediator.SendAsync<GetFileWithCustomNameQuery, FileResponse>(new GetFileWithCustomNameQuery(),
        CancellationToken.None);
Console.WriteLine($"GetFilePng: file size {filePngResponse.Length}, file name {fileWithCustomNameQuery.FileName}");

var getListQuery = new GetListQuery();
var response = await httpMediator.SendAsync<GetListQuery, GetListResponse>(getListQuery, CancellationToken.None);
Console.WriteLine($"GetListQuery: items count {response.Items.Length}, X-Total-Count {response.GetTotalCount()}");

var queryParams = new GetTestWithQueryParamsQuery
{
    TestIntParam = 123,
    TestStringParam = "TestStringParam",
    TestNullableIntParam = null,
    TestNullableIntParam2 = 1234,
    TestNullableStringParam = null,
    TestNullableStringParam2 = "TestNullableStringParam2",
    DateTimeParam = DateTime.Now,
    DateTimeOffsetParam = DateTimeOffset.Now,
    ArrayParam = ["1", "2", "3"],
    ListParam = ["3", "2", "1"],
    InnerObject = new GetTestWithQueryInnerObject
    {
        Name = "Inner object name",
        DeepInner = new GetTestWithQueryInnerInnerObject()
        {
            Name = "Deep inner object name"
        }
    },
    TestEnumParam = GetTestWithQueryParamsEnum.TestEnum2,
    Key = 1
};
var queryParamsResponse =
    await httpMediator.SendAsync<GetTestWithQueryParamsQuery, string>(queryParams, CancellationToken.None);
Console.WriteLine(queryParamsResponse);


var pngBase64 =
    "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAApgAAAKYB3X3/OAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAANCSURBVEiJtZZPbBtFFMZ/M7ubXdtdb1xSFyeilBapySVU8h8OoFaooFSqiihIVIpQBKci6KEg9Q6H9kovIHoCIVQJJCKE1ENFjnAgcaSGC6rEnxBwA04Tx43t2FnvDAfjkNibxgHxnWb2e/u992bee7tCa00YFsffekFY+nUzFtjW0LrvjRXrCDIAaPLlW0nHL0SsZtVoaF98mLrx3pdhOqLtYPHChahZcYYO7KvPFxvRl5XPp1sN3adWiD1ZAqD6XYK1b/dvE5IWryTt2udLFedwc1+9kLp+vbbpoDh+6TklxBeAi9TL0taeWpdmZzQDry0AcO+jQ12RyohqqoYoo8RDwJrU+qXkjWtfi8Xxt58BdQuwQs9qC/afLwCw8tnQbqYAPsgxE1S6F3EAIXux2oQFKm0ihMsOF71dHYx+f3NND68ghCu1YIoePPQN1pGRABkJ6Bus96CutRZMydTl+TvuiRW1m3n0eDl0vRPcEysqdXn+jsQPsrHMquGeXEaY4Yk4wxWcY5V/9scqOMOVUFthatyTy8QyqwZ+kDURKoMWxNKr2EeqVKcTNOajqKoBgOE28U4tdQl5p5bwCw7BWquaZSzAPlwjlithJtp3pTImSqQRrb2Z8PHGigD4RZuNX6JYj6wj7O4TFLbCO/Mn/m8R+h6rYSUb3ekokRY6f/YukArN979jcW+V/S8g0eT/N3VN3kTqWbQ428m9/8k0P/1aIhF36PccEl6EhOcAUCrXKZXXWS3XKd2vc/TRBG9O5ELC17MmWubD2nKhUKZa26Ba2+D3P+4/MNCFwg59oWVeYhkzgN/JDR8deKBoD7Y+ljEjGZ0sosXVTvbc6RHirr2reNy1OXd6pJsQ+gqjk8VWFYmHrwBzW/n+uMPFiRwHB2I7ih8ciHFxIkd/3Omk5tCDV1t+2nNu5sxxpDFNx+huNhVT3/zMDz8usXC3ddaHBj1GHj/As08fwTS7Kt1HBTmyN29vdwAw+/wbwLVOJ3uAD1wi/dUH7Qei66PfyuRj4Ik9is+hglfbkbfR3cnZm7chlUWLdwmprtCohX4HUtlOcQjLYCu+fzGJH2QRKvP3UNz8bWk1qMxjGTOMThZ3kvgLI5AzFfo379UAAAAASUVORK5CYII=";
var bytes = Convert.FromBase64String(pngBase64);
using var stream = new MemoryStream(bytes);
var importFileStreamWithNameCommand = new ImportFileStreamWithNameCommand();
importFileStreamWithNameCommand.SetFile(stream, "img.png");
var importFileStreamWithNameResponse =
    await httpMediator.SendAsync<ImportFileStreamWithNameCommand, FileStreamResponse>(importFileStreamWithNameCommand,
        CancellationToken.None);
Console.WriteLine(
    $"ImportFileStreamWithNameCommand: file size {importFileStreamWithNameResponse.File.Length}, file name {importFileStreamWithNameResponse.FileName}");

using var stream2 = new MemoryStream(bytes);
var importFileStreamCommand = new ImportFileStreamCommand();
importFileStreamCommand.SetFile(stream2, "img.png");
var importFileStreamResponse =
    await httpMediator.SendAsync<ImportFileStreamCommand, Stream>(importFileStreamCommand, CancellationToken.None);
Console.WriteLine($"ImportFileStreamCommand: file size {importFileStreamResponse.Length}");

using var stream3 = new MemoryStream(bytes);
var importFileBytesWithNameCommand = new ImportFileBytesWithNameCommand();
importFileBytesWithNameCommand.SetFile(stream3, "img.png");
var importFileBytesWithNameResponse =
    await httpMediator.SendAsync<ImportFileBytesWithNameCommand, FileResponse>(importFileBytesWithNameCommand,
        CancellationToken.None);
Console.WriteLine(
    $"ImportFileBytesWithNameCommand: file size {importFileBytesWithNameResponse.File.Length}, file name {importFileBytesWithNameResponse.FileName}");

using var stream4 = new MemoryStream(bytes);
var importFileBytesCommand = new ImportFileBytesCommand();
importFileBytesCommand.SetFile(stream4, "img.png");
var importFileBytesResponse =
    await httpMediator.SendAsync<ImportFileBytesCommand, byte[]>(importFileBytesCommand, CancellationToken.None);
Console.WriteLine($"ImportFileBytesCommand: file size {importFileBytesResponse.Length}");

var getByKey7WithDifferentTypesQuery = new GetByKey7WithDifferentTypesQuery
{
    Key1 = 1,
    Key2 = "key2",
    Key3 = long.MaxValue,
    Key4 = true,
    Key5 = DateTimeOffset.UtcNow,
    Key6 = Guid.NewGuid(),
    Key7 = 123.456m
};
var getByKey7WithDifferentTypesResponse = await httpMediator.SendAsync<GetByKey7WithDifferentTypesQuery, string>(getByKey7WithDifferentTypesQuery, CancellationToken.None);
Console.WriteLine(getByKey7WithDifferentTypesResponse);
