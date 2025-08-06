// See https://aka.ms/new-console-template for more information

using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.HttpMediator;
using SmokeTestWebApi.UseCase.Files.Queries.GetFile;
using SmokeTestWebApi.UseCase.Files.Queries.GetFilePng;
using SmokeTestWebApi.UseCase.Files.Queries.GetFileTxt;
using SmokeTestWebApi.UseCase.Files.Queries.GetFileWithCustomName;
using SmokeTestWebApi.UseCase.Test.Commands.Create;
using SmokeTestWebApi.UseCase.Test.Commands.CreateBy2Keys;
using SmokeTestWebApi.UseCase.Test.Commands.CreateBy6Keys;
using SmokeTestWebApi.UseCase.Test.Commands.CreateByKey;
using SmokeTestWebApi.UseCase.Test.Commands.Delete;
using SmokeTestWebApi.UseCase.Test.Commands.DeleteByKey;
using SmokeTestWebApi.UseCase.Test.Commands.DeleteByKey2;
using SmokeTestWebApi.UseCase.Test.Commands.DeleteByKey7;
using SmokeTestWebApi.UseCase.Test.Commands.Post;
using SmokeTestWebApi.UseCase.Test.Commands.PostBy2Keys;
using SmokeTestWebApi.UseCase.Test.Commands.PostBy7Keys;
using SmokeTestWebApi.UseCase.Test.Commands.PostByKey;
using SmokeTestWebApi.UseCase.Test.Commands.Update;
using SmokeTestWebApi.UseCase.Test.Commands.UpdateBy2Keys;
using SmokeTestWebApi.UseCase.Test.Commands.UpdateBy7Keys;
using SmokeTestWebApi.UseCase.Test.Commands.UpdateByKey;
using SmokeTestWebApi.UseCase.Test.Queries.Get;
using SmokeTestWebApi.UseCase.Test.Queries.GetByKey;
using SmokeTestWebApi.UseCase.Test.Queries.GetByKey2;
using SmokeTestWebApi.UseCase.Test.Queries.GetByKey7;
using SmokeTestWebApi.UseCase.Test.Queries.GetEmpty;
using SmokeTestWebApi.UseCase.Test.Queries.GetList;
using SmokeTestWebApi.UseCase.Test.Queries.GetWithSuffix;

var baseApiUrl = "api";
var httpClientName = "baseHttpClient";
var serviceCollection = new ServiceCollection();
serviceCollection.AddHttpClient(httpClientName, client => { client.BaseAddress = new Uri("http://localhost:5035/"); });
serviceCollection.AddScoped<IHttpMediator, HttpMediator>(c => new HttpMediator(c, baseApiUrl, httpClientName));

var provider = serviceCollection.BuildServiceProvider();
var httpMediator = provider.GetRequiredService<IHttpMediator>();

Console.WriteLine(await httpMediator.SendAsync<GetEmptyTestQuery, string>(new GetEmptyTestQuery {TestData = "test"}, CancellationToken.None));
Console.WriteLine(await httpMediator.SendAsync<GetTestQuery, string>(new GetTestQuery {TestData = "test"}, CancellationToken.None));
var getTestByKeyQuery = new GetTestByKeyQuery();
getTestByKeyQuery.SetKey(123);
Console.WriteLine(await httpMediator.SendAsync<GetTestByKeyQuery, string>(getTestByKeyQuery, CancellationToken.None));
var getTestByKey2Query = new GetTestByKey2Query();
getTestByKey2Query.SetKey1(123);
getTestByKey2Query.SetKey2(123);
Console.WriteLine(await httpMediator.SendAsync<GetTestByKey2Query, string>(getTestByKey2Query, CancellationToken.None));
var getTestByKey7Query = new GetTestByKey7Query(){};
getTestByKey7Query.SetKey1(123);
getTestByKey7Query.SetKey2(123);
getTestByKey7Query.SetKey3(123);
getTestByKey7Query.SetKey4(123);
getTestByKey7Query.SetKey5(123);
getTestByKey7Query.SetKey6(123);
getTestByKey7Query.SetKey7(123);
Console.WriteLine(await httpMediator.SendAsync<GetTestByKey7Query, string>(getTestByKey7Query, CancellationToken.None));
Console.WriteLine(await httpMediator.SendAsync<GetTestQuery, string>(new GetTestQuery(){TestData = "test"}, CancellationToken.None));
var getTestWithSuffixQuery = new GetTestWithSuffixQuery()
{
    TestData = "test"
};
Console.WriteLine(await httpMediator.SendAsync<GetTestWithSuffixQuery, string>(getTestWithSuffixQuery, CancellationToken.None));

Console.WriteLine((await httpMediator.SendAsync<CreateTestCommand, CreateTestResponse>(new CreateTestCommand {TestData = "test"}, CancellationToken.None)).Value);
var createTestByKeyCommand = new CreateTestByKeyCommand();
createTestByKeyCommand.SetKey(123);
Console.WriteLine((await httpMediator.SendAsync<CreateTestByKeyCommand, CreateTestByKeyResponse>(createTestByKeyCommand, CancellationToken.None)).Value);
var createTestBy2KeysCommand = new CreateTestBy2KeysCommand();
createTestBy2KeysCommand.SetKey1(123);
createTestBy2KeysCommand.SetKey2(123);
Console.WriteLine((await httpMediator.SendAsync<CreateTestBy2KeysCommand, CreateTestBy2KeysResponse>(createTestBy2KeysCommand, CancellationToken.None)).Value);
var createTestBy6KeysCommand = new CreateTestBy6KeysCommand
{
    TestData = "test"
};
createTestBy6KeysCommand.SetKey1(123);
createTestBy6KeysCommand.SetKey2(123);
createTestBy6KeysCommand.SetKey3(123);
createTestBy6KeysCommand.SetKey4(123);
createTestBy6KeysCommand.SetKey5(123);
createTestBy6KeysCommand.SetKey6(123);
Console.WriteLine((await httpMediator.SendAsync<CreateTestBy6KeysCommand, CreateTestBy6KeysResponse>(createTestBy6KeysCommand, CancellationToken.None)).Value);

Console.WriteLine(await httpMediator.SendAsync<PostTestCommand, string>(new PostTestCommand {TestData = "test"}, CancellationToken.None));
var postTestByKeyCommand = new PostTestByKeyCommand();
postTestByKeyCommand.SetKey(123);
Console.WriteLine(await httpMediator.SendAsync<PostTestByKeyCommand, string>(postTestByKeyCommand, CancellationToken.None));
var postTestBy2KeysCommand = new PostTestBy2KeysCommand();
postTestBy2KeysCommand.SetKey1(123);
postTestBy2KeysCommand.SetKey2(123);
Console.WriteLine(await httpMediator.SendAsync<PostTestBy2KeysCommand, string>(postTestBy2KeysCommand, CancellationToken.None));
var postTestBy7KeysCommand = new PostTestBy7KeysCommand
{
    TestData = "test"
};
postTestBy7KeysCommand.SetKey1(123);
postTestBy7KeysCommand.SetKey2(123);
postTestBy7KeysCommand.SetKey3(123);
postTestBy7KeysCommand.SetKey4(123);
postTestBy7KeysCommand.SetKey5(123);
postTestBy7KeysCommand.SetKey6(123);
postTestBy7KeysCommand.SetKey7(123);
Console.WriteLine(await httpMediator.SendAsync<PostTestBy7KeysCommand, string>(postTestBy7KeysCommand, CancellationToken.None));

Console.WriteLine(await httpMediator.SendAsync<UpdateTestCommand, string>(new UpdateTestCommand {TestData = "test"}, CancellationToken.None));
var updateTestByKeyCommand = new UpdateTestByKeyCommand();
updateTestByKeyCommand.SetKey(123);
Console.WriteLine(await httpMediator.SendAsync<UpdateTestByKeyCommand, string>(updateTestByKeyCommand, CancellationToken.None));
var updateTestBy2KeysCommand = new UpdateTestBy2KeysCommand();
updateTestBy2KeysCommand.SetKey1(123);
updateTestBy2KeysCommand.SetKey2(123);
Console.WriteLine(await httpMediator.SendAsync<UpdateTestBy2KeysCommand, string>(updateTestBy2KeysCommand, CancellationToken.None));
var updateTestBy7KeysCommand = new UpdateTestBy7KeysCommand
{
    TestData = "test"
};
updateTestBy7KeysCommand.SetKey1(123);
updateTestBy7KeysCommand.SetKey2(123);
updateTestBy7KeysCommand.SetKey3(123);
updateTestBy7KeysCommand.SetKey4(123);
updateTestBy7KeysCommand.SetKey5(123);
updateTestBy7KeysCommand.SetKey6(123);
updateTestBy7KeysCommand.SetKey7(123);
Console.WriteLine(await httpMediator.SendAsync<UpdateTestBy7KeysCommand, string>(updateTestBy7KeysCommand, CancellationToken.None));

Console.WriteLine(await httpMediator.SendAsync(new DeleteTestCommand {TestData = "test"}, CancellationToken.None));
var deleteTestByKeyCommand = new DeleteTestByKeyCommand()
{
    TestData = "test"
};
deleteTestByKeyCommand.SetKey(123);
Console.WriteLine(await httpMediator.SendAsync(deleteTestByKeyCommand, CancellationToken.None));
var deleteTestBy2KeysCommand = new DeleteTestBy2KeysCommand
{
    TestData = "test"
};
deleteTestBy2KeysCommand.SetKey1(123);
deleteTestBy2KeysCommand.SetKey2(123);
Console.WriteLine(await httpMediator.SendAsync(deleteTestBy2KeysCommand, CancellationToken.None));
var deleteTestBy7KeysCommand = new DeleteTestBy7KeysCommand
{
    TestData = "test"
};
deleteTestBy7KeysCommand.SetKey1(123);
deleteTestBy7KeysCommand.SetKey2(123);
deleteTestBy7KeysCommand.SetKey3(123);
deleteTestBy7KeysCommand.SetKey4(123);
deleteTestBy7KeysCommand.SetKey5(123);
deleteTestBy7KeysCommand.SetKey6(123);
deleteTestBy7KeysCommand.SetKey7(123);
Console.WriteLine(await httpMediator.SendAsync(deleteTestBy7KeysCommand, CancellationToken.None));

var fileResponse = await httpMediator.SendAsync<GetFileQuery, byte[]>(new GetFileQuery {Base64String = Convert.ToBase64String("test"u8.ToArray())}, CancellationToken.None);
Console.WriteLine($"GetFileQuery: file size {fileResponse.Length}, ToBase64String {Convert.ToBase64String(fileResponse)}, UTF8:{Encoding.UTF8.GetString(fileResponse)}");
var getFileTxtResponse = await httpMediator.SendAsync<GetFileTxtQuery, byte[]>(new GetFileTxtQuery {Base64String = Convert.ToBase64String("test"u8.ToArray())}, CancellationToken.None);
Console.WriteLine($"GetFileTxtQuery: file size {getFileTxtResponse.Length}, ToBase64String {Convert.ToBase64String(getFileTxtResponse)}, UTF8:{Encoding.UTF8.GetString(getFileTxtResponse)}");
var filePngResponse = await httpMediator.SendAsync<GetFilePngQuery, byte[]>(new GetFilePngQuery(), CancellationToken.None);
Console.WriteLine($"GetFilePng: file size {filePngResponse.Length}");
var fileWithCustomNameQuery = await httpMediator.SendAsync<GetFileWithCustomNameQuery, FileResponse>(new GetFileWithCustomNameQuery(), CancellationToken.None);
Console.WriteLine($"GetFilePng: file size {filePngResponse.Length}, file name {fileWithCustomNameQuery.FileName}");

var getListQuery = new GetListQuery();
var response = await httpMediator.SendAsync<GetListQuery, GetListResponse>(getListQuery, CancellationToken.None);
Console.WriteLine($"GetListQuery: items count {response.Items.Length}, X-Total-Count {response.GetTotalCount()}");

